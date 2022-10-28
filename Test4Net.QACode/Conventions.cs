namespace Test4Net.QACode;

internal static class Conventions
{
    /// <summary>
    /// Application environments
    /// </summary>
    public enum Env
    {
        Dev,
        Qa,
        PreProd,
        Prod
    }

    /// <summary>
    /// Define keys to global variables
    /// </summary>
    public enum GlobalKeyVariables
    {
        AppEnvironment
    }
}