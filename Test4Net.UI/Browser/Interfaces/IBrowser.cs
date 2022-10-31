using OpenQA.Selenium;

namespace Test4Net.UI.Browser.Interfaces;

/// <summary>
/// Html browser
/// </summary>
public interface IBrowser
{
    /// <summary>
    /// Configuration id when the browser was created
    /// </summary>
    string ConfigurationId { get; }

    /// <inheritdoc cref="IJavaScript"/>
    IJavaScript JavaScript { get; }

    /// <summary>
    /// Driver
    /// </summary>
    IWebDriver Driver { get; }
}