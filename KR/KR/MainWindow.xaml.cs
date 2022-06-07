using System;
using System.Windows.Controls;
using KR_OP;

namespace KR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            double[,] AdjMatrix =
            {
                {0, -2, 3, -3},
                {Double.PositiveInfinity, 0, 2, Double.PositiveInfinity},
                {Double.PositiveInfinity, Double.PositiveInfinity, 0, -3},
                {4, 5, 5, 0}
            };
            Graph graph = new Graph(AdjMatrix);
            GraphGrid.Children.Add(DrawGraph(graph));
            DrawGraph(graph);
        }
        public static Canvas DrawGraph(Graph graph)
        {
            Canvas GraphField = new Canvas();
            GraphField.Width = 800;
            GraphField.Height = 400;
            foreach (var vertex in graph.Vertices)
            {
                Canvas.SetTop(vertex.DrawVertex(), vertex.Y);
                Canvas.SetLeft(vertex.DrawVertex(), vertex.X);
                Canvas.SetTop(vertex.DrawName(), vertex.Y);
                Canvas.SetTop(vertex.DrawName(), vertex.X);
                GraphField.Children.Add(vertex.DrawVertex());
                GraphField.Children.Add(vertex.DrawName());
            }
            foreach (var edge in graph.Edges)
            {
                Canvas.SetTop(edge.DrawLine(), 0);
                Canvas.SetTop(edge.DrawArrow(), 0);
                Canvas.SetTop(edge.DrawWeight(), (edge.To.Y)-2);
                Canvas.SetTop(edge.DrawWeight(), (edge.To.X)-2);
                GraphField.Children.Add(edge.DrawLine());
                GraphField.Children.Add(edge.DrawArrow());
                GraphField.Children.Add(edge.DrawWeight());
            }

            return GraphField;
        }
    }
}