namespace Test4Net.Logging.Interfaces;

/// <summary>
/// Logger contract
/// </summary>
public interface ILog
{
    /// <summary>
    /// Log information
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    void Info(string message, params object[] args);

    /// <summary>
    /// Log debug
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    void Debug(string message, params object[] args);

    /// <summary>
    /// Log warning
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    void Warn(string message, params object[] args);

    /// <summary>
    /// Log error
    /// </summary>
    /// <param name="exception">Exception</param>>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    void Error(Exception exception, string message = null, params object[] args);
}