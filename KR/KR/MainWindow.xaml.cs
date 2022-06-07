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
                var circle = vertex.DrawVertex();
                Canvas.SetTop(circle, vertex.X);
                Canvas.SetLeft(circle, vertex.Y);
                GraphCanvas.Children.Add(circle);

                var name = vertex.DrawName();
                Canvas.SetTop(name, vertex.X);
                Canvas.SetLeft(name, vertex.Y);
                GraphCanvas.Children.Add(name);
            }
            foreach (var edge in graph.Edges)
            {
                var line = edge.DrawLine();
                Canvas.SetTop(line, edge.To.Y - 2);
                Canvas.SetLeft(line, edge.To.X - 2);
                GraphCanvas.Children.Add(line);
                var arrow = edge.DrawArrow();
                Canvas.SetTop(arrow, edge.To.Y - 2);
                Canvas.SetLeft(arrow, edge.To.X - 2);
                GraphCanvas.Children.Add(arrow);
                var weight = edge.DrawWeight();
                Canvas.SetTop(weight, edge.To.Y - 2);
                Canvas.SetLeft(weight, edge.To.X - 2);
                GraphCanvas.Children.Add(weight);
            }
        }
    }
}