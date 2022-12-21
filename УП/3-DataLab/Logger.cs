using System.IO;

public class Logger
{
    StreamWriter _writer;

    public Logger()
    {
        _writer = new StreamWriter(File.Create($"Log.txt"));
    }

    public void Log(string message)
    {
        _writer.WriteLine(message);
        _writer.Flush();
    }

    ~Logger()
    {
        _writer.Dispose();
    }
}