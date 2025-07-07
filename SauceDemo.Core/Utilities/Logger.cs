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
        /// Gets the SeleniumLog Serilog logger instance.
        /// </summary>
        public static ILogger? SeleniumLog { get; private set; }

        /// <summary>
        /// Gets the NUnitLog Serilog logger instance.
        /// </summary>
        public static ILogger? NUnitLog { get; private set; }

        /// <summary>
        /// Initializes the logger with console and rolling file sinks.
        /// Call once at test startup or app initialization.
        /// </summary>
        public static void Init()
        {
            var baseDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            var logDir = Path.Combine(baseDir, "logs");
            Directory.CreateDirectory(logDir);

            SeleniumLog = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(Path.Combine(logDir, "Selenium-.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            NUnitLog = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(Path.Combine(logDir, "NUnit-.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
