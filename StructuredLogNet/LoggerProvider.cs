using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace StructuredLogNet;

internal interface IStructuredLoggerProvider
{
    IStructuredLogger CreateStructuredLogger(string categoryName);
}

internal class LoggerProvider : ILoggerProvider, IStructuredLoggerProvider
{
    private readonly ConcurrentDictionary<string, Logger> _loggers =
        new(StringComparer.OrdinalIgnoreCase);
    private readonly IEnumerable<Func<ILoggerTarget>> _loggerTargets;
    private readonly string _applicationName;

    private bool disposedValue;

    internal LoggerProvider(IEnumerable<Func<ILoggerTarget>> targets)
    {
        _applicationName = Assembly.GetEntryAssembly()?.GetName()?.Name ?? "";
        _loggerTargets = targets;
    }

    public ILogger CreateLogger(string categoryName) => CreateStructuredLogger(categoryName);

    public IStructuredLogger CreateStructuredLogger(string categoryName) =>
        _loggers.GetOrAdd(
            categoryName,
            name => new Logger(name, _applicationName, CreateTargets())
        );

    private ILoggerTarget[] CreateTargets()
    {
        var result = new List<ILoggerTarget>();
        foreach (var fct in _loggerTargets)
        {
            result.Add(fct.Invoke());
        }

        return result.ToArray();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _loggers.Clear();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
