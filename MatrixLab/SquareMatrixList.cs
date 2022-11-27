using System;
using System.Text;

public class SquareMatrixList
{
    private SquareMatrix[] _matrices;

    public SquareMatrixList(SquareMatrix[] matrices) => _matrices = matrices;

    public void Sort()
    {
        var sorted = false;
        while (!sorted)
        {
            for (int i = 0; i < _matrices.Length - 1; i++)
            {
                sorted = true;
                if (_matrices[i] > _matrices[i + 1])
                {
                    var tmp = _matrices[i];
                    _matrices[i] = _matrices[i + 1];
                    _matrices[i + 1] = tmp;
                    sorted = false;
                }
            }
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