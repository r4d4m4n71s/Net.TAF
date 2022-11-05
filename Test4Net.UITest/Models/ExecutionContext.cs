using Test4Net.Test.Interfaces;
using Test4Net.UITest.Interfaces;

namespace Test4Net.UITest.Models;

/// <summary>
/// Test execution context
/// </summary>
public class ExecutionContext :  IExecutionContext<IUiTestConfiguration>
{
    /// <summary>
    /// Test case configuration
    /// </summary>
    public IUiTestConfiguration Configuration { get; set; }

    /// <summary>
    /// Constructor from json dic
    /// </summary>
    /// <param name="configurationId">Unique configuration identifier</param>
    /// <param name="fromJsonDic">Configuration dic in json format</param>
    public ExecutionContext(string configurationId, string fromJsonDic) => 
        Configuration = new UiTestConfiguration(fromJsonDic, configurationId);

    /// <summary>
    /// Constructor from dic
    /// </summary>
    /// <param name="configurationId">Unique configuration identifier</param>
    /// <param name="values">Configuration values</param>
    public ExecutionContext(string configurationId, IDictionary<string, object> values) =>
        Configuration = new UiTestConfiguration(values, configurationId);
}