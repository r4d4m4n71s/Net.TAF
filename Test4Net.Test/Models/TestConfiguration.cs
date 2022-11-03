using Test4Net.Test.Interfaces;
using Test4Net.Util.Json;

namespace Test4Net.Test.Models;

public class TestConfiguration : ITestConfiguration
{
    public TestConfiguration()
    { }

    /// <summary>
    /// Constructor from json dic
    /// </summary>
    /// <param name="fromJsonDic"></param>
    /// <param name="id">Configuration key to look up into dic</param>
    public TestConfiguration(string fromJsonDic, string id)
    {
        Id = id;
        Values = fromJsonDic.GetJsonNode($"$..{id}").ToDictionary();
    }

    public string Id { get; set; }

    public IDictionary<string, object> Values { get; set; }
}