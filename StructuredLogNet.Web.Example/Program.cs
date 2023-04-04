using StructuredLogNet.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddInMemoryCollection(
    new Dictionary<string, string?> { { "StructuredLogNet:log_path", Path.GetTempPath() } }
);

builder.AddStructuredLogging();

var app = builder.Build();

app.MapGet(
    "/",
    (ILogger<Program> logger) =>
    {
        logger.LogInformation("Coucou");
        return "Hello World!";
    }
);

app.Run();
