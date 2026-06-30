// <copyright file="DashboardMenu.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.UI.Components
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SauceDemo.UI.Base;
    
    /// <summary>
    /// Represents the dashboard side menu (burger menu) in the SauceDemo application.
    /// Provides methods to open the menu and perform actions such as logging out.
    /// </summary>
    public class DashboardMenu : BaseComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardMenu"/> class.
        /// </summary>
        /// <param name="driver">The WebDriver instance used to interact with the page.</param>
        public DashboardMenu(IWebDriver driver) 
            : base(driver)
        {
        }

        // === LOCATORS ===
        private readonly By menuButton = By.Id("react-burger-menu-btn");
        private readonly By logoutLink = By.Id("logout_sidebar_link");
        private readonly By menuItems = By.CssSelector(".bm-item.menu-item");
        private readonly By aboutLink = By.Id("about_sidebar_link");

        public void Open()
        {
            Click(menuButton);
            Wait.Until(d => d.FindElements(menuItems).Count > 0);
        }

        public bool IsMenuVisible(int timeoutSeconds = 5)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
                return wait.Until(_ =>
                {
                    var elements = Driver.FindElements(menuButton); // reuse the existing menuButton locator
                    return elements.Count > 0 && elements[0].Displayed;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public IList<string> GetOptions()
        {
            Wait.Until(d =>
                d.FindElements(menuItems).Count >= 4);

            return Driver.FindElements(menuItems)
                .Select(e => e.Text.Trim())
                .ToList();
        }

        public WebDriverWait WaitAboutToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => GetOptions().Contains("About"));
            return wait;
        }
        
        public void ClickAbout()
        {
            var element = Wait.Until(d => d.FindElement(aboutLink));
            element.Click();
        }
        
        public void Logout()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            var logoutButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.Id("logout_sidebar_link")));

            Driver.FindElement(By.Id("logout_sidebar_link")).Click();
        }
    }
}
#pragma warning restore SA1600