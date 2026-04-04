// <copyright file="LoginPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Pages
{
    using OpenQA.Selenium;
    using SauceDemo.Core.Config;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Page Object Model for the Login Page of saucedemo.com.
    /// Provides methods for interacting with login form elements and performing authentication.
    /// </summary>
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        // === LOCATORS ===
        private readonly By usernameInput = By.CssSelector("#user-name");
        private readonly By passwordInput = By.CssSelector("#password");
        private readonly By loginButton   = By.CssSelector("#login-button");
        private readonly By errorMessage  = By.CssSelector("h3[data-test='error']");

        // === ACTIONS ===

        /// <summary>
        /// Navigates to the SauceDemo login page and waits until the page is fully loaded.
        /// </summary>
        public void Open()
        {
            NavigateTo(TestConfig.BaseUrl);
            
            // Wait for the login button to be visible to ensure the page is interactive
            Wait.Until(d => d.FindElement(loginButton).Displayed);
        }

        /// <summary>
        /// Enters the specified username into the username field.
        /// </summary>
        /// <param name="username">The username to enter.</param>
        public void EnterUsername(string username)
        {
            TypeText(usernameInput, username);
        }

        /// <summary>
        /// Enters the specified password into the password field.
        /// </summary>
        /// <param name="password">The password to enter.</param>
        public void EnterPassword(string password)
        {
            TypeText(passwordInput, password);
        }

        /// <summary>
        /// Clears the username input field.
        /// </summary>
        public void ClearUsername()
        {
            ClearField(usernameInput);
        }

        /// <summary>
        /// Clears the password input field.
        /// </summary>
        public void ClearPassword()
        {
            ClearField(passwordInput);
        }

        /// <summary>
        /// Clicks the Login button to submit the form.
        /// </summary>
        public void ClickLogin()
        {
            Click(loginButton);
        }

        /// <summary>
        /// Performs the complete login flow with the given credentials.
        /// This method stays on the Login page after clicking the button.
        /// Useful for negative test scenarios or when verifying login page behavior.
        /// </summary>
        /// <param name="username">The username to use for login.</param>
        /// <param name="password">The password to use for login.</param>
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        /// <summary>
        /// Performs login using the provided credentials and returns the DashboardPage.
        /// This is the most commonly used method for happy-path scenarios.
        /// </summary>
        /// <param name="username">The username to use for login.</param>
        /// <param name="password">The password to use for login.</param>
        /// <returns>The <see cref="DashboardPage"/> after successful login.</returns>
        public DashboardPage LoginAs(string username, string password)
        {
            Login(username, password);
            return new DashboardPage(Driver);
        }

        /// <summary>
        /// Gets the error message text displayed after a failed login attempt.
        /// </summary>
        /// <returns>The error message text shown on the page.</returns>
        public string GetErrorMessage()
        {
            return GetElementText(errorMessage);
        }

        /// <summary>
        /// Checks whether the login page is currently displayed.
        /// </summary>
        /// <returns>True if the login page is loaded and the login button is present; otherwise, false.</returns>
        public bool IsLoaded()
        {
            return Driver.Url.Contains("saucedemo.com") &&
                   Driver.FindElements(loginButton).Any();
        }
    }
}