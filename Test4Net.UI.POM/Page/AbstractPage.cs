using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Test4Net.UI.POM.Page.Interfaces;
using Test4Net.UI.WebBrowser.Browser.Interfaces;

namespace Test4Net.UI.POM.Page;

/// <summary>
/// Html page with browser capabilities 
/// </summary>
public abstract class AbstractPage : IPage
{
    private readonly IWebDriver _driver;

    public IWebBrowser Browser { get; set; }

    protected AbstractPage(IWebBrowser browser)
    {
        Browser = browser;
        _driver = browser.Driver;
        // Init html controls
        SeleniumExtras.PageObjects.PageFactory.InitElements(browser.Driver, this);
    }

    /// <inheritdoc cref="IPage.Parent" />
    public IPage Parent { get; set; }

    /// <inheritdoc cref="IPage.Title" />
    public string Title => _driver.Title;

    /// <inheritdoc cref="IPage.PageSource" />
    public string PageSource => _driver.PageSource;

    /// <inheritdoc cref="IPage.CurrentWindowHandle" />
    public string CurrentWindowHandle => _driver.CurrentWindowHandle;

    /// <inheritdoc cref="IPage.WindowHandles" />
    public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;

    /// <inheritdoc />
    public void GoToUrl(Uri url) => _driver.Navigate().GoToUrl(url);

    /// <inheritdoc />
    public IEnumerable<IWebElement> FindElements(By by) => _driver.FindElements(by);

    /// <inheritdoc />
    public void NavigateBack() => _driver.Navigate().Back();
        
    /// <inheritdoc />
    public void NavigateForward() => _driver.Navigate().Forward();

    /// <inheritdoc />
    public void Refresh() => _driver.Navigate().Refresh();

    /// <inheritdoc cref="IPage.Close" />
    public void Close() => _driver.Close();

    /// <inheritdoc cref="IPage.Quit" />
    public void Quit() => _driver.Quit();

    /// <inheritdoc cref="IPage.FindElement" />
    public IWebElement FindElement(By by) => _driver.FindElement(by);

    /// <inheritdoc />
    ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by) => _driver.FindElements(by);

    /// <inheritdoc cref="IPage.Manage" />
    public IOptions Manage() => _driver.Manage();

    /// <inheritdoc cref="IPage.Navigate" />
    public INavigation Navigate() => _driver.Navigate();

    /// <inheritdoc cref="IPage.SwitchTo" />
    public ITargetLocator SwitchTo() => _driver.SwitchTo();

    /// <summary>
    /// Gets or sets the URL the browser is currently displaying.
    /// </summary>
    public string Url { get; set; }

    /// <inheritdoc cref="Dispose" />
    public void Dispose() => _driver.SwitchTo();

    /// <inheritdoc cref="FindParent{T}"/>
    public TP FindParent<TP>() where TP : IPage => 
        Parent == null ? default : Parent is TP page ? page : Parent.FindParent<TP>();

    /// <inheritdoc cref="IPage.Rules" />
    public ValidationRules Rules { get; set; } = new();
}