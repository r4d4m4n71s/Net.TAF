using System.Reflection;
using OpenQA.Selenium;
using Test4Net.UI.WebBrowser.Driver.Interfaces;
using Test4Net.UI.WebBrowser.Util;

namespace Test4Net.UI.WebBrowser.Driver;

/// <summary>
/// Selenium web driver factory
/// </summary>
public class DriverFactory : IDriverFactory, IDisposable
{
    /// <summary>
    /// Driver setups
    /// </summary>
    private readonly IDictionary<string, IDriverSetup> _setups;

    /// <summary>
    /// Driver instances
    /// </summary>
    private readonly IDictionary<string, IWebDriver> _drivers;

    /// <summary>
    /// Constructor
    /// </summary>
    private DriverFactory()
    {
        _drivers = new Dictionary<string, IWebDriver>();
        _setups = new Dictionary<string, IDriverSetup>();
    }

    /// <summary>
    /// Constructor from json driver settings configuration
    /// </summary>
    /// <param name="driverSettingsAsJson">list of multiple setups</param>
    public DriverFactory(string driverSettingsAsJson) : this() => 
        _setups = FromJson(driverSettingsAsJson);

    /// <summary>
    /// Constructor from driver setup
    /// </summary>
    /// <param name="setupId"></param>
    /// <param name="setup"></param>
    public DriverFactory(string setupId, IDriverSetup setup) : this() =>
        AddSetup(setupId, setup);

    /// <summary>
    /// Get driver instance mapping driver from configuration
    /// If driver exist factory returns it, on the contrary factory creates a new one,
    /// adds to factory list and returns it
    /// </summary>
    /// <param name="setupId">Setup id</param>
    /// <param name="driverSetupAsDic">Driver options set as json dic</param>
    /// <returns>Driver instance</returns>
    public DriverFactory(string setupId, IDictionary<string, object> driverSetupAsDic) : this() =>
        AddSetup(setupId, driverSetupAsDic);

    /// <summary>
    /// Constructor from driver setups
    /// </summary>
    /// <param name="setups"></param>
    public DriverFactory(IDictionary<string, IDriverSetup> setups) : this() =>
        _setups = setups;

    /// <summary>
    /// Add new setup to dictionary
    /// </summary>
    /// <param name="setupId"></param>
    /// <param name="setup"></param>
    public void AddSetup(string setupId, IDriverSetup setup) =>
        _setups.Add(setupId, setup);

    /// <summary>
    /// Add new setup from json to dictionary
    /// </summary>
    /// <param name="setupId"></param>
    /// <param name="driverSetupAsDic">Driver options set as json dic</param>
    public void AddSetup(string setupId, IDictionary<string, object> driverSetupAsDic) =>
        _setups.Add(setupId, FromDic(driverSetupAsDic));

    /// <summary>
    /// Get driver instance from driver setup
    /// If driver exist factory returns it, on the contrary factory creates a new one,
    /// adds to factory list and returns it
    /// </summary>
    /// <param name="setupId"></param>
    /// <returns>Driver instance</returns>
    public IWebDriver Get(string setupId) =>
        Get<IWebDriver>(setupId);

    /// <summary>
    /// Get driver instance from driver setup
    /// If driver exist factory returns it, on the contrary factory creates a new one,
    /// adds to factory list and returns it
    /// </summary>
    /// <param name="setupId"></param>
    /// <returns>Driver instance</returns>
    public T Get<T>(string setupId) where T : IWebDriver
    {
        if(_drivers.ContainsKey(setupId))
            return (T)_drivers[setupId];

        _drivers.Add(setupId, CreateDriver(_setups[setupId]));
        return (T)_drivers[setupId];
    }

    /// <summary>
    /// Create setup configurations from json
    /// </summary>
    /// <param name="driverSettingsAsJson"></param>
    /// <returns></returns>
    public static IDictionary<string, IDriverSetup> FromJson(string driverSettingsAsJson)
    {
        var setups = new Dictionary<string, IDriverSetup>();

        // ForEach setup
        driverSettingsAsJson.CreateOptionsStructure().ForEach(
            setupAsDic =>
                setups.Add(setupAsDic["Id"].ToString()!, FromDic(setupAsDic)));

        return setups;
    }

    /// <summary>
    /// Instances a new DriverSetup from dic
    /// </summary>
    /// <param name="setupAsDic"></param>
    /// <returns></returns>
    public static IDriverSetup FromDic(IDictionary<string, object> setupAsDic)
    {
        // Instance setup and store in setups dic
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        return (IDriverSetup)Activator.CreateInstance(
            Type.GetType($"{assemblyName}.Driver.Configuration.Type.Flavors.{setupAsDic["type"]}Setup, {assemblyName}", true),
            args: setupAsDic)!;
    }

    /// <summary>
    /// Create driver instance
    /// </summary>
    /// <returns></returns>
    private IWebDriver CreateDriver(IDriverSetup setup)
    {
        setup.ApplyPreConstructionFeatures();
        var driver = setup.Create();
        setup.ApplyPostConstructionFeatures(driver);
        return driver;
    }

    /// <summary>
    /// Disposes a driver from dic
    /// </summary>
    /// <param name="setupId"></param>
    public void Dispose(string setupId)
    {
        _drivers[setupId].Quit();
        _drivers.Remove(setupId);
        //GC.SuppressFinalize(_drivers[setupId]);
    }

    /// <summary>
    /// Disposes all created drivers
    /// </summary>
    public void Dispose()
    {
        foreach (var driverInstance in _drivers)
            driverInstance.Value.Quit();

        _drivers.Clear();
    }
}