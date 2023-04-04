using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructuredLogNet;
using StructuredLogNet.Example;

Console.WriteLine("Welcome in StructuredLogNet");

var services = new ServiceCollection();
services.AddTransient<StructuredLoggerExample, StructuredLoggerExample>();
services.AddTransient<LoggerExample, LoggerExample>();

using var loggerFactory = LoggerFactory.Create(
    builder => builder.AddStructuredLog(services).AddConsoleLogger().Build()
);

var serviceProvider = services.BuildServiceProvider();

// structured log
var structuredLoggerExample = serviceProvider.GetRequiredService<StructuredLoggerExample>();
structuredLoggerExample.Log();

// standard log
var loggerExample = serviceProvider.GetRequiredService<LoggerExample>();
loggerExample.Log();
