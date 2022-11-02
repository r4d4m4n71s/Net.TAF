using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using Test4Net.UI.WebBrowser.Driver.Configuration.Models;
using Test4Net.UI.WebBrowser.Util;
using Test4Net.Util.Json;

namespace Test4Net.QACode.UI.Driver.Configuration.Models;

[TestClass]
public class ModelsTest
{
    [TestMethod]
    [TestCategory("unit")]
    public void Create_ChromeOptionsBuilder()
    {
        var options = new ChromeOptions
        {
            AcceptInsecureCertificates = true,
            AndroidOptions = new ChromiumAndroidOptions("AndroidPackage") { AndroidActivity = "activity", AndroidProcess = "any" },
            BinaryLocation = "c:/someLocation/driver.exe",
            BrowserVersion = "browser version",
            DebuggerAddress = "some ip add",
            LeaveBrowserRunning = true,
            MinidumpPath = "_minidumpPath",
            PerformanceLoggingPreferences = new ChromiumPerformanceLoggingPreferences
            {
                BufferUsageReportingInterval = TimeSpan.FromSeconds(5),
                IsCollectingNetworkEvents = true,
                IsCollectingPageEvents = true
            },
            PageLoadStrategy = PageLoadStrategy.Eager,
            PlatformName = "Windows",
            UseStrictFileInteractability = true,
            UseWebSocketUrl = true,
            //UseWebView = true, //availaable just for edge
            UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
            Proxy = new Proxy { IsAutoDetect = false, HttpProxy = "127.0.0.1:8888" }
        };
        options.AddArguments("--arg1", "--arg2", "--arg3");
        options.AddAdditionalOption("xxxxx", "xxxxx");

        //options.EnableMobileEmulation("Device name");
        options.EnableMobileEmulation(new ChromiumMobileEmulationDeviceSettings
        {
            UserAgent = "User agent name",
            PixelRatio = 10.15,
            EnableTouchEvents = true,
            Height = 200,
            Width = 200
        });

        //using (ShimsContext.Create())
        //{
            //System.IO.Fakes.ShimFile.ExistsString = s => true;
            //System.IO.Fakes.ShimFile.ReadAllBytesString = s => Array.Empty<byte>();
            //options.AddExtensions("ext1", "ext2", "etc");
            options.AddEncodedExtensions(Convert.ToBase64String(new byte[] { }).ToString());
            options.AddUserProfilePreference("preference", 123);
            options.AddWindowTypes("winT1");
            var optionsAsString = options.ToString();
            var builder = new ChromiumBuilder<ChromeOptions>(options);
            AssertDriverOptions(builder.Options);
            AssertChromiumOptions(builder.Options);
        //}
    }

    [TestMethod]
    [TestCategory("unit")]
    public void CreateChromeOptionsFromBuilderDic_Ok()
    {
        var optionsAsDic = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),
            "TestData/Drivers/GeneralSettingsExtructure.json")).CreateOptionsStructure("chrome_1");

        var options = new ChromiumBuilder<ChromeOptions>(optionsAsDic).Build().Options;
        AssertDriverOptions(options);
        AssertChromiumOptions(options);
    }

    [TestMethod]
    [TestCategory("unit")]
    public void Create_EdgeOptionsBuilder()
    {
        var options = new EdgeOptions
        {
            BinaryLocation = "c:/someLocation/driver.exe",
            AcceptInsecureCertificates = true,
            AndroidOptions = new ChromiumAndroidOptions("AndroidPackage") { AndroidActivity = "activity" },
            BrowserVersion = "browser version",
            DebuggerAddress = "some ip add",
            LeaveBrowserRunning = true,
            MinidumpPath = "_minidumpPath",
            PerformanceLoggingPreferences = new ChromiumPerformanceLoggingPreferences { BufferUsageReportingInterval = TimeSpan.FromSeconds(5) },
            PageLoadStrategy = PageLoadStrategy.Eager,
            PlatformName = "Windows",
            UseStrictFileInteractability = true,
            UseWebSocketUrl = true,
            UseWebView = true,
            UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
            Proxy = new Proxy(new Dictionary<string, object> { { "x1", "xR" } })
        };
        options.AddArguments("--arg1", "--arg2", "--arg3");
        options.EnableMobileEmulation("Device name");
        options.AddAdditionalOption("xxxxx", "xxxxx");

        //using (ShimsContext.Create())
        //{
            //System.IO.Fakes.ShimFile.ExistsString = s => true;
            //System.IO.Fakes.ShimFile.ReadAllBytesString = s => Array.Empty<byte>();
            //options.AddExtensions("ext1", "ext2", "etc");
            var optionsAsString = options.ToString();
            var builder = new ChromiumBuilder<EdgeOptions>(options);
            AssertDriverOptions(builder.Options);
            AssertChromiumOptions(builder.Options);
        //}
    }

    [TestMethod]
    [TestCategory("unit")]
    public void CreateEdgeOptionsFromBuilderDic_Ok()
    {
        var optionsAsDic = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),
            "TestData/Drivers/GeneralSettingsExtructure.json")).CreateOptionsStructure("edge_2");

        var extraOptions = new EdgeOptions
        {
            UseWebView = true
        };

        var options = new ChromiumBuilder<EdgeOptions>(optionsAsDic, extraOptions).Build().Options;
        options.UseWebView.Should().BeTrue();
        AssertDriverOptions(options);
        AssertChromiumOptions(options);
    }

    [TestMethod]
    [TestCategory("unit")]
    public void Create_FireFoxOptionsBuilder()
    {
        var options = new FirefoxOptions
        {
            // BinaryLocation = "c:/someLocation/driver.exe",
            BrowserExecutableLocation = "c:/someLocation/driver.exe",
            AcceptInsecureCertificates = true,
            AndroidOptions = new FirefoxAndroidOptions("AndroidPackage") { AndroidActivity = "activity" },
            BrowserVersion = "browser version",
            // DebuggerAddress = "some ip add",
            // LeaveBrowserRunning = true,
            // MinidumpPath = "MinidumpPath",
            // PerformanceLoggingPreferences = new ChromiumPerformanceLoggingPreferences { BufferUsageReportingInterval = TimeSpan.FromSeconds(5) },
            PageLoadStrategy = PageLoadStrategy.Eager,
            PlatformName = "Windows",
            UseStrictFileInteractability = true,
            UseWebSocketUrl = true,
            // UseWebView = true,
            UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
            Proxy = new Proxy(new Dictionary<string, object> { { "x1", "xR" } })
        };
        options.AddArguments("--arg1", "--arg2", "--arg3");
        options.AddAdditionalOption("xxxxx", "xxxxx");
        // options.EnableMobileEmulation("Device name");

        //using (ShimsContext.Create())
        //{
            //System.IO.Fakes.ShimFile.ExistsString = s => true;
            //System.IO.Fakes.ShimFile.ReadAllBytesString = s => Array.Empty<byte>();
            // options.AddExtensions("ext1", "ext2", "etc");
            var optionsAsString = options.ToString();
            var builder = new FirefoxBuilder(options);
            AssertDriverOptions(builder.Options);
        //}
    }


    [TestMethod]
    [TestCategory("unit")]
    public void CreateFirefoxOptionsFromBuilderDic_Ok()
    {
        var optionsAsDic = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),
            "TestData/Drivers/GeneralSettingsExtructure.json")).CreateOptionsStructure("firefox_3");

        var extraOptions = new FirefoxOptions
        {
            EnableDevToolsProtocol = true
        };

        var options = new FirefoxBuilder(optionsAsDic, extraOptions).Build().Options;
        options.EnableDevToolsProtocol.Should().BeTrue();
        //options.AsDic()["prefs"].ToString().Should().BeEquivalentTo("123");
        AssertDriverOptions(options);
       
    }

    [TestMethod]
    [TestCategory("unit")]
    public void CreateSafariOptionsFromBuilderDic_Ok()
    {
        var optionsAsDic = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),
           "TestData/Drivers/GeneralSettingsExtructure.json")).CreateOptionsStructure("safari_4");

        var options = new SafariBuilder(optionsAsDic).Build().Options;
        AssertDriverOptions(options);

        options.BrowserName.Should().NotBeEmpty();
        options.EnableAutomaticInspection.Should().BeTrue();
        options.EnableAutomaticProfiling.Should().BeTrue();
    }

    [TestMethod]
    [TestCategory("unit")]
    public void Create_SafariOptionsBuilder()
    {
        var options = new SafariOptions
        {
            // BinaryLocation = "c:/someLocation/driver.exe",
            AcceptInsecureCertificates = true,
            // AndroidOptions = new ChromiumAndroidOptions("AndroidPackage") { AndroidActivity = "activity" },
            BrowserVersion = "browser version",
            // DebuggerAddress = "some ip add",
            // LeaveBrowserRunning = true,
            // MinidumpPath = "MinidumpPath",
            // PerformanceLoggingPreferences = new ChromiumPerformanceLoggingPreferences { BufferUsageReportingInterval = TimeSpan.FromSeconds(5) },
            PageLoadStrategy = PageLoadStrategy.Eager,
            PlatformName = "Windows",
            UseStrictFileInteractability = true,
            UseWebSocketUrl = true,
            // UseWebView = true,
            UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
            Proxy = new Proxy(new Dictionary<string, object> { { "x1", "xR" } }),

            // BrowserName = "Browser Name",
            EnableAutomaticInspection = true,
            EnableAutomaticProfiling = true
        };
        // options.AddArguments("--arg1", "--arg2", "--arg3");
        //options.EnableMobileEmulation("Device name");
        options.AddAdditionalOption("xxxxx", "xxxxx");
        var optionsAsString = options.ToString();

        var builder = new SafariBuilder(options);
        AssertDriverOptions(builder.Options);

        builder.Options.BrowserName.Should().NotBeEmpty();
        builder.Options.EnableAutomaticInspection.Should().BeTrue();
        builder.Options.EnableAutomaticProfiling.Should().BeTrue();
    }

    [TestMethod]
    [TestCategory("unit")]
    public void Create_BrowserStackOptionsModel_FromJson_Ok()
    {
        var optionsAsDic = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),
           "TestData/Drivers/GeneralSettingsExtructure.json")).CreateOptionsStructure("bsWin11Chrome");

        optionsAsDic.Should().NotBeNull();
        optionsAsDic.Should().NotBeEmpty();
        optionsAsDic.Should().ContainKey("serviceUrl");

        optionsAsDic.Should().ContainKey("bstack:options");
        optionsAsDic["bstack:options"].JsonToDic().Should().ContainKey("resolution");

        var options = new BrowserStackBuilder<ChromeOptions>(optionsAsDic).Build().Options;
        AssertDriverOptions(options);
        AssertChromiumOptions(options);
    }

    /// <summary>
    /// Assert driver options
    /// Commented not covered options
    /// </summary>
    /// <param name="options"></param>
    private static void AssertDriverOptions(DriverOptions options)
    {
        using (new AssertionScope())
        {
            options.Should().NotBeNull();
            // options.Arguments.Should().NotBeEmpty();
            options.AcceptInsecureCertificates.Should().BeTrue();
            // options.AndroidOptions.Should().NotBeNull();
            // options.BinaryLocation.Should().NotBeEmpty();
            options.BrowserVersion.Should().NotBeEmpty();
            // options.DebuggerAddress.Should().NotBeEmpty();
            // options.Extensions.Should().NotBeEmpty();
            // options.LeaveBrowserRunning.Should().BeTrue();
            // options.MinidumpPath.Should().NotBeEmpty();
            // options.PerformanceLoggingPreferences.Should().NotBeNull();
            options.PageLoadStrategy.Should().Be(PageLoadStrategy.Eager);
            options.PlatformName.Should().NotBeEmpty();
            options.UseStrictFileInteractability.Should().BeTrue();
            options.UseWebSocketUrl.Should().BeTrue();
            options.UnhandledPromptBehavior.Should().Be(UnhandledPromptBehavior.Accept);
            options.Proxy.Should().NotBeNull();
            options.AsDic().Should().ContainKey("xxxxx");
        }
    }

    /// <summary>
    /// Assert Chromium options
    /// commented options alredy covered by <see cref="AssertDriverOptions"/>
    /// </summary>
    /// <param name="options"></param>
    private static void AssertChromiumOptions(ChromiumOptions options)
    {
        using (new AssertionScope())
        {
            // options.Should().NotBeNull();
            options.Arguments.Should().NotBeEmpty();
            // options.AcceptInsecureCertificates.Should().BeTrue();
            options.AndroidOptions.Should().NotBeNull();
            options.BinaryLocation.Should().NotBeEmpty();
            // options.BrowserVersion.Should().NotBeEmpty();
            options.DebuggerAddress.Should().NotBeEmpty();
            // options.Extensions.Should().NotBeEmpty();
            options.LeaveBrowserRunning.Should().BeTrue();
            options.MinidumpPath.Should().BeEquivalentTo("_minidumpPath");
            options.PerformanceLoggingPreferences.Should().NotBeNull();
            // options.PageLoadStrategy.Should().Be(PageLoadStrategy.Eager);
            // options.PlatformName.Should().NotBeEmpty();
            // options.UseStrictFileInteractability.Should().BeTrue();
            // options.UseWebSocketUrl.Should().BeTrue();
            // options.UnhandledPromptBehavior.Should().Be(UnhandledPromptBehavior.Accept);
            // options.Proxy.Should().NotBeNull();        
        }
    }
}