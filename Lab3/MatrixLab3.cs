


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public class MatrixLab3
{

        protected int xLength => _matrix.GetLength(0);
        protected int yLength => _matrix.GetLength(1);
        private double[,] _matrix;

        public MatrixLab3(double[,] matrix) => _matrix = matrix;

        // Task 1.1
        public MatrixLab3()
        {
            List<int> dimentions;

            System.Console.WriteLine("Insert dimentions of matrix");
            while (!TryReadInts(2, out dimentions))
            {
                System.Console.WriteLine("Try again Insert dimentions of matrix");
            }
            int n = dimentions[0];
            int k = dimentions[1];

            List<int> members;
            System.Console.WriteLine($"Insert {n * k} numbers");
            while (!TryReadInts(n * k, out members))
            {
                System.Console.WriteLine($"Try again Insert {n * k} numbers");
            }

            _matrix = new double[n,k];
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < k; y++)
                {
                    _matrix[x,y] = members[n * x + y];
                }
            }
        }

        bool TryReadInts(int amount, out List<int> ints)
        {
            ints = new List<int>(amount);

            while (ints.Count < amount)
            {
                System.Console.Write($"Insert {amount - ints.Count} ints :");
                var str = Console.ReadLine();
                ints.AddRange(Array.ConvertAll(str!.Split(' '), int.Parse));
            }

            if (ints.Count != amount)
            {
                return false;
            }

            return true;
        }
        
        //Task 1.2
        public MatrixLab3(int size, int upRangeFrom , int upRangeTo, double lowRangeFrom, double lowRangeTo)
        {
            _matrix = new double[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (x - y >= 0)
                    {
                        _matrix[x,y] = lowRangeFrom + (lowRangeTo - lowRangeFrom) * Random.Shared.NextDouble();

                    }
                    else
                    {
                        _matrix[x,y] = Random.Shared.Next(upRangeFrom, upRangeTo);
                    }
                }
            }
        }

        //Task 1.3
        public MatrixLab3(int size)
        {
            _matrix = new double[size, size];
            var nextNumber =  size * (size + 1) /2;
            for (int i = 0; i < size; i++)
            {
                if (i%2 == 0)
                {
                    for (int c = 0; c < size - i; c++)
                    {
                        _matrix[(size - 1) - c - i ,(size - 1) - c] = nextNumber;
                        nextNumber--;
                    }
                }
                else
                {
                    for (int c = 0; c < size - i; c++)
                    {
                        _matrix[c ,i + c] = nextNumber;
                        nextNumber--;
                    }
                }
            }
        }

        //Task 2
        public void DiagonalSort()
        {
            if(_matrix.GetLength(0) != _matrix.GetLength(1))
                throw new Exception("Matrix need to be square");

            var size = _matrix.GetLength(0);
            for (int n = 0; n < size; n++)
            {
                var smallestNum =  _matrix[n, n];
                var smallestNumX = n;
                var smallestNumY = n;
                //finding next smallest number 
                for (int x = n; x < size; x++)
                {
                    for (int y = n; y < size; y++)
                    {
                        if (_matrix[x,y] < smallestNum)
                        {
                            smallestNum = _matrix[x,y];
                            smallestNumX = x;
                            smallestNumY = y;
                        }
                    }
                }

                //Moving rows
                for (int x = 0; x < size; x++)
                {
                    var buffer = _matrix[n, x];
                    _matrix[n, x] = _matrix[smallestNumX, x];
                    _matrix[smallestNumX, x] = buffer;
                }
                //Moving columns
                for (int y = 0; y < size; y++)
                {
                    var buffer = _matrix[y, n];
                    _matrix[y, n] = _matrix[y, smallestNumY];
                    _matrix[y, smallestNumY] = buffer;
                }
            }
        }

        public bool TestDiagonalSort()
        {
            if(_matrix.GetLength(0) != _matrix.GetLength(1))
                throw new Exception("Matrix need to be square");

            var prevMember = _matrix[0,0];
            for (int n = 1; n < xLength; n++)
            {
                if (prevMember > _matrix[n,n])
                    return false;

                prevMember = _matrix[n,n];
            }
            return true;
        }

        public void Add(MatrixLab3 matrix)
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

        public void Subtract(MatrixLab3 matrix)
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

        public void Multiply(double number)
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    _matrix[x, y] *= number;
                }
            }
        }

        public void Multiply(MatrixLab3 matrix)
        {
            if(xLength != matrix.yLength)
                throw new Exception("Matrix x should be equal other matrix y");

            var result = new double[xLength,xLength];
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < xLength; y++)
                {
                    for (int i = 0; i < yLength; i++)
                    {
                        result[x,y] += _matrix[x, i] * matrix._matrix[i, y];
                    }
                }
            }
            _matrix = result;
        }

        public MatrixLab3 Transpose()
        {
            var tMatrix = new double[yLength, xLength];
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

        public static MatrixLab3 operator *(MatrixLab3 matrix1, MatrixLab3 matrix2)
        {
            matrix1.Multiply(matrix2);
            return matrix1;
        }

        public static MatrixLab3 operator *(double number, MatrixLab3 matrix)
        {
            matrix.Multiply(number);
            return matrix;
        }
        
        public static MatrixLab3 operator -(MatrixLab3 matrix1, MatrixLab3 matrix2)
        {
            matrix1.Subtract(matrix2);
            return matrix1;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int x = 0; x < _matrix.GetLength(0); x++)
            {
                for (int y = 0; y < _matrix.GetLength(1); y++)
                {
                    builder.Append($"{_matrix[x,y],7 :F2}".Replace(".00", "   "));
                }
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }
}