using OpenQA.Selenium;
using Test4Net.UI.Driver.Interfaces;

namespace Test4Net.UI.Driver.Configuration.Type;

/// <summary>
/// Common setup 
/// </summary>
public abstract class AbstractSetup<T> : IDriverSetup where T : DriverOptions
{
    /// <summary>
    /// Setup builder
    /// </summary>
    protected IOptionsBuilder<T> Builder { get; }

    /// <summary>
    /// Creates a setup from T builder
    /// </summary>
    /// <param name="builder"></param>
    protected AbstractSetup(IOptionsBuilder<T> builder) => 
        Builder = builder;
        
    /// <inheritdoc cref="IDriverSetup"/>
    public abstract IWebDriver Create();

    /// <inheritdoc cref="IDriverSetup.ApplyPreConstructionFeatures"/>
    public virtual void ApplyPreConstructionFeatures() =>
        Builder.Build();

    /// <inheritdoc cref="IDriverSetup.ApplyPostConstructionFeatures"/>
    public void ApplyPostConstructionFeatures(IWebDriver driver) =>
        Builder.PerformPostConstructionSetup(driver);
}