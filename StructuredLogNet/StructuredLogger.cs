using Microsoft.Extensions.Logging;

namespace StructuredLogNet;

/// <summary>
/// A structured logger interface
/// </summary>
public interface IStructuredLogger : ILogger
{
    LogLevel Level { get; set; }

    void Log(LogItem item);
}

/// <summary>
/// A structured logger interface
/// </summary>
public interface IStructuredLogger<T> : IStructuredLogger, ILogger<T> { }

/// <summary>
/// Interface to implement for create a new target
/// </summary>
public interface ILoggerTarget
{
    void Log(LogItem item);
}

internal class StructuredLogger<T> : IStructuredLogger<T>
{
    private readonly IStructuredLogger _logger;

    public StructuredLogger(IStructuredLoggerProvider factory)
    {
        _logger = factory.CreateStructuredLogger(typeof(T).FullName ?? string.Empty);
    }

    public LogLevel Level
    {
        get => _logger.Level;
        set => _logger.Level = value;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull => BeginScope(state);

    public bool IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    ) => _logger.Log(logLevel, eventId, state, exception, formatter);

    public void Log(LogItem item) => _logger.Log(item);
}

internal class Logger : IStructuredLogger
{
    private readonly string _categoryName;
    private readonly string _applicationName;
    private readonly ILoggerTarget[] _targets;

    public Logger(string categoryName, string applicationName, ILoggerTarget[] targets)
    {
        _categoryName = categoryName;
        _applicationName = applicationName;
        _targets = targets;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull => default!;

    public LogLevel Level { get; set; } = LogLevel.Information;

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= Level;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        var logItem = new LogItem
        {
            Level = logLevel,
            Message = formatter(state, exception),
            Exception = exception?.StackTrace,
            EventId = $"{eventId.Id}-{eventId.Name}"
        };

        Log(logItem);
    }

    public void Log(LogItem item)
    {
        if (!IsEnabled(item.Level))
        {
            return;
        }

        item.ApplicationName = _applicationName;
        item.CategoryName = _categoryName;

        foreach (var target in _targets)
        {
            target.Log(item);
        }
    }
}
