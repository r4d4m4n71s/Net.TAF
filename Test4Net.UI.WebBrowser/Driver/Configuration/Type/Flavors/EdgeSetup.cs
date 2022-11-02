using OpenQA.Selenium.Edge;
using Test4Net.UI.WebBrowser.Driver.Configuration.Models;
using Test4Net.UI.WebBrowser.Util;

namespace Test4Net.UI.WebBrowser.Driver.Configuration.Type.Flavors;

/// <summary>
/// Local edge driver setup
/// </summary>
public class EdgeSetup : AbstractSetup<EdgeOptions>
{
    /// <summary>
    /// Creates setup from a options set
    /// <see cref="ChromiumBuilder{T}"/>
    /// </summary>
    /// <param name="setup">Setup as dic</param>
    public EdgeSetup(IDictionary<string, object> setup) : base(new ChromiumBuilder<EdgeOptions>(setup))
    { }

    /// <summary>
    /// Creates setup from driverOptions
    /// </summary>
    /// <param name="options"></param>
    public EdgeSetup(EdgeOptions options) : base(new ChromiumBuilder<EdgeOptions>(options))
    { }

    /// <summary>
    /// Creates setup from dic and driverOptions
    /// </summary>
    /// <param name="setup"></param>
    /// <param name="options"></param>
    public EdgeSetup(IDictionary<string, object> setup, EdgeOptions options) :
        base(new ChromiumBuilder<EdgeOptions>(setup, options))
    { }

    /// <inheritdoc/>
    public override EdgeDriver Create() => string.IsNullOrEmpty(Builder.Options.AsDic()["driverPath"].ToString()) ?
        new EdgeDriver(Builder.Options) :
        new EdgeDriver(Builder.Options.AsDic()["driverPath"].ToString(), Builder.Options);
}