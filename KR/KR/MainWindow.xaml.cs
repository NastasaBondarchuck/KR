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
    }
}