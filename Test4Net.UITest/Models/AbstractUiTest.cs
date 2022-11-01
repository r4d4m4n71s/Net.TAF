using Test4Net.Test.Interfaces;
using Test4Net.Test.Models;
using Test4Net.UI.Browser.Interfaces;
using Test4Net.UI.Page;
using Test4Net.UI.Page.Interfaces;
using Test4Net.UITest.Interfaces;

namespace Test4Net.UITest.Models;
/// <summary>
/// Define base ui model for instrument testing
/// </summary>
public abstract class AbstractUiTest : AbstractTest
{
    /// <inheritdoc cref="PageFactory"/>
    private readonly IPageFactory _pageFactory = new PageFactory(LogProvider);

    /// <summary>
    /// Browser factory
    /// </summary>
    protected IBrowserFactory BrowserFactory { get; set; }

    /// <inheritdoc cref="AbstractTest.Context"/>
    protected new ITestContext<IUiTestConfiguration> Context { get; set; }

    /// <summary>
    /// Builds a page, validates and return it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetPage<T>() where T : AbstractPage
    {
        var page = _pageFactory.GetPage<T>(BrowserFactory.Get(Context.Configuration.Name));

        // Derive page rules validation to the concrete test class
        if (ValidatePageRules(page))
            return page;

        return default;
    }

    /// <summary>
    /// Define validate rules <see cref="ValidatePageRules"/> for a page
    /// </summary>
    /// <param name="page">target page to add rules</param>
    /// <returns>true if rules validation is success</returns>
    protected abstract bool ValidatePageRules(AbstractPage page);
}