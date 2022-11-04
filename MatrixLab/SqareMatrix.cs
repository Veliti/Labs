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
}