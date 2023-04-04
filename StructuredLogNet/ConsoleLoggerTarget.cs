using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace StructuredLogNet;

internal class ConsoleLoggerTarget : ILoggerTarget
{
    public void Log(LogItem item)
    {
        // write header
        SetColors(item.Level);
        Console.WriteLine(
            $"[{item.EventDate}]:[{item.ApplicationName, -12}]:[{item.Level, -12}] {item.CategoryName}"
        );
        ResetColor();

        // write message
        if (!string.IsNullOrWhiteSpace(item.Message))
        {
            Console.WriteLine(item.Message);
        }

        // write exception
        if (item.Exception != null)
        {
            Console.WriteLine(item.Exception);
        }

        // extra field
        if (item.ExtraFields != null)
        {
            var extraFieldExtra = JsonSerializer.Serialize(item.ExtraFields);
            Console.WriteLine(extraFieldExtra);
        }
    }

    private static void SetColors(LogLevel level)
    {
        switch (level)
        {
            case LogLevel.Critical:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                break;
            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            case LogLevel.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            case LogLevel.Information:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            case LogLevel.Debug:
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            case LogLevel.Trace:
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                break;
        }
    }

    private static void ResetColor()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.BackgroundColor = ConsoleColor.Black;
    }
}
