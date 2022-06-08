using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KR_OP
{
    public class GraphVertex
    {
        public int Number { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public GraphVertex(int number, int x, int y)
        {
            Number = number;
            X = x;
            Y = y;
        }
        
        public override string ToString() => Number.ToString();

        public Ellipse DrawVertex()
        {
            Ellipse vertex = new Ellipse
            {
                Stroke = Brushes.DarkGray,
                Fill = Brushes.Pink,
                Width = 30,
                Height = 30,
                Name = $"v{Number}",
            };
            return vertex;
        }
        public TextBox DrawName(){
            TextBox name = new TextBox
            {
                FontFamily = new FontFamily("Tekton Pro"),
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                IsReadOnly = true,
                IsEnabled = false,
                BorderBrush = Brushes.Transparent,
                FontSize = 15,
                Width = 30,
                Height = 30,
                Foreground = Brushes.Navy,
                Background = Brushes.Transparent,
                Text = $"{Number}",
            };
            return name;
        }
    }
}