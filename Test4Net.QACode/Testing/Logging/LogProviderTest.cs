using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Test4Net.Logging.Adapter;
using Test4Net.Logging.Interfaces;
using Test4Net.Logging.Provider;

namespace Test4Net.QACode.Testing.Logging;

[TestClass]
public class LogProviderTest : BaseTest
{
    [TestMethod]
    [TestCategory("unit")]
    public void CreateLogger_Instance_Ok() =>
        PerformLogging(new LogProvider().GetLogger<Program>());
    
    [TestMethod]
    [TestCategory("unit")]
    public void CreateLogger_DecorateFromConfiguration_Ok() =>
        PerformLogging(new LogProvider(builder => builder.Decorate(Configuration.GetSection("Logging")))
            .GetLogger<Program>());

    [TestMethod]
    [TestCategory("unit")]
    public void CreateLoggers_UsingDependencyInjection_Ok()
    {
        // Define loggers
        var loggerTypes = new[]
            { typeof(LogAdapter<LogProviderTest>), typeof(LogAdapter<Program>) };

        // Call constructor, decorate optional
        var logProvider = new LogProviderAsService(loggerTypes,
            builder => builder.Decorate(Configuration.GetSection("Logging")));

        // Request logger to service provider
        PerformLogging(logProvider.GetLogger<LogProviderTest>());
        PerformLogging(logProvider.GetLogger<Program>());
    }
        
    [TestMethod]
    [TestCategory("integration")]
    public void CreateLoggers_IntegrateLog4net_Ok()
    {
        var logger = new LogProvider(builder => 
            builder.Decorate(Configuration.GetSection("Logging"))
                .AddLog4Net(Path.Combine(SetupPath, "log4net.config"))).GetLogger(GetType());
            
        PerformLogging(logger);
    }
    
    private void PerformLogging(ILog log)
    {
        log!.Info(".......This is a info message !!!", 1, 2, 3);
        log.Warn(".......This is a warn message !!!", 1, 2, 3);
        log.Debug(".......This is a debug message !!!", 1, 2, 3);
        log.Error(new ArgumentException(".......No arguments!!"), ".......This is a error message !!!", 1, 2, 3);
    }
}