using System;
using System.Text;

public class SquareMatrixList
{
    private SquareMatrix[] _matrices;

    public SquareMatrixList(SquareMatrix[] matrices) => _matrices = matrices;

    public void Sort()
    {
        for (int i = 0; i < _matrices.Length; i++)
        {
            var item = _matrices[i];
            var currentIndex = i;

            while (currentIndex > 0 && _matrices[currentIndex - 1] > item)
            {
                _matrices[currentIndex] = _matrices[currentIndex - 1];
                currentIndex--;
            }

            _matrices[currentIndex] = item;
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        for (int i = 0; i < _matrices.Length; i++)
        {
            builder.Append($"{i}) det:{_matrices[i].GetDeterminant()}" + Environment.NewLine);
            builder.Append(_matrices[i].ToString());
            builder.Append(Environment.NewLine);
        }
        return builder.ToString();
    }
}