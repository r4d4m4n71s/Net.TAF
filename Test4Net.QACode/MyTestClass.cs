using Microsoft.Extensions.Configuration;
using Test4Net.Test;

namespace Test4Net.QACode
{
    [TestClass]
    public class MyTestClass : AbstractTest
    {
        /// <summary>
        /// Configure test class from settings
        /// </summary>
        public MyTestClass() : 
            base(configure => configure.AddJsonFile("Configuration/Qa.settings.json")) // Set Configuration
        {
            ConfigureLogger(Configuration.GetSection("Logging")); // From configuration get logging
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var environmentUrl = Configuration["QaEnvUrl"];
            Log.Info($"Loading environment url: {environmentUrl} from configuration file");
        }
    }
}
