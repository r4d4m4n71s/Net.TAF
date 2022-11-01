using OpenQA.Selenium;

namespace Test4Net.UI.WebDriver.Driver.Interfaces;

/// <summary>
/// Driver setup contract
/// </summary>
public interface IDriverSetup 
{
    /// <summary>
    /// Creates a driver instance
    /// </summary>
    /// <returns>Driver instance</returns>
    IWebDriver Create();

    /// <summary>
    /// Before driver instance, apply features
    /// </summary>
    void ApplyPreConstructionFeatures();

    /// <summary>
    /// After driver instance, apply features
    /// </summary>
    /// <param name="driver">Target</param>
    void ApplyPostConstructionFeatures(IWebDriver driver);
}