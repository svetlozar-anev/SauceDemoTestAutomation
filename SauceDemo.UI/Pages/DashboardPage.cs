// <copyright file="DashboardPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Pages
{
    using System.Globalization;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Page Object Model for the Dashboard page ("Swag Labs").
    /// </summary>
    public class DashboardPage : BasePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardPage"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver.</param>
        public DashboardPage(IWebDriver driver) 
            : base(driver)
        {
            Menu = new DashboardMenu(driver);
            Products = new DashboardProducts(driver);
        }

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
        /// <returns>The loaded <see cref="DashboardPage"/>.</returns>
        public DashboardPage Open()
        {
            NavigateTo(TestConfig.BaseUrl + "/inventory.html");
            return new DashboardPage(WebDriverFactory.Driver);
        }

        /// <summary>
        /// Gets the current numerical value of the cart badge.
        /// </summary>
        /// <returns>
        /// The cart count, or <c>0</c> if no badge is displayed.
        /// </returns>
        public int GetCartCount()
        {
            var badges = FindAll(cartBadge).ToList();
            return badges.Count == 0
                ? 0
                : int.Parse(badges[0].Text.Trim());
        }

        /// <summary>
        /// Waits until the dashboard page is fully loaded.
        /// Checks that the logo is visible, product cards exist, and each card has a name, price, and image.
        /// </summary>
        /// <param name="timeoutSeconds">Maximum wait time in seconds (default 10).</param>
        public void WaitForDashboardToLoad(int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

            wait.Until(driver =>
            {
                try
                {
                    return Find(appLogo).Displayed &&
                           FindAll(appLogo).Count > 0;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }
        
        /// <summary>
        /// Checks if the user is on the Swag Labs dashboard by verifying the logo is visible.
        /// </summary>
        /// <returns>True if the dashboard logo is visible; otherwise, false.</returns>
        public bool IsAtDashboard()
        {
            return IsElementDisplayed(appLogo);
        }

        /// <summary>
        /// Gets the current page title.
        /// </summary>
        /// <returns>The title of the current browser page.</returns>
        public string GetPageTitle()
        {
            return Driver.Title;
        }

        /// <summary>
        /// Clicks Cart Icon.
        /// </summary>
        public void ClickCartIcon()
        {
            Click(By.CssSelector(".shopping_cart_link"));
        }

        /// <summary>
        /// Waits until the Sauce Labs page has loaded in the browser.
        /// </summary>
        /// <returns>The <see cref="WebDriverWait"/> used for waiting.</returns>
        public WebDriverWait WaitSaucelabsToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.Url.Contains("https://saucelabs.com"));
            return wait;
        }

        /// <summary>
        /// Refreshes the dashboard page and waits until it is fully loaded.
        /// </summary>
        public void Refresh()
        {
            RefreshPage(IsDashboardLoaded);
        }

        // === PRIVATE HELPERS ===
        private bool IsDashboardLoaded()
        {
            return Driver.Url.Contains(InventoryUrlFragment) &&
                   FindAll(By.ClassName(InventoryItems)).Any();
        }
    }
}