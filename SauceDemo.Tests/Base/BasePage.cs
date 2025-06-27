// <copyright file="BasePage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Base
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Abstract base class for all page objects.
    /// Provides shared WebDriver instance and common UI interaction helpers.
    /// </summary>
    public abstract class BasePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// Initializes the BasePage with the thread-safe WebDriver and default WebDriverWait.
        /// </summary>
        protected BasePage()
        {
            this.Driver = WebDriverFactory.Driver;
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(TestConfig.WebDriverWaitSeconds));
        }

        /// <summary>
        /// Gets thread-safe WebDriver instance from the WebDriverFactory.
        /// </summary>
        protected IWebDriver Driver { get; }

        /// <summary>
        /// Gets webDriverWait instance for waiting conditions.
        /// </summary>
        protected WebDriverWait Wait { get; }

        /// <summary>
        /// Navigates the browser to a specific URL.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        public void NavigateTo(string url)
        {
            this.Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Waits until the element is visible in the DOM and returns it.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The visible <see cref="IWebElement"/> found by the locator.</returns>
        protected IWebElement WaitForElementVisible(By locator)
        {
            return this.Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /// <summary>
        /// Waits until the element is clickable in the DOM and returns it.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The clickable <see cref="IWebElement"/> found by the locator.</returns>
        protected IWebElement WaitForElementClickable(By locator)
        {
            return this.Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        /// <summary>
        /// Clicks on an element after waiting until it’s clickable.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        protected void Click(By locator)
        {
            this.WaitForElementClickable(locator).Click();
        }

        /// <summary>
        /// Clears a field and types the specified text after waiting for visibility.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="text">The text to type into the element.</param>
        protected void TypeText(By locator, string text)
        {
            var element = this.WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Gets the text from a visible element.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The text content of the visible element.</returns>
        protected string GetElementText(By locator)
        {
            return this.WaitForElementVisible(locator).Text;
        }

        /// <summary>
        /// Checks if an element is displayed on the page.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>True if the element is displayed; otherwise, false.</returns>
        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                return this.WaitForElementVisible(locator).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Clears Fields with a little 'hack' added to make tests pass.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        protected void ClearField(By locator)
        {
            var element = this.WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(" ");
            element.SendKeys(Keys.Backspace);
            element.SendKeys(Keys.Tab);
        }
    }
}
