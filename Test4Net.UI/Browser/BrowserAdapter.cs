using OpenQA.Selenium;
using Test4Net.UI.Browser.Interfaces;

namespace Test4Net.UI.Browser;

/// <summary>
/// <see cref="IWebDriver"/> Browser
/// with javascript capabilities
/// </summary>
public sealed class BrowserAdapter : IBrowser
{
    /// <summary>
    /// T Driver
    /// </summary>
    public IWebDriver Driver { get; }

    /// <inheritdoc/>
    public IJavaScript JavaScript { get; }

    public BrowserAdapter(IWebDriver driver)
    {
        Driver = driver;
        JavaScript = new JavaScriptAdapter(this);
    }
}