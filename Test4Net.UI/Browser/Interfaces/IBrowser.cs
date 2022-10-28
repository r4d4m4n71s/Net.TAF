using OpenQA.Selenium;

namespace Test4Net.UI.Browser.Interfaces;

/// <summary>
/// Html browser
/// </summary>
public interface IBrowser
{
    /// <inheritdoc cref="IJavaScript"/>
    IJavaScript JavaScript { get; }

    /// <summary>
    /// Driver
    /// </summary>
    IWebDriver Driver { get; }
}