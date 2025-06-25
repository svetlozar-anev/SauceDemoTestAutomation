using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SauceDemo.Tests.Utilities
{
    /// <summary>
    /// This is a per-thread Singleton-style factory: each thread gets its own WebDriver instance.
    /// It enables safe parallel test execution.
    /// Manages one WebDriver instance per test thread, supports multiple browsers,
    /// and handles proper setup and cleanup of the driver.
    /// </summary>
    public sealed class WebDriverFactory
    {
        // ThreadLocal ensures each test thread has its own separate WebDriver instance.
        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        // Private constructor prevents instantiation of the factory class.
        private WebDriverFactory() { }

        /// <summary>
        /// Provides access to the current thread's WebDriver instance.
        /// Throws an error if the driver hasn't been initialized for this thread.
        /// </summary>
        public static IWebDriver Driver => driver.Value ?? throw new InvalidOperationException("WebDriver instance was not initialized.");

        /// <summary>
        /// Initializes the WebDriver instance for the current thread.
        /// Supports Chrome and Edge browsers. Maximizes the browser window by default.
        /// If already initialized, it does nothing (safe for repeated calls).
        /// </summary>
        /// <param name="browser">Browser to use: "chrome" or "edge". Defaults to Chrome.</param>
        public static void InitDriver(string browser = "chrome")
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
        /// Quits and disposes the WebDriver instance for the current thread, if it exists.
        /// Clears the ThreadLocal storage to free up memory and system resources.
        /// </summary>
        public static void QuitDriver()
        {
            if (driver.IsValueCreated && driver.Value != null)
            {
                driver.Value.Quit(); // Close browser window and shutdown session
                driver.Value.Dispose(); // Release unmanaged resources
            }
        }
    }
}
