using System;
using System.IO;
using System.Collections.Generic;

//var = 7
public static class Lab
{
    #region Task1
        public static void Task1()
        {
            var file = "task1.bin";
            var path = Path.Combine(Environment.CurrentDirectory, file);
    
            var amount = 120;
            CreateFileRandomNumbers(path, amount);
            var minmax = FindMinMaxInFile(path);
            Console.WriteLine(minmax);
        }
    
        private static void CreateFileRandomNumbers(string path, int amount)
        {
            using (var streamWriter = new BinaryWriter(File.Create(path)))
            {
                for (int i = 0; i < amount; i++)
                {
                    streamWriter.Write(Random.Shared.Next(0, 1000));
                }
            }
        }
    
        private static (int min, int max) FindMinMaxInFile(string path)
        {
            var min = int.MaxValue;
            var max = int.MinValue;
            using (var streamReader = new BinaryReader(File.OpenRead(path)))
            { 
                while (streamReader.BaseStream.Position != streamReader.BaseStream.Length)
                {
                    var value = streamReader.ReadInt32();
                    if(value > max) max = value;
                    if(value < min) min = value;
                }
            }
            return (min, max);
        }
#endregion

    #region Task2
        public static void Task2()
        {
            var file = "task2.bin";
            var path = Path.Combine(Environment.CurrentDirectory, file);
    
            var amount = Random.Shared.Next(36);
            CreateFileRandomNumbers(path, amount);
            var max = FindMinMaxInFile(path).max;
            System.Console.WriteLine($"Max: {max}");
            Console.WriteLine(MatrixToString(CreateMatrixAndReplace(path, max)));
        }
    
        private static int[,] CreateMatrixAndReplace(string path, int replace)
        {
            int[,] matrix;
            using (var streamReader = new BinaryReader(File.OpenRead(path)))
            {
                var count = streamReader.BaseStream.Length / sizeof(int);
                var size = (int)Math.Ceiling(Math.Sqrt(count));
                matrix = new int[size, size];
    
                for (int i = 0; i < size * size; i++)
                {
                    if(streamReader.BaseStream.Position != streamReader.BaseStream.Length)
                    {
                        var value = streamReader.ReadInt32();
                        if(value != replace)
                            matrix[i / size, i % size] = value;
                        else
                            matrix[i / size, i % size] = 0;
                    }
                    else
                        matrix[i / size, i % size] = 0;
                }
            }
            return matrix;
        }
        public static string MatrixToString(int[,] matrix)
        {
            var builder = new System.Text.StringBuilder();
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    builder.Append($"{matrix[x,y],7}");
                }
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }
#endregion

    #region Task3
        public static void Task3(ToyData[] toys, int priceFilter, int ageFilter)
        {
            var file = "task3.bin";
            var path = Path.Combine(Environment.CurrentDirectory, file);
    
            WriteToyData(toys, path);
    
            var readToys = ReadToyData(path);
    
            foreach (var toy in readToys)
            {
                if (toy.Price <= priceFilter && toy.AgeRestriction.min <= ageFilter && toy.AgeRestriction.max >= ageFilter)
                {
                    Console.WriteLine(toy.Name);
                }
            }
        }
    
        private static void WriteToyData(ToyData[] toys, string path)
        {
            using (var streamWriter = new BinaryWriter(File.Create(path)))
            {
                foreach (var item in toys)
                {
                    streamWriter.Write(item.Name);
                    streamWriter.Write(item.Price);
                    streamWriter.Write(item.AgeRestriction.min);
                    streamWriter.Write(item.AgeRestriction.max);
                }
            }
        }
    
        private static ToyData[] ReadToyData(string path)
        {
            var toys = new List<ToyData>();
            using (var streamReader = new BinaryReader(File.OpenRead(path)))
            {
                while (streamReader.BaseStream.Length != streamReader.BaseStream.Position)
                {
                    toys.Add(
                        new ToyData(
                    streamReader.ReadString(),
                    streamReader.ReadInt32(),
                    (streamReader.ReadInt32(),
                    streamReader.ReadInt32()))
                    );
                }
            }
            return toys.ToArray();
        }
    #endregion

    #region Task4
            public static void Lab4(int amount)
            {
                var file = "task4.txt";
                var path = Path.Combine(Environment.CurrentDirectory, file);
        
                using (var streamWriter = new StreamWriter(File.Create(path)))
                {
                    for (int i = 0; i < amount; i++)
                    {
                        streamWriter.WriteLine(Random.Shared.Next(10));
                    }
                }
        
                List<int> readNumbers = new();
        
                using (var streamReader = new StreamReader(File.OpenRead(path)))
                {
                    while (!streamReader.EndOfStream)
                    {
                        readNumbers.Add(int.Parse(streamReader.ReadLine()!));
                    }
                }
                var maxNumber = int.MinValue;
                var maxNumberTimes = 0;
        
                foreach (var n in readNumbers)
                {
                    if(maxNumber < n)
                    {
                        maxNumber = n;
                        maxNumberTimes = 1;
                    }
                    else if (maxNumber == n)
                        maxNumberTimes += 1;
                }
                System.Console.WriteLine($"Max Number {maxNumber} times: {maxNumberTimes}");
            }
    #endregion

    #region Task5
            public static void Task5(int amount)
            {
                var file = "task5.txt";
                var path = Path.Combine(Environment.CurrentDirectory, file);
        
                //write to file
                using (var streamWriter = new StreamWriter(File.Create(path)))
                {
                    for (int i = 0; i < amount; i++)
                    {
                        streamWriter.Write(Random.Shared.Next());
                        streamWriter.Write(' ');
                    }
                }
        
        
                //read to list
                List<int> readNumbers = new();
                using (var streamReader = new StreamReader(File.OpenRead(path)))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var chars = new List<char>();
                        while (streamReader.Peek() != ' ')
                        {
                            chars.Add((char)streamReader.Read());
                        }
                        //clear space
                        streamReader.Read();
        
                        int number = int.Parse(chars.ToArray());
                        System.Console.WriteLine(number);
                        readNumbers.Add(number);
                    }
                }
        
                //поиск чётных чисел
                int chet = 0;
                foreach (var item in readNumbers)
                {
                    if (item % 2 == 0)
                        chet++;
                }
                System.Console.WriteLine($"чётных чисел {chet}");        
            }
    #endregion

    public static void Task6(string word)
    {
        var fileFrom = "task6raw.txt";
        var file = "task6.txt";
        var pathFrom = Path.Combine(Environment.CurrentDirectory, fileFrom);
        var path = Path.Combine(Environment.CurrentDirectory, file);

        using (var steamWriter = new StreamWriter(File.Create(path)))
        {
            using (var readerFrom = new StreamReader(File.OpenRead(pathFrom)))
            {
                while (!readerFrom.EndOfStream)
                {
                    var line = readerFrom.ReadLine();
                    if(line!.Contains(word))
                        steamWriter.WriteLine(line);
                }
            }
        }
    }

}