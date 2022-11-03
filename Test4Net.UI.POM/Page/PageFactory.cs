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

    /// <summary>
    /// Define default page rules to apply across all page instances
    /// </summary>
    private readonly Func<IPage, bool> _defineDefaultPageRules;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="webBrowser">Browser to feed pages</param>
    public PageFactory(IWebBrowser webBrowser) => 
        _webBrowser = webBrowser;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="webBrowser">Browser to feed pages</param>
    /// <param name="defineDefaultPageRules">Default validation rules applied to all pages</param>
    public PageFactory(IWebBrowser webBrowser, 
        Func<IPage, bool> defineDefaultPageRules)
    {
        _webBrowser = webBrowser;
        _defineDefaultPageRules = defineDefaultPageRules;
    }

    /// <summary>
    /// Get page, applying rules if are available 
    /// </summary>
    /// <typeparam name="T">Page type</typeparam>
    /// <returns>Page instance</returns>
    public T GetPage<T>(params object[] args) where T : IPage
    {
        var page = (T)Activator.CreateInstance(typeof(T), _webBrowser);
        
        if (_defineDefaultPageRules != null)
            return _defineDefaultPageRules(page) ? page : default;
        
        return page;
    }
}