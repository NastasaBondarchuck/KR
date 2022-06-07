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
            GraphCanvas.Background = Brushes.Transparent;
            DrawGraph(graph);
        }
        public void DrawGraph(Graph graph)
        {
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
            
            foreach (var edge in graph.Edges)
            {
                // var line = edge.DrawLine();
                // Canvas.SetTop(line, 0);
                // Canvas.SetLeft(line, 0);
                // GraphCanvas.Children.Add(line);
                var arrow = edge.DrawArrow();
                Canvas.SetTop(arrow, 0);
                Canvas.SetLeft(arrow, 0);
                GraphCanvas.Children.Add(arrow);
                // var weight = edge.DrawWeight();
                // double x = (edge.From.X + edge.To.X) * (1 / 3);
                // double y = (edge.From.Y + edge.To.Y) * (1 / 3);
                // Canvas.SetTop(weight, edge.To.Y  + y);
                // Canvas.SetLeft(weight, edge.To.X + x);
                // GraphCanvas.Children.Add(weight);
            }
            
            
            
        }
    }
}