using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KR
{
    /// <summary>
    /// Class for control adjacency matrix, its resize and refill.
    /// </summary>
    public class MatrixViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Field with value for getting selected size choosing by user.
        /// </summary>
        private int _selectedSize = 2;
        /// <summary>
        /// Field with value for creating adjacency matrix filling by user.
        /// </summary>
        private double[,] _matrix;
        /// <summary>
        /// Field with value for creating result matrix that will be changed and displayed on screen.
        /// </summary>
        private double[,] _resultmatrix;

        /// <summary>
        /// Property with creating and changing adjacency matrix.
        /// </summary>
        public double[,] Matrix 
        {
            get => _matrix;

            set {
                _matrix = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Property with creating and changing result matrix.
        /// </summary>
        public double[,] ResultMatrix
        {
            get => _resultmatrix;

            set {
                _resultmatrix = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Property for setting list with possible size of adjacency matrix and graph.
        /// </summary>
        public List<int> Sizes => new List<int>() {
            2,3,4,5,6,7,8,9,10
        };
        /// <summary>
        /// Property for setting selected size to resize adjacency matrix.
        /// </summary>
        public int SelectedSize {
            get => _selectedSize;
            set {
                _selectedSize = value;
                OnPropertyChanged();
                ChangeMatrixSize(value);
            }
        }
        /// <summary>
        /// Event that controls all changes in properties of the matrices.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Constructor that refills and resizes matrix to default settings.
        /// </summary>
        public MatrixViewModel()
        {
            RefillMatrix();
        }
        /// <summary>
        /// Method that sets default adjacency matrix.
        /// </summary>
        /// <returns>Adjacency matrix with default settings with "double[,]" type.</returns>
        public double[,] RefillMatrix()
        {
            Matrix = new double[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Matrix[i, j] = double.PositiveInfinity;
                }
            
                Matrix[i, i] = 0;
            }

            return Matrix;
        }
        /// <summary>
        /// Method that changes adjacency matrix' size to selected by user.
        /// </summary>
        /// <param name="size">Selected by user size.</param>
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
        /// <summary>
        /// Method that dynamical changes properties of matrices. 
        /// </summary>
        /// <param name="name">Name of changed property.</param>
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
