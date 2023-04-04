using Microsoft.AspNetCore.Builder;

namespace StructuredLogNet.Web;

public static class WebApplicationExtensions
{
    public static WebApplication UseStructuredLogging(this WebApplication app)
    {
        app.UseMiddleware<PerformanceMiddleware>();
        return app;
    }
}
