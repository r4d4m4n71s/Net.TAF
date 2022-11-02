namespace Test4Net.UI.WebBrowser.Browser.Interfaces
{
    public interface IBrowserFactory
    {
        /// <summary>
        /// Get browser instance from driver setup
        /// If driver exist factory returns it, on the contrary factory creates a new one,
        /// adds to factory list and returns it
        /// </summary>
        /// <param name="setupId"></param>
        /// <returns>Driver instance</returns>
        public IWebBrowser Get(string setupId);

        /// <summary>
        /// Get browser instance from driver setup
        /// If driver exist factory returns it, on the contrary factory creates a new one,
        /// adds to factory list and returns it
        /// </summary>
        /// <param name="setupId"></param>
        /// <returns>Driver instance</returns>
        public T Get<T>(string setupId) where T : IWebBrowser;
    }
}
