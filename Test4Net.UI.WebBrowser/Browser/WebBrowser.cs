using OpenQA.Selenium;
using Test4Net.UI.WebBrowser.Browser.Interfaces;

namespace Test4Net.UI.WebBrowser.Browser;

/// <summary>
/// <see cref="IWebDriver"/> Browser
/// with javascript capabilities
/// </summary>
public class WebBrowser : IWebBrowser
{
    /// <inheritdoc/>
    public string ConfigurationId { get; }

    /// <summary>
    /// T Driver
    /// </summary>
    public IWebDriver Driver { get; }

    /// <inheritdoc/>
    public IJavaScript JavaScript { get; }
    
    public WebBrowser(string configurationId, IWebDriver driver)
    {
        ConfigurationId = configurationId;
        Driver = driver;
        ConfigurationId = configurationId;
        JavaScript = new JavaScriptAdapter(this);
    }

    /// <summary>
    /// Disposes the driver
    /// </summary>
    public void Dispose() => Driver.Quit();
}