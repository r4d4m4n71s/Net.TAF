using OpenQA.Selenium;

namespace Test4Net.UI.Driver.Interfaces;

/// <summary>
/// Driver options builder
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOptionsBuilder<out T> where T : DriverOptions
{
    /// <summary>
    /// Apply pre-construction logic
    /// </summary>
    IOptionsBuilder<T> Build();

    /// <summary>
    /// Apply post-construction logic
    /// </summary>
    /// <param name="driver"></param>
    void PerformPostConstructionSetup(IWebDriver driver);

    /// <summary>
    /// Driver options from setup parameters
    /// </summary>
    T Options { get; }
}