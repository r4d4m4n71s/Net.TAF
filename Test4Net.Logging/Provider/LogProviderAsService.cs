using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Test4Net.Logging.Adapter;
using Test4Net.Logging.Interfaces;

namespace Test4Net.Logging.Provider;

/// <summary>
/// .Net core log provider implementation using DI approach
/// </summary>
public class LogProviderAsService : ILogProvider
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Enroll loggers using dependency injection
    /// </summary>
    /// <param name="loggerTypes">Logger classes</param>
    /// <param name="decorateConfiguration">Additional log configuration</param>
    public LogProviderAsService(Type[] loggerTypes, Action<ILoggingBuilder> decorateConfiguration = null)
    {
        var loggerServices = new ServiceCollection();
            
        // Inject loggers
        foreach (var type in loggerTypes)
            loggerServices.AddSingleton(type);

        _serviceProvider = loggerServices.AddLogging(
                decorateConfiguration ?? (x => x.AddConsole().SetMinimumLevel(LogLevel.Debug)))
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true });
    }

    /// <summary>
    /// Get logger from service provider
    /// </summary>
    /// <typeparam name="T">Target logging class</typeparam>
    /// <returns><see cref="ILog"/> instance</returns>
    public ILog GetLogger<T>() =>_serviceProvider.GetService<LogAdapter<T>>();

    /// <summary>
    /// Get logger from service provider
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ILog GetLogger(Type type) => null;
    
    /// <summary>
    /// Get microsoft ILogger from service provider
    /// </summary>
    /// <typeparam name="T">Target logging class</typeparam>
    /// <returns><see cref="ILogger{T}"/> instance</returns>
    public ILogger<T> GetNativeLogger<T>() => _serviceProvider.GetService<ILogger<T>>();
}