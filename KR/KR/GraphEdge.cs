using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KR
{
    /// <summary>
    /// Class for creating edge of the graph.
    /// </summary>
    public class GraphEdge
    {
        /// <summary>
        /// Property for setting information about vertex, which is start of the edge.
        /// </summary>
        public GraphVertex From { get; set; }
        /// <summary>
        /// Property for setting information about vertex. which is end of the edge.
        /// </summary>
        public GraphVertex To { get; set; }
        /// <summary>
        /// Property for setting weight of the edge.
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Constructor that creates edge with all necessary attributes.
        /// </summary>
        /// <param name="from">Value for setting start-vertex of the edge.</param>
        /// <param name="to">Value for setting end-vertex of the edge.</param>
        /// <param name="weight">Value for setting weight of the edge.</param>
        public GraphEdge(GraphVertex from, GraphVertex to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
        /// <summary>
        /// Method that creates visual model of arrow between start- and end-vertices with tip contains edge's weight.
        /// </summary>
        /// <returns>Visual model of the edge with "Canvas" type.</returns>
        public Canvas DrawEdge ()
        {
            // x1, y1 - start coordinates of central edge's line;
            // x2, y2 - end coordinates of central edge's line;
            // X, Y - differences between start and end coordinates;
            // d - length of central edge's line;
            // X3, Y3 - coordinates of dot on the central line that is extreme length of arrow's lines;
            // Xp, Yp - inverted coordinates those set direction of arrow's lines;
            // X4, Y4 - coordinates of end of the left arrow's line;
            // X5, Y5 - coordinates of end of the right arrow's line;
            // line - main object of the edge;
            // weight - tooltip with the value of edge's weight;
            // tip - circle that is the box for tooltip reaction.
            double x1 = From.X + 15;
            double x2 = To.X + 15;
            double y1 = From.Y + 15;
            double y2 = To.Y + 15;
            double X = x2 - x1;
            double Y = y2 - y1;
            
            double d = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            
            double X3 = x2 - (X / d) * 30;
            double Y3 = y2 - (Y / d) * 30;

            double Xp = y2 - y1;
            double Yp = x1 - x2;
 
            double X4 = X3 + (Xp / d) * 5;
            double Y4 = Y3 + (Yp / d) * 5;
            double X5 = X3 - (Xp / d) * 5;
            double Y5 = Y3 - (Yp / d) * 5;
            
            Canvas _edge = new Canvas();
            
            
            Line line = new Line
            {
                Stroke = Brushes.Gray,
                StrokeThickness = 2,
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            };
            _edge.Children.Add(line);
 
            line = new Line
            {
                Stroke = Brushes.Gray,
                StrokeThickness = 2,
                X1 = x2 - (X / d) * 15,
                Y1 = y2 - (Y / d) * 15,
                X2 = X4,
                Y2 = Y4
            };
            _edge.Children.Add(line);
 
            line = new Line
            {
                Stroke = Brushes.Gray,
                StrokeThickness = 2,
                X1 = x2 - (X / d) * 15,
                Y1 = y2 - (Y / d) * 15,
                X2 = X5,
                Y2 = Y5
            };
            _edge.Children.Add(line);
            
            ToolTip weight = new ToolTip
            {
                FontFamily = new FontFamily("Tekton Pro"),
                FontSize = 15,
                Foreground = Brushes.White,
                Background = Brushes.PaleVioletRed,
                Content = $"{Weight}",
                StaysOpen = true,
            };
            Ellipse tip = new Ellipse()
            {
                Height = 20,
                Width = 20,
                Fill = Brushes.Transparent,
            };
            Canvas.SetTop(tip, y2 - (Y / d) * 25 - 10);
            Canvas.SetLeft(tip,  x2 - (X / d) * 25 - 10);
            Panel.SetZIndex(tip, 10);
            tip.ToolTip = weight;
            _edge.Children.Add(tip);
            return _edge;
        }
    }
}