using OpenQA.Selenium.Safari;
using Test4Net.UI.WebBrowser.Driver.Configuration.Models;

namespace Test4Net.UI.WebBrowser.Driver.Configuration.Type.Flavors;

/// <summary>
/// Browser stack safari setup
/// </summary>
public class BrowserStackSafariSetup : RemoteAbstractSetup<SafariOptions>
{
    /// <summary>
    /// Create browser stack safari setup from a options set
    /// <see cref="BrowserStackBuilder{T}"/>
    /// </summary>
    /// <param name="optionsAsDic">Options set as dic</param>
    public BrowserStackSafariSetup(IDictionary<string, object> optionsAsDic) : 
        base(new BrowserStackBuilder<SafariOptions>(optionsAsDic))
    { }

    /// <summary>
    /// Create browser stack safari setup from firefox options
    /// </summary>
    /// <param name="options"></param>
    public BrowserStackSafariSetup(SafariOptions options) :
        base(new BrowserStackBuilder<SafariOptions>(options))
    { }

    /// <summary>
    /// Create browser stack safari setup from dic and firefox options
    /// </summary>
    /// <param name="optionsAsDic"></param>
    /// <param name="options"></param>
    public BrowserStackSafariSetup(IDictionary<string, object> optionsAsDic, SafariOptions options) :
        base(new BrowserStackBuilder<SafariOptions>(optionsAsDic, options))
    { }
}