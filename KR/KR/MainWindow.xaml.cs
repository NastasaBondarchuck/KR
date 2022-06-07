using System;
using System.Collections.Generic;
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
            //Pointing UI to refer to this class properties
            DataContext = this;
            Graph graph = new Graph(AdjMatrix);
            GraphGrid.Children.Add(DrawGraph(graph));
            DrawGraph(graph);
        }
        //Moved matrix into property for UI's access 
        public double[,] AdjMatrix => new double[,] {
                {0, -2, 3, -3},
                {double.PositiveInfinity, 0, 2, double.PositiveInfinity},
                {double.PositiveInfinity, double.PositiveInfinity, 0, -3},
                {4, 5, 5, 0 }
        };
        //Property for sizes list
        public List<int> Sizes => new List<int>() {
            2,3,4,5
        };
        //Property for selected size (updates automaticly)
        public int SelectedSize { get; set; } = 2;

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
                Canvas.SetTop(edge.DrawWeight(), edge.To.Y - 2);
                Canvas.SetTop(edge.DrawWeight(), edge.To.X - 2);
                GraphField.Children.Add(edge.DrawLine());
                GraphField.Children.Add(edge.DrawArrow());
                GraphField.Children.Add(edge.DrawWeight());
            }

            return GraphField;
        }
    }
}