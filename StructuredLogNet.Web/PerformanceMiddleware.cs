namespace StructuredLogNet.Web;

using Microsoft.AspNetCore.Http;
using System.Diagnostics;

internal class PerformanceMiddleware
{
    private readonly RequestDelegate _next;

    public PerformanceMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IStructuredLogger<PerformanceMiddleware> logger)
    {
        var stopwatch = Stopwatch.StartNew();

        if (!context.Request.Headers.ContainsKey("X-Correlation-Id"))
        {
            context.Request.Headers.Add("X-Correlation-Id", Guid.NewGuid().ToString("D"));
        }

        await _next(context);

        logger.Info(
            $"{context.Request.Path}: {stopwatch.ElapsedMilliseconds}",
            new[] { ("duration", stopwatch.ElapsedMilliseconds.ToString()) }
        );
    }
}
