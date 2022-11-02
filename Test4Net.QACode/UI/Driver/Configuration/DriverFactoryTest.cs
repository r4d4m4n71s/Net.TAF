using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Test4Net.UI.WebBrowser.Driver;
using Test4Net.UI.WebBrowser.Driver.Configuration.Type.Flavors;
using Test4Net.UI.WebBrowser.Util;

namespace Test4Net.QACode.UI.Driver.Configuration;

[TestClass]
public class DriverFactoryTest : BaseUiTest
{
    [TestMethod]
    [TestCategory("integration")]
    public void CreateChromeLocal_FromConfiguration_Ok() =>
        new DriverFactory( DriverSettings.CreateOptionsStructure("Chrome")).Get("Chrome").Dispose();

    [TestMethod]
    [TestCategory("integration")]
    public void CreateEdgeLocal_FromConfiguration_Ok() =>
        new DriverFactory(DriverSettings).Get<EdgeDriver>("Edge").Dispose();

    [TestMethod]
    [TestCategory("integration")]
    public void CreateFireFoxLocal_FromConfiguration_Ok() =>
        new DriverFactory(DriverSettings).Get<FirefoxDriver>("FireFox").Dispose();
    
    [TestMethod]
    [TestCategory("integration")]
    public void CreateBrowserStackChrome_Ok()
    {
        var options = new ChromeOptions();
        options.AddAdditionalOption("bstack:options", new Dictionary<string, object>
        {
            { "os", "Windows" },
            {"osVersion", "11"},
            {"userName", "johnfranco_xcUFk8"},
            {"accessKey", "cc6mavEwvsW16mFEPNze"}
        });
        options.AddAdditionalOption("xxxx","xxxxx");
        var x = options.ToCapabilities();    
        var setup = new BrowserStackChromeSetup(options);
        var instance = new DriverFactory("BSWin11Chrome", setup).Get("BSWin11Chrome");
        instance.Dispose();
    }

    [TestMethod]
    [TestCategory("integration")]
    public void CreateBrowserStackChrome_FromConfiguration_Ok() =>
        new DriverFactory(DriverSettings.CreateOptionsStructure("BSWin11Chrome")).Get<RemoteWebDriver>("BSWin11Chrome").Dispose();

    [TestMethod]
    [TestCategory("integration")]
    public void CreateBrowserStackEdge_FromConfiguration_Ok() =>
        new DriverFactory(DriverSettings.CreateOptionsStructure("BSWin11Edge")).Get("BSWin11Edge").Dispose();
}