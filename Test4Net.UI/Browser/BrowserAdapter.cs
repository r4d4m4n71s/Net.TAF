using OpenQA.Selenium;
using Test4Net.UI.Browser.Interfaces;

namespace Test4Net.UI.Browser;

/// <summary>
/// <see cref="IWebDriver"/> Browser
/// with javascript capabilities
/// </summary>
public sealed class BrowserAdapter : IBrowser
{
    /// <inheritdoc/>
    public string ConfigurationId { get; }

    /// <summary>
    /// T Driver
    /// </summary>
    public IWebDriver Driver { get; }

    /// <inheritdoc/>
    public IJavaScript JavaScript { get; }
    
    public BrowserAdapter(string configurationId, IWebDriver driver)
    {
        ConfigurationId = configurationId;
        Driver = driver;
        ConfigurationId = configurationId;
        JavaScript = new JavaScriptAdapter(this);
    }
}