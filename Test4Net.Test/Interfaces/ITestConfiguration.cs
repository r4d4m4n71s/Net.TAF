namespace Test4Net.Test.Interfaces;

/// <summary>
/// Configuration for test running
/// </summary>
public interface ITestConfiguration
{
    /// <summary>
    /// Configuration name
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Configuration values
    /// </summary>
    IDictionary<string, object> Values { get; set; }
}