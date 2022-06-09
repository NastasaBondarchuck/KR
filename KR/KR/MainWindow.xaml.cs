using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MatrixViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MatrixViewModel();
            DataContext = ViewModel;
            
        }
        public void DrawGraph(Graph graph)
        {
            foreach (var edge in graph.Edges)
            {
                var arrow = edge.DrawEdge();
                Canvas.SetTop(arrow, 0);
                Canvas.SetLeft(arrow, 0);
                GraphCanvas.Children.Add(arrow);
            }
            
            foreach (var vertex in graph.Vertices)
            {
                var circle = vertex.DrawVertex();
                Canvas.SetTop(circle, vertex.Y);
                Canvas.SetLeft(circle, vertex.X);
                GraphCanvas.Children.Add(circle);
            
                var name = vertex.DrawName();
                Canvas.SetTop(name, vertex.Y);
                Canvas.SetLeft(name, vertex.X);
                GraphCanvas.Children.Add(name);
            } 
            
        }

        public void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ViewModel.Matrix.GetLength(0); i++)
            {
                if (ViewModel.Matrix[i, i] != 0)
                {
                    ViewModel.Matrix[i, i] = 0D;
                }
            }
            Graph graph = new Graph(ViewModel.Matrix);
            GraphCanvas.Background = Brushes.Transparent;
            DrawGraph(graph);
        }
        public void FillPathes(int[,] PathMatrix)
        {
            Result.Text = "";
            for (int i = 0; i < PathMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < PathMatrix.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        string path = Algorithms.FindPath(PathMatrix, i, j);
                        Result.Text += $"Path from {i + 1} to {j + 1} is: {path}\n";
                    }
                }
            }
        }

        public void ResultButton_OnClick(object sender, RoutedEventArgs e)
        {
            int[,] pathMatrix = Algorithms.CreatePathMatrix(ViewModel.Matrix);
            if (FloydButton.IsChecked != null && FloydButton.IsChecked.Value)
            {
                try
                {
                    int iterationCounter = 0;
                    ViewModel.ResultMatrix = Algorithms.FloydAlgorythm(ViewModel.Matrix, pathMatrix, ref iterationCounter);
                    FillPathes(pathMatrix);
                    Result.Text += $"\nNumber of iterations: {iterationCounter}\n" +
                                   "Algorithm complexity is O(n^3).";
                    AddToFile();
                    ResultButton.IsEnabled = false;
                    FloydButton.IsEnabled = false;
                    DansigButton.IsEnabled = false;
                }
                catch (Exception)
                {
                    NegativeContour();
                }
            }
            else if (DansigButton.IsChecked != null && DansigButton.IsChecked.Value)
            {
                try
                {
                    int iterationCounter = 0;
                    ViewModel.ResultMatrix = Algorithms.DansigAlgorythm(ViewModel.Matrix, pathMatrix, ref iterationCounter);
                    FillPathes(pathMatrix);
                    Result.Text += $"\nNumber of iterations: {iterationCounter}\n" +
                                   "Algorithm complexity is O(n^3).";
                    AddToFile();
                    ResultButton.IsEnabled = false;
                    FloydButton.IsEnabled = false;
                    DansigButton.IsEnabled = false;
                }
                catch (Exception)
                {
                    NegativeContour();
                }
            }
            else
            {
                string messageBoxText = "Choose algorithm, please!";
                string caption = "Choosing Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }
            
        }
        
        public void NegativeContour()
        {
            string messageBoxText = "Graph has negative contour!\n" +
                                    "Try to change adjacency matrix.";
            string caption = "Negative Contour";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            ViewModel.Matrix = ViewModel.RefillMatrix();
            ViewModel.SelectedSize = 2;
        }
        private void AddToFile()
        {
            string fileName = "ResultFile.txt";
            StreamWriter file = new StreamWriter(fileName, false);
            file.Write("Result Matrix: \n");
            for (int i = 0; i < ViewModel.ResultMatrix.GetLength(0) ; i++)
            {
                for (int j = 0; j < ViewModel.ResultMatrix.GetLength(1); j++)
                {
                    file.Write($"{ViewModel.ResultMatrix[i, j]}\t");
                }
                file.Write("\n");
            }
            file.Write("Result Paths: \n");
            file.Write(Result.Text);
            file.Close();
        }
        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            Result.Text = "Path from __ to __ is: ";
            ResultButton.IsEnabled = true;
            ViewModel.Matrix = ViewModel.RefillMatrix();
            ViewModel.ResultMatrix = new double[1,1];
            ViewModel.SelectedSize = 2;
            FloydButton.IsChecked = false;
            DansigButton.IsChecked = false;
            FloydButton.IsEnabled = true;
            DansigButton.IsEnabled = true;
            GraphCanvas.Children.Clear();
        }
    }
}