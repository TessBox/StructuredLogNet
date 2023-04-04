using System.Text.Json;

namespace StructuredLogNet;

internal class FileLoggerTarget : ILoggerTarget
{
    private readonly string _logPath;

    public FileLoggerTarget(string logPath)
    {
        _logPath = logPath;

        if (!Directory.Exists(_logPath))
        {
            Directory.CreateDirectory(_logPath);
        }
    }

    public void Log(LogItem item)
    {
        var fileName = Path.Combine(_logPath, DateTime.UtcNow.ToString("yyMMddHHmm") + ".log");
        try
        {
            using StreamWriter w = File.AppendText(fileName);
            var data = JsonSerializer.Serialize(item);
            w.WriteLine(data);
            w.Flush();
        }
        catch (Exception ex)
        {
            File.AppendAllLines(
                $"{fileName}.error",
                new[] { ex.Message, ex.StackTrace ?? string.Empty }
            );
        }
    }
}
