using System.Diagnostics.Contracts;
using OpenQA.Selenium;
using Test4Net.UI.Browser.Interfaces;
using Test4Net.Util.Thread;

namespace Test4Net.UI.Browser;

/// <summary>
/// Javascript handler
/// </summary>
internal sealed class JavaScriptAdapter : IJavaScript 
{
    private readonly IBrowser _browser;
    private readonly IJavaScriptExecutor _js;

    public JavaScriptAdapter(IBrowser browser)
    {
        _browser = browser;
        _js = (IJavaScriptExecutor)browser.Driver;
    }

    /// <inheritdoc/>
    public object Execute(string javaScript, params object[] args) => _js.ExecuteScript(javaScript, args);

    /// <inheritdoc/>
    public void WaitReadyState()
    {
        Contract.Assume(_browser != null);
        var ready = new Func<bool>(() => (bool) Execute("return document.readyState == 'complete'"));
        Contract.Assert(Waiter.SpinWait(ready, TimeSpan.FromSeconds(60), TimeSpan.FromMilliseconds(100)));
    }

    /// <inheritdoc/>
    public void WaitAjax()
    {
        Contract.Assume(_browser != null);
        var ready = new Func<bool>(() => (bool) Execute("return (typeof($) === 'undefined') ? true : !$.active;"));
        Contract.Assert(Waiter.SpinWait(ready, TimeSpan.FromSeconds(60), TimeSpan.FromMilliseconds(100)));
    }

    /// <inheritdoc/>
    public void FireJQueryEvent(IWebElement element, JavaScriptEvent javaScriptEvent)
    {
        var eventName = javaScriptEvent.Name;
        Execute($"$(arguments[0]).{eventName}();", element);
    }
}