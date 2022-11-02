using Test4Net.UI.POM.Page.Interfaces;
using Test4Net.UI.WebBrowser.Browser.Interfaces;

namespace Test4Net.UI.POM.Page;

/// <summary>
/// Abstract page model
/// </summary>
public abstract class AbstractPage : PageAdapter
{
    /// <summary>
    /// Validation rules associated to the page
    /// </summary>
    public ValidationRules Rules { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="browser">Browser instance</param>
    /// <param name="parent">Parent page</param>
    protected AbstractPage(IWebBrowser browser, IPage parent = default) : base(browser, parent)
    {
        Rules = new ValidationRules();

        // Init html controls
        SeleniumExtras.PageObjects.PageFactory.InitElements(browser.Driver, this);
    }
}