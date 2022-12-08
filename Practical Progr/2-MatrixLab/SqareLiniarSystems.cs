using System;
using System.Collections.Generic;

public class SquareLinierSystems : SquareMatrix
{
    private int[] _freeElements;

    public SquareLinierSystems(int[,] matrix, int[] freeElements) : base(matrix)
    {
        if (freeElements.Length != Size)
            throw new Exception("dimension of free elements not equals to dimension of matrix");

        _freeElements = freeElements;
    }
    
    public bool TryToCalculateSolution(out float[]? solution)
    {
        var det = GetDeterminant();
        if (det == 0)
        {
            solution = null;
            return false;
        }

        solution = new float[Size];
        var extendedMatrix = new int[Size, Size + 1];
        var extensionIndex = Size;

        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                extendedMatrix[x,y] = _matrix[x,y];
            }
        }
        for (int i = 0; i < Size; i++)
        {
            extendedMatrix[i, extensionIndex] = _freeElements[i];
        }

        for (int n = 0; n < Size; n++)
        {
            var includedY = new List<int>(System.Linq.Enumerable.Range(0, Size));
            includedY[n] = extensionIndex;

            solution[n] = (float)CalculateMinor(extendedMatrix ,new List<int>(System.Linq.Enumerable.Range(0, Size)), includedY) / det;
        }

        return true;
    }
}        