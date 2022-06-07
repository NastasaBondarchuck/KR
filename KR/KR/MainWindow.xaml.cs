using System;
using System.Collections.Generic;
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
            Graph graph = new Graph(ViewModel.Matrix);
            GraphCanvas.Background = Brushes.Transparent;
            DrawGraph(graph);
        }

        public void ResultButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button resultButton = (Button) sender;
            int[,] pathMatrix = Algorythms.CreatePathMatrix(ViewModel.Matrix);
            if (FloydButton.IsChecked.Value)
            {
                ViewModel.ResultMatrix = Algorythms.FloydAlgorythm(ViewModel.Matrix, pathMatrix);
                FillPathes(pathMatrix);
            }
            else if (DansigButton.IsChecked.Value)
            {
                ViewModel.ResultMatrix = Algorythms.DansigAlgorythm(ViewModel.Matrix, pathMatrix);
                FillPathes(pathMatrix);
            }
            
        }

        public void FillPathes(int[,] PathMatrix)
        {
            ResultPathes.Text = "";
            for (int i = 0; i < PathMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < PathMatrix.GetLength(1); j++)
                {
                    string path = Algorythms.FindPath(PathMatrix, i, j);
                    ResultPathes.Text += $"Path from {i+1} to {j+1} is: {path}\n";
                }
            }
        }
    }
}