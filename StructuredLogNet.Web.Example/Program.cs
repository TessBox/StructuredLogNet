using StructuredLogNet.Web;

var builder = WebApplication.CreateBuilder(args);

var path = Path.GetTempPath();
builder.Configuration.AddInMemoryCollection(
    new Dictionary<string, string?> { { "structured_log_net:log_path", path } }
);

builder.AddStructuredLogging();

var app = builder.Build();

app.UseStructuredLogging();

app.MapGet(
    "/",
    (ILogger<Program> logger) =>
    {
        logger.LogInformation("Log to {path}", path);
        return "Hello World!";
    }
);

app.Run();
