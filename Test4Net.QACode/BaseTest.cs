using Microsoft.Extensions.Configuration;
using Test4Net.Test.Models;

namespace Test4Net.QACode;

[TestClass]
public abstract class BaseTest : AbstractTest
{
    /// <summary>
    /// Path to folder with configurations
    /// </summary>
    protected static readonly string SetupPath = Path.Join(Directory.GetCurrentDirectory(), "/Configuration");

    protected readonly IConfiguration Configuration;

    /// <summary>
    /// Init configuration from json settings
    /// </summary>
    protected BaseTest()
    {
        Environment.SetEnvironmentVariable(Conventions.EnvironmentVariableName.AppEnv.ToString(), Conventions.Env.Qa.ToString());
        
        Configuration = new ConfigurationBuilder()
            .SetBasePath(SetupPath)
            .AddEnvironmentVariables()
            .AddJsonFile($"{Environment.GetEnvironmentVariable(Conventions.Env.Qa.ToString())}.settings.json",
                optional: true, reloadOnChange: true).Build();
        LogProvider = ConfigureLogger(Configuration.GetSection("Logging"));
    }
}