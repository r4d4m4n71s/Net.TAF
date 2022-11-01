using Test4Net.Test.Interfaces;

namespace Test4Net.UITest.Interfaces;

/// <summary>
/// Configuration for test running
/// </summary>
public interface IUiTestConfiguration : ITestConfiguration
{
    /// <summary>
    /// Platform, operative system
    /// </summary>
    string Platform { get; set; }

    /// <summary>
    /// Browser nave
    /// </summary>
    string Browser { get; set; }

    /// <summary>
    /// View Port
    /// </summary>
    string ViewPort { get; set; }
}