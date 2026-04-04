// <copyright file="DashboardMenu.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SauceDemo.UI.Base;
    
    /// <summary>
    /// Represents the dashboard side menu (burger menu) in the SauceDemo application.
    /// Provides methods to open the menu and perform actions such as logging out.
    /// </summary>
    public class DashboardMenu : BasePage
    {
        public DashboardMenu(IWebDriver driver) 
            : base(driver)
        {
        }

        // === LOCATORS ===
        private readonly By menuButton = By.Id("react-burger-menu-btn");
        private readonly By logoutLink = By.Id("logout_sidebar_link");
        private readonly By menuItems = By.CssSelector(".bm-item.menu-item");
        private readonly By aboutLink = By.Id("about_sidebar_link");

        /// <summary>
        /// Opens the dashboard side menu by clicking the burger menu button
        /// and waits until the menu items are loaded and visible.
        /// </summary>
        public void Open()
        {
            Click(menuButton);
            Wait.Until(d => d.FindElements(menuItems).Count > 0);
        }

        /// <summary>
        /// Checks if the dashboard menu (burger menu) is currently visible on the page.
        /// </summary>
        /// <param name="timeoutSeconds">Maximum time to wait for the menu to appear.</param>
        /// <returns>True if the menu button is visible within the timeout.</returns>
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

        /// <summary>
        ///  Gets all the menu options in the dashboard menu.
        /// </summary>
        /// <return>Returns all the captured menu options in the dashboard.</return>
        public IList<string> GetOptions()
        {
            Wait.Until(d =>
                d.FindElements(menuItems).Count >= 4);

            return Driver.FindElements(menuItems)
                .Select(e => e.Text.Trim())
                .ToList();
        }

        /// <summary>
        /// Waits until the "About" menu option is visible.
        /// </summary>
        /// <returns>The <see cref="WebDriverWait"/> used for waiting.</returns>
        public WebDriverWait WaitAboutToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => GetOptions().Contains("About"));
            return wait;
        }

        /// <summary>
        /// Clicks the "About" link in the Dashboard menu.
        /// </summary>
        public void ClickAbout()
        {
            var element = Wait.Until(d => d.FindElement(aboutLink));
            element.Click();
        }

        /// <summary>
        /// Opens the side menu and then clicks the logout link to log the current user out.
        /// </summary>
        public void Logout()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));

            var logoutButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.Id("logout_sidebar_link")));

            Driver.FindElement(By.Id("logout_sidebar_link")).Click();
        }
    }
}