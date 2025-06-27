// <copyright file="LoginPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Pages
{
    using OpenQA.Selenium;
    using SauceDemo.Core.Config;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Page Object Model for the Login Page of saucedemo.com
    /// Contains locators and methods for interacting with login elements.
    /// </summary>
    public class LoginPage : BasePage
    {
        // === LOCATORS ===
        private readonly By usernameInput = By.CssSelector("#user-name");
        private readonly By passwordInput = By.CssSelector("#password");
        private readonly By loginButton = By.CssSelector("#login-button");
        private readonly By errorMessage = By.CssSelector("h3[data-test='error']");

        // === ACTIONS ===

        /// <summary>
        /// Loads the login page.
        /// </summary>
        public void Open()
        {
            this.NavigateTo(TestConfig.BaseUrl);
        }

        /// <summary>
        /// Enters the username into the input field.
        /// </summary>
        /// <param name="username">The username to be entered.</param>
        public void EnterUsername(string username)
        {
            this.TypeText(this.usernameInput, username);
        }

        /// <summary>
        /// Enters the password into the input field.
        /// </summary>
        /// <param name="password">The password to be entered.</param>
        public void EnterPassword(string password)
        {
            this.TypeText(this.passwordInput, password);
        }

        /// <summary>
        /// Clears Username field.
        /// </summary>
        public void ClearUsername()
        {
            this.ClearField(this.usernameInput);
        }

        /// <summary>
        /// Clears Password field.
        /// </summary>
        public void ClearPassword()
        {
            this.ClearField(this.passwordInput);
        }

        /// <summary>
        /// Clicks the Login button.
        /// </summary>
        public void ClickLogin()
        {
            this.Click(this.loginButton);
        }

        /// <summary>
        /// Gets the error message text displayed after a failed login.
        /// </summary>
        /// <returns>The error message text shown on the login page.</returns>
        public string GetErrorMessage()
        {
            return this.GetElementText(this.errorMessage);
        }

        /// <summary>
        /// Fills in username and password and clicks login.
        /// </summary>
        /// <param name="username">The username to be entered.</param>
        /// <param name="password">The password to be entered.</param>
        public void Login(string username, string password)
        {
            this.EnterUsername(username);
            this.EnterPassword(password);
            this.ClickLogin();
        }
    }
}
