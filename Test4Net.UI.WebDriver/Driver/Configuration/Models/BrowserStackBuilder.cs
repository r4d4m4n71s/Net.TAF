using OpenQA.Selenium;

namespace Test4Net.UI.WebDriver.Driver.Configuration.Models;

/// <inheritdoc/>
public class BrowserStackBuilder<T> : AbstractBuilder<T> where T : DriverOptions, new()
{
    private readonly Uri _browserStackServiceUrl = new("https://hub.browserstack.com/wd/hub/");

    /// <inheritdoc/>
    public BrowserStackBuilder(IDictionary<string, object> optionsAsDic) : base(optionsAsDic)
    {
        if(!OptionsAsDic.ContainsKey("serviceUrl"))
            OptionsAsDic.Add("serviceUrl", _browserStackServiceUrl);

        SetBrowserStackOptions();
    }

    /// <inheritdoc/>
    public BrowserStackBuilder(DriverOptions options) : base(options) =>
        Options.AddAdditionalOption("serviceUrl", _browserStackServiceUrl);

    /// <inheritdoc/>
    public BrowserStackBuilder(IDictionary<string, object> optionsAsDic, DriverOptions options) : base(optionsAsDic, options) 
    {
        if (!OptionsAsDic.ContainsKey("serviceUrl"))
            OptionsAsDic.Add("serviceUrl", _browserStackServiceUrl);

        SetBrowserStackOptions();
    }
        
    /// <summary>
    /// Get browser stack options from dictionary and set as additional option set
    /// </summary>
    private void SetBrowserStackOptions(string optionsKey = "bstack:options")
    {
        if (!OptionsAsDic.ContainsKey(optionsKey))
            throw new ArgumentException($"{optionsKey} is required");
        /*
        var options = OptionsAsDicAsDic[optionsKey].JsonToDic();

        // browserName raises a OS/Browser combination invalid
        options.Remove("browserName");

        Options.AddAdditionalOption(optionsKey, options);*/
    }
}