// <copyright file="DashboardProducts.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Pages
{
    using System.Globalization;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Component for interacting with products on the dashboard page.
    /// </summary>
    public class DashboardProducts : BasePage
    {
        public DashboardProducts(IWebDriver driver) 
            : base(driver)
        {
        }

        // === LOCATORS ===
        private readonly By productCards = By.CssSelector(".inventory_item");
        private readonly By productNames = By.CssSelector(".inventory_item_name");
        private readonly By productPrices = By.CssSelector(".inventory_item_price");
        private readonly By productImages = By.CssSelector(".inventory_item_img img");

        private readonly By sortDropdown = By.CssSelector(".product_sort_container");

        // === PRODUCT ACTIONS ===

        /// <summary>
        /// Gets the product name (title text) at the specified index.
        /// Useful for validating detail page content.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <returns>Product name as shown on the dashboard.</returns>
        public string GetNameByIndex(int index)
        {
            return FindAll(productNames).ToList()[index].Text.Trim();
        }

        /// <summary>
        /// Clicks a product image by its index in the product list.
        /// </summary>
        /// <param name="index">Zero-based index of the product image to click.</param>
        public void ClickImageByIndex(int index)
        {
            var elements = FindAll(productImages).ToList();
            elements[index].Click();
        }

        /// <summary>
        /// Clicks a product title by its index in the product list.
        /// </summary>
        /// <param name="index">Zero-based index of the product to click.</param>
        public void ClickTitleByIndex(int index)
        {
            var elements = FindAll(productNames).ToList();
            elements[index].Click();
        }

        /// <summary>
        /// Returns product prices as decimals (USD-aware).
        /// </summary>
        /// <returns>Returns list of decimal values.</returns>
        public List<decimal> GetPricesAsDecimal()
        {
            var elements = FindAll(productPrices);
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
        /// Gets all product cards.
        /// </summary>
        /// <returns>List of product card IWebElement objects.</returns>
        public IList<IWebElement> GetAllCards() => FindAll(productCards).ToList();

        /// <summary>
        /// Gets all product names.
        /// </summary>
        /// <returns>List of all product names.</returns>
        public IList<string> GetAllNames() =>
            FindAll(productNames).Select(e => e.Text).ToList();

        /// <summary>
        /// Gets all product prices.
        /// </summary>
        /// <returns>List of all product prices.</returns>
        public IList<string> GetAllPrices() =>
            FindAll(productPrices).Select(e => e.Text).ToList();

        /// <summary>
        /// Gets all product image elements.
        /// </summary>
        /// <returns>List of all product images.</returns>
        public IReadOnlyCollection<IWebElement> GetAllImages() =>
            FindAll(productImages);

        /// <summary>
        /// Returns all product images after ensuring they are fully loaded.
        /// </summary>
        /// <returns>Collection of fully loaded image elements.</returns>
        public IReadOnlyCollection<IWebElement> GetAllImagesLoaded()
        {
            var images = GetAllImages();

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            foreach (var img in images)
            {
                wait.Until(driver =>
                {
                    var js = (IJavaScriptExecutor)driver;

                    var result = js.ExecuteScript(
                        "return arguments[0].complete && arguments[0].naturalWidth > 0",
                        img);

                    return result as bool? == true;
                });
            }

            return images;
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
            var dropdown = Find(sortDropdown);
            var select = new SelectElement(dropdown);
            select.SelectByText(visibleText);
        }
    }
}