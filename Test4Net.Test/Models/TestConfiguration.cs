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
    /// <param name="name">Configuration key to look up into dic</param>
    public TestConfiguration(string fromJsonDic, string name)
    {
        Name = name;
        Values = fromJsonDic.GetJsonNode($"$..{name}").ToDictionary();
    }

    public string Name { get; set; }

    public IDictionary<string, object> Values { get; set; }
}