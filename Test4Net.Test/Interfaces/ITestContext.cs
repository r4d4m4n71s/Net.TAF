namespace Test4Net.Test.Interfaces;

public interface ITestContext<T> where T : ITestConfiguration
{
    /// <summary>
    /// Test case configuration
    /// </summary>
    T Configuration { get; set; }
}