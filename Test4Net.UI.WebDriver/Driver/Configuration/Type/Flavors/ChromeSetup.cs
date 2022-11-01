using OpenQA.Selenium.Chrome;
using Test4Net.UI.WebDriver.Driver.Configuration.Models;
using Test4Net.UI.WebDriver.Util;

namespace Test4Net.UI.WebDriver.Driver.Configuration.Type.Flavors;

/// <summary>
/// Local chrome driver setup
/// </summary>
public class ChromeSetup : AbstractSetup<ChromeOptions>
{
    /// <summary>
    /// Creates setup from a options set
    /// <see cref="ChromiumBuilder{T}"/>
    /// </summary>
    /// <param name="setup">Setup as dic</param>
    public ChromeSetup(IDictionary<string, object> setup) : base(new ChromiumBuilder<ChromeOptions>(setup))
    { }

    /// <summary>
    /// Creates setup from driverOptions
    /// </summary>
    /// <param name="options"></param>
    public ChromeSetup(ChromeOptions options) : base(new ChromiumBuilder<ChromeOptions>(options))
    { }

    /// <summary>
    /// Creates setup from dic and driverOptions
    /// </summary>
    /// <param name="setup"></param>
    /// <param name="options"></param>
    public ChromeSetup(IDictionary<string, object> setup, ChromeOptions options) : 
        base(new ChromiumBuilder<ChromeOptions>(setup, options))
    { }

    /// <inheritdoc/>
    public override ChromeDriver Create() => string.IsNullOrEmpty(Builder.Options.AsDic()["driverPath"].ToString()) ?
        new ChromeDriver(Builder.Options) :
        new ChromeDriver(Builder.Options.AsDic()["driverPath"].ToString(), Builder.Options);
}