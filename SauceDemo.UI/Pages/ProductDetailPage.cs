// <copyright file="ProductDetailPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Pages
{
    using OpenQA.Selenium;
    using SauceDemo.UI.Base;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Page Object Model for the product detail page.
    /// </summary>
    public class ProductDetailPage : BasePage
    {
        private readonly By title = By.CssSelector(".inventory_details_name");
        private readonly By description = By.CssSelector(".inventory_details_desc");
        private readonly By price = By.CssSelector(".inventory_details_price");
        private readonly By backButton = By.CssSelector(".inventory_details_back_button");

        /// <summary>
        /// Navigates back to the dashboard page.
        /// </summary>
        public void ReturnToDashboard(int timeoutSeconds = 10)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            var backBtn = wait.Until(d => d.FindElement(backButton));
            backBtn.Click();
        }

        /// <summary>
        /// Gets the product title as displayed on the detail page.
        /// </summary>
        /// <returns>
        /// The product title exactly as displayed on the UI, with leading and trailing
        /// whitespace removed.
        /// </returns>
        public string GetTitle() => Driver.FindElement(title).Text.Trim();

        /// <summary>
        /// Gets the product price.
        /// </summary>
        /// <returns>
        /// The price string including the currency symbol, for example "$29.99",
        /// trimmed of any surrounding whitespace.
        /// </returns>
        public string GetPrice() => Driver.FindElement(price).Text.Trim();

        /// <summary>
        /// Gets the product description.
        /// </summary>
        /// <returns>
        /// The full textual description of the product, trimmed of whitespace.
        /// </returns>
        public string GetDescription() => Driver.FindElement(description).Text.Trim();

        /// <summary>
        /// Checks whether the detail page is loaded properly.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the product title element is displayed; otherwise <c>false</c>.
        /// This is used as a minimal indicator that the correct page has been reached.
        /// </returns>
        public bool IsLoaded()
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(title));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
