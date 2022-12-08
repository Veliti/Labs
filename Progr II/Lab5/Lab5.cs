using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public static class Lab5
{

public static IEnumerable<int> RandomInts(int amount)
{
    var random = new Random(10);
    for (int i = 0; i < amount; i++)
    {
        yield return random.Next(0, 10);
    }
}

public static void Print<T>(IEnumerable<T> items)
{
    foreach (var item in items)
    {
        System.Console.Write($"{item} ");
    }
    System.Console.WriteLine();
}

#region Task1
    public static IEnumerable<T> ReturnRepeatingElements<T>(IEnumerable<T> data)
    {
        return  from item in data 
                group item by item 
                into grouped

                from result in grouped.Distinct()
                where grouped.Count() > 1
                select result;
    }

#endregion

#region Task2
    public static LinkedList<T> SwitchElements<T>(LinkedList<T> list)
    {
        var current = list.First!.Next;
        for (int i = 0; i < list.Count - 2; i++)
        {
            if(!current!.Next!.Value!.Equals(current.Previous!.Value))
            {
                var tmp = current.Next.Value;
                current.Next.Value = current.Previous.Value;
                current.Previous.Value = tmp;
            }
            current = current.Next;
        }

        return list;
    }
#endregion

#region Task3
    // languages :"eng", "jpn", "mandarin", "french", "turkish"
    public enum Lang
    {
        eng,
        jpn,
        mandarin,
        french,
        turkish
    }

    public static string DoTask3(HashSet<Lang>[] workers)
    {
        HashSet<Lang> everyOneKnows = new HashSet<Lang>(Enum.GetValues<Lang>());
        foreach (var worker in workers)
        {
            everyOneKnows.IntersectWith(worker);
        }

        HashSet<Lang> atLeastOneKnow = new();
        foreach (var worker in workers)
        {
            atLeastOneKnow.UnionWith(worker);
        }

        var noOneKnows = new HashSet<Lang>(Enum.GetValues<Lang>());
        foreach (var item in workers)
        {
            noOneKnows.ExceptWith(item);
        }

        return $"Everyone Knows: {everyOneKnows.ParseLang()} At least one know: {atLeastOneKnow.ParseLang()} No one knows: {noOneKnows.ParseLang()}";
    }

    private static string ParseLang(this IEnumerable<Lang> langs)
    {
        var builder = new System.Text.StringBuilder();
        foreach (var item in langs)
        {
            builder.Append(Enum.GetName<Lang>(item) + " ");
        }
        return builder.ToString();
    }

#endregion

#region Task4
    public static IEnumerable<char> DoTask4(string text)
    {
        var words = text.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var letters = new HashSet<char>();
        foreach (var word in words)
        {
            letters.Add(Char.ToUpper(word[0]));
        }

        var alphabet = System.Text.Unicode.UnicodeRanges.Cyrillic;
        return from letter in letters
                where letter >= alphabet.FirstCodePoint && letter <= alphabet.FirstCodePoint + alphabet.Length
                select letter;
    }
#endregion

#region Task5
    record Data(string FullName, int School, int Result);

    public static void DoTask5(string file)
    {
        try
        {
            using (var streamReader = new StreamReader(file))
            {
                var dataList = new List<Data>();
                while (!streamReader.EndOfStream)
                {
                    var words = streamReader.ReadLine()!.Split(' ' , StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    var data = new Data
                    (
                        FullName : $"{words[0]} {words[1]}",
                        School :  int.Parse(words[2]),
                        Result : int.Parse(words[3])
                    );
                    if (data.School == 50)
                    {
                        dataList.Add(data);
                    }
                }
                dataList.Sort((y, x) => x.Result.CompareTo(y.Result));

                foreach (var item in dataList)
                    {
                        System.Console.WriteLine($"{item.FullName} {item.School} {item.Result}");
                    }

                var dataEnumerator = dataList.GetEnumerator();
                var resultData = new List<Data>(dataEnumerator.TakeRepeating(2));

                if (resultData.Count > 2)
                {
                    System.Console.WriteLine(resultData.Count);
                }
                else
                {
                    foreach (var item in resultData)
                    {
                        System.Console.WriteLine(item.FullName);
                    }
                }
                
            }
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e);
        }
    }

    static IEnumerable<Data> TakeRepeating(this IEnumerator<Data> data, int times)
    {
        if(data.Current == null & !data.MoveNext()) yield break;

        for (int i = 0; i < times; i++)
        {
            if(data.Current == null) yield break;

            int previous = data.Current.Result;
            yield return data.Current;

            while (data.MoveNext() && previous == data.Current.Result)
            {
                previous = data.Current.Result;
                yield return data.Current;
            }
        }
    }
#endregion

}