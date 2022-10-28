using Microsoft.Extensions.Configuration;
using Test4Net.UI.Driver;
using Test4Net.UI.Page;
using Test4Net.UITest;

namespace Test4Net.QACode.UI;

[TestClass]
public abstract class BaseUiTest : AbstractUiTest
{
    protected string DriverSettings { get; }

    /// <summary>
    /// Path to folder with configurations
    /// </summary>
    protected static readonly string SetupPath = Path.Join(Directory.GetCurrentDirectory(), "/Configuration");

    /// <summary>
    /// Init configuration from json settings
    /// </summary>
    protected BaseUiTest() : base(configuration => configuration
        .SetBasePath(SetupPath)
        .AddEnvironmentVariables()
        .AddJsonFile($"{Environment.GetEnvironmentVariable(Conventions.Env.Qa.ToString())}.settings.json",
            optional: true, reloadOnChange: true))
    {
        Environment.SetEnvironmentVariable(Conventions.GlobalKeyVariables.AppEnvironment.ToString(), Conventions.Env.Qa.ToString());
        LogProvider = ConfigureLogger(Configuration.GetSection("Logging"));
        PageFactory = new PageFactory(LogProvider);
        DriverSettings = File.ReadAllText(Path.Combine(SetupPath, "driver.settings.json"));
        DriverFactory = new DriverFactory(DriverSettings);
    }
}