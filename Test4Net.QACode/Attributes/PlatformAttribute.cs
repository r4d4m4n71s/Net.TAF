using Test4Net.QACode.Constants;

namespace Test4Net.QACode.Attributes;

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