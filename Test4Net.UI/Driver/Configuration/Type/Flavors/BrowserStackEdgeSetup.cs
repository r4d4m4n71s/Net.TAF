using OpenQA.Selenium.Edge;
using Test4Net.UI.Driver.Configuration.Models;

namespace Test4Net.UI.Driver.Configuration.Type.Flavors;

/// <summary>
/// Browser stack edge setup
/// </summary>
public class BrowserStackEdgeSetup : RemoteAbstractSetup<EdgeOptions>
{
    /// <summary>
    /// Create browser stack edge setup from a options set
    /// <see cref="BrowserStackBuilder{T}"/>
    /// </summary>
    /// <param name="optionsAsDic">Options set as dic</param>
    public BrowserStackEdgeSetup(IDictionary<string, object> optionsAsDic) : 
        base(new BrowserStackBuilder<EdgeOptions>(optionsAsDic))
    { }

    /// <summary>
    /// Create browser stack edge setup from edge options
    /// </summary>
    /// <param name="options"></param>
    public BrowserStackEdgeSetup(EdgeOptions options) :
        base(new BrowserStackBuilder<EdgeOptions>(options))
    { }

    /// <summary>
    /// Create browser stack edge setup from dic and edge options
    /// </summary>
    /// <param name="optionsAsDic"></param>
    /// <param name="options"></param>
    public BrowserStackEdgeSetup(IDictionary<string, object> optionsAsDic, EdgeOptions options) :
        base(new BrowserStackBuilder<EdgeOptions>(optionsAsDic, options))
    { }
}