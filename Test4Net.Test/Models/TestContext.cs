using Test4Net.Test.Interfaces;

namespace Test4Net.Test.Models;

/// <summary>
/// Test execution context
/// </summary>
public class TestContext : ITestContext<TestConfiguration>
{
    /// <summary>
    /// Test case configuration
    /// </summary>
    public TestConfiguration Configuration { get; set; }

    /// <summary>
    /// Constructor from json dic
    /// </summary>
    /// <param name="configurationName"></param>
    /// <param name="fromJsonDic"></param>
    public TestContext(string configurationName, string fromJsonDic)
    {
        Configuration = new TestConfiguration(fromJsonDic, configurationName);
    }
}