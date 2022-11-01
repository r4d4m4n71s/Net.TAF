using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using Test4Net.UI.POM.Elements.Interfaces;

namespace Test4Net.UI.POM.Elements
{
    /// <inheritdoc cref="IHtmlElement"/>
    public abstract class AbstractHtmlElement : IHtmlElement
    {
        /// <summary>
        /// Gets the tag name of this element.
        /// </summary>
        public string TagName => WrappedElement.TagName;

        /// <summary>
        /// Gets the innerText of this element, without any leading or trailing whitespace,
        /// and with other whitespace collapsed.
        /// </summary>
        public string Text => WrappedElement.Text;

        /// <summary>
        /// Gets a value indicating whether or not this element is enabled.
        /// </summary>
        public bool Enabled => WrappedElement.Enabled;

        /// <summary>
        /// Gets a value indicating whether or not this element is selected.
        /// </summary>
        public bool Selected => WrappedElement.Selected;

        /// <summary>
        /// Gets a Point object containing the coordinates of the upper-left corner
        /// of this element relative to the upper-left corner of the page.
        /// </summary>
        public Point Location => WrappedElement.Location;

        /// <summary>
        /// Gets a Size object containing the height and width of this element.
        /// </summary>
        public Size Size => WrappedElement.Size;

        /// <summary>
        /// Gets a value indicating whether or not this element is displayed.
        /// </summary>
        public bool Displayed => WrappedElement.Displayed;

        /// <summary>
        /// Clears the content of this element.
        /// </summary>
        public void Clear() => WrappedElement.Clear();

        /// <summary>
        /// Clicks this element.
        /// </summary>
        public void Click() => WrappedElement.Click();

        /// <summary>
        /// Finds the first IWebElement using the given method.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public IWebElement FindElement(By by) => WrappedElement.FindElement(by);

        /// <summary>
        /// Finds all IWebElements within the current context using the given mechanism.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by) => WrappedElement.FindElements(by);

        /// <summary>
        /// Gets the value of the specified attribute for this element.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttribute(string attributeName) => WrappedElement.GetAttribute(attributeName);

        /// <summary>
        /// Gets the value of a CSS property of this element.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetCssValue(string propertyName) => WrappedElement.GetCssValue(propertyName);

        /// <summary>
        ///  returns the value of the attribute with the given name but not the property with the same name
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetDomAttribute(string attributeName) => WrappedElement.GetDomAttribute(attributeName);

        /// <summary>
        /// return the current value, even if this has been modified after the page has been loaded.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetDomProperty(string propertyName) => WrappedElement.GetDomProperty(propertyName);

        /// <inheritdoc />
        public ISearchContext GetShadowRoot() => WrappedElement.GetShadowRoot();

        /// <summary>
        /// Use this method to simulate typing into an element, which may set its value.
        /// </summary>
        /// <param name="text"></param>
        public void SendKeys(string text) => WrappedElement.SendKeys(text);

        /// <summary>
        /// If this current element is a form, or an element within a form,
        /// then this will be submitted to the remote server.
        /// </summary>
        public void Submit() => WrappedElement.Submit();

        /// <summary>
        /// Gets the <see cref="IWebElement" /> wrapped.
        /// </summary>
        public IWebElement WrappedElement { get; set; }
    }
}
