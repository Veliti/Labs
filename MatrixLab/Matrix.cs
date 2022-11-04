using System;
using System.Collections.Generic;
using System.Text;

public class Matrix
    {
        protected int xLength => _matrix.GetLength(0);
        protected int yLength => _matrix.GetLength(1);
        protected int[,] _matrix;

        public Matrix(int[,] matrix)
        {  
            if (matrix == null) 
                throw new Exception("matrix size is 0");

            _matrix = matrix;
        }

        public void Add(Matrix matrix)
        {
            if (xLength != matrix.xLength || yLength != matrix.yLength)
                throw new Exception("Cannot add matrices of different size");

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    _matrix[x, y] += matrix._matrix[x, y];
                }
            }
        }

        public void Subtract(Matrix matrix)
        {
            if (xLength != matrix.xLength || yLength != matrix.yLength) 
                throw new Exception("Cannot subtract matrices of different size");
                
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    _matrix[x, y] -= matrix._matrix[x, y];
                }
            }
        }

        public void Multiply(int number)
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    _matrix[x, y] *= number;
                }
            }
        }

        public void Transpose()
        {
            var tMatrix = new int[yLength, xLength];
            for (int i = 0; i < yLength; i++)
            {
                for (int j = 0; j < xLength; j++)
                {
                    tMatrix[i, j] = _matrix[j, i];
                }
            }
            _matrix = tMatrix;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    builder.Append($"{_matrix[x,y], 10}");
                }
                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
        
        protected int CalculateMinor(int[,] matrix , List<int> includedX, List<int> includedY)
            {
                if(xLength - includedX.Count != yLength - includedY.Count)
                    throw new Exception("Trying to calculate determinant of non-square minor");

                if (includedX.Count == 2)
                {
                    return  matrix[includedX[0], includedY[0]]* matrix[includedX[1], includedY[1]]
                           - matrix[includedX[0], includedY[1]] * matrix[includedX[1], includedY[0]];
                }

                var determinant = 0;

                var x = includedX[0];
                var n = 0;
                foreach (var y in includedY)
                {
                    var newX = new List<int>(includedX);
                    var newY = new List<int>(includedY);
                    newX.Remove(x);
                    newY.Remove(y);
                    determinant += (int)Math.Pow(-1, n) * matrix[x, y] * CalculateMinor(matrix ,newX, newY);
                    n++;
                }

                return determinant;
            }
    }
