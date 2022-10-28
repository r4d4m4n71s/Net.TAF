namespace Test4Net.UI.Page.Interfaces
{
    public interface IResolutionAttribute
    {
        /// <summary>
        /// Size of the browser window, minus the scroll bars and tool bars
        /// </summary>
        /// <returns>Px With x height</returns>
        string GetViewPortSize();
    }
}
