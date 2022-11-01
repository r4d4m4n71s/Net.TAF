using OpenQA.Selenium;

namespace Test4Net.UI.POM.Browser.Interfaces;

/// <summary>
/// Javascript functions
/// </summary>
public interface IJavaScript
{
    /// <summary>
    /// Execute js function
    /// </summary>
    /// <param name="javaScript"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    object Execute(string javaScript, params object[] args);

    /// <summary>
    /// Await ajax exec
    /// </summary>
    void WaitAjax();

    /// <summary>
    /// Wait document.readyState
    /// </summary>
    void WaitReadyState();

    /// <summary>
    /// Fire jQuery event
    /// </summary>
    /// <param name="element"></param>
    /// <param name="javaScriptEvent"></param>
    void FireJQueryEvent(IWebElement element, JavaScriptEvent javaScriptEvent);
}