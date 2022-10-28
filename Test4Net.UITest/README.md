# Test4Net.UITest

## Introduction 
[Net core logger][net-core-logging].  Can incorporate log4net with [log4net aspNetCore][log4Net-aspNetCore] extension.

### Examples
Simple logger
```csharp
var log = LogProvider._GetLogger<Program>();
log.Info("Info message !!!", 1, 2, 3);
log.Warn("Warn message !!!", 1, 2, 3);
log.Debug("Debug message !!!", 1, 2, 3);
log.Error(new ArgumentException(".......No arguments!!"), "Error message !!!", 1, 2, 3);
```
Decorating logger with configuration
```csharp
var log = LogProvider._GetLogger<Program>(builder => builder.Decorate(TestConfiguration.GetSection("Logging")))
```
where json format is:
```json
{
  "Logging": {
    "LogLevel": { // All providers, LogLevel applies to all the enabled providers.
      "Default": "Error", // Default logging, Error and higher.
      "Microsoft": "Warning" // All Microsoft* categories, Warning and higher.
    },
    "Debug": { // Debug provider.
      "LogLevel": {
        "Default": "Information", // Overrides preceding LogLevel:Default setting.
        "Microsoft.Hosting": "Trace" // Debug:Microsoft.Hosting category.
      }
    },
    "EventSource": { // EventSource provider
      "LogLevel": {
        "Default": "Warning" // All categories of EventSource provider.
      }
    }
  }
}
```
More about [log formatter][log-formatter]
Creating logger using dependency injection
```csharp
 // Define loggers
var loggerTypes = new[]
    { typeof(LogAdapter<TargetClassA>), typeof(LogAdapter<TargetClassB>) };

// Call constructor, decorate optional
var logProvider = new LogProvider(loggerTypes,
    builder => builder.Decorate(TestConfiguration.GetSection("Logging")));
    
// Call loggers
var classAlogger = logProvider.GetLogger<TargetClass1>()
var classBlogger = logProvider.GetLogger<TargetClass1>()
```
Integrating log4net using [log4Net-aspNetCore] nuget
```csharp
var logger = LogProvider._GetLogger<LogProviderTest>(builder => 
            builder.Decorate(TestConfiguration.GetSection("Logging"))
                .AddLog4Net("log4net.config"));
```
[git-repo-url]: <https://github.com/r4d4m4n71s/Test4Net>
[net-core-logging]: <https://learn.microsoft.com/en-us/dotnet/core/extensions/logging>
[log4Net-aspNetCore]: <https://github.com/huorswords/Microsoft.Extensions.Logging.Log4Net.AspNetCore>
[log-formatter]:<https://learn.microsoft.com/en-us/dotnet/core/extensions/console-log-formatter>