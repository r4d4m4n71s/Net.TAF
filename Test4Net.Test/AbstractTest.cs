using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Test4Net.Logging.Interfaces;
using Test4Net.Logging.Provider;

namespace Test4Net.Test;

/// <summary>
/// Define a base model for instrument testing
/// </summary>
public abstract class AbstractTest
{
    /// <summary>
    /// Log provider
    /// </summary>
    protected ILogProvider LogProvider { get; set; }

    /// <summary>
    /// Test name or scenario name
    /// </summary>
    public string TestName { get; set; }

    /// <summary>
    /// Log test messages 
    /// </summary>
    public ILog Log => LogProvider.GetLogger(GetType());

    /// <summary>
    /// Test configuration
    /// </summary>
    protected IConfiguration Configuration { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    protected AbstractTest(IConfiguration configuration, ILogProvider logProvider)
    {
        Configuration = configuration;
        LogProvider = logProvider;
    }

    /// <summary>
    /// Constructor from configuration action
    /// </summary>
    /// <param name="configure"></param>
    protected AbstractTest(Func<IConfigurationBuilder, IConfigurationBuilder> configure) => 
        Configuration = configure(new ConfigurationBuilder()).Build();

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