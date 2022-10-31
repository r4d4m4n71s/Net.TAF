using Test4Net.Logging.Interfaces;
using Test4Net.UI.Browser.Interfaces;
using Test4Net.UI.Page.Interfaces;

namespace Test4Net.UI.Page;

/// <summary>
/// Provides page objects
/// </summary>
public class PageFactory : IPageFactory
{
    /// <summary>
    /// Log provider
    /// </summary>
    private readonly ILogProvider _logProvider;

    public PageFactory(ILogProvider logProvider) => 
        _logProvider = logProvider;

    /// <summary>
    /// Get page instance
    /// </summary>
    /// <typeparam name="T">Page type</typeparam>
    /// <param name="browser">Browser instance</param>
    /// <returns></returns>
    public T GetPage<T>(IBrowser browser) where T : IPage => 
        GetPage<T>(browser, _logProvider.GetLogger<T>());

    /// <summary>
    /// Get page instance from args
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    /// <returns></returns>
    private T GetPage<T>(params object[] args) where T : IPage =>
        (T)Activator.CreateInstance(typeof(T), args);
}