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
        private static readonly By UsernameInput = By.CssSelector("#user-name");
        private static readonly By PasswordInput = By.CssSelector("#password");
        private static readonly By LoginButton = By.CssSelector("#login-button");
        private static readonly By ErrorMessage = By.CssSelector("h3[data-test='error']");

        // === ACTIONS ===
        public void Open()
        {
            NavigateTo(TestConfig.BaseUrl);
            
            Wait.Until(d => d.FindElement(LoginButton).Displayed);
        }
        
        public void EnterUsername(string username)
        {
            TypeText(UsernameInput, username);
        }
        
        public void EnterPassword(string password)
        {
            TypeText(PasswordInput, password);
        }
        
        public void ClearUsername()
        {
            ClearField(UsernameInput);
        }
        
        public void ClearPassword()
        {
            ClearField(PasswordInput);
        }
        
        public void ClickLogin()
        {
            Click(LoginButton);
        }

        public string GetErrorMessage()
        {
            return GetElementText(ErrorMessage);
        }
        
        public bool IsLoaded()
        {
            return Driver.Url.Contains("saucedemo.com") &&
                   Driver.FindElements(LoginButton).Any();
        }
    }
}
#pragma warning restore SA1600