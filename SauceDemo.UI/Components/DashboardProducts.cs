// <copyright file="DashboardProducts.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.UI.Components
{
    using System.Globalization;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Component for interacting with products on the dashboard page.
    /// </summary>
    public class DashboardProducts : BaseComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardProducts"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver.</param>
        public DashboardProducts(IWebDriver driver) 
            : base(driver)
        {
        }

        // === LOCATORS ===
        private readonly By productCards = By.CssSelector(".inventory_item");
        private readonly By productNames = By.CssSelector(".inventory_item_name");
        private readonly By productPrices = By.CssSelector(".inventory_item_price");
        private readonly By productImages = By.CssSelector(".inventory_item_img img");
        
        private readonly By addToCartButtons = By.CssSelector(".inventory_item button");

        private readonly By sortDropdown = By.CssSelector(".product_sort_container");

        // === PRODUCT ACTIONS ===
        public string GetNameByIndex(int index)
        {
            return FindAll(productNames).ToList()[index].Text.Trim();
        }
        
        public void ClickImageByIndex(int index)
        {
            var elements = FindAll(productImages).ToList();
            elements[index].Click();
        }
        
        public void ClickTitleByIndex(int index)
        {
            var elements = FindAll(productNames).ToList();
            elements[index].Click();
        }
        
        public void AddItemToCart(int index)
        {
            FindAll(addToCartButtons).ToList()[index].Click();
        }

        public void ClickRemoveByIndex(int index)
        {
            var buttons = FindAll(By.CssSelector(".inventory_item button"));
            buttons.ToList()[index].Click();
        }
        
        public string GetAddToCartButtonLabel(int index)
        {
            return FindAll(addToCartButtons).ToList()[index].Text.Trim();
        }
        
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

        public IList<IWebElement> GetAllCards() => FindAll(productCards).ToList();

        public IList<string> GetAllNames() =>
            FindAll(productNames).Select(e => e.Text).ToList();

        public IList<string> GetAllPrices() =>
            FindAll(productPrices).Select(e => e.Text).ToList();

        public IReadOnlyCollection<IWebElement> GetAllImages() =>
            FindAll(productImages);
        
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
        
        public void SelectSortOption(string visibleText)
        {
            var dropdown = Find(sortDropdown);
            var select = new SelectElement(dropdown);
            select.SelectByText(visibleText);
        }
        
        public void ClickAddToCartByName(string productName)
        {
            GetProductButton(productName).Click();
        }
        
        public string GetButtonLabel(string productName)
        {
            return GetProductButton(productName).Text.Trim();
        }
        
        public bool IsItemInCart(int index)
        {
            return GetAddToCartButtonLabel(index) == "Remove";
        }
        
        // === PRIVATE HELPERS ===
        private IWebElement GetProductButton(string productName)
        {
            return FindProductContainer(productName)
                .FindElement(By.CssSelector("button"));
        }
        
        private IWebElement FindProductContainer(string productName)
        {
            return Driver.FindElements(By.CssSelector(".inventory_item"))
                .First(e => e.Text.Contains(productName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
#pragma warning disable SA1600