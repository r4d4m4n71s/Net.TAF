namespace Test4Net.UI.Attributes
{
    /// <summary>
    /// Platform attribute contract
    /// </summary>
    public interface IPlatformSupportedAttribute
    {
        /// <summary>
        /// Get platform name from attribute
        /// </summary>
        /// <returns></returns>
        public string GetPlatformName();
    }
}
