using Test4Net.Test.Interfaces;
using Test4Net.Test.Models;
using Test4Net.UI.POM.Page;
using Test4Net.UI.POM.Page.Interfaces;
using Test4Net.UI.WebBrowser.Browser;
using Test4Net.UI.WebBrowser.Driver;
using Test4Net.UI.WebBrowser.Util;
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
    protected new IExecutionContext<IUiTestConfiguration> ExecContext { get; set; }

    /// <inheritdoc cref="UI.POM.Page.PageFactory"/>
    protected IPageFactory PageFactory;

    /// <summary>
    /// Initialize factories
    /// </summary>
    /// <param name="setupId">setup profile id to use</param>
    /// <param name="driverSettings">json file with driver setups</param>
    public virtual void InitFactories(string setupId, string driverSettings)
    {
        ExecContext = ExecutionContextFromSettings(setupId, driverSettings);

        var driverFactory = new DriverFactory(ExecContext.Configuration.Id, ExecContext.Configuration.Values);
        PageFactory = new PageFactory(new BrowserFactory(driverFactory), ExecContext.Configuration.Id, DefineDefaultPageRules);
    }

    /// <summary>
    /// Define default validation rules applied across all generated pages <see cref="ValidationRules"/>
    /// </summary>
    /// <returns>true if rules validation must be pass success</returns>
    protected virtual bool DefineDefaultPageRules(IPage page) => true;

    /// <summary>
    /// Dispose unhandled resources
    /// </summary>
    public virtual void Dispose() => 
        PageFactory.Dispose();

    /// <summary>
    /// Define execution context from settings file
    /// </summary>
    /// <param name="setupId"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static IExecutionContext<IUiTestConfiguration> ExecutionContextFromSettings(string setupId, string settings) => 
        new ExecutionContext(setupId, settings.CreateOptionsStructureForANode(setupId));
}