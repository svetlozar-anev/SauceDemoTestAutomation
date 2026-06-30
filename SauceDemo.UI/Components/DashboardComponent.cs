// <copyright file="DashboardComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.UI.Components
{
    using OpenQA.Selenium;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Base;

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
        
        public HeaderComponent Header { get; }
        
        public DashboardMenu Menu { get; }
        
        public DashboardProducts Products { get; }

        // === CONSTANTS ===
        private const string InventoryUrlFragment = "inventory.html";
        private const string InventoryItems = "inventory_item";

        // === LOCATORS ===
        private readonly By appLogo = By.CssSelector(".app_logo");
        private readonly By cartBadge = By.CssSelector(".shopping_cart_badge");

        // === PAGE ACTIONS ===
        public DashboardComponent Open()
        {
            NavigateTo(TestConfig.BaseUrl + "/inventory.html");
            return this;
        }
        
        public void WaitForDashboardToLoad(int timeoutSeconds = 10)
        {
            Wait.Until(d =>
                Find(appLogo).Displayed &&
                Products.GetAllCards().Any());
        }
        
        public bool IsAtDashboard()
        {
            return IsElementDisplayed(appLogo);
        }
    }
}
#pragma warning restore SA1600