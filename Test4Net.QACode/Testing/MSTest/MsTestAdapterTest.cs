using Microsoft.Extensions.Logging;
using Test4Net.Logging.Provider;

namespace Test4Net.QACode.Testing.MSTest;

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
        Log.Info("xxx");
        //new Action(() => Log.Info("xxx")).Should().NotThrow();
    }
}

