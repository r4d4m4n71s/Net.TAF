﻿using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Test4Net.UI.Browser.Interfaces;
using Test4Net.UI.Page.Interfaces;

namespace Test4Net.UI.Page;

/// <summary>
/// Html page with browser capabilities 
/// </summary>
public abstract class PageAdapter : IPage
{
    private readonly IBrowser _browser;
    private readonly IWebDriver _driver;

    protected PageAdapter(IBrowser browser, IPage parent = default)
    {
        _browser = browser;
        Parent = parent;
        _driver = browser.Driver;
        JsExecutor = browser.JavaScript;
    }

    /// <inheritdoc cref="IJavaScript" />
    public IJavaScript JsExecutor { get; }

    /// <inheritdoc cref="IPage.Parent" />
    public IPage Parent { get; }

    /// <inheritdoc cref="IPage.Title" />
    public string Title => _driver.Title;

    /// <inheritdoc cref="IPage.PageSource" />
    public string PageSource => _driver.PageSource;

    /// <inheritdoc cref="IPage.CurrentWindowHandle" />
    public string CurrentWindowHandle => _driver.CurrentWindowHandle;

    /// <inheritdoc cref="IPage.WindowHandles" />
    public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;

    /// <inheritdoc />
    public void GoToUrl(string url) => _driver.Navigate().GoToUrl(url);

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
}