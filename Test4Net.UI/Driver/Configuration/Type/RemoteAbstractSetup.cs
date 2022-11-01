using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Test4Net.UI.Driver.Interfaces;
using Test4Net.UI.Util;

namespace Test4Net.UI.Driver.Configuration.Type;

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