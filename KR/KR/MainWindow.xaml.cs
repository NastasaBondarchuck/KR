using System;
using System.Collections.Generic;
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
            Graph graph = new Graph(ViewModel.Matrix);
            GraphCanvas.Background = Brushes.Magenta;
            DrawGraph(graph);
        }
        public void DrawGraph(Graph graph)
        {
            foreach (var vertex in graph.Vertices)
            {
                Canvas.SetTop(GraphCanvas, vertex.Y);
                Canvas.SetLeft(GraphCanvas, vertex.X);
                GraphCanvas.Children.Add(vertex.DrawVertex());
                GraphCanvas.Children.Add(vertex.DrawName());
            }
            foreach (var edge in graph.Edges)
            {
                Canvas.SetTop(GraphCanvas, edge.To.Y - 2);
                Canvas.SetLeft(GraphCanvas, edge.To.X - 2);
                GraphCanvas.Children.Add(edge.DrawLine());
                GraphCanvas.Children.Add(edge.DrawArrow());
                GraphCanvas.Children.Add(edge.DrawWeight());
            }
        }
    }
}