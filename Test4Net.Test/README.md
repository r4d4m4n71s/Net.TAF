# Test4Net.Test

## Introduction 
Define a abstract model to instrument testing

### Examples
Test with logger and configuration model
```csharp
[TestClass]
public class MyTestClass : AbstractTestModel
{
    /// <summary>
    /// Get logger 
    /// </summary>
    /// <returns>Logger</returns>
    protected override ILog GetLogger() => GetLogger<MyTestClass>();

    /// <summary>
    /// Configure test class from a settings file
    /// </summary>
    public MyTestClass() : base( 
        configure => configure.AddJsonFile("appSetting.json")) { }

    [TestMethod]
    public void MyTestMethod()
    {
        var environmentUrl = Configuration["QaEnvUrl"];
        Log.Info($"Loading environment url: {environmentUrl} from configuration file");
    }
}
```
More about [logger and configuration][log-formatterxx]
[Test4Net-Logging]:<https://learn.microsoft.com/en-us/dotnet/core/extensions/console-log-formatter>