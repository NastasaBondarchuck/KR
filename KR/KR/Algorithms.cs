using System;

namespace KR
{
    public static class Algorithms
    {
        private static double[,] CreateResultMatrix(double[,] adjMatrix)
        {
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
        public static int[,] CreatePathMatrix(double[,] adjMatrix)
        {
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
        public static double[,] FloydAlgorythm(double[,] adjMatrix, int[,] pathMatrix)
        {
            double[,] resultMatrix = CreateResultMatrix(adjMatrix);
            for (int k = 0; k < resultMatrix.GetLength(0); k++)
            {
                ChooseMin(resultMatrix, pathMatrix, resultMatrix.GetLength(0), k);
            }

            return resultMatrix;
        }
        
        
        public static double[,] DansigAlgorythm(double[,] adjMatrix, int[,] pathMatrix)
        {
            double[,] resultMatrix = CreateResultMatrix(adjMatrix);
            adjMatrix[0, 0] = 0;
            for (int m = 2; m < resultMatrix.GetLength(0); m++)
            {
                for (int k = 0; k < m; k++)
                {
                    ChooseMin(resultMatrix, pathMatrix, m, k);
                }
                FindMinMj(m, resultMatrix, pathMatrix);
                FindMinIm(m, resultMatrix, pathMatrix);
            }
            LastStep(resultMatrix, pathMatrix);
            return resultMatrix;
            
        }
        public static void ChooseMin(double[,] adjMatrix, int[,] pathMatrix, int m, int k)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
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
        public static void LastStep(double[,] adjMatrix, int[,] pathMatrix)
        {
            for (int k = 0; k < adjMatrix.GetLength(0); k++)
            {
                ChooseMin(adjMatrix, pathMatrix, adjMatrix.GetLength(0), k);
            }
        }
        public static void FindMinIm(int m, double[,] adjMatrix, int[,] pathMatrix)
        {
            for (int i = 0; i < m; i++)
            {
                double min = adjMatrix[i, m];
                for (int k = 0; k < m; k++)
                {
                    if (i!=k && adjMatrix[i, k] + adjMatrix[k, m] <= min)
                    {
                        pathMatrix[i, m] = k;
                        min = adjMatrix[i, k] + adjMatrix[k, m];
                    }
                }
                adjMatrix[i, m] = min;
            }
        }
        public static void FindMinMj(int m, double[,] adjMatrix, int[,] pathMatrix)
        {
            for (int j = 0; j < m; j++)
            {
                double min = adjMatrix[m, j];
                for (int k = 0; k < m; k++)
                {
                    if (j!=k && adjMatrix[m, k] + adjMatrix[k, j] <= min)
                    {
                        pathMatrix[m, j] = k;
                        min = adjMatrix[m, k] + adjMatrix[k, j];
                    }
                }
                adjMatrix[m, j] = min;
            }
        }
    
        public static string FindPath(int[,] pathMatrix, int from, int to)
        {
            string path = $"{@from+1}->";
            while (pathMatrix[@from, to] != to)
            {
                int temp1 = pathMatrix[@from, to];
                while (pathMatrix[@from, temp1] != temp1)
                {
                    int temp2 = pathMatrix[@from, temp1];
                    path += $"{pathMatrix[@from, temp1]+1}->";
                    @from = temp2;
                    
                }
                path += $"{pathMatrix[@from, to]+1}->";
                @from = pathMatrix[pathMatrix[@from, to], to];
            }

            path += $"{to+1}";
            return path;
        }
    }
} 
