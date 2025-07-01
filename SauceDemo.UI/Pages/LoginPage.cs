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
        private readonly By usernameInput = By.Id("user-name");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By errorMessage = By.CssSelector("h3[data-test='error']");

        // === ACTIONS ===

        /// <summary>
        /// Loads the login page.
        /// </summary>
        public void Open()
        {
            NavigateTo(TestConfig.BaseUrl);
        }

        /// <summary>
        /// Enters the username into the input field.
        /// </summary>
        /// <param name="username">The username to be entered.</param>
        public void EnterUsername(string username)
        {
            TypeText(usernameInput, username);
        }

        /// <summary>
        /// Enters the password into the input field.
        /// </summary>
        /// <param name="password">The password to be entered.</param>
        public void EnterPassword(string password)
        {
            TypeText(passwordInput, password);
        }

        /// <summary>
        /// Clears Username field.
        /// </summary>
        public void ClearUsername()
        {
            ClearField(usernameInput);
        }

        /// <summary>
        /// Clears Password field.
        /// </summary>
        public void ClearPassword()
        {
            ClearField(passwordInput);
        }

        /// <summary>
        /// Clicks the Login button.
        /// </summary>
        public void ClickLogin()
        {
            Click(loginButton);
        }

        /// <summary>
        /// Gets the error message text displayed after a failed login.
        /// </summary>
        /// <returns>The error message text shown on the login page.</returns>
        public string GetErrorMessage()
        {
            return GetElementText(errorMessage);
        }

        /// <summary>
        /// Fills in username and password and clicks login.
        /// </summary>
        /// <param name="username">The username to be entered.</param>
        /// <param name="password">The password to be entered.</param>
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }
    }
}
