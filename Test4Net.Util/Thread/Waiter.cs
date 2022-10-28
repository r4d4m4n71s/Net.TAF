using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Test4Net.Util.Thread;

/// <summary>
/// Waiter task approach
/// <remarks>https://github.com/sayems/csharp.webdriver/blob/master/Selenium/core/Waiter.cs</remarks>
/// </summary>
public class Waiter
{
    private readonly TimeSpan _checkInterval;
    private readonly Stopwatch _stopwatch;
    private readonly TimeSpan _timeout;

    private Waiter(TimeSpan timeout)
        : this(timeout, TimeSpan.FromSeconds(1))
    {
    }

    private Waiter(TimeSpan timeout, TimeSpan checkInterval)
    {
        Contract.Requires(timeout >= TimeSpan.Zero);
        Contract.Requires(checkInterval >= TimeSpan.Zero);

        _timeout = timeout;
        _checkInterval = checkInterval;
        _stopwatch = Stopwatch.StartNew();
    }

    public bool IsSatisfied { get; private set; } = true;

    public static Waiter WithTimeout(TimeSpan timeout, TimeSpan pollingInterval)
    {
        return new Waiter(timeout, pollingInterval);
    }

    public static Waiter WithTimeout(TimeSpan timeout)
    {
        return new Waiter(timeout);
    }

    public Waiter WaitFor(Func<bool> condition)
    {
        Contract.Requires(condition != null);

        if (!IsSatisfied)
            return this;

        while (!condition())
        {
            var sleepAmount = Min(_timeout - _stopwatch.Elapsed, _checkInterval);

            if (sleepAmount < TimeSpan.Zero)
            {
                IsSatisfied = false;
                break;
            }

            System.Threading.Thread.Sleep(sleepAmount);
        }

        return this;
    }

    public void EnsureSatisfied()
    {
        if (!IsSatisfied)
            throw new TimeoutException();
    }

    public void EnsureSatisfied(string message)
    {
        Contract.Requires(message != null);

        if (!IsSatisfied)
            throw new TimeoutException(message);
    }

    public static bool SpinWait(Func<bool> condition, TimeSpan timeout)
    {
        return SpinWait(condition, timeout, TimeSpan.FromSeconds(1));
    }

    public static bool SpinWait(Func<bool> condition, TimeSpan timeout, TimeSpan pollingInterval)
    {
        return WithTimeout(timeout, pollingInterval).WaitFor(condition).IsSatisfied;
    }

    public static bool Try(Action action)
    {
        Exception exception;

        return Try(action, out exception);
    }

    public static bool Try(Action action, out Exception exception)
    {
        Contract.Requires(action != null);

        try
        {
            action();
            exception = null;

            return true;
        }
        catch (Exception e)
        {
            exception = e;

            return false;
        }
    }

    public static Func<bool> MakeTry(Action action)
    {
        return () => Try(action);
    }
    /*
    /// <summary>
    /// Repeatedly applies this instance's input value to the given function until one of the following occurs: the function returns neither null nor 
    /// falsethe function throws an exception that is not in the list of ignored exception typesthe timeout expires
    /// </summary>
    /// <typeparam name="TResult">The delegate's expected return type</typeparam>
    /// <param name="condition">A delegate taking an object of type T as its parameter, and returning a TResult</param>
    /// <param name="polls">Number of polls to do on the given timeout</param>
    /// <returns>The delegate's return value</returns>
    public TResult Until<TResult>(Func<bool> condition, int polls = 5)
    {
        // Create a seed
        double currentTime = 0.01;

        // Initialize polling interval at the seed
        var pollingInterval = TimeSpan.FromSeconds(currentTime);

        // Calculate an interval
        var interval = Math.Pow(Timeout. TotalSeconds / 0.01, 1 / (double)polls);

        // Return a wait that modifies the polling interval
        return base.Until(w => {
            // Capture the current time for delta
            var lastTime = currentTime;
            // Get the new interval
            currentTime *= interval;
            // Update the polling interval to the new time
            PollingInterval = TimeSpan.FromSeconds(Math.Min(currentTime, Timeout.TotalSeconds) - lastTime);

            // Evaluate the condition
            return condition(w);
        });

    }
    */

    private static T Min<T>(T left, T right) where T : IComparable<T>
    {
        return left.CompareTo(right) < 0 ? left : right;
    }
}