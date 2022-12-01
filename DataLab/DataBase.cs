using System.IO;
using System.Collections.Generic;
using System.Text.Json;


public class DataBase<T> where T : Entry
{
    public string Type => nameof(T);
    public string FileName => $"{Type}.bin";

    List<T> _data;
    Logger _logger;

    public DataBase(Logger logger)
    {
        _logger = logger;

        if (File.Exists(FileName))
        {
            _logger.Log($"{Type}: Database is found");
            _data = new();
        }
        else
        {
            File.Create(FileName);
            _logger.Log($"{Type}: Database is created");
            _data = ReadFile();
        }
    }

    public void Place(T entry)
    {
        
    }

    public List<T> Find(System.Predicate<T> match)
    {
        return _data.FindAll(match);
    }

    List<T> ReadFile()
    {
        using (var fReader = File.OpenRead(FileName))
        {
            return JsonSerializer.Deserialize<List<T>>(fReader)!;
        }
    }

    void WriteFile()
    {
        using (var fWriter = File.OpenWrite(FileName))
        {
            JsonSerializer.Serialize(fWriter, _data);
        }
        _logger.Log($"{Type}: Database is written to");
    }

    void WriteBackUp()
    {
        using (var fWriter = File.OpenWrite($"{Type}.json"))
        {
            JsonSerializer.Serialize(fWriter, _data);
        }
    }

}