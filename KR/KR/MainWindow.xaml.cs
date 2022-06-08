using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using KR_OP;

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
            Button buttonOk = (Button) sender;
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
            ResultPathes.Text = "";
            for (int i = 0; i < PathMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < PathMatrix.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        string path = Algorythms.FindPath(PathMatrix, i, j);
                        ResultPathes.Text += $"Path from {i + 1} to {j + 1} is: {path}\n";
                    }
                }
            }
        }

        public void ResultButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button resultButton = (Button) sender;
            int[,] pathMatrix = Algorythms.CreatePathMatrix(ViewModel.Matrix);
            if (FloydButton.IsChecked.Value)
            {
                try
                {
                    ViewModel.ResultMatrix = Algorythms.FloydAlgorythm(ViewModel.Matrix, pathMatrix);
                    FillPathes(pathMatrix);
                    AddToFile();
                }
                catch (Exception exception)
                {
                    NegativeContour();
                }
            }
            else if (DansigButton.IsChecked.Value)
            {
                try
                {
                    ViewModel.ResultMatrix = Algorythms.DansigAlgorythm(ViewModel.Matrix, pathMatrix);
                    FillPathes(pathMatrix);
                    AddToFile();
                }
                catch (Exception exception)
                {
                    NegativeContour();
                }
            }
            else
            {
                string messageBoxText = "Choose algorythm, please!";
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
            file.Write("Result Pathes: \n");
            file.Write(ResultPathes.Text);
            file.Close();
        }

        private void RandomButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.RandomMatrix();
            DataContext = ViewModel;
        }
    }
}