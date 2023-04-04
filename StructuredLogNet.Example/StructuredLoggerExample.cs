namespace StructuredLogNet.Example;

public class StructuredLoggerExample
{
    private readonly IStructuredLogger<StructuredLoggerExample> _logger;

    public StructuredLoggerExample(IStructuredLogger<StructuredLoggerExample> logger)
    {
        _logger = logger;
    }

    public void Log()
    {
        _logger.Info("Structured log info", ("key1", "val1"), ("key2", "val2"));
    }
}
