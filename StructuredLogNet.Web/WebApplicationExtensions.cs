using System;
using TessBox.Sdk.Dotnet.Apps.WebApps.BackgroundJobs;
using TessBox.Sdk.Dotnet.BackgroundJobs;
using TessBox.Sdk.Dotnet.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StructuredLogNet.Web;

internal static class WebApplicationExtensions
{
    public static void RunLoggingJob(this WebApplication app)
    {
        var jobQueue = (JobQueue)app.Services.GetRequiredService<IJobQueue>();
        AsyncHelper.RunSync(() => jobQueue.QueueAlwaysOnCoreJobAsync<FileLoggerJob>());
    }
}
