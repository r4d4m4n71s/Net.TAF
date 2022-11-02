using System.Drawing;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Test4Net.UI.WebBrowser.Driver.Interfaces;
using Test4Net.UI.WebBrowser.Util;
using Test4Net.Util.Json;

namespace Test4Net.UI.WebBrowser.Driver.Configuration.Models;

/// <inheritdoc cref="IOptionsBuilder{T}"/>
public abstract class AbstractBuilder<T> : IOptionsBuilder<T> where T : DriverOptions, new()
{
    #region Attributes

    /// <summary>
    /// Options from external configuration
    /// keep separated from main options in case of rebuilding reassign them up
    /// </summary>
    private readonly T _extraOptions;

    /// <summary>
    /// <inheritdoc cref="IOptionsBuilder{T}"/>
    /// Driver options <see href="https://www.selenium.dev/documentation/webdriver/drivers/options/">about</see>
    /// </summary>
    public T Options { get; protected set; }

    /// <summary>
    /// Options as dictionary
    /// </summary>
    protected IDictionary<string, object> OptionsAsDic { get; }

    #endregion

    #region Constructors
    /// <summary>
    /// Creates options model from dictionary
    /// </summary>
    /// <param name="optionsAsDic"></param>
    protected AbstractBuilder(IDictionary<string, object> optionsAsDic) => OptionsAsDic = optionsAsDic;

    /// <summary>
    /// Creates options model from driver options
    /// </summary>
    /// <param name="options"></param>
    protected AbstractBuilder(DriverOptions options)
    {
        Options = (T)options;
        OptionsAsDic = new Dictionary<string, object>();
    }

    /// <summary>
    /// Creates options model from driver options
    /// and dictionary prioritizing this as first
    /// </summary>
    /// <param name="optionsAsDic"></param>
    /// <param name="options"></param>
    protected AbstractBuilder(IDictionary<string, object> optionsAsDic, DriverOptions options)
    {
        _extraOptions = (T)options;
        OptionsAsDic = optionsAsDic;
    }
    #endregion

    #region Public Methods

    /// <inheritdoc cref="IOptionsBuilder{T}.Build"/>
    public IOptionsBuilder<T> Build()
    {
        if(OptionsAsDic.Any())
            Options = (_extraOptions ?? new T()).BuildFromDictionary(OptionsAsDic);
        return this;
    }
    
    /// <inheritdoc cref="IOptionsBuilder{T}.PerformPostConstructionSetup"/>
    public virtual void PerformPostConstructionSetup(IWebDriver driver)
    {
        SetTimeouts(driver);
        SetViewPort(driver);
    }
    #endregion

    #region Virtual Methods   


    #region Post construction features
    /// <summary>
    /// Set timeout
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="timeoutsKey"></param>
    protected virtual void SetTimeouts(IWebDriver driver, string timeoutsKey = "timeouts")
    {
        /*if (SetupAsDic.ContainsKey(timeoutsKey))
        {
            var timeoutsFromDic = SetupAsDic[timeoutsKey].ToDictionary();
            var timeouts = driver.Manage().Timeouts();
            timeouts.AsynchronousJavaScript = TimeSpan.FromMilliseconds(Convert.ToDouble(timeoutsFromDic["AsynchronousJavaScript"]));
            timeouts.ImplicitWait = TimeSpan.FromMilliseconds(Convert.ToDouble(timeoutsFromDic["ImplicitWait"]));
            timeouts.PageLoad = TimeSpan.FromMilliseconds(Convert.ToDouble(timeoutsFromDic["PageLoad"]));
        }*/

        if (Options.AsDic().ContainsKey(timeoutsKey))
        {
            var timeoutsFromDic = Options.AsDic()[timeoutsKey].JsonToDic();
            var timeouts = driver.Manage().Timeouts();
            timeouts.AsynchronousJavaScript = TimeSpan.FromMilliseconds(Convert.ToDouble(timeoutsFromDic["AsynchronousJavaScript"]));
            timeouts.ImplicitWait = TimeSpan.FromMilliseconds(Convert.ToDouble(timeoutsFromDic["ImplicitWait"]));
            timeouts.PageLoad = TimeSpan.FromMilliseconds(Convert.ToDouble(timeoutsFromDic["PageLoad"]));
        }
    }

    /// <summary>
    /// Resizes browser from view port configuration
    /// updates conf dictionary
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="viewPortKey"></param>
    protected virtual void SetViewPort(IWebDriver driver, string viewPortKey = "viewPort")
    {
        /*if (!SetupAsDic.ContainsKey(viewPortKey))
        {
            var viewPortFromDic = SetupAsDic[viewPortKey].ToString();

            // Separate numeric values including decimals from string
            var pat = @"(?=.*?).*?(\d+\.?\d*)";
            var matches = new Regex(pat, RegexOptions.IgnoreCase).Matches(viewPortFromDic!);
            driver.Manage().Window.Size = new Size(Convert.ToInt32(matches[0].Value), Convert.ToInt32(matches[0].Value));
        }*/

        if (Options.AsDic().ContainsKey(viewPortKey))
        {
            var viewPort = Options.AsDic()[viewPortKey].ToString();
            // Separate numeric values including decimals from string
            var pat = @"(?=.*?).*?(\d+\.?\d*)";
            var matches = new Regex(pat, RegexOptions.IgnoreCase).Matches(viewPort!);
            driver.Manage().Window.Size = new Size(Convert.ToInt32(matches[0].Value), Convert.ToInt32(matches[0].Value));
        }
    }
    #endregion
    #endregion
}