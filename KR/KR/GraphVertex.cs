using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KR
{
    /// <summary>
    /// Class for creating vertex of the graph.
    /// </summary>
    public class GraphVertex
    {
        /// <summary>
        ///Property with name (or number) of the vertex.
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Property with X-coordinate of the vertex on window.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Property with Y-coordinate of the vertex on window.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Constructor that creates vertex with necessary attributes.
        /// </summary>
        /// <param name="number">Value for setting name (or  number) of vertex.</param>
        /// <param name="x">Value for setting X-coordinate of vertex.</param>
        /// <param name="y">Value for setting Y-coordinate of vertex.</param>
        public GraphVertex(int number, int x, int y)
        {
            Number = number;
            X = x;
            Y = y;
        }
        /// <summary>
        /// Method that creates circle, which is visual model of vertex.
        /// </summary>
        /// <returns>Visual model of vertex with "Ellipse" type.</returns>
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
        /// <summary>
        /// Method that adds name (or number) to visual model of vertex.
        /// </summary>
        /// <returns>Name (or number) of vertex with "Textbox" type.</returns>
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