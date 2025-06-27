// <copyright file="WebDriverFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Core.Utilities
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;

    /// <summary>
    /// This is a per-thread Singleton-style factory: each thread gets its own WebDriver instance.
    /// It enables safe parallel test execution.
    /// Manages one WebDriver instance per test thread, supports multiple browsers,
    /// and handles proper setup and cleanup of the driver.
    /// </summary>
    public sealed class WebDriverFactory
    {
        /// <summary>
        /// Holds the WebDriver instance for the current thread.
        /// </summary>
        private static ThreadLocal<IWebDriver?> driver = new ThreadLocal<IWebDriver?>();

        /// <summary>
        /// Prevents a default instance of the <see cref="WebDriverFactory"/> class from being created.
        /// </summary>
        private WebDriverFactory()
        {
        }

        /// <summary>
        /// Gets the current thread's WebDriver instance.
        /// Throws <see cref="InvalidOperationException"/> if the driver is not initialized.
        /// </summary>
        public static IWebDriver Driver => driver.Value ?? throw new InvalidOperationException("WebDriver instance was not initialized.");

        /// <summary>
        /// Initializes the WebDriver instance for the current thread with the specified browser.
        /// If a driver already exists, it will be quit and disposed before creating a new one.
        /// </summary>
        /// <param name="browser">The browser to use (e.g., "chrome" or "edge").</param>
        /// <exception cref="ArgumentException">Thrown when an unsupported browser is specified.</exception>
        public static void InitDriver(string browser)
        {
            QuitDriver();

            IWebDriver webDriver;

            switch (browser.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    webDriver = new ChromeDriver(chromeOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    webDriver = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }

            driver.Value = webDriver;
        }

        /// <summary>
        /// Quits and disposes of the current thread's WebDriver instance if it exists.
        /// </summary>
        public static void QuitDriver()
        {
            if (driver.IsValueCreated && driver.Value != null)
            {
                driver.Value.Quit();
                driver.Value.Dispose();
                driver.Value = null;
            }
        }
    }
}
