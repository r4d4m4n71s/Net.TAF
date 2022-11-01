using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Test4Net.Logging.Interfaces;
using Test4Net.Logging.Provider;
using Test4Net.Test.Interfaces;

namespace Test4Net.Test.Models;

/// <summary>
/// Define a base model for instrument testing
/// </summary>
public abstract class AbstractTest
{
    /// <summary>
    /// Test context
    /// </summary>
    protected ITestContext<ITestConfiguration> Context { get; set; }

    /// <summary>
    /// Log provider
    /// </summary>
    protected static ILogProvider LogProvider = new LogProvider();

    /// <summary>
    /// Log test messages 
    /// </summary>
    public ILog Log => LogProvider.GetLogger(GetType());

    /// <summary>
    /// Configure log from action builder
    /// </summary>
    /// <param name="configure"></param>
    protected ILogProvider ConfigureLogger(Action<ILoggingBuilder> configure) =>
        new LogProvider(configure);

    /// <summary>
    /// Configure log from configuration section
    /// </summary>
    protected ILogProvider ConfigureLogger(IConfigurationSection loggerConfigurationSection) =>
        ConfigureLogger(builder => builder.Decorate(loggerConfigurationSection));
}