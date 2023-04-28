using Microsoft.Extensions.Logging;

namespace StructuredLogNet;

public static class ILoggerExtensions
{
    //--- Info ---------------------------------------------------------------------------------------------------------
    public static void Info(
        this ILogger logger,
        string message,
        params (string Key, string Value)[] extraFields
    )
    {
        if (logger is not IStructuredLogger structuredLogger)
        {
#pragma warning disable CA2254 // Template should be a static expression
            logger.LogInformation(message);
#pragma warning restore CA2254 // Template should be a static expression

            return;
        }

        var logItem = new LogItem
        {
            Level = LogLevel.Information,
            Message = message,
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        structuredLogger.Log(logItem);
    }

    //--- Warn ---------------------------------------------------------------------------------------------------------

    public static void Warn(
        this ILogger logger,
        string message,
        params (string Key, string Value)[] extraFields
    )
    {
        if (logger is not IStructuredLogger structuredLogger)
        {
#pragma warning disable CA2254 // Template should be a static expression
            logger.LogWarning(message);
#pragma warning restore CA2254 // Template should be a static expression

            return;
        }

        var logItem = new LogItem
        {
            Level = LogLevel.Warning,
            Message = message,
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        structuredLogger.Log(logItem);
    }

    //--- Error --------------------------------------------------------------------------------------------------------
    public static void Error(
        this IStructuredLogger logger,
        Exception exception,
        params (string Key, string Value)[] extraFields
    )
    {
        if (logger is not IStructuredLogger structuredLogger)
        {
#pragma warning disable CA2254 // Template should be a static expression
            logger.LogError(exception, exception.Message);
#pragma warning restore CA2254 // Template should be a static expression

            return;
        }

        var logItem = new LogItem
        {
            Level = LogLevel.Error,
            Exception = exception.ToString(),
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        logger.Log(logItem);
    }

    public static void Error(
        this ILogger logger,
        string message,
        params (string Key, string Value)[] extraFields
    )
    {
        if (logger is not IStructuredLogger structuredLogger)
        {
#pragma warning disable CA2254 // Template should be a static expression
            logger.LogError(message);
#pragma warning restore CA2254 // Template should be a static expression

            return;
        }

        var logItem = new LogItem
        {
            Level = LogLevel.Error,
            Message = message,
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        structuredLogger.Log(logItem);
    }

    public static void Error(
        this IStructuredLogger logger,
        string message,
        Exception exception,
        params (string Key, string Value)[] extraFields
    )
    {
        var logItem = new LogItem
        {
            Level = LogLevel.Error,
            Message = message,
            Exception = exception.ToString(),
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        logger.Log(logItem);
    }

    //--- Critial ------------------------------------------------------------------------------------------------------
    public static void Critical(
        this ILogger logger,
        Exception exception,
        params (string Key, string Value)[] extraFields
    )
    {
        if (logger is not IStructuredLogger structuredLogger)
        {
#pragma warning disable CA2254 // Template should be a static expression
            logger.LogCritical(exception, exception.Message);
#pragma warning restore CA2254 // Template should be a static expression

            return;
        }

        var logItem = new LogItem
        {
            Level = LogLevel.Critical,
            Exception = exception.ToString(),
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        structuredLogger.Log(logItem);
    }

    public static void Critical(
        this ILogger logger,
        string message,
        params (string Key, string Value)[] extraFields
    )
    {
        if (logger is not IStructuredLogger structuredLogger)
        {
#pragma warning disable CA2254 // Template should be a static expression
            logger.LogCritical(message);
#pragma warning restore CA2254 // Template should be a static expression

            return;
        }

        var logItem = new LogItem
        {
            Level = LogLevel.Critical,
            Message = message,
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        structuredLogger.Log(logItem);
    }

    public static void Critical(
        this ILogger logger,
        string message,
        Exception exception,
        params (string Key, string Value)[] extraFields
    )
    {
        if (logger is not IStructuredLogger structuredLogger)
        {
#pragma warning disable CA2254 // Template should be a static expression
            logger.LogCritical(exception, message);
#pragma warning restore CA2254 // Template should be a static expression

            return;
        }

        var logItem = new LogItem
        {
            Level = LogLevel.Critical,
            Message = message,
            Exception = exception.ToString(),
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        structuredLogger.Log(logItem);
    }
}
