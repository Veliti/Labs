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

        public Matrix Multiply(int number)
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    _matrix[x, y] *= number;
                }
            }
            return this;
        }

        //Умножение на матрицу
        public void Multiply(Matrix matrix)
        {
            if(xLength != matrix.yLength)
                throw new Exception("Matrix x should be equal other matrix y");

            var result = new int[xLength,matrix.yLength];
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < matrix.yLength; y++)
                {
                    for (int i = 0; i < xLength; i++)
                    {
                        result[x,y] += _matrix[x, i] * matrix._matrix[i, y];
                    }
                }
            }
            
            _matrix = result;
        }

        public Matrix Transpose()
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
            return this;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    builder.Append($"{_matrix[x,y], 3}");
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

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            matrix1.Multiply(matrix2);
            return matrix1;
        }
        //умножение на число
        public static Matrix operator *(int number, Matrix matrix)
        {
            matrix.Multiply(number);
            return matrix;
        }
    }
