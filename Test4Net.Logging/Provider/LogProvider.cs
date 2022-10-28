using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Test4Net.Logging.Adapter;
using Test4Net.Logging.Interfaces;

namespace Test4Net.Logging.Provider;

/// <summary>
/// .Net core log provider implementation
/// </summary>
public class LogProvider : ILogProvider
{
    /// <summary>
    /// for each logged class, store a single logger instance reference
    /// where Type is the target logged class and ILog the related logger
    /// </summary>
    private static readonly IDictionary<Type, object> LogInstances = new Dictionary<Type, object>();

    /// <summary>
    /// Perform builder decoration  
    /// </summary>
    private readonly Action<ILoggingBuilder> _decorateLogger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="decorateLogger">Perform builder decoration</param>
    public LogProvider(Action<ILoggingBuilder> decorateLogger = null) => 
        _decorateLogger = decorateLogger;

    /// <summary>
    /// Creates a new logger factory a returns a
    /// a new <see cref="ILog"/> instance
    /// </summary>
    /// <typeparam name="T">Target logging class</typeparam>
    /// <returns><see cref="ILog"/> instance</returns>
    public ILog GetLogger<T>() => new LogAdapter<T>(GetNativeLogger<T>());

    /// <summary>
    /// Creates a new logger factory a returns a
    /// a new <see cref="ILog"/> instance
    /// </summary>
    /// <typeparam name="T">Target logging class</typeparam>
    /// <returns><see cref="ILog"/> instance</returns>
    public ILog GetLogger(Type type) => new LogAdapter(GetNativeLogger(type));

    /// <summary>
    /// Creates a new logger factory a returns a
    /// a new <see cref="ILogger"/> instance
    /// </summary>
    /// <returns><see cref="ILogger"/> instance</returns>
    public ILogger GetNativeLogger(Type target)
    {
        if (LogInstances.ContainsKey(target))
            return (ILogger)LogInstances[target];

        var logger = LoggerFactory.Create(_decorateLogger ?? 
            (x => x.AddDebug().AddConsole().SetMinimumLevel(LogLevel.Information))).CreateLogger(target);
        LogInstances.Add(target, logger);

        // Todo: include this class as part of LoggerFactory services and return when needs, see log4net add to ILoggingBuilder example

        return logger;  
    }

    /// <summary>
    /// Creates a new logger factory a returns a
    /// a new <see cref="ILogger{T}"/> instance
    /// </summary>
    /// <typeparam name="T">Target logging class</typeparam>
    /// <returns><see cref="ILogger{T}"/> instance</returns>
    public ILogger<T> GetNativeLogger<T>()
    {
        if (LogInstances.ContainsKey(typeof(T)))
            return (ILogger<T>)LogInstances[typeof(T)];

        var logger = LoggerFactory.Create(_decorateLogger ??
            (x => x.AddDebug().AddConsole().SetMinimumLevel(LogLevel.Information))).CreateLogger<T>();
        LogInstances.Add(typeof(T), logger);

        return logger;
    }
}

public static class LoggingBuilderUtil
{
    /// <summary>
    /// Decorate a log builder
    /// </summary>
    /// <param name="builder">For this builder</param>
    /// <param name="configuration">Decorate with this configuration</param>
    /// <returns>Decorated builder</returns>
    public static ILoggingBuilder Decorate(this ILoggingBuilder builder, IConfiguration configuration)
    {
        builder.AddConfiguration(configuration);
        builder.AddDebug();
        builder.AddConsole();
        return builder;
    }
}