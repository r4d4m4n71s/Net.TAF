using OpenQA.Selenium.Firefox;
using Test4Net.UI.WebDriver.Driver.Configuration.Models;
using Test4Net.UI.WebDriver.Util;

namespace Test4Net.UI.WebDriver.Driver.Configuration.Type.Flavors;

/// <summary>
/// Local firefox driver setup
/// </summary>
public class FirefoxSetup : AbstractSetup<FirefoxOptions>
{
    /// <summary>
    /// Creates setup from a options set
    /// <see cref="FirefoxBuilder"/>
    /// </summary>
    /// <param name="setup">Setup as dic</param>
    public FirefoxSetup(IDictionary<string, object> setup) : base(new FirefoxBuilder(setup))
    { }

    /// <summary>
    /// Creates setup from driverOptions
    /// </summary>
    /// <param name="options"></param>
    public FirefoxSetup(FirefoxOptions options) : base(new FirefoxBuilder(options))
    { }

    /// <summary>
    /// Creates setup from dic and driverOptions
    /// </summary>
    /// <param name="setup"></param>
    /// <param name="options"></param>
    public FirefoxSetup(IDictionary<string, object> setup, FirefoxOptions options) :
        base(new FirefoxBuilder(setup, options))
    { }

    /// <inheritdoc/>
    public override FirefoxDriver Create() => string.IsNullOrEmpty(Builder.Options.AsDic()["driverPath"].ToString()) ?
        new FirefoxDriver(Builder.Options) :
        new FirefoxDriver(Builder.Options.AsDic()["driverPath"].ToString(), Builder.Options);
}