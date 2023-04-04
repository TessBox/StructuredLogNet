using Microsoft.Extensions.Logging;

namespace StructuredLogNet;

public static class IStructuredLoggerExtensions
{
    //--- Info ---------------------------------------------------------------------------------------------------------
    public static void Info(
        this IStructuredLogger logger,
        string message,
        params (string Key, string Value)[] extraFields
    )
    {
        var logItem = new LogItem
        {
            Level = LogLevel.Information,
            Message = message,
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        logger.Log(logItem);
    }

    //--- Warn ---------------------------------------------------------------------------------------------------------

    public static void Warn(
        this IStructuredLogger logger,
        string message,
        params (string Key, string Value)[] extraFields
    )
    {
        var logItem = new LogItem
        {
            Level = LogLevel.Warning,
            Message = message,
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        logger.Log(logItem);
    }

    //--- Error --------------------------------------------------------------------------------------------------------
    public static void Error(
        this IStructuredLogger logger,
        Exception exception,
        params (string Key, string Value)[] extraFields
    )
    {
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
        this IStructuredLogger logger,
        string message,
        params (string Key, string Value)[] extraFields
    )
    {
        var logItem = new LogItem
        {
            Level = LogLevel.Error,
            Message = message,
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        logger.Log(logItem);
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
        this IStructuredLogger logger,
        Exception exception,
        params (string Key, string Value)[] extraFields
    )
    {
        var logItem = new LogItem
        {
            Level = LogLevel.Critical,
            Exception = exception.ToString(),
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        logger.Log(logItem);
    }

    public static void Critical(
        this IStructuredLogger logger,
        string message,
        params (string Key, string Value)[] extraFields
    )
    {
        var logItem = new LogItem
        {
            Level = LogLevel.Critical,
            Message = message,
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        logger.Log(logItem);
    }

    public static void Critical(
        this IStructuredLogger logger,
        string message,
        Exception exception,
        params (string Key, string Value)[] extraFields
    )
    {
        var logItem = new LogItem
        {
            Level = LogLevel.Critical,
            Message = message,
            Exception = exception.ToString(),
            ExtraFields = new Dictionary<string, string>(
                extraFields.Select(t => new KeyValuePair<string, string>(t.Key, t.Value)).ToList()
            )
        };

        logger.Log(logItem);
    }
}
