using Test4Net.Logging.Interfaces;

namespace Test4Net.Test.Models
{
    public static class Logger
    {
        /// <summary>
        /// Maintain a single instance of logProvider classes
        /// </summary>
        private static readonly IDictionary<Type, object> LogInstances = new Dictionary<Type, object>();

        /// <summary>
        /// Log provider single instance
        /// </summary>
        private static ILogProvider _logProvider;

        /// <summary>
        /// Update log provider configuration
        /// </summary>
        /// <param name="logProvider"></param>
        public static void Configure(ILogProvider logProvider) =>
            _logProvider ??= logProvider;

        public static ILogProvider GetLogProvider() => _logProvider;

        /// <summary>
        /// Creates a new logger factory a returns a
        /// a new <see cref="ILog"/> instance
        /// </summary>
        /// <typeparam name="T">Target logging class</typeparam>
        /// <returns><see cref="ILog"/> instance</returns>
        public static ILog GetLogger<T>()
        {
            if (!LogInstances.ContainsKey(typeof(T)))
                LogInstances.Add(typeof(T), _logProvider.GetLogger<T>());

            return (ILog)LogInstances[typeof(T)];
        }

        /// <summary>
        /// Creates a new logger factory a returns a
        /// a new <see cref="ILog"/> instance
        /// </summary>
        /// <returns><see cref="ILog"/> instance</returns>
        public static ILog GetLogger(Type type)
        {
            if (!LogInstances.ContainsKey(type))
                LogInstances.Add(type, _logProvider.GetLogger(type));

            return (ILog)LogInstances[type];
        }
    }
}
