using Microsoft.Extensions.Configuration;
using Test4Net.UI.POM.Browser;
using Test4Net.UI.POM.Page;
using Test4Net.UITest.Models;
using TestContext = Test4Net.UITest.Models.TestContext;

namespace Test4Net.QACode.UI;

[TestClass]
public abstract class BaseUiTest : AbstractUiTest
{
    protected string DriverSettings { get; }

    protected readonly IConfiguration Configuration;

    /// <summary>
    /// Path to folder with configurations
    /// </summary>
    protected static readonly string SetupPath = Path.Join(Directory.GetCurrentDirectory(), "/Configuration");

    /// <summary>
    /// Init configuration from json settings
    /// </summary>
    protected BaseUiTest() 
    {
        Environment.SetEnvironmentVariable(Conventions.EnvironmentVariableName.BrowserProfile.ToString(), "Chrome");
        
        Configuration = new ConfigurationBuilder()
            .SetBasePath(SetupPath)
                .AddEnvironmentVariables()
                .AddJsonFile($"{Environment.GetEnvironmentVariable(Conventions.Env.Qa.ToString())}.settings.json",
                    optional: true, reloadOnChange: true).Build();

        LogProvider = ConfigureLogger(Configuration.GetSection("Logging"));
        
        DriverSettings = File.ReadAllText(Path.Combine(SetupPath, "driver.settings.json"));

        var execProfile =
            Environment.GetEnvironmentVariable(Conventions.EnvironmentVariableName.BrowserProfile.ToString());

        Context = new TestContext(execProfile, DriverSettings);
        BrowserFactory = new BrowserFactory(DriverSettings);
    }

    /// <summary>
    /// Define validate rules <see cref="ValidatePageRules"/> for a page
    /// </summary>
    /// <param name="page">target page to add rules</param>
    /// <returns>true if rules validation is success</returns>
    protected override bool ValidatePageRules(AbstractPage page)
    {
        page.Rules.AddPlatformRule(Context.Configuration.Platform)
            .AddBrowserRule(Context.Configuration.Browser)
            .AddViewPortRule(Context.Configuration.ViewPort);

        return page.Rules.ValidateRules(faults => faults.NegotiateDefaultRules(Context.Configuration));
    }
}