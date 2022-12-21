using System;
using System.Collections.Generic;
using System.IO;


Random random = new Random(1001);

var names = ParseFileToWords("data/names.txt");


var dictionary = GenerateMatrixDictionary(100);
PrintDictionary(dictionary);



Dictionary<int, List<SquareMatrix>> GenerateMatrixDictionary(int amount)
{
    var dictionary = new Dictionary<int, List<SquareMatrix>>();
    for (int i = 0; i < amount; i++)
    {
        var matrix = new SquareMatrix(GetRandom2by2Array());
        var determinant = matrix.GetDeterminant();

        if (dictionary.ContainsKey(determinant))
        {
            dictionary[determinant].Add(matrix);
        }
        else
        {
            dictionary.Add(determinant, new List<SquareMatrix>(){matrix});
        }
    }

    return dictionary;
}

void PrintDictionary(Dictionary<int, List<SquareMatrix>> dictionary)
{
    foreach (var item in dictionary)
    {
        Console.WriteLine($"{item.Key})");
        foreach (var matrix in item.Value)
        {
            Console.WriteLine(matrix.ToString());
        }
    }
}

List<string> ParseFileToWords(string path)
{
    return new(File.ReadAllText(path).Split(new[]{'\n', ' '}, System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries));
}

int[,] GetRandom2by2Array()
{
    var array = new int[2,2];
    for (int x = 0; x < 2; x++)
    {
        for (int y = 0; y < 2; y++)
        {
            array[x,y] = random.Next(0, 5);
        }
    }
    return array;
}
