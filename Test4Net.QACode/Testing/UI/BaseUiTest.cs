using Microsoft.Extensions.Configuration;
using Test4Net.QACode.Constants;
using Test4Net.UI.POM.Page;
using Test4Net.UI.POM.Page.Interfaces;
using Test4Net.UITest.Models;

namespace Test4Net.QACode.Testing.UI;

[TestClass]
public abstract class BaseUiTest : AbstractUiTest
{
    /// <summary>
    /// Path to folder with configurations
    /// </summary>
    protected static readonly string SetupPath = Path.Join(Directory.GetCurrentDirectory(), "/Configuration");

    protected static string DriverSettings { get; } = File.ReadAllText(Path.Combine(SetupPath, "driver.settings.json"));

    protected readonly IConfiguration Configuration;
    
    /// <summary>
    /// Init configuration from json settings
    /// </summary>
    protected BaseUiTest() 
    {
        Environment.SetEnvironmentVariable(Conventions.EnvironmentVariableName.BrowserProfile.ToString(), "Chrome");
        
        InitFactories(Environment.GetEnvironmentVariable(
            Conventions.EnvironmentVariableName.BrowserProfile.ToString()), DriverSettings);

        Configuration = new ConfigurationBuilder()
            .SetBasePath(SetupPath)
                .AddEnvironmentVariables()
                .AddJsonFile($"{Environment.GetEnvironmentVariable(Conventions.Env.Qa.ToString())}.settings.json",
                    optional: true, reloadOnChange: true).Build();

        LogProvider = ConfigureLogger(Configuration.GetSection("Logging"));
    }

    /// <summary>
    /// Define validate rules <see cref="ValidationRules"/> for a page
    /// </summary>
    /// <param name="page">target page to add rules</param>
    /// <returns>true if rules validation is success</returns>
    protected override bool DefineDefaultPageRules(IPage page)
    {
        page.Rules.AddPlatformRule(ExecContext.Configuration.Platform)
            .AddBrowserRule(ExecContext.Configuration.Browser)
            .AddViewPortRule(ExecContext.Configuration.ViewPort);

        return page.Rules.Validate(faults => faults.NegotiateDefaultRules(ExecContext.Configuration));
    }
}