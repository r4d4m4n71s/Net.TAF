using OpenQA.Selenium;

namespace Test4Net.UI.Elements.Interfaces
{
    /// <summary>
    /// Represents an HTML element. Generally, all interesting operations to do with interacting
    /// with a page will be performed through this interface.
    /// </summary>
    public interface IHtmlElement : IWebElement, IWrapsElement
    {
    }
}
