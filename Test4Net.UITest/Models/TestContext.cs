using Test4Net.Test.Interfaces;
using Test4Net.UITest.Interfaces;

namespace Test4Net.UITest.Models;

/// <summary>
/// Test execution context
/// </summary>
public class TestContext : ITestContext<IUiTestConfiguration>
{
    /// <summary>
    /// Test case name
    /// </summary>
    public string TestName { get; set; }
    
    /// <summary>
    /// Test case configuration
    /// </summary>
    public IUiTestConfiguration Configuration { get; set; }

    /// <summary>
    /// Constructor from json dic
    /// </summary>
    /// <param name="testName"></param>
    /// <param name="configurationName"></param>
    /// <param name="fromJsonDic"></param>
    public TestContext(string testName, string configurationName, string fromJsonDic)
    {
        TestName = testName;
        Configuration = new UiTestConfiguration(fromJsonDic, configurationName);
    }
}