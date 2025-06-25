using OpenQA.Selenium;
using SauceDemo.Tests.Base;

namespace SauceDemo.Tests.Pages
{
    /// <summary>
    /// Page Object Model for the post-login Dashboard page ("Swag Labs").
    /// Validates successful login by checking for known elements or title.
    /// </summary>
    public class DashboardPage : BasePage
    {
        // === LOCATORS ===

        private readonly By AppLogo = By.CssSelector(".app_logo"); // Common identifier on the dashboard

        /// <summary>
        /// Checks if the user is on the Swag Labs dashboard by verifying the logo is visible.
        /// </summary>
        public bool IsAtDashboard()
        {
            return IsElementDisplayed(AppLogo);
        }

        /// <summary>
        /// Gets the current page title.
        /// </summary>
        public string GetPageTitle()
        {
            return Driver.Title;
        }
    }
}
