using Test4Net.Test.Interfaces;

namespace Test4Net.Test.Models;

/// <summary>
/// Test execution context
/// </summary>
public class ExecutionContext : IExecutionContext<TestConfiguration>
{
    /// <summary>
    /// Test case configuration
    /// </summary>
    public TestConfiguration Configuration { get; set; }

    /// <summary>
    /// Constructor from json dic
    /// </summary>
    /// <param name="configurationId">Unique configuration identifier from json</param>
    /// <param name="fromJsonDic">Configuration dic in json format</param>
    public ExecutionContext(string configurationId, string fromJsonDic) => 
        Configuration = new TestConfiguration(fromJsonDic, configurationId);

    /// <summary>
    /// Constructor from dic
    /// </summary>
    /// <param name="configurationId">Unique configuration identifier</param>
    /// <param name="values">Configuration values</param>
    public ExecutionContext(string configurationId, IDictionary<string, object> values) =>
        Configuration = new TestConfiguration(values, configurationId);
}