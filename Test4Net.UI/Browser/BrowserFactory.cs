using Test4Net.UI.Browser.Interfaces;
using Test4Net.UI.Driver;
using Test4Net.UI.Driver.Interfaces;

namespace Test4Net.UI.Browser
{
    /// <summary>
    /// Browser instances factory
    /// </summary>
    public class BrowserFactory : IBrowserFactory
    {
        /// <summary>
        /// Driver factory to process requests
        /// </summary>
        private readonly IDriverFactory _driverFactory;

        /// <summary>
        /// If a browser is already created return it, otherwise creates, add and return it.
        /// </summary>
        private readonly IDictionary<string, IBrowser> _browserInstances = new Dictionary<string, IBrowser>();

        /// <summary>
        /// Creates new browser factory with a driver factory
        /// </summary>
        /// <param name="driverFactory"></param>
        public BrowserFactory(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }

        /// <summary>
        /// Creates new browser factory and sets
        /// a new driver factory using a json with a list
        /// of driver configurations <see cref="DriverFactory"/>
        /// </summary>
        /// <param name="driverSettingsAsJson">Dic of driver configurations in json format</param>
        public BrowserFactory(string driverSettingsAsJson)
        {
            _driverFactory = new DriverFactory(driverSettingsAsJson);
        }

        /// <summary>
        /// Gets a browser instance from configuration id
        /// </summary>
        /// <param name="setupId"></param>
        /// <returns></returns>
        public IBrowser Get(string setupId)
        {
            if(!_browserInstances.ContainsKey(setupId))
                _browserInstances.Add(setupId, new BrowserAdapter(setupId, _driverFactory.Get(setupId)));

            return _browserInstances[setupId];
        }

        /// <summary>
        /// Gets a T browser instance from configuration id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setupId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Get<T>(string setupId) where T : IBrowser => (T)Get(setupId);
    }
}
