using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test4Net.Util.Json;

/// <summary>
/// Interaction with json structures
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Clone object using json serialization approach
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <returns>Copy</returns>
    public static T DeepClone<T>(this T instance) => 
        JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(instance));

    /// <summary>
    /// Uses jPath to get json node
    /// </summary>
    /// <param name="json"></param>
    /// <param name="jPath"></param>
    /// <returns></returns>
    public static string GetJsonNode(this string json, string jPath) => 
        JObject.Parse(json).SelectToken(jPath)?.ToString();

    /// <summary>
    /// Serializes object to json
    /// </summary>
    /// <param name="_object"></param>
    /// <returns>string as json</returns>
    public static string ToJson(this object _object) =>
        JsonConvert.SerializeObject(_object);

    /// <summary>
    /// Converts json into dictionary
    /// </summary>
    /// <param name="json">Json structure</param>
    /// <returns>Dictionary</returns>
    public static IDictionary<string, object> ToDictionary(this string json) => 
        JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

    /// <summary>
    /// Converts object into dictionary
    /// </summary>
    /// <param name="json">Json structure</param>
    /// <returns>Dictionary</returns>
    public static IDictionary<string, object> JsonToDic(this object json) =>
        ToDictionary(json.ToString()!);

    /// <summary>
    /// Merge two json sources in a single
    /// </summary>
    /// <param name="target">To</param>
    /// <param name="reference">From</param>
    /// <param name="strategy">MergeArrayHandling, union by default</param>
    /// <returns></returns>
    public static string Merge(this string target, string reference, MergeArrayHandling strategy = MergeArrayHandling.Replace)
    {
        var targetObj = JObject.Parse(target);
        targetObj.Merge(JObject.Parse(reference), new JsonMergeSettings
        {
            // union array values together to avoid duplicates
            MergeArrayHandling = strategy
        });

        return targetObj.ToString();
    }
}