using Microsoft.Extensions.Logging;
using Test4Net.Logging.Interfaces;

namespace Test4Net.Logging.Adapter;

public class LogAdapter : ILog
{
    /// <summary>
    /// Logger
    /// </summary>
    private readonly ILogger _logger;
        
    /// <summary>
    /// Constructor from logger
    /// </summary>
    /// <param name="logger"></param>
    public LogAdapter(ILogger logger) => _logger = logger;
        
    /// <inheritdoc cref="Interfaces.ILog.Info"/>
    public void Info(string message, params object[] args)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            _logger.LogInformation(message, args);
    }

    /// <inheritdoc cref="Interfaces.ILog.Debug"/>
    public void Debug(string message, params object[] args)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug(message, args);
    }

    /// <inheritdoc cref="Interfaces.ILog.Warn"/>
    public void Warn(string message, params object[] args)
    {
        if (_logger.IsEnabled(LogLevel.Warning))
            _logger.LogWarning(message, args);
    }

    /// <inheritdoc cref="Interfaces.ILog.Error"/>
    public void Error(Exception e, string message = null, params object[] args)
    {
        if (_logger.IsEnabled(LogLevel.Error))
            _logger.LogError(e, message, args);
    }
}

public class LogAdapter<T> : LogAdapter
{
    public LogAdapter(ILogger<T> logger) : base(logger)
    {
    }
}