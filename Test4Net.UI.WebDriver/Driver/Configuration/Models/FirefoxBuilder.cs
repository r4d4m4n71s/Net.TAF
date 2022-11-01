using OpenQA.Selenium.Firefox;

namespace Test4Net.UI.WebDriver.Driver.Configuration.Models;

/// <inheritdoc cref="AbstractBuilder{T}"/>
/// <seealso url="https://developer.mozilla.org/en-US/docs/Web/WebDriver/Capabilities/firefoxOptions"/>
public class FirefoxBuilder : AbstractBuilder<FirefoxOptions>
{
    #region Attributes
    private const string DriverOptionsKey = "DriverOptions";
    #endregion

    #region Constructors
    /// <inheritdoc />
    public FirefoxBuilder(IDictionary<string, object> optionsAsDic) : base(optionsAsDic)
    { }

    /// <inheritdoc />
    public FirefoxBuilder(FirefoxOptions options) : base(options)
    { }

    /// <inheritdoc />
    public FirefoxBuilder(IDictionary<string, object> optionsAsDic, FirefoxOptions options) : base(optionsAsDic, options)
    { }
    #endregion

    #region Public Methods

    #endregion

    #region Overwritten Methods

    #endregion
}