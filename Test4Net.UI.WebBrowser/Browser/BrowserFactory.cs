using Test4Net.UI.WebBrowser.Browser.Interfaces;
using Test4Net.UI.WebBrowser.Driver.Interfaces;

namespace Test4Net.UI.WebBrowser.Browser;

/// <summary>
/// Browser instances factory
/// </summary>
public class BrowserFactory : IBrowserFactory
{
    /// <summary>
    /// Driver factory to process requests
    /// </summary>
    private readonly IDriverFactory _driverFactory;

    /// <summary>
    /// Creates new browser factory with a driver factory
    /// </summary>
    /// <param name="driverFactory"></param>
    public BrowserFactory(IDriverFactory driverFactory) => 
        _driverFactory = driverFactory;
    
    /// <summary>
    /// Gets a browser instance from configuration id
    /// </summary>
    /// <param name="setupId"></param>
    /// <returns></returns>
    public IWebBrowser Get(string setupId) => 
        new WebBrowser(setupId, _driverFactory.Get(setupId));

    /// <summary>
    /// Gets a T browser instance from configuration id
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="setupId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public T Get<T>(string setupId) where T : IWebBrowser => (T)Get(setupId);
}