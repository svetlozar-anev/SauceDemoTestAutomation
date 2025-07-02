// <copyright file="Logger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace SauceDemo.Core.Utilities
{
    using Serilog;

    /// <summary>
    /// Provides a static Serilog logger instance for application-wide logging.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Gets the Serilog logger instance.
        /// </summary>
        public static ILogger? Log { get; private set; }

        /// <summary>
        /// Initializes the logger with console and rolling file sinks.
        /// Call once at test startup or app initialization.
        /// </summary>
        public static void Init()
        {
            Log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("..\\..\\..\\..\\logs\\SauceDemo-.log", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
        }
    }
}
