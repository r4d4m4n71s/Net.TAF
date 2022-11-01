using OpenQA.Selenium;

namespace Test4Net.UI.WebDriver.Driver.Interfaces
{
    /// <summary>
    /// Selenium web driver factory
    /// </summary>
    public interface IDriverFactory
    {
        /// <summary>
        /// Get driver instance from driver setup
        /// If driver exist factory returns it, on the contrary factory creates a new one,
        /// adds to factory list and returns it
        /// </summary>
        /// <param name="setupId"></param>
        /// <returns>Driver instance</returns>
        public IWebDriver Get(string setupId);

        /// <summary>
        /// Get driver instance from driver setup
        /// If driver exist factory returns it, on the contrary factory creates a new one,
        /// adds to factory list and returns it
        /// </summary>
        /// <param name="setupId"></param>
        /// <returns>Driver instance</returns>
        public T Get<T>(string setupId) where T : IWebDriver;
    }
}
