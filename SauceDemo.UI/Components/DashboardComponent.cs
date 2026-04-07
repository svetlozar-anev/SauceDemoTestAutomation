// <copyright file="DashboardComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Components
{
    using OpenQA.Selenium;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Page Object Model for the Dashboard page ("Swag Labs").
    /// </summary>
    public class DashboardComponent : BaseComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardComponent"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver.</param>
        public DashboardComponent(IWebDriver driver) 
            : base(driver)
        {
            Header = new HeaderComponent(driver);
            Menu = new DashboardMenu(driver);
            Products = new DashboardProducts(driver);
        }
        
        /// <summary>
        /// Gets the header component for the dashboard.
        /// </summary>
        public HeaderComponent Header { get; }

        /// <summary>
        /// Gets the navigation menu component of the dashboard.
        /// </summary>
        public DashboardMenu Menu { get; }
        
        /// <summary>
        /// Gets the products section component of the dashboard.
        /// </summary>
        public DashboardProducts Products { get; }

        // === CONSTANTS ===
        private const string InventoryUrlFragment = "inventory.html";
        private const string InventoryItems = "inventory_item";

        // === LOCATORS ===
        private readonly By appLogo = By.CssSelector(".app_logo");
        private readonly By cartBadge = By.CssSelector(".shopping_cart_badge");

        // === PAGE ACTIONS ===

        /// <summary>
        /// Opens the dashboard page.
        /// </summary>
        /// <returns>The loaded <see cref="DashboardComponent"/>.</returns>
        public DashboardComponent Open()
        {
            NavigateTo(TestConfig.BaseUrl + "/inventory.html");
            return this;
        }

        /// <summary>
        /// Waits until the dashboard page is fully loaded.
        /// Checks that the logo is visible, product cards exist, and each card has a name, price, and image.
        /// </summary>
        /// <param name="timeoutSeconds">Maximum wait time in seconds (default 10).</param>
        public void WaitForDashboardToLoad(int timeoutSeconds = 10)
        {
            Wait.Until(d =>
                Find(appLogo).Displayed &&
                Products.GetAllCards().Any());
        }
        
        /// <summary>
        /// Checks if the user is on the Swag Labs dashboard by verifying the logo is visible.
        /// </summary>
        /// <returns>True if the dashboard logo is visible; otherwise, false.</returns>
        public bool IsAtDashboard()
        {
            return IsElementDisplayed(appLogo);
        }
    }
}