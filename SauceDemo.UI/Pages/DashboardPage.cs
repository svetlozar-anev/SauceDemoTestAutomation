// <copyright file="DashboardPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Pages
{
    using System.Globalization;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SauceDemo.Core.Config;
    using SauceDemo.UI.Base;
    
    /// <summary>
    /// Page Object Model for the Dashboard page ("Swag Labs").
    /// Validates successful login by checking for known elements or title.
    /// </summary>
    public class DashboardPage : BasePage
    {
        // === CONSTANTS ===
        private const string InventoryUrlFragment = "inventory.html";
        private const string InventoryItems = "inventory_item";
        
        // === LOCATORS ===
        private readonly By appLogo = By.CssSelector(".app_logo");
        private readonly By productCards = By.CssSelector(".inventory_item");
        private readonly By productNames = By.CssSelector(".inventory_item_name");
        private readonly By productPrices = By.CssSelector(".inventory_item_price");
        private readonly By productImages = By.CssSelector(".inventory_item_img img");
        private readonly By sortDropdown = By.CssSelector(".product_sort_container");

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
            return new DashboardPage();
        }

        /// <summary>
        /// Clicks the "Add to cart" button for the product at the given index.
        /// </summary>
        /// <param name="index">Zero-based index of the product whose button will be clicked.</param>
        public void ClickAddToCartByIndex(int index)
        {
            Driver.FindElements(addToCartButtons)[index].Click();
        }

        /// <summary>
        /// Retrieves the label text of the "Add to cart" button for the product at the given index.
        /// </summary>
        /// <param name="index">Zero-based index of the product.</param>
        /// <returns>The trimmed button label.</returns>
        public string GetAddToCartButtonLabel(int index)
        {
            return Driver.FindElements(addToCartButtons)[index].Text.Trim();
        }

        /// <summary>
        /// Gets the current numerical value of the cart badge.
        /// </summary>
        /// <returns>
        /// The cart count, or <c>0</c> if no badge is displayed.
        /// </returns>
        public int GetCartCount()
        {
            var badges = Driver.FindElements(cartBadge);
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
                    var freshButtons = Driver.FindElements(addToCartButtons);
                    
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
        /// Gets the product name (title text) at the specified index.
        /// Useful for validating detail page content.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <returns>Product name as shown on the dashboard.</returns>
        public string GetProductNameByIndex(int index)
        {
            return Driver.FindElements(productNames)[index].Text.Trim();
        }

        /// <summary>
        /// Clicks a product image by its index in the product list.
        /// </summary>
        /// <param name="index">Zero-based index of the product image to click.</param>
        public void ClickProductImageByIndex(int index)
        {
            var elements = Driver.FindElements(productImages);
            elements[index].Click();
        }

        /// <summary>
        /// Clicks a product title by its index in the product list.
        /// </summary>
        /// <param name="index">Zero-based index of the product to click.</param>
        public void ClickProductTitleByIndex(int index)
        {
            var elements = Driver.FindElements(productNames);
            elements[index].Click();
        }

        /// <summary>
        /// Returns product prices as decimals (USD-aware).
        /// </summary>
        /// <returns>Returns list of decimal values.</returns>
        public List<decimal> GetProductPricesAsDecimal()
        {
            var elements = Driver.FindElements(productPrices);
            var list = new List<decimal>(elements.Count);

            foreach (var element in elements)
            {
                var text = element.Text.Trim() ?? string.Empty;

                // Try parse as currency
                if (decimal.TryParse(
                        text,
                        NumberStyles.Currency,
                        CultureInfo.GetCultureInfo("en-US"),
                        out var value))
                {
                    list.Add(value);
                }
                else
                {
                    // push a negative value so test fails clearly (or throw)
                    throw new FormatException($"Unable to parse product price: '{text}'");
                }
            }

            return list;
        }

        /// <summary>
        /// Selects a sort option from the product sort dropdown.
        /// </summary>
        /// /// <param name="visibleText">
        /// The exact visible text of the dropdown option to select.
        /// Valid options include:
        /// - "Name (A to Z)"
        /// - "Name (Z to A)"
        /// - "Price (low to high)"
        /// - "Price (high to low)".
        /// </param>
        public void SelectSortOption(string visibleText)
        {
            // todo:
            var dropdown = Driver.FindElement(sortDropdown);
            var select = new SelectElement(dropdown);
            select.SelectByText(visibleText);
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
                    return driver.FindElement(appLogo).Displayed &&
                           driver.FindElements(productCards).Count > 0;
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
        /// Gets all product cards.
        /// </summary>
        /// <returns>List of product card IWebElement objects.</returns>
        public IList<IWebElement> GetAllProductCards() => Driver.FindElements(productCards);

        /// <summary>
        /// Gets all product names.
        /// </summary>
        /// <returns>List of all product names.</returns>
        public IList<string> GetAllProductNames() =>
            Driver.FindElements(productNames).Select(e => e.Text).ToList();

        /// <summary>
        /// Gets all product prices.
        /// </summary>
        /// <returns>List of all product prices.</returns>
        public IList<string> GetAllProductPrices() =>
            Driver.FindElements(productPrices).Select(e => e.Text).ToList();

        /// <summary>
        /// Gets all product image elements.
        /// </summary>
        /// <returns>List of all product images.</returns>
        public IList<IWebElement> GetAllProductImages() =>
            Driver.FindElements(productImages);

        /// <summary>
        /// Opens Burger menu.
        /// </summary>
        public void OpenMenu()
        {
            var menuButton = Driver.FindElement(By.Id("react-burger-menu-btn"));
            menuButton.Click();
        }

        /// <summary>
        /// Gets Menu Options.
        /// </summary>
        /// <returns>List of Menu Options.</returns>
        public IList<string> GetMenuOptions()
        {
               Wait.Until(d =>
                   d.FindElements(By.CssSelector(".bm-item.menu-item")).Count >= 4);
           
               return Driver.FindElements(By.CssSelector(".bm-item.menu-item"))
                            .Select(e => e.Text.Trim())
                            .ToList();
        }

        /// <summary>
        /// Clicks Remove button (index based).
        /// </summary>
        /// <param name="index">Index.</param>
        public void ClickRemoveByIndex(int index)
        {
            var buttons = Driver.FindElements(By.CssSelector(".inventory_item button"));
            buttons[index].Click();
        }

        /// <summary>
        /// Clicks Cart Icon.
        /// </summary>
        public void ClickCartIcon()
        {
            Driver.FindElement(By.CssSelector(".shopping_cart_link")).Click();
        }

        /// <summary>
        /// Clicks Logout.
        /// </summary>
        public void ClickLogout()
        {
            var locator = By.Id("logout_sidebar_link");

            var element = Wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return el.Displayed && el.Enabled ? el : null;
            });

            element.Click();
        }

        /// <summary>
        /// Waits until the "About" menu option is visible.
        /// </summary>
        /// <returns>The <see cref="WebDriverWait"/> used for waiting.</returns>
        public WebDriverWait WaitAboutToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => GetMenuOptions().Contains("About"));
            return wait;
        }

        /// <summary>
        /// Waits until the Sauce Labs page has loaded in the browser.
        /// </summary>
        /// <returns>The <see cref="WebDriverWait"/> used for waiting.</returns>
        public WebDriverWait WaitSauceDemoComToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.Url.Contains("https://www.saucedemo.com"));
            return wait;
        }

        /// <summary>
        /// Waits for images to load.
        /// </summary>
        public void WaitForImagesToLoad()
        {
            Wait.Until(driver =>
            {
                var images = driver.FindElements(By.CssSelector(".inventory_item_img img"));

                return images.All(img =>
                {
                    var result = ((IJavaScriptExecutor)driver)
                        .ExecuteScript("return arguments[0].complete && arguments[0].naturalWidth > 0", img);

                    return result is bool loaded && loaded;
                });
            });
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
        
        // === PRIVATE HELPERS ===
        private IWebElement GetProductButton(string productName)
        {
            return FindProductContainer(productName)
                .FindElement(productButton);
        }
        
        /// <summary>
        /// Locator for the "About" link in the sidebar menu.
        /// </summary>
        private readonly By aboutLink = By.Id("about_sidebar_link");

        /// <summary>
        /// Clicks the "About" link in the sidebar menu.
        /// </summary>
        public void ClickAbout()
        {
            var element = Wait.Until(d => d.FindElement(aboutLink));
            element.Click();
        }
        
        /// <summary>
        /// Refreshes the dashboard page and waits until it is fully loaded.
        /// </summary>
        public void Refresh()
        {
            RefreshPage(IsDashboardLoaded);
        }
        
        private bool IsDashboardLoaded()
        {
            return Driver.Url.Contains(InventoryUrlFragment) &&
                   Driver.FindElements(By.ClassName(InventoryItems)).Any();
        }

        private IWebElement FindProductContainer(string productName)
        {
            return Driver.FindElements(productCards)
                .First(e => e.Text.Contains(productName, StringComparison.OrdinalIgnoreCase));
        }
    }
}