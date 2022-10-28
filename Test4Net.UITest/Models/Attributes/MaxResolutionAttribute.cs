using System.Drawing;

namespace Test4Net.UITest.Models.Attributes;

/// <summary>
/// Size of the browser window, minus the scroll bars and tool bars
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
public class ViewPortAttribute : Attribute
{
    /// <summary>
    /// Browser size
    /// </summary>
    public readonly Size Size;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height">By default -1 in case of not required</param>
    public ViewPortAttribute(int width, int height = -1) => Size = new Size(width, height);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="size"></param>
    public ViewPortAttribute(Size size) => Size = size;
}