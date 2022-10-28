using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using Test4Net.Logging.Interfaces;
using Test4Net.Test;
using Test4Net.UI.Browser;
using Test4Net.UI.Driver;
using Test4Net.UI.Page;

namespace Test4Net.UITest;
/// <summary>
/// Define base ui model for instrument testing
/// </summary>
public abstract class AbstractUiTest : AbstractTest
{
    /// <inheritdoc cref="UI.Page.PageFactory"/>
    protected PageFactory PageFactory { get; set; }

    /// <inheritdoc cref="UI.Driver.DriverFactory"/>
    protected DriverFactory DriverFactory { get; set; }

    /// <inheritdoc />
    protected AbstractUiTest(
        IConfiguration configuration,
        ILogProvider logProvider,
        PageFactory pageFactory,
        DriverFactory driverFactory) : base(configuration, logProvider)
    {
        PageFactory = pageFactory;
        DriverFactory = driverFactory;
    }

    protected AbstractUiTest(Func<IConfigurationBuilder, IConfigurationBuilder> configure) : base(configure){ }
    
    public T GetPage<T>() where T : AbstractPage
    {
        var browser = new BrowserAdapter(DriverFactory.Get<ChromeDriver>("Chrome"));

        //var pagePlatformAttributes = typeof(T).GetCustomAttributes(typeof(PlatformAttribute), true);

        var page = PageFactory.GetPage<T>(browser);

        page.Rules.ValidateRules(faults => true);

        return page;
    }
}