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
    private readonly IBrowserFactory _browserFactory;

    /// <summary>
    /// Browser setup id to browser requesting
    /// </summary>
    private readonly string _setupId;

    /// <summary>
    /// Web browser, single instance for all page calls
    /// </summary>
    private IWebBrowser _webBrowser;

    /// <summary>
    /// Define default page rules to apply across all page instances
    /// </summary>
    private readonly Func<IPage, bool> _defineDefaultPageRules;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="browserFactory">Browser to feed pages</param>
    /// <param name="setupId">Browser setup id to browser requesting</param>
    public PageFactory(IBrowserFactory browserFactory, string setupId)
    {
        _browserFactory = browserFactory;
        _setupId = setupId;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="browserFactory">Browser factory to feed pages</param>
    /// <param name="setupId">Browser setup id to browser requesting</param>
    /// <param name="defineDefaultPageRules">Default validation rules applied to all pages</param>
    public PageFactory(IBrowserFactory browserFactory, string setupId,
        Func<IPage, bool> defineDefaultPageRules)
    {
        _browserFactory = browserFactory;
        _defineDefaultPageRules = defineDefaultPageRules;
        _setupId = setupId;
    }

    /// <summary>
    /// Get page, applying rules if are available 
    /// </summary>
    /// <typeparam name="T">Page type</typeparam>
    /// <returns>Page instance</returns>
    public T GetPage<T>(params object[] args) where T : IPage
    {
        // single browser instance for this page factory request
        _webBrowser ??= _browserFactory.Get(_setupId);

        var page = (T)Activator.CreateInstance(typeof(T), _webBrowser);
        
        if (_defineDefaultPageRules != null)
            return _defineDefaultPageRules(page) ? page : default;
        
        return page;
    }

    /// <summary>
    /// Disposes the web browser instance
    /// </summary>
    public void Dispose() => _webBrowser?.Dispose();
}