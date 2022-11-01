using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Test4Net.UI.Page.Interfaces;

/// <summary>
/// HTML page model 
/// </summary>
public interface IPage : IWebDriver
{
    /// <summary>
    /// The parent page.
    /// </summary>
    public IPage Parent { get; }

    /// <summary>
    /// Gets the title of the current browser window.
    /// </summary>
    new string Title { get; }

    /// <summary>
    /// Gets the source of the page last loaded by the browser.
    /// </summary>
    new string PageSource { get; }

    /// <summary>
    /// Gets the current window handle, which is an opaque handle to this window that uniquely identifies it within this driver instance.
    /// </summary>
    new string CurrentWindowHandle { get; }

    /// <summary>
    /// Gets the window handles of open browser windows.
    /// </summary>
    new ReadOnlyCollection<string> WindowHandles { get; }

    /// <summary>
    /// Navigate to url
    /// </summary>
    /// <param name="url"></param>
    void GoToUrl(string url);

    /// <summary>
    /// Finds the first IWebElement using the given method.
    /// </summary>
    /// <param name="by"></param>
    /// <returns></returns>
    new IWebElement FindElement(By by);

    /// <summary>
    /// Finds the first IWebElement using the given method.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    new IEnumerable<IWebElement> FindElements(By selector);

    /// <summary>
    /// Navigate to the back page
    /// </summary>
    void NavigateBack();

    /// <summary>
    /// Navigate to the forward page
    /// </summary>
    void NavigateForward();

    /// <summary>
    /// Refresh browser
    /// </summary>
    void Refresh();

    /// <summary>
    /// Close the current window, quitting the browser if it is the last window currently open
    /// </summary>
    new void Close();

    /// <summary>
    /// Quits this driver, closing every associated window.
    /// </summary>
    new void Quit();

    /// <summary>
    /// Instructs the driver to change its settings.
    /// </summary>
    /// <returns></returns>
    new IOptions Manage();

    /// <summary>
    /// Instructs the driver to navigate the browser to another location.
    /// </summary>
    /// <returns></returns>
    new INavigation Navigate();

    /// <summary>
    /// Instructs the driver to send future commands to a different frame or window.
    /// </summary>
    /// <returns></returns>
    new ITargetLocator SwitchTo();

    /// <summary>
    /// Given an IPage, look up towards   returning the first matching parent
    /// </summary>
    /// <typeparam name="T">A type T.</typeparam>
    /// <returns>An object of type T.</returns>
    T FindParent<T>() where T : IPage;
}