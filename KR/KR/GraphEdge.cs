using System;
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

        // public Line DrawLine()
        // {
        //     Line edge = new Line
        //     {
        //         X1 = From.X+15,
        //         X2 = To.X+15,
        //         Y1 = From.Y+15,
        //         Y2 = To.Y+15,
        //         Stroke = Brushes.Black,
        //     };
        //     return edge;
        // }
        
        public Canvas DrawArrow ()
        {
            double x1 = From.X + 15;
            double x2 = To.X + 15;
            double y1 = From.Y + 15;
            double y2 = To.Y + 15;
            Canvas arrow = new Canvas();
            double d = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
 
            double X = x2 - x1;
            double Y = y2 - y1;
 
            double X3 = x2 - (X / d) * 25;
            double Y3 = y2 - (Y / d) * 25;
 
            double Xp = y2 - y1;
            double Yp = x1 - x2;
 
            double X4 = X3 + (Xp / d) * 5;
            double Y4 = Y3 + (Yp / d) * 5;
            double X5 = X3 - (Xp / d) * 5;
            double Y5 = Y3 - (Yp / d) * 5;
 
            Line line = new Line
            {
                Stroke = Brushes.Black,
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2
            };
            arrow.Children.Add(line);
 
            line = new Line
            {
                Stroke = Brushes.Black,
                X1 = x2 - (X / d) * 10,
                Y1 = y2 - (Y / d) * 10,
                X2 = X4,
                Y2 = Y4
            };
            ToolTip weight = new ToolTip
            {
                FontFamily = new FontFamily("Tekton Pro"),
                FontSize = 15,
                Foreground = Brushes.White,
                Background = Brushes.PaleVioletRed,
                Content = $"{Weight}",
            };
            line.ToolTip = weight;
            arrow.Children.Add(line);
 
            line = new Line
            {
                Stroke = Brushes.Black,
                X1 = x2 - (X / d) * 10,
                Y1 = y2 - (Y / d) * 10,
                X2 = X5,
                Y2 = Y5
            };
            line.ToolTip = weight;
            arrow.Children.Add(line);
            return arrow;
        }
        // public Polyline DrawArrow()
        // {
        //     Point Point1 = new Point(To.X + 15,  To.Y);
        //     Point Point2 = new Point(To.X + 15, To.Y + 15);
        //     Point Point3 = new Point(To.X + 30, To.Y + 15);
        //     PointCollection pointCollection = new PointCollection();
        //     pointCollection.Add(Point1);
        //     pointCollection.Add(Point2);
        //     pointCollection.Add(Point3);
        //     
        //     Polyline arrow = new Polyline
        //     {
        //         Stroke = Brushes.Black,
        //         Points = pointCollection,
        //     };
        //     return arrow;
        // }

        // public TextBlock DrawWeight()
        // {
        //     TextBlock weight = new TextBlock
        //     {
        //         FontFamily = new FontFamily("Tekton Pro"),
        //         FontSize = 15,
        //         Foreground = Brushes.Black,
        //         Background = Brushes.Transparent,
        //         Text = $"{Weight}",
        //     };
        //     return weight;
        // }
    }
}