using Test4Net.Test.Interfaces;
using Test4Net.Util.Json;

namespace Test4Net.Test.Models;

public class TestConfiguration : ITestConfiguration
{
    public TestConfiguration()
    { }

    public TestConfiguration(string fromJsonDic, string name)
    {
        Name = name;
        Values = fromJsonDic.GetJsonNode($"$..{name}").ToDictionary();
    }

    public string Name { get; set; }

    public IDictionary<string, object> Values { get; set; }
}