// <copyright file="DashboardPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Pages
{
    using OpenQA.Selenium;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Page Object Model for the post-login Dashboard page ("Swag Labs").
    /// Validates successful login by checking for known elements or title.
    /// </summary>
    public class DashboardPage : BasePage
    {
        // === LOCATORS ===
        private readonly By appLogo = By.CssSelector(".app_logo"); // Common identifier on the dashboard

        /// <summary>
        /// Checks if the user is on the Swag Labs dashboard by verifying the logo is visible.
        /// </summary>
        /// <returns>True if the dashboard logo is visible; otherwise, false.</returns>
        public bool IsAtDashboard()
        {
            return this.IsElementDisplayed(this.appLogo);
        }

        /// <summary>
        /// Gets the current page title.
        /// </summary>
        /// <returns>The title of the current browser page.</returns>
        public string GetPageTitle()
        {
            return this.Driver.Title;
        }
    }
}