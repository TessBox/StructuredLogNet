using Microsoft.Extensions.Logging;

namespace StructuredLogNet.Example;

public class LoggerExample
{
    private readonly ILogger<LoggerExample> _logger;

    public LoggerExample(ILogger<LoggerExample> logger)
    {
        _logger = logger;
    }

    public void Log()
    {
        _logger.LogInformation("From ILogger");
    }
}
