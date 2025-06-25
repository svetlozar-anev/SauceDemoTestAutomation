using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SauceDemo.Tests.Utilities;
using SeleniumExtras.WaitHelpers;

namespace SauceDemo.Tests.Base
{
    /// <summary>
    /// Abstract base class for all page objects.
    /// Provides shared WebDriver instance and common UI interaction helpers.
    /// </summary>
    public abstract class BasePage
    {
        /// <summary>
        /// Thread-safe WebDriver instance from the WebDriverFactory.
        /// </summary>
        protected IWebDriver Driver { get; }

        /// <summary>
        /// WebDriverWait instance for waiting conditions.
        /// </summary>
        protected WebDriverWait Wait { get; }

        /// <summary>
        /// Initializes the BasePage with the thread-safe WebDriver and default WebDriverWait.
        /// </summary>
        protected BasePage()
        {
            Driver = WebDriverFactory.Driver;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); // TODO: Extractable to config
        }

        /// <summary>
        /// Navigates the browser to a specific URL.
        /// </summary>
        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Waits until the element is visible in the DOM and returns it.
        /// </summary>
        protected IWebElement WaitForElementVisible(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /// <summary>
        /// Waits until the element is clickable in the DOM and returns it.
        /// </summary>
        protected IWebElement WaitForElementClickable(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        /// <summary>
        /// Clicks on an element after waiting until it’s clickable.
        /// </summary>
        protected void Click(By locator)
        {
            WaitForElementClickable(locator).Click();
        }

        /// <summary>
        /// Clears a field and types the specified text after waiting for visibility.
        /// </summary>
        protected void TypeText(By locator, string text)
        {
            var element = WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Gets the text from a visible element.
        /// </summary>
        protected string GetElementText(By locator)
        {
            return WaitForElementVisible(locator).Text;
        }

        /// <summary>
        /// Checks if an element is displayed on the page.
        /// </summary>
        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                return WaitForElementVisible(locator).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Clears Fields with a little 'hack' added to make tests pass.
        /// </summary>
        protected void ClearField(By locator)
        {
            var element = WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(" ");
            element.SendKeys(Keys.Backspace);
            element.SendKeys(Keys.Tab);
        }
    }
}
