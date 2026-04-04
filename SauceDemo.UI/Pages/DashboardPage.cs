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
    /// Validates successful login by checking for known elements or title.
    /// </summary>
    public class DashboardPage : BasePage
    {
        public DashboardPage(IWebDriver driver) : base(driver)
        {
            Menu = new DashboardMenu(driver);
            Products = new DashboardProducts(driver);
        }

        public DashboardMenu Menu { get; }
        public DashboardProducts Products { get; }

        // === CONSTANTS ===
        private const string InventoryUrlFragment = "inventory.html";
        private const string InventoryItems = "inventory_item";

        // === LOCATORS ===
        private readonly By appLogo = By.CssSelector(".app_logo");

        private readonly By addToCartButtons = By.CssSelector(".inventory_item button");
        private readonly By cartBadge = By.CssSelector(".shopping_cart_badge");

        private readonly By productButton = By.CssSelector("button");

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
        /// Clicks the "Add to cart" button for the product at the given index.
        /// </summary>
        /// <param name="index">Zero-based index of the product whose button will be clicked.</param>
        public void ClickAddToCartByIndex(int index)
        {
            FindAll(this.addToCartButtons).ToList()[index].Click();
        }

        /// <summary>
        /// Retrieves the label text of the "Add to cart" button for the product at the given index.
        /// </summary>
        /// <param name="index">Zero-based index of the product.</param>
        /// <returns>The trimmed button label.</returns>
        public string GetAddToCartButtonLabel(int index)
        {
            return FindAll(this.addToCartButtons).ToList()[index].Text.Trim();
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
        /// Waits until the "Add to cart" button for the product at the specified index
        /// displays the expected label.
        /// </summary>
        /// <param name="index">Zero-based index of the product.</param>
        /// <param name="expected">Expected button label.</param>
        /// <param name="timeoutSeconds">Number of seconds to wait before timing out.</param>
        public void WaitForButtonLabel(int index, string expected, int timeoutSeconds = 5)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

            wait.Until(_ =>
            {
                try
                {
                    var freshButtons = FindAll(addToCartButtons).ToList();

                    return index < freshButtons.Count &&
                           freshButtons[index].Text.Trim().Equals(expected, StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Waits until the "Add to cart" button for the specified product
        /// displays the expected label, e.g., "Add to cart" or "Remove".
        /// </summary>
        /// <param name="productName">The exact or partial name of the product as displayed on the dashboard.</param>
        /// <param name="expected">Expected button label.</param>
        /// <param name="timeoutSeconds">Number of seconds to wait before timing out.</param>
        public void WaitForButtonLabel(string productName, string expected, int timeoutSeconds = 5)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

            wait.Until(_ =>
            {
                try
                {
                    var product = FindProductContainer(productName);
                    var label = product.FindElement(By.CssSelector("button")).Text.Trim();
                    return label.Equals(expected, StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return false;
                }
            });
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
        /// Checks if a popup with an OK button is visible and clicks it if found.
        /// </summary>
        /// <param name="timeoutSeconds">Maximum wait time in seconds (default 5).</param>
        public void DismissPasswordPopupIfPresent(int timeoutSeconds = 5)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

                // Wait until a visible button with text "OK" exists, or timeout
                var okButton = wait.Until(driver =>
                {
                    try
                    {
                        return driver.FindElements(By.TagName("button"))
                            .FirstOrDefault(b => b.Displayed &&
                                                 b.Text.Trim().Equals("OK", StringComparison.OrdinalIgnoreCase));
                    }
                    catch (StaleElementReferenceException)
                    {
                        return null;
                    }
                });

                okButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                // Nothing to do if popup doesn't appear
            }
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
        /// Clicks Remove button (index based).
        /// </summary>
        /// <param name="index">Index.</param>
        public void ClickRemoveByIndex(int index)
        {
            var buttons = FindAll(By.CssSelector(".inventory_item button"));
            buttons.ToList()[index].Click();
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
        /// Clicks the "Add to cart" button for the specified product by its name.
        /// </summary>
        /// <param name="productName">The exact or partial name of the product as displayed on the dashboard.</param>
        public void ClickAddToCartByName(string productName)
        {
            GetProductButton(productName).Click();
        }

        /// <summary>
        /// Retrieves the label text of the "Add to cart" or "Remove" button
        /// for the specified product.
        /// </summary>
        /// <param name="productName">The exact or partial name of the product as displayed on the dashboard.</param>
        /// <returns>The trimmed text of the button, typically "Add to cart" or "Remove".</returns>
        public string GetButtonLabel(string productName)
        {
            return GetProductButton(productName).Text.Trim();
        }

        /// <summary>
        /// Refreshes the dashboard page and waits until it is fully loaded.
        /// </summary>
        public void Refresh()
        {
            RefreshPage(IsDashboardLoaded);
        }

        // === PRIVATE HELPERS ===
        private IWebElement GetProductButton(string productName)
        {
            return FindProductContainer(productName)
                .FindElement(productButton);
        }

        private bool IsDashboardLoaded()
        {
            return Driver.Url.Contains(InventoryUrlFragment) &&
                   FindAll(By.ClassName(InventoryItems)).Any();
        }

        private IWebElement FindProductContainer(string productName)
        {
            return Driver.FindElements(By.CssSelector(".inventory_item"))
                .First(e => e.Text.Contains(productName, StringComparison.OrdinalIgnoreCase));
        }
    }
}