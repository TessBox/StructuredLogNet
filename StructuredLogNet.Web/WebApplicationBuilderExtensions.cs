using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TessBox.Sdk.Dotnet.Apps.WebApps.Configuration;

namespace StructuredLogNet.Web;

internal static class WebApplicationBuilderExtensions
{
    public static LoggerProvider AddLogging(this WebApplicationBuilder builder, IAppInfo appInfo)
    {
        var logItemQueue = new LogItemQueue();
        builder.Services.AddSingleton<ILogItemQueue>(logItemQueue);

        // logger
        var loggerProvider = LoggerProviderFactory
            .With(builder.Services, builder.Logging, appInfo)
            .AddConsoleLogger()
            .AddLoggerTarget(() => new QueueFileLoggerTarget(logItemQueue))
            .Build();

        return loggerProvider;
    }
}
