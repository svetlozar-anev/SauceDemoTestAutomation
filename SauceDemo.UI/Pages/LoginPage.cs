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
        /// <returns>
        /// An instance of <see cref="LoginPage"/> representing the loaded login page.
        /// </returns>
        public LoginPage OpenLoginPage()
        {
         NavigateTo(TestConfig.BaseUrl);
         return new LoginPage();
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

        /// <summary>
        /// Logs in using the specified user's credentials and navigates to the dashboard page.
        /// </summary>
        /// <param name="user">The <see cref="User"/> containing the username and password to use for login.</param>
        /// <returns>
        /// An instance of <see cref="DashboardPage"/> representing the dashboard after a successful login.
        /// </returns>
        public DashboardPage LoginAs(User user)
        {
            EnterUsername(user.username);
            EnterPassword(user.password);
            ClickLogin();

            return new DashboardPage();
        }

        public record User(string username, string password)
        {
            public static User Standard => new ("standard_user", "secret_sauce");
        }
    }
}
