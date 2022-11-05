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
    /// <param name="commonsKey">Node with the common information</param>
    /// <returns>Options as dic</returns>
    public static IDictionary<string, object> CreateOptionsStructureForANode(this string driverSettingsAsJson, string driverId, string commonsKey = "Commons")
    {
        var driverSettingsAsDic = driverSettingsAsJson.ToDictionary();

        bool hasCommons;
        if (!(hasCommons = driverSettingsAsDic.ContainsKey(commonsKey)))
        {
            // Validate with the first character to lower case
            commonsKey = string.Concat(commonsKey.First().ToString().ToLower(), commonsKey.AsSpan(1));
            hasCommons = driverSettingsAsDic.ContainsKey(commonsKey);
        }

        var target = hasCommons ? driverSettingsAsDic[commonsKey].ToString().
                Merge(driverSettingsAsDic[driverId].ToString()).ToDictionary() : 
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
    /// <param name="commonsKey">Node with the common information</param>
    /// <returns>Options as list of dic</returns>
    public static List<IDictionary<string, object>> CreateOptionsStructure(this string driverSettingsAsJson, string commonsKey = "Commons")
    {
        var setups = new List<IDictionary<string, object>>();
        var driverSettingsAsDic = driverSettingsAsJson.ToDictionary();

        bool hasCommons;
        if (!(hasCommons = driverSettingsAsDic.ContainsKey(commonsKey)))
        {
            // Validate with the first character to lower case
            commonsKey = string.Concat(commonsKey.First().ToString().ToLower(), commonsKey.AsSpan(1));
            hasCommons = driverSettingsAsDic.ContainsKey(commonsKey);
        }

        var commons = string.Empty;
        if (hasCommons)
        {
            commons = driverSettingsAsDic[commonsKey].ToString();
            driverSettingsAsDic.Remove(commonsKey);
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