using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Test4Net.UI.WebDriver.Driver.Interfaces;
using Test4Net.UI.WebDriver.Util;

namespace Test4Net.UI.WebDriver.Driver.Configuration.Type;

/// <summary>
/// Remote abstract setup
/// </summary>
public abstract class RemoteAbstractSetup<T> : AbstractSetup <T> where T : DriverOptions
{
    /// <inheritdoc/>
    protected RemoteAbstractSetup(IOptionsBuilder<T> builder) : base(builder)
    { }

    /// <inheritdoc/>
    public override RemoteWebDriver Create() => 
        new(new Uri(Builder.Options.AsDic()["serviceUrl"].ToString()!), Builder.Options);
}