using System;
using System.Collections.Generic;

public class SquareMatrix : Matrix
{

    public int Size => xLength;

    public SquareMatrix(int[,] matrix) : base(matrix)
    {
        if (matrix.GetLength(0) != matrix.GetLength(1))
            throw new Exception("Wrong matrix init: not square array");
    }

            public void Add(SquareMatrix matrix)
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    _matrix[x, y] += matrix._matrix[x, y];
                }
            }
        }

        public void Subtract(SquareMatrix matrix)
        {  
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    _matrix[x, y] -= matrix._matrix[x, y];
                }
            }
        }

    public int GetDeterminant()
    {
        return CalculateMinor(
            _matrix,
            new List<int>(System.Linq.Enumerable.Range(0, Size)), 
            new List<int>(System.Linq.Enumerable.Range(0, Size)));
    }

    public static bool operator >(SquareMatrix matrix1, SquareMatrix matrix2) => matrix1.GetDeterminant() > matrix2.GetDeterminant();

    public static bool operator <(SquareMatrix matrix1, SquareMatrix matrix2) => matrix1.GetDeterminant() < matrix2.GetDeterminant();

    public static implicit operator int(SquareMatrix matrix) => matrix.GetDeterminant();

    //умножение на матрицу
    public static SquareMatrix operator *(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        matrix1.Multiply((Matrix)matrix2);
        return matrix1;
    }
    //умножение на число
    public static SquareMatrix operator *(int number, SquareMatrix matrix)
    {
        matrix.Multiply(number);
        return matrix;
    }
    //минус
    public static SquareMatrix operator -(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        matrix1.Subtract(matrix2);
        return matrix1;
    }
}