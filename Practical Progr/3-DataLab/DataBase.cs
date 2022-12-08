using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json;
using System;

public class DataBase<T> : System.IDisposable
{
    public ReadOnlyCollection<T> Data {get{{_logger?.Log($"{Type}: Data have been read"); return _data.AsReadOnly();}}}

    string Type => typeof(T).FullName!;
    string FileName => $"{Type}.bin";

    List<T> _data;
    Logger? _logger;

    public DataBase(Logger? logger = null)
    {
        _logger = logger;
        _data = ReadFile();
    }

    public void Add(T entry)
    {
        WriteBackUp();
        _data.Add(entry);
        _logger?.Log($"{Type}: Added entry at end");
    }

    public void Remove(int index)
    {
        WriteBackUp();
        _data.RemoveAt(index);
        _logger?.Log($"{Type}: Removed entry at {index}");
    }

    public void Insert(int index, T entry)
    {
        WriteBackUp();
        _data.Insert(index, entry);
        _logger?.Log($"{Type}: Inserted entry at {index}");
    }

    public void Replace(int index, T replacement)
    {
        WriteBackUp();
        _data[index] = replacement;
        _logger?.Log($"{Type}: replaced entry at index {index}");
    }

    public int[]? FindEntrys(System.Predicate<T> match)
    {
        var lastIndex = 0;
        var indexes = new List<int>();
        while (lastIndex != -1)
        {
            lastIndex = _data.FindIndex(lastIndex, match);
            if(lastIndex != -1) indexes.Add(lastIndex); 
        }
        return indexes.ToArray();
    }

    public int FindEntry(System.Predicate<T> match)
    {
        return _data.FindIndex(0, match);
    }

    List<T> ReadFile()
    {
        try
        {
            if (File.Exists(FileName))
            {
                _logger?.Log($"{Type}: Database is found");
                using (var fReader = File.OpenRead(FileName))
                {
                    return JsonSerializer.Deserialize<List<T>>(fReader)!;
                }
            }
            else
            {
                _logger?.Log($"{Type}: Database is created");
                return new List<T>();
            }
        }
        catch (System.Exception)
        {
            _logger?.Log($"{Type}: Database is corrupted or smth");
            throw;
        }
    }

    void WriteFile()
    {
        using (var fWriter = File.Create(FileName))
        {
            JsonSerializer.Serialize(fWriter, _data);
        }
        _logger?.Log($"{Type}: Database is written to");
    }

    void WriteBackUp()
    {
        using (var fWriter = File.Create($"{Type}.json"))
        {
            JsonSerializer.Serialize(fWriter, _data);
        }
    }

    public void Dispose()
    {
        WriteFile();
    }

    public T this[int index] => Data[index];
}