using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SauceDemo.Tests.Utilities;

namespace SauceDemo.Core.Base
{
    /// <summary>
    /// Abstract base class for all page objects.
    /// Provides shared WebDriver and common web interaction methods.
    /// </summary>
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }
        protected WebDriverWait Wait { get; }

        /// <summary>
        /// Initializes the BasePage with the thread-safe driver and default wait.
        /// </summary>
        protected BasePage()
        {
            Driver = SauceDemo.Tests.Utilities.WebDriverFactory.Driver;

            // You can extract this timeout into a Config later
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }
    }
}
