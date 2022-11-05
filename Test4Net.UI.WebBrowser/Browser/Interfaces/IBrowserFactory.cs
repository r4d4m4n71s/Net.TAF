namespace Test4Net.UI.WebBrowser.Browser.Interfaces;

public interface IBrowserFactory
{
    /// <summary>
    /// Get browser instance from driver setup
    /// </summary>
    /// <param name="setupId"></param>
    /// <returns>Driver instance</returns>
    public IWebBrowser Get(string setupId);

    /// <summary>
    /// Get browser instance from driver setup
    /// </summary>
    /// <param name="setupId"></param>
    /// <returns>Driver instance</returns>
    public T Get<T>(string setupId) where T : IWebBrowser;
}