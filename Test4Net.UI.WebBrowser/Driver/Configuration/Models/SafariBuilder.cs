using OpenQA.Selenium.Safari;

namespace Test4Net.UI.WebBrowser.Driver.Configuration.Models;

/// <inheritdoc cref="AbstractBuilder{T}"/>
public class SafariBuilder : AbstractBuilder<SafariOptions>
{
    #region Constructors

    /// <summary>
    /// Creates options model from dictionary
    /// </summary>
    /// <param name="optionsAsDic</param>
    public SafariBuilder(IDictionary<string, object> optionsAsDic) : base(optionsAsDic)
    { }

    /// <summary>
    /// Creates options model from firefox options
    /// </summary>
    /// <param name="options"></param>
    public SafariBuilder(SafariOptions options) : base(options)
    { }

    /// <summary>
    /// Creates options model from setup dic and firefox options
    /// </summary>
    /// <param name="optionsAsDic"></param>
    /// <param name="options"></param>
    public SafariBuilder(IDictionary<string, object> optionsAsDic, SafariOptions options) : base(optionsAsDic, options)
    { }
    #endregion

    #region Public Methods

    #endregion

    #region Overwritten Methods
    #endregion
}