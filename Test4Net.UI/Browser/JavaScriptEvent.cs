namespace Test4Net.UI.Browser;

/// <summary>
/// Javacript event handler
/// </summary>
public readonly struct JavaScriptEvent
{
    public readonly string Name;

    /// <inheritdoc />
    public JavaScriptEvent(string name) : this() => Name = name;

    public static implicit operator string(JavaScriptEvent jEvent) => jEvent.Name;

    public override string ToString() => Name;

    public static readonly JavaScriptEvent KeyUp = new("keyup");
    public static readonly JavaScriptEvent Click = new("click");
}