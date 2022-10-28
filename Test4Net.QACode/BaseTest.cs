using Microsoft.Extensions.Configuration;
using Test4Net.Test;

namespace Test4Net.QACode;

[TestClass]
public abstract class BaseTest : AbstractTest
{
    /// <summary>
    /// Path to folder with configurations
    /// </summary>
    protected static readonly string SetupPath = Path.Join(Directory.GetCurrentDirectory(), "/Configuration");

    /// <summary>
    /// Init configuration from json settings
    /// </summary>
    protected BaseTest() : base(configuration => configuration
        .SetBasePath(SetupPath)
        .AddEnvironmentVariables()
        .AddJsonFile($"{Environment.GetEnvironmentVariable(Conventions.Env.Qa.ToString())}.settings.json", 
            optional: true, reloadOnChange: true))
    {
        
        Environment.SetEnvironmentVariable(Conventions.GlobalKeyVariables.AppEnvironment.ToString(), Conventions.Env.Qa.ToString());
        LogProvider = ConfigureLogger(Configuration.GetSection("Logging"));
    }
}