using Test4Net.Util.Json;

namespace Test4Net.UI.WebBrowser.Util;

/// <summary>
/// Driver setup utilities like data conversion
/// and resources locator
/// </summary>
public static class DriverSetupExtensions
{
    /// <summary>
    /// From a json driver settings structure
    /// builds a options dic,
    /// Strategy: merge the common options to the
    /// desired option identified by the driverId
    /// </summary>
    /// <param name="driverSettingsAsJson"></param>
    /// <param name="driverId"></param>
    /// <returns>Options as dic</returns>
    public static IDictionary<string, object> CreateOptionsStructure(this string driverSettingsAsJson, string driverId)
    {
        var driverSettingsAsDic = driverSettingsAsJson.ToDictionary();

        var target = driverSettingsAsDic.ContainsKey("commons") ? 
            driverSettingsAsDic["commons"].ToString().Merge(driverSettingsAsDic[driverId].ToString()).ToDictionary() : 
            driverSettingsAsDic[driverId].JsonToDic();
            
        target.Add("Id", driverId);
        return target;
    }

    /// <summary>
    /// From a json driver settings structure
    /// builds a options dic,
    /// Strategy: merge the common options through different
    /// setups
    /// </summary>
    /// <param name="driverSettingsAsJson"></param>
    /// <returns>Options as list of dic</returns>
    public static List<IDictionary<string, object>> CreateOptionsStructure(this string driverSettingsAsJson)
    {
        var setups = new List<IDictionary<string, object>>();
        var driverSettingsAsDic = driverSettingsAsJson.ToDictionary();

        var commons = string.Empty;
        if (driverSettingsAsDic.ContainsKey("commons"))
        {
            commons = driverSettingsAsDic["commons"].ToString();
            driverSettingsAsDic.Remove("commons");
        }

        foreach (var setupAsDicKvp in driverSettingsAsDic)
        {
            var setupAsDic =
                (!string.IsNullOrEmpty(commons) ? commons.Merge(
                    setupAsDicKvp.Value.ToString()) : setupAsDicKvp.Value).JsonToDic();
            
            setupAsDic.Add("Id", setupAsDicKvp.Key);
            setups.Add(setupAsDic);
        }

        return setups;
    }
}