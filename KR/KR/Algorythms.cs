using System;

namespace KR_OP
{
    public class Algorythms
    {
        public static void FloydAlgorythm(double[,] AdjMatrix, int[,] PathMatrix)
        {
            for (int k = 0; k < AdjMatrix.GetLength(0); k++)
            {
                ChooseMin(AdjMatrix, PathMatrix, AdjMatrix.GetLength(0), k);
                for (int i = 0; i < AdjMatrix.GetLength(0); i++)
                {
                    if (AdjMatrix[i,i]<0)
                    {
                        Console.WriteLine("Negative contour!");
                        break;
                    }
                }
            }
        }
        
        public static void DansigAlgorythm(double[,] AdjMatrix, int[,] PathMatrix)
        {
            for (int m = 2; m < AdjMatrix.GetLength(0); m++)
            {
                for (int k = 0; k < m; k++)
                {
                    ChooseMin(AdjMatrix, PathMatrix, m, k);
                }
                FindMinMJ(m, AdjMatrix, PathMatrix);
                FindMinIM(m, AdjMatrix, PathMatrix);
            }
            LastStep(AdjMatrix, PathMatrix);
        }
        public static void ChooseMin(double[,] AdjMatrix, int[,] PathMatrix, int m, int k)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i!=j && AdjMatrix[i, k] + AdjMatrix[k, j] < AdjMatrix[i, j])
                    {
                        AdjMatrix[i, j] = AdjMatrix[i, k] + AdjMatrix[k, j];
                        PathMatrix[i, j] = k;
                    }
                }
            }
        }
        public static void LastStep(double[,] AdjMatrix, int[,] PathMatrix)
        {
            for (int k = 0; k < AdjMatrix.GetLength(0); k++)
            {
                ChooseMin(AdjMatrix, PathMatrix, AdjMatrix.GetLength(0), k);
            }
        }
        public static void FindMinIM(int m, double[,] AdjMatrix, int[,] PathMatrix)
        {
            for (int i = 0; i < m; i++)
            {
                double min = AdjMatrix[i, m];
                for (int k = 0; k < m; k++)
                {
                    if (i!=k && AdjMatrix[i, k] + AdjMatrix[k, m] <= min)
                    {
                        PathMatrix[i, m] = k;
                        min = AdjMatrix[i, k] + AdjMatrix[k, m];
                    }
                }
                AdjMatrix[i, m] = min;
            }
        }
        public static void FindMinMJ(int m, double[,] AdjMatrix, int[,] PathMatrix)
        {
            for (int j = 0; j < m; j++)
            {
                double min = AdjMatrix[m, j];
                for (int k = 0; k < m; k++)
                {
                    if (j!=k && AdjMatrix[m, k] + AdjMatrix[k, j] <= min)
                    {
                        PathMatrix[m, j] = k;
                        min = AdjMatrix[m, k] + AdjMatrix[k, j];
                    }
                }
                AdjMatrix[m, j] = min;
            }
        }
    
        public static string FindPath(int[,] PathMatrix, int From, int To)
        {
            string path = $"{From}->";
            while (PathMatrix[From, To] != To)
            {
                int temp1 = PathMatrix[From, To];
                while (PathMatrix[From, temp1] != temp1)
                {
                    int temp2 = PathMatrix[From, temp1];
                    path += $"{PathMatrix[From, temp1]}->";
                    From = temp2;
                    
                }
                path += $"{PathMatrix[From, To]}->";
                From = PathMatrix[PathMatrix[From, To], To];
            }

            path += $"{To}";
            return path;
        }
    }
} 
