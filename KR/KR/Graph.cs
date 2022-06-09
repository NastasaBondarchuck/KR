using System;
using System.Collections.Generic;

namespace KR
{
    /// <summary>
    /// Class for creating graph based on an adjacency matrix.
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Field with value for filling list of the vertices of the graph. 
        /// </summary>
        private List<GraphVertex> _vertices = new List<GraphVertex>();
        /// <summary>
        /// Field with value for filling list of the edges of the graph. 
        /// </summary>
        private List<GraphEdge> _edges = new List<GraphEdge>();
        /// <summary>
        /// Property with adjacency matrix of the graph.
        /// </summary>
        public double[,] AdjMatrix { get; set; }
        /// <summary>
        /// Constructor that creates lists of the vertices and the edges, when adjacency matrix is filled.
        /// </summary>
        /// <param name="matrix"></param>
        public Graph(double[,] matrix) {
            AdjMatrix = matrix;
            SetVertices();
            SetEdges();
        }
        /// <summary>
        /// Property with list of the vertices of the graph.
        /// </summary>
        public List<GraphVertex> Vertices => _vertices;
        /// <summary>
        /// Property with list of the edges of the graph.
        /// </summary>
        public List<GraphEdge> Edges => _edges;

        /// <summary>
        /// Method that fills list of vertices with information from the adjacency matrix.
        /// </summary>
        private void SetVertices()
        {
            _vertices = new List<GraphVertex>();
            for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            {
                
                if (i < AdjMatrix.GetLength(0) / 2)
                {
                    int x = 100 + 150 * i;
                    int y = 50 - 50 * (i % 2);
                    _vertices.Add(new GraphVertex(i + 1, x, y));
                }
                else
                {
                    int x = 100 + 150 * (i-AdjMatrix.GetLength(0) / 2);
                    int y = 200 - 50 * (i % 2);
                    _vertices.Add(new GraphVertex(i + 1, x, y));
                }
            }

        }
        /// <summary>
        /// Method that fills list of edges with information from the adjacency matrix.
        /// </summary>
        private void SetEdges()
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