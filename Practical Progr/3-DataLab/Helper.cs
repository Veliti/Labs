using System.Collections.Generic;

public static class ConsoleHelper
{
    public static void Print(string[] list, string? search)
    {
        if (search == null) search = string.Empty;
        foreach (var item in list)
        {
            if (item.Contains(search))
            {
                System.Console.WriteLine(item);
            }
        }
    }

    public static string EnumerableToString<T>(IEnumerable<T> words)
    {
        var line = "";
        foreach (var item in words)
        {
            line += item + " ";
        }
        return line;
    }

    public static int[] ParseMarks(IEnumerable<string> words)
    {
        var ints = new List<int>();
        foreach (var word in words)
        {
            var mark = int.Parse(word);
            if(mark <= 5 && mark >= 2) ints.Add(mark);
            else throw new System.Exception($"Wrong argument: {EnumerableToString(ints)} {mark}!");
        }

        return ints.ToArray();
    }
}