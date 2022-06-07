using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace KR_OP
{
    public class Graph
    {
        private List<GraphVertex> _vertices = new List<GraphVertex>();
        private List<GraphEdge> _edges = new List<GraphEdge>();
        public double[,] AdjMatrix { get; set; }

        public Graph(double[,] matrix) {
            AdjMatrix = matrix;
            SetVertices();
            SetEdges();
        }
        public List<GraphVertex> Vertices {
            get => _vertices;
            private set => _vertices = value;
        }
        public List<GraphEdge> Edges {
            get => _edges;
            private set => _edges = value;
        }

        public void SetVertices()
        {
            _vertices = new List<GraphVertex>();
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                if (i <= AdjMatrix.GetLength(0) / 2)
                {
                    int x = 100 + 150 * i;
                    int y = 100;
                    _vertices.Add(new GraphVertex(i + 1, x, y));
                }
                else
                {
                    int x = 100 + 150 * i;
                    int y = 300;
                    _vertices.Add(new GraphVertex(i + 1, x, y));
                }
            }

        }

        public void SetEdges()
        {
            _edges = new List<GraphEdge>();
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < AdjMatrix.GetLength(1); j++)
                {
                    if (!Double.IsInfinity(AdjMatrix[i, j]) && i != j)
                    {
                        _edges.Add(new GraphEdge(Vertices[i], Vertices[j], AdjMatrix[i, j]));
                    }
                }
            }
        }
    }
}