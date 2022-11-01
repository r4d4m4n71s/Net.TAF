using System.Collections;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Test4Net.Test.Interfaces;
using Test4Net.Test.Models;
using Test4Net.UITest;
using Test4Net.UITest.Interfaces;
using Test4Net.Util.Json;

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