using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace StructuredLogNet.Web;

internal static class WebApplicationExtensions
{
    public static void UseStructuredLogging(this WebApplication app)
    {
        var jobQueue = (JobQueue)app.Services.GetRequiredService<IJobQueue>();
        AsyncHelper.RunSync(() => jobQueue.QueueAlwaysOnCoreJobAsync<FileLoggerJob>());
    }
}
