using Test4Net.Logging.Interfaces;
using Test4Net.UI.Browser.Interfaces;
using Test4Net.UI.Page.Interfaces;

namespace Test4Net.UI.Page;

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
    /// Page logger
    /// </summary>
    protected ILog Log { get; set; }

    protected AbstractPage(IBrowser browser, ILog log, IPage parent = default) : base(browser, parent)
    {
        Log = log;
        Rules = new ValidationRules(log);

        // Init html controls
        SeleniumExtras.PageObjects.PageFactory.InitElements(browser.Driver, this);
    }
}