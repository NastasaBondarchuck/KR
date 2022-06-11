using System;

namespace KR
{
    /// <summary>
    /// Class for running search shortest ways algorithms.
    /// </summary>
    public static class Algorithms
    {
        /// <summary>
        /// Accessory method that creates result matrix by coping elements' values from adjacency matrix.
        /// </summary>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <returns>Result matrix that contains all shortest paths between all vertices with "double[,]" type.</returns>
        private static double[,] CreateResultMatrix(double[,] adjMatrix)
        {
            // i, j - indices of result matrix' elements.
            double[,] resultMatrix = new double[adjMatrix.GetLength(0), adjMatrix.GetLength(1)];
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    resultMatrix[i, j] = adjMatrix[i, j];
                }
            }
            return resultMatrix;
        }
        /// <summary>
        /// Accessory method that creates path matrix with start values those are equal indices of matrix' elements. 
        /// </summary>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user. (Using only for check size of path matrix.)</param>
        /// <returns>Path matrix that contains all vertices are enable from another vertices with "int[,]" type.</returns>
        public static int[,] CreatePathMatrix(double[,] adjMatrix)
        {
            // i, j - indices of path matrix' elements.
            int[,] pathMatrix = new int[adjMatrix.GetLength(0),adjMatrix.GetLength(1)];
            for (int i = 0; i < pathMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < pathMatrix.GetLength(1); j++)
                {
                    pathMatrix[i, j] = j;
                }
            }

            return pathMatrix;
            
        }
        /// <summary>
        /// Method that runs floyd algorithm and changes adjacency and path matrices.
        /// </summary>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <param name="pathMatrix">Matrix that contains all vertices are enable from another vertices.</param>
        /// <param name="iterationCounter">Counter of iterations of that algorithm.</param>
        /// <returns>Result matrix with shortest paths' values between all vertices.</returns>
        public static double[,] FloydAlgorithm(double[,] adjMatrix, int[,] pathMatrix, ref int iterationCounter)
        {
            // k - intermediate vertex' number.
            double[,] resultMatrix = CreateResultMatrix(adjMatrix);
            for (int k = 0; k < resultMatrix.GetLength(0); k++)
            {
                ChooseMin(resultMatrix, pathMatrix, resultMatrix.GetLength(0), k, ref iterationCounter);
            }

            return resultMatrix;
        }
        
        /// <summary>
        /// Method that runs dansig algorithm and changes adjacency and path matrices.
        /// </summary>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <param name="pathMatrix">Matrix that contains all vertices are enable from another vertices.</param>
        /// <param name="iterationCounter">Counter of iterations of that algorithm.</param>
        /// <returns>Result matrix with shortest paths' values between all vertices.</returns>
        public static double[,] DansigAlgorythm(double[,] adjMatrix, int[,] pathMatrix,  ref int iterationCounter)
        {
            // m - size of the sub-matrix of adjacency matrix;
            // k - intermediate vertex' number.
            double[,] resultMatrix = CreateResultMatrix(adjMatrix);
            adjMatrix[0, 0] = 0;
            for (int m = 2; m < resultMatrix.GetLength(0); m++)
            {
                for (int k = 0; k < m; k++)
                {
                    ChooseMin(resultMatrix, pathMatrix, m, k, ref iterationCounter);
                }
                FindMinMj(m, resultMatrix, pathMatrix, ref iterationCounter);
                iterationCounter++;
                FindMinIm(m, resultMatrix, pathMatrix, ref iterationCounter);
                iterationCounter++;
            }
            LastStep(resultMatrix, pathMatrix, ref iterationCounter);
            return resultMatrix;
            
        }
        /// <summary>
        /// Accessory method that chooses and replaces elements of the adjacency matrix to minimal value;
        /// replaces elements of the path matrix to specific values.
        /// </summary>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <param name="pathMatrix">Matrix that contains all vertices are enable from another vertices.</param>
        /// <param name="m">Extreme value of indices.</param>
        /// <param name="k">Intermediate vertex between two vertices.</param>
        /// <param name="iterationCounter">Counter of iterations of the algorithm.</param>
        /// <exception cref="Exception">Negative contour exception.</exception>
        public static void ChooseMin(double[,] adjMatrix, int[,] pathMatrix, int m, int k, ref int iterationCounter)
        {
            // i, j - indices of adjacency matrix' elements.
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    iterationCounter++;
                    if (adjMatrix[i, k] + adjMatrix[k, j] < adjMatrix[i, j])
                    {
                        adjMatrix[i, j] = adjMatrix[i, k] + adjMatrix[k, j];
                        pathMatrix[i, j] = k;
                    }

                    if (adjMatrix[i, i] < 0)
                    {
                        throw new Exception();
                    }
                }
            }
        }
        /// <summary>
        /// Accessor method that runs last step of dansig algorithm
        /// with enumeration of all vertices same with floyd algorithm.
        /// </summary>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <param name="pathMatrix">Matrix that contains all vertices are enable from another vertices.</param>
        /// <param name="iterationCounter">Counter of iterations of the algorithm.</param>
        public static void LastStep(double[,] adjMatrix, int[,] pathMatrix, ref int iterationCounter)
        {
            // k - intermediate vertex' number.
            for (int k = 0; k < adjMatrix.GetLength(0); k++)
            {
                ChooseMin(adjMatrix, pathMatrix, adjMatrix.GetLength(0), k, ref iterationCounter);
            }
        }
        /// <summary>
        /// Accessor method that finds minimal value and replace specific element's value to that minimum, while j=m.
        /// </summary>
        /// <param name="m">Extreme value of indices.</param>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <param name="pathMatrix">Matrix that contains all vertices are enable from another vertices.</param>
        /// <param name="iterationCounter">Counter of iterations of the algorithm.</param>
        public static void FindMinIm(int m, double[,] adjMatrix, int[,] pathMatrix, ref int iterationCounter)
        {
            // i - index of adjacency matrix' elements;
            // k - intermediate vertex' number.
            for (int i = 0; i < m; i++)
            {
                double min = adjMatrix[i, m];
                for (int k = 0; k < m; k++)
                {
                    //iterationCounter++;
                    if (i!=k && adjMatrix[i, k] + adjMatrix[k, m] < min)
                    {
                        pathMatrix[i, m] = k;
                        min = adjMatrix[i, k] + adjMatrix[k, m];
                    }
                }
                adjMatrix[i, m] = min;
            }
        }
        /// <summary>
        /// Accessor method that finds minimal value and replace specific element's value to that minimum, while i=m.
        /// </summary>
        /// <param name="m">Extreme value of indices.</param>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <param name="pathMatrix">Matrix that contains all vertices are enable from another vertices.</param>
        /// <param name="iterationCounter">Counter of iterations of the algorithm.</param>
        public static void FindMinMj(int m, double[,] adjMatrix, int[,] pathMatrix, ref int iterationCounter)
        {
            // j - index of adjacency matrix' elements;
            // k - intermediate vertex' number.
            for (int j = 0; j < m; j++)
            {
                double min = adjMatrix[m, j];
                for (int k = 0; k < m; k++)
                {
                    //iterationCounter++;
                    if (j!=k && adjMatrix[m, k] + adjMatrix[k, j] < min)
                    {
                        pathMatrix[m, j] = k;
                        min = adjMatrix[m, k] + adjMatrix[k, j];
                    }
                }
                adjMatrix[m, j] = min;
            }
        }

        /// <summary>
        /// Method that find path between two vertices.
        /// </summary>
        /// <param name="pathMatrix">Matrix that contains all vertices are enable from another vertices.</param>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <param name="from">Name (or number) of start-vertex.</param>
        /// <param name="to">Name (or number) of end-vertex.</param>
        /// <returns>Path between two vertices in format "start->middle...->middle->end" with "string" type.</returns>
        public static string FindPath(int[,] pathMatrix, double[,] adjMatrix, int from, int to)
        {
            // temp1, temp2 - accessory temporary variables.
            string path = "";
            if (!CheckPathPresence(adjMatrix, from, to, ref path))
            {
                return path;
            }
            path = $"{from+1}->";
            
            while (pathMatrix[from, to] != to)
            {
                int temp1 = pathMatrix[from, to];
                while (pathMatrix[from, temp1] != temp1 && CheckPathPresence(adjMatrix, from, temp1, ref path))
                {
                    int temp2 = pathMatrix[from, temp1];
                    path += $"{pathMatrix[from, temp1]+1}->";
                    from = temp2;
                
                }
                path += $"{pathMatrix[from, to]+1}->";
                from = pathMatrix[pathMatrix[from, to], to];
            }

            path += $"{to+1}";
            return path;
        }
        /// <summary>
        /// Method that checks path presence between two vertices.
        /// </summary>
        /// <param name="adjMatrix">Adjacency matrix that is filled by user.</param>
        /// <param name="from">Name (or number) of start-vertex.</param>
        /// <param name="to">Name (or number) of end-vertex.</param>
        /// <param name="path">Path between two vertices.</param>
        /// <returns></returns>
        public static bool CheckPathPresence(double[,] adjMatrix, int from, int to, ref string path)
        {
            if (Double.IsInfinity(adjMatrix[from, to]))
            {
                path = "none.";
                return false;
            }

            return true;
        }
    }
} 
