namespace Test4Net.Test.Interfaces;

/// <summary>
/// Configuration for test running
/// </summary>
public interface ITestConfiguration
{
    /// <summary>
    /// Configuration unique id
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// Configuration values
    /// </summary>
    IDictionary<string, object> Values { get; set; }
}