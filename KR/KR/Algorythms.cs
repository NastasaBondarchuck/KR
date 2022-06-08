using System;
using System.IO;
using System.Windows;
using KR;

namespace KR_OP
{
    public class Algorythms
    {
        public static int[,] CreatePathMatrix(double[,] AdjMatrix)
        {
            int[,] PathMatrix = new int[AdjMatrix.GetLength(0),AdjMatrix.GetLength(1)];
            for (int i = 0; i < PathMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < PathMatrix.GetLength(1); j++)
                {
                    PathMatrix[i, j] = j;
                }
            }

            return PathMatrix;
        }
        public static double[,] FloydAlgorythm(double[,] AdjMatrix, int[,] PathMatrix)
        {
            // double[,] AdjMatrix = new double[AdjMatrix.GetLength(0),AdjMatrix.GetLength(1)];
            // for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            // {
            //     for (int j = 0; j < AdjMatrix.GetLength(1); j++)
            //     {
            //         AdjMatrix[i, j] = AdjMatrix[i, j];
            //     }
            // }
            
            for (int k = 0; k < AdjMatrix.GetLength(0); k++)
            {
                ChooseMin(AdjMatrix, PathMatrix, AdjMatrix.GetLength(0), k);
                // for (int i = 0; i < AdjMatrix.GetLength(0); i++)
                // {
                //     if (AdjMatrix[i,i]<0)
                //     {
                //         string messageBoxText = "Graph have negative contour!\n" +
                //                                 "Try to change adjacency matrix.";
                //         string caption = "Negative Contour";
                //         MessageBoxButton button = MessageBoxButton.OK;
                //         MessageBoxImage icon = MessageBoxImage.Information;
                //         MessageBoxResult result;
                //
                //         result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.None);
                //     }
                // }
            }

            return AdjMatrix;
        }
        
        public static double[,] DansigAlgorythm(double[,] AdjMatrix, int[,] PathMatrix)
        {
            // double[,] AdjMatrix = new double[AdjMatrix.GetLength(0),AdjMatrix.GetLength(1)];
            // for (int i = 0; i < AdjMatrix.GetLength(0); i++)
            // {
            //     for (int j = 0; j < AdjMatrix.GetLength(1); j++)
            //     {
            //         AdjMatrix[i, j] = AdjMatrix[i, j];
            //     }
            // }
            
            AdjMatrix[0, 0] = 0;
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
            return AdjMatrix;
        }
        public static void ChooseMin(double[,] AdjMatrix, int[,] PathMatrix, int m, int k)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (AdjMatrix[i, k] + AdjMatrix[k, j] < AdjMatrix[i, j])
                    {
                        AdjMatrix[i, j] = AdjMatrix[i, k] + AdjMatrix[k, j];
                        PathMatrix[i, j] = k;
                    }

                    if (AdjMatrix[i, i] < 0)
                    {
                        MainWindow checkContour = new MainWindow();
                        checkContour.CheckNegativeContour();
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
            string path = $"{From+1}->";
            while (PathMatrix[From, To] != To)
            {
                int temp1 = PathMatrix[From, To];
                while (PathMatrix[From, temp1] != temp1)
                {
                    int temp2 = PathMatrix[From, temp1];
                    path += $"{PathMatrix[From, temp1]+1}->";
                    From = temp2;
                    
                }
                path += $"{PathMatrix[From, To]+1}->";
                From = PathMatrix[PathMatrix[From, To], To];
            }

            path += $"{To+1}";
            return path;
        }
    }
} 
