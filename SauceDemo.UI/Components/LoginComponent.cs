// <copyright file="LoginComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.UI.Components
{
    using OpenQA.Selenium;
    using SauceDemo.Core.Config;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Page Object Model for the Login Page of saucedemo.com.
    /// Provides methods for interacting with login form elements and performing authentication.
    /// </summary>
    public class LoginComponent : BaseComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginComponent"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance to be used by this page object.</param>
        public LoginComponent(IWebDriver driver) 
            : base(driver)
        {
        }

        // === LOCATORS ===
        private readonly By usernameInput = By.CssSelector("#user-name");
        private readonly By passwordInput = By.CssSelector("#password");
        private readonly By loginButton = By.CssSelector("#login-button");
        private readonly By errorMessage = By.CssSelector("h3[data-test='error']");

        // === ACTIONS ===
        public void Open()
        {
            NavigateTo(TestConfig.BaseUrl);
            
            Wait.Until(d => d.FindElement(loginButton).Displayed);
        }
        
        public void EnterUsername(string username)
        {
            TypeText(usernameInput, username);
        }
        
        public void EnterPassword(string password)
        {
            TypeText(passwordInput, password);
        }
        
        public void ClearUsername()
        {
            ClearField(usernameInput);
        }
        
        public void ClearPassword()
        {
            ClearField(passwordInput);
        }
        
        public void ClickLogin()
        {
            Click(loginButton);
        }
        
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }
        
        public DashboardComponent LoginAs(string username, string password)
        {
            Login(username, password);
            return new DashboardComponent(Driver);
        }

        public string GetErrorMessage()
        {
            return GetElementText(errorMessage);
        }
        
        public bool IsLoaded()
        {
            return Driver.Url.Contains("saucedemo.com") &&
                   Driver.FindElements(loginButton).Any();
        }
    }
}
#pragma warning restore SA1600