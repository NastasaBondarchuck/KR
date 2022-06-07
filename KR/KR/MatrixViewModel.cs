using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KR
{
    public class MatrixViewModel : INotifyPropertyChanged
    {
        private int _selectedSize = 2;
        private double[,] _matrix;
        private double[,] _resultmatrix;

        public double[,] Matrix 
        {
            get {
                return _matrix;
            }

            set {
                _matrix = value;
                OnPropertyChanged();
            }
        }

        public double[,] ResultMatrix
        {
            get {
                return _resultmatrix;
            }

            set {
                _resultmatrix = value;
                OnPropertyChanged();
            }
        }

        
        //Property for sizes list
        public List<int> Sizes => new List<int>() {
            2,3,4,5,6,7,8,9,10
        };

        //Property for selected size (updates automaticly)
        public int SelectedSize {
            get {
                return _selectedSize;
            }
            set {
                _selectedSize = value;
                OnPropertyChanged();
                ChangeMatrixSize(value);
            }
        }
        private void GetResultMatrix(double[,] matrix)
        {
            ResultMatrix = matrix;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public MatrixViewModel()
        {
            RefillMatrix();
        }
        private void RefillMatrix()
        {
            // Matrix = new double[,]{
            //     { 0, -2, 3, -3},
            //     { Double.PositiveInfinity, 0, 2, Double.PositiveInfinity},
            //     { Double.PositiveInfinity, Double.PositiveInfinity, 0, -3},
            //     { 4, 5, 5, 0}
            // };
            Matrix = new double[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Matrix[i, j] = double.PositiveInfinity;
                }
            
                Matrix[i, i] = 0;
            }
        }
        private void ChangeMatrixSize(int size)
        {
            double[,] buffer = new double[Matrix.GetLength(0), Matrix.GetLength(1)];
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    buffer[i, j] = Matrix[i, j];
                }
            }
            Matrix = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                        Matrix[i, j] = i < buffer.GetLength(0) && j < buffer.GetLength(1) ? buffer[i, j] : double.PositiveInfinity;
                    else
                        Matrix[i, j] = 0D;
                }
            }
        }
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
