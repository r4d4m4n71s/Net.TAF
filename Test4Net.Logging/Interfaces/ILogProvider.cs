namespace Test4Net.Logging.Interfaces;

/// <summary>
/// Log provider common interface
/// </summary>
public interface ILogProvider
{
    /// <summary>
    /// Get logger implementation
    /// </summary>
    /// <typeparam name="T">Logger type</typeparam>
    /// <returns>Logger</returns>
    ILog GetLogger<T>();

    /// <summary>
    /// Get logger implementation
    /// </summary>
    /// <param name="type">logger type</param>
    /// <returns>Logger</returns>
    ILog GetLogger(Type type);
}