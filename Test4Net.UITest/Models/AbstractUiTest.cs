using Test4Net.Test.Interfaces;
using Test4Net.Test.Models;
using Test4Net.UI.POM.Page;
using Test4Net.UI.POM.Page.Interfaces;
using Test4Net.UI.WebBrowser.Browser;
using Test4Net.UI.WebBrowser.Browser.Interfaces;
using Test4Net.UI.WebBrowser.Driver;
using Test4Net.UITest.Interfaces;

namespace Test4Net.UITest.Models;
/// <summary>
/// Define base ui model for instrument testing
/// </summary>
public abstract class AbstractUiTest : AbstractTest, IDisposable
{
    /// <summary>
    /// Test context
    /// </summary>
    protected new IExecutionContext<IUiTestConfiguration> ExecContext { get; }

    /// <inheritdoc cref="UI.POM.Page.PageFactory"/>
    protected readonly IPageFactory PageFactory;

    private readonly IBrowserFactory _browserFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pageFactory"></param>
    public AbstractUiTest(IPageFactory pageFactory = null) => 
        PageFactory = pageFactory;

    /// <summary>
    /// Constructor from test context <see cref="IExecutionContext{T}"/>>
    /// </summary>
    /// <param name="execContext"></param>
    public AbstractUiTest(IExecutionContext<IUiTestConfiguration> execContext)
    {
        ExecContext = execContext;

        var driverFactory = new DriverFactory(ExecContext.Configuration.Id, ExecContext.Configuration.Values);
        _browserFactory = new BrowserFactory(driverFactory);
        PageFactory = new PageFactory(_browserFactory.Get(ExecContext.Configuration.Id), DefineDefaultPageRules);
    }

    /// <summary>
    /// Define default validation rules applied across all generated pages <see cref="ValidationRules"/>
    /// </summary>
    /// <returns>true if rules validation must be pass success</returns>
    protected virtual bool DefineDefaultPageRules(IPage page) => true;

    /// <summary>
    /// Dispose unhandled resources
    /// </summary>
    public virtual void Dispose()
    {
        ((BrowserFactory)_browserFactory).Dispose();
    }
}