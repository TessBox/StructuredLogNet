using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace StructuredLogNet;

public static class LoggingBuilderExtension
{
    public static LoggerProviderFactory AddStructuredLog(
        this ILoggingBuilder builder,
        IServiceCollection services
    )
    {
        return LoggerProviderFactory.With(services, builder);
    }
}

public sealed class LoggerProviderFactory
{
    private readonly IServiceCollection _services;
    private readonly ILoggingBuilder _loggingBuilder;
    private readonly List<Func<ILoggerTarget>> _targets = new();

    public static LoggerProviderFactory With(
        IServiceCollection services,
        ILoggingBuilder loggingBuilder
    )
    {
        return new LoggerProviderFactory(services, loggingBuilder);
    }

    public static IStructuredLogger Default()
    {
        return new Logger("Default", "", new[] { new ConsoleLoggerTarget() });
    }

    private LoggerProviderFactory(IServiceCollection services, ILoggingBuilder loggingBuilder)
    {
        _services = services;
        _loggingBuilder = loggingBuilder;
    }

    public LoggerProviderFactory AddLoggerTarget(Func<ILoggerTarget> factory)
    {
        _targets.Add(factory);
        return this;
    }

    public LoggerProviderFactory AddConsoleLogger()
    {
        return AddLoggerTarget(() => new ConsoleLoggerTarget());
    }

    public ILoggerProvider Build()
    {
        var provider = new LoggerProvider(_targets);

        _services.AddSingleton<IStructuredLoggerProvider>(provider);
        _services.AddTransient(typeof(IStructuredLogger<>), typeof(StructuredLogger<>));
        _services.AddTransient(typeof(ILogger<>), typeof(StructuredLogger<>));

        _loggingBuilder.ClearProviders();
        _loggingBuilder.AddProvider(provider);

        return provider;
    }
}
