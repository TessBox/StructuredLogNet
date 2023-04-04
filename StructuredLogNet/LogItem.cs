using Microsoft.Extensions.Logging;

namespace StructuredLogNet;

public class LogItem
{
    public string ApplicationName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public DateTime EventDate { get; set; } = DateTime.UtcNow;

    public LogLevel Level { get; set; } = LogLevel.Information;

    public string? EventId { get; set; }

    public string? Message { get; set; }

    public string? Exception { get; set; }

    public IDictionary<string, string>? ExtraFields { get; set; }
}
