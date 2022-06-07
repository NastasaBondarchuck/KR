using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KR_OP
{
    public class GraphEdge
    {
        public GraphVertex From { get; set; }
        public GraphVertex To { get; set; }
        public double Weight { get; set; }
        public GraphEdge(GraphVertex from, GraphVertex to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public Line DrawLine()
        {
            Line edge = new Line
            {
                X1 = From.X,
                X2 = To.X,
                Y1 = From.Y,
                Y2 = To.Y,
                Stroke = Brushes.Black,
            };
            return edge;
        }
        
        public Polyline DrawArrow()
        {
            Point Point1 = new Point((int)To.X - 5, (int) To.Y - 5);
            Point Point2 = new Point((int)To.X, (int) To.Y);
            Point Point3 = new Point((int)To.X + 5, (int) To.Y + 5);
            PointCollection pointCollection = new PointCollection();
            pointCollection.Add(Point1);
            pointCollection.Add(Point2);
            pointCollection.Add(Point3);
            
            Polyline arrow = new Polyline
            {
                Stroke = Brushes.Black,
                Points = pointCollection,
            };
            return arrow;
        }

        public TextBlock DrawWeight()
        {
            TextBlock weight = new TextBlock
            {
                FontFamily = new FontFamily("Tekton Pro"),
                FontSize = 15,
                Foreground = Brushes.Black,
                Background = Brushes.Transparent,
                Text = $"{Weight}",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            return weight;
        }
    }
}