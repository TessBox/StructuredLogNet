# StructuredLogNet
StructuredLogNet is a free structured logger for .NET.

It is compatible with ILogger interface.


## Installation

dotnet add package StructuredLogNet

## Setup 

```csharp
using var loggerFactory = LoggerFactory.Create(
    builder => builder.AddStructuredLog(services).AddConsoleLogger().Build()
);
```

## Usage

```csharp
 _logger.Info("From ILogger", ("Key1", "Value1"), ("Key2", "Value2"));
```

# StructuredLogNet.Web
StructuredLogNet is a free structured logger for Asp Net Core

It is compatible with ILogger interface.

The outfile is a json with a structure as :

```json

{
    "ApplicationName":"StructuredLogNet.Web.Example",
    "CategoryName":"StructuredLogNet.Web.LoggingHostedService","EventDate":"2023-04-22T10:11:00.079665Z",
    "Level":2,
    "EventId":null,
    "Message":"Log to file",
    "Exception":null,
    "ExtraFields":{
        "LogPath":"/Users/damien/Dev"
        }
    }
```


## Installation

dotnet add package StructuredLogNet.Web

## Setup 

To configure the path where the library store the output file, modify the key :
structured_log_net:log_path

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddInMemoryCollection(
    new Dictionary<string, string?> { { "structured_log_net:log_path", Path.GetTempPath() } }
);

builder.AddStructuredLogging();

var app = builder.Build();

app.UseStructuredLogging();

```

## Usage

```csharp
app.MapGet(
    "/",
    (ILogger<Program> logger) =>
    {
        logger.LogInformation("Coucou");
        return "Hello World!";
    }
);
```

# Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

# License

[MIT](https://choosealicense.com/licenses/mit/)