using OpenQA.Selenium.Firefox;
using Test4Net.UI.WebBrowser.Driver.Configuration.Models;

namespace Test4Net.UI.WebBrowser.Driver.Configuration.Type.Flavors;

/// <summary>
/// Browser stack firefox setup
/// </summary>
public class BrowserStackFirefoxSetup : RemoteAbstractSetup<FirefoxOptions>
{
    /// <summary>
    /// Create browser stack firefox setup from a options set
    /// <see cref="BrowserStackBuilder{T}"/>
    /// </summary>
    /// <param name="optionsAsDic">Options set as dic</param>
    public BrowserStackFirefoxSetup(IDictionary<string, object> optionsAsDic) : 
        base(new BrowserStackBuilder<FirefoxOptions>(optionsAsDic))
    { }

    /// <summary>
    /// Create browser stack firefox setup from firefox options
    /// </summary>
    /// <param name="options"></param>
    public BrowserStackFirefoxSetup(FirefoxOptions options) :
        base(new BrowserStackBuilder<FirefoxOptions>(options))
    { }

    /// <summary>
    /// Create browser stack firefox setup from dic and firefox options
    /// </summary>
    /// <param name="optionsAsDic"></param>
    /// <param name="options"></param>
    public BrowserStackFirefoxSetup(IDictionary<string, object> optionsAsDic, FirefoxOptions options) :
        base(new BrowserStackBuilder<FirefoxOptions>(optionsAsDic, options))
    { }
}