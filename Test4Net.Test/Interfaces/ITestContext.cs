namespace Test4Net.Test.Interfaces;

public interface ITestContext 
{
        
    /// <summary>
    /// Test results directory
    /// </summary>
    string TestResultsFolder { get; set; }

    /// <summary>
    /// Test logs directory for files
    /// </summary>
    string LogFolder { get; set; }
}

public class TestContext : ITestContext
{
    /// <inheritdoc />
    public string TestName { get; set; }

    /// <inheritdoc />
    public string TestResultsFolder { get; set; }

    /// <inheritdoc />
    public string LogFolder { get; set; }
}