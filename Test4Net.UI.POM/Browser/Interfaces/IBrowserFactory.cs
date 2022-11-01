namespace Test4Net.UI.POM.Browser.Interfaces
{
    public interface IBrowserFactory
    {
        /// <summary>
        /// Get browser instance from driver setup
        /// If driver exist factory returns it, on the contrary factory creates a new one,
        /// adds to factory list and returns it
        /// </summary>
        /// <param name="setupId"></param>
        /// <returns>Driver instance</returns>
        public IBrowser Get(string setupId);

        /// <summary>
        /// Get browser instance from driver setup
        /// If driver exist factory returns it, on the contrary factory creates a new one,
        /// adds to factory list and returns it
        /// </summary>
        /// <param name="setupId"></param>
        /// <returns>Driver instance</returns>
        public T Get<T>(string setupId) where T : IBrowser;
    }
}
