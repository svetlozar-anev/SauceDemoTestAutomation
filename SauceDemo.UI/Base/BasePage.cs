// <copyright file="BasePage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Base
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Pages;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Abstract base class for all page objects.
    /// Provides shared WebDriver instance and common UI interaction helpers.
    /// </summary>
    #pragma warning disable SA1600 // ElementsMustBeDocumented
    public abstract class BasePage
    {
        protected BasePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfig.WebDriverWaitSeconds));
        }

        protected IWebDriver Driver { get; }

        protected WebDriverWait Wait { get; }
        
        protected IWebElement Find(By by)
        {
            return Wait.Until(d => d.FindElement(by));
        }

        protected IReadOnlyCollection<IWebElement> FindAll(By by)
        {
            return Wait.Until(d => d.FindElements(by));
        }
        
        protected void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
        
        protected void Click(By by)
        {
            var element = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            element.Click();
        }
        
        protected void TypeText(By locator, string text)
        {
            var element = WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetElementText(By locator)
        {
            return WaitForElementVisible(locator).Text;
        }

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

        protected void ClearField(By locator)
        {
            var element = WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(" ");
            element.SendKeys(Keys.Backspace);
            element.SendKeys(Keys.Tab);
        }

        protected IWebElement WaitForElementClickable(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        protected void RefreshPage(Func<bool> condition)
        {
            Driver.Navigate().Refresh();
            Wait.Until(_ => condition());
        }

        private IWebElement WaitForElementVisible(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }
    }
    #pragma warning restore SA1600 // ElementsMustBeDocumented
}