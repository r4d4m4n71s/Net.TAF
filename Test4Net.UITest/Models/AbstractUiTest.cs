using Test4Net.Logging.Interfaces;
using Test4Net.Test.Interfaces;
using Test4Net.Test.Models;
using Test4Net.UI.POM.Page;
using Test4Net.UI.POM.Page.Interfaces;
using Test4Net.UI.WebBrowser.Browser.Interfaces;
using Test4Net.UITest.Interfaces;

namespace Test4Net.UITest.Models;
/// <summary>
/// Define base ui model for instrument testing
/// </summary>
public abstract class AbstractUiTest : AbstractTest
{
    /// <summary>
    /// Test context
    /// </summary>
    protected new ITestContext<IUiTestConfiguration> Context { get; set; }

    /// <inheritdoc cref="UI.POM.Page.PageFactory"/>
    protected IPageFactory PageFactory { get; set; }


    /// <summary>
    /// Builds a page, validates and return it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetPage<T>() where T : AbstractPage
    {
        var page = PageFactory.GetPage<T>();

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