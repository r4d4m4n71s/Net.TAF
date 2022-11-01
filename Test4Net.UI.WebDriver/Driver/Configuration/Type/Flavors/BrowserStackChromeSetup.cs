using OpenQA.Selenium.Chrome;
using Test4Net.UI.WebDriver.Driver.Configuration.Models;

namespace Test4Net.UI.WebDriver.Driver.Configuration.Type.Flavors;

/// <summary>
/// Browser stack chrome setup
/// </summary>
public class BrowserStackChromeSetup : RemoteAbstractSetup<ChromeOptions>
{
    /// <summary>
    /// Create browser stack chrome setup from a options set
    /// <see cref="BrowserStackBuilder{T}"/>
    /// </summary>
    /// <param name="optionsAsDic">Options set as dic</param>
    public BrowserStackChromeSetup(IDictionary<string, object> optionsAsDic) : 
        base(new BrowserStackBuilder<ChromeOptions>(optionsAsDic))
    { }

    /// <summary>
    /// Create browser stack chrome setup from chrome options
    /// </summary>
    /// <param name="options"></param>
    public BrowserStackChromeSetup(ChromeOptions options) :
        base(new BrowserStackBuilder<ChromeOptions>(options))
    { }

    /// <summary>
    /// Create browser stack chrome setup from dic and chrome options
    /// </summary>
    /// <param name="optionsAsDic"></param>
    /// <param name="options"></param>
    public BrowserStackChromeSetup(IDictionary<string, object> optionsAsDic, ChromeOptions options) :
        base(new BrowserStackBuilder<ChromeOptions>(optionsAsDic, options))
    { }
}