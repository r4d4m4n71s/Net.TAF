using FluentAssertions;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Test4Net.Logging.Provider;
using Test4Net.Test.Interfaces;
using Test4Net.UI.Elements;

namespace Test4Net.QACode.MSTest;

[TestClass]
public class MsTestAdapterTest : BaseTest
{
    public TestContext TestContext { get; set; }


    /// <inheritdoc />
    public MsTestAdapterTest()
    {
    }

    [TestMethod]
    public void LoggerOverriding_Log4Net_Ok()
    {
        // Log 4 net approach example
        ConfigureLogger(builder =>
            builder.Decorate(Configuration.GetSection("Logging"))
                .AddLog4Net(Path.Combine(Configuration["ConfigurationPath"], "log4net.config")));
        
        new Action(() => Log.Info("xxx")).Should().NotThrow();
    }
}

