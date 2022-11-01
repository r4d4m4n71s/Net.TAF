using OpenQA.Selenium.Chromium;

namespace Test4Net.UI.WebDriver.Driver.Configuration.Models;

/// <inheritdoc cref="AbstractBuilder{T}"/>
/// <seealso url="https://learn.microsoft.com/en-us/microsoft-edge/webdriver-chromium/capabilities-edge-options"/>
/// <seealso url="https://chromedriver.chromium.org/capabilities"/>
public class ChromiumBuilder<T> : AbstractBuilder<T> where T : ChromiumOptions, new()
{
    #region Attributes
    private const string DriverOptionsKey = "DriverOptions";
    #endregion

    #region Constructors
    /// <inheritdoc />
    public ChromiumBuilder(IDictionary<string, object> optionsAsDic) : base(optionsAsDic)
    { }

    /// <inheritdoc />
    public ChromiumBuilder(ChromiumOptions options) : base(options)
    { }

    /// <inheritdoc />
    public ChromiumBuilder(IDictionary<string, object> optionsAsDic, ChromiumOptions options) : base(optionsAsDic, options)
    { }

    #endregion

    #region Overwritten Methods
   
    #endregion
}