using Test4Net.UI.POM.Page.Interfaces;
using Test4Net.UI.WebBrowser.Browser.Interfaces;

namespace Test4Net.UI.POM.Page;

/// <summary>
/// Provides page objects
/// </summary>
public class PageFactory : IPageFactory
{
    /// <summary>
    /// Browser
    /// </summary>
    private readonly IWebBrowser _webBrowser;

    public PageFactory(IWebBrowser webBrowser) => 
        _webBrowser = webBrowser;

    /// <summary>
    /// Get page instance
    /// </summary>
    /// <typeparam name="T">Page type</typeparam>
    /// <returns></returns>
    public T GetPage<T>() where T : IPage => 
        GetPage<T>(_webBrowser);

    /// <summary>
    /// Get page instance from args
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    /// <returns></returns>
    private T GetPage<T>(params object[] args) where T : IPage =>
        (T)Activator.CreateInstance(typeof(T), args);
}