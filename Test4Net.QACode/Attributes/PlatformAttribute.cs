using Test4Net.UITest.Constants;

namespace Test4Net.UITest.Models.Attributes;

/// <summary>
/// Usage: Decorate pages and elements with a supported platform
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
public class PlatformAttribute : Attribute
{
    /// <summary>
    /// Platform id
    /// </summary>
    public Pf Pf { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="platform"></param>
    public PlatformAttribute(Pf platform) =>
        Pf = platform;
}