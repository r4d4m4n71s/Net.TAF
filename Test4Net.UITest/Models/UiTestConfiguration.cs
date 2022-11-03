using Test4Net.Test.Models;
using Test4Net.UITest.Interfaces;

namespace Test4Net.UITest.Models;

public class UiTestConfiguration : TestConfiguration, IUiTestConfiguration
{
    public UiTestConfiguration()
    { }

    /// <summary>
    /// Constructor from json dic
    /// </summary>
    /// <param name="fromJsonDic"></param>
    /// <param name="id">Configuration key to look up into dic</param>
    public UiTestConfiguration(string fromJsonDic, string id) : base(fromJsonDic, id)
    {
        Platform = Values.ContainsKey(nameof(Platform)) ? Values[nameof(Platform)].ToString() : string.Empty;
        Browser = Values.ContainsKey(nameof(Browser)) ? Values[nameof(Browser)].ToString() : string.Empty;
        ViewPort = Values.ContainsKey(nameof(ViewPort)) ? Values[nameof(ViewPort)].ToString() : string.Empty;
    }

    public string Platform { get; set; }
    public string Browser { get; set; }
    public string ViewPort { get; set; }
}