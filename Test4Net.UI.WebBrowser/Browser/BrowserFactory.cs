using Test4Net.UI.WebBrowser.Browser.Interfaces;
using Test4Net.UI.WebBrowser.Driver;
using Test4Net.UI.WebBrowser.Driver.Interfaces;

namespace Test4Net.UI.WebBrowser.Browser
{
    /// <summary>
    /// Browser instances factory
    /// </summary>
    public class BrowserFactory : IBrowserFactory, IDisposable
    {
        /// <summary>
        /// Driver factory to process requests
        /// </summary>
        private readonly IDriverFactory _driverFactory;

        /// <summary>
        /// If a browser is already created return it, otherwise creates, add and return it.
        /// </summary>
        private readonly IDictionary<string, IWebBrowser> _browserInstances = new Dictionary<string, IWebBrowser>();

        /// <summary>
        /// Creates new browser factory with a driver factory
        /// </summary>
        /// <param name="driverFactory"></param>
        public BrowserFactory(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }
        
        /// <summary>
        /// Gets a browser instance from configuration id
        /// </summary>
        /// <param name="setupId"></param>
        /// <returns></returns>
        public IWebBrowser Get(string setupId)
        {
            if(!_browserInstances.ContainsKey(setupId))
                _browserInstances.Add(setupId, new WebBrowser(setupId, _driverFactory.Get(setupId)));

            return _browserInstances[setupId];
        }

        /// <summary>
        /// Gets a T browser instance from configuration id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setupId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Get<T>(string setupId) where T : IWebBrowser => (T)Get(setupId);

        /// <summary>
        /// Disposes a browser from dic
        /// </summary>
        /// <param name="setupId"></param>
        public void Dispose(string setupId)
        {
            ((DriverFactory)_driverFactory).Dispose(setupId);
            _browserInstances.Remove(setupId);
            //GC.SuppressFinalize(_drivers[setupId]);
        }

        /// <summary>
        /// Disposes all browsers from dic
        /// </summary>
        public void Dispose()
        {
            ((DriverFactory)_driverFactory).Dispose();
            _browserInstances.Clear();
        }
    }
}
