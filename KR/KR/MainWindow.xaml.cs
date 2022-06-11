using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KR
{
    /// <summary>
    /// Main class for working with MainWindow's objects.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Property that creates and connect matrices from window and from code. 
        /// </summary>
        public MatrixViewModel ViewModel { get; set; }

        /// <summary>
        /// Main method that starts programme.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MatrixViewModel();
            DataContext = ViewModel;
            
        }
        /// <summary>
        /// Method that draws graph on screen.
        /// </summary>
        /// <param name="graph">Object of class Graph.</param>
        public void DrawGraph(Graph graph)
        {
            // arrow - visual model of edge between two vertices;
            // circle - visual model of vertex;
            // name - visual model of vertex' name.
            
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
        /// <summary>
        /// Method that is called, when user clicks a button named "OK".
        /// </summary>
        /// <param name="sender">Button "OK" that check input information and displays graph with calling method "DrawGraph".</param>
        /// <param name="e">Contains information about event, in that case - about clicking a button.</param>
        public void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            // i - index of adjacency matrix' elements;
            // graph - object of class Graph.
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
        /// <summary>
        /// Method that fills result textbox with information about all paths from all vertices of graph.
        /// </summary>
        /// <param name="pathMatrix">The matrix that contains all vertices are enable from another vertices.</param>
        public void FillPathes(int[,] pathMatrix, double[,] adjMatrix)
        {
            // i, j - indices of path matrix' elements;
            // path - intermediate variable to save path between two vertices. 
            Result.Text = "";
            for (int i = 0; i < pathMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < pathMatrix.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        string path = Algorithms.FindPath(pathMatrix, adjMatrix, i, j);
                        Result.Text += $"Path from {i + 1} to {j + 1} is: {path}\n";
                    }
                }
            }
        }
        /// <summary>
        /// Method that is called, when user clicks a button named "ResultButton".
        /// </summary>
        /// <param name="sender">Button "OK" that check presence of negative contour in graph and
        /// displays results on screen depending on the chosen algorithm.</param>
        /// <param name="e">Contains information about event, in that case - about clicking a button.</param>
        public void ResultButton_OnClick(object sender, RoutedEventArgs e)
        {
            // iterationCounter - variable that contains current number of iterations;
            // messageBoxText - variable that contains messageBox' content;
            // caption - variable that contains messageBox' caption;
            // button - variable that contains type of button-element of messageBox; 
            // icon - variable that contains type of image-element of messageBox.
            int[,] pathMatrix = Algorithms.CreatePathMatrix(ViewModel.Matrix);
            if (FloydButton.IsChecked != null && FloydButton.IsChecked.Value)
            {
                try
                {
                    int iterationCounter = 0;
                    ViewModel.ResultMatrix = Algorithms.FloydAlgorithm(ViewModel.Matrix, pathMatrix, ref iterationCounter);
                    FillPathes(pathMatrix, ViewModel.ResultMatrix);
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
                    FillPathes(pathMatrix, ViewModel.ResultMatrix);
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
        /// <summary>
        /// Method that displays dialogue window with message about negative contour presence.
        /// </summary>
        public void NegativeContour()
        {
            // messageBoxText - variable that contains messageBox' content;
            // caption - variable that contains messageBox' caption;
            // button - variable that contains type of button-element of messageBox; 
            // icon - variable that contains type of image-element of messageBox.   
            string messageBoxText = "Graph has negative contour!\n" +
                                    "Try to change adjacency matrix.";
            string caption = "Negative Contour";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            ViewModel.Matrix = ViewModel.RefillMatrix();
            ViewModel.SelectedSize = 2;
        }
        /// <summary>
        /// Method that adding results to external file.
        /// </summary>
        private void AddToFile()
        {
            // fileName - path to external file;
            // file - variable for editing external file.
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
        /// <summary>
        /// Method that is called, when user clicks a button named "Clear".
        /// </summary>
        /// <param name="sender">Button "OK" that clear all input and output elements and give user ability to enter information again.</param>
        /// <param name="e">Contains information about event, in that case - about clicking a button.</param>
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