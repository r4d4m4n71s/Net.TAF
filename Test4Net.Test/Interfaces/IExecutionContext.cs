namespace Test4Net.Test.Interfaces;

/// <summary>
/// Defines execution context for current test
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IExecutionContext<T> where T : ITestConfiguration
{
    /// <summary>
    /// Test case configuration
    /// </summary>
    T Configuration { get; set; }
}