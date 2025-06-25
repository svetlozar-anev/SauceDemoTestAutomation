using OpenQA.Selenium;
using SauceDemo.Tests.Base;

namespace SauceDemo.Tests.Pages
{
    /// <summary>
    /// Page Object Model for the Login Page of saucedemo.com
    /// Contains locators and methods for interacting with login elements.
    /// </summary>
    public class LoginPage : BasePage
    {
        // === LOCATORS ===

        private readonly By UsernameInput = By.CssSelector("#user-name");
        private readonly By PasswordInput = By.CssSelector("#password");
        private readonly By LoginButton = By.CssSelector("#login-button");
        private readonly By ErrorMessage = By.CssSelector("h3[data-test='error']");

        // === ACTIONS ===

        /// <summary>
        /// Loads the login page.
        /// </summary>
        public void Open()
        {
            NavigateTo("https://www.saucedemo.com/");
            // DisableAutoFill();
           // DisableAutofillRobustly();
        }

        public void DisableAutofillRobustly()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            IWebElement usernameElement = Driver.FindElement(UsernameInput);
            IWebElement passwordElement = Driver.FindElement(PasswordInput);

            // JavaScript to first set autocomplete attributes, then clear with a slight delay
            // and dispatch events. This is a robust attempt to counter aggressive autofill.
            string script = @"
            var usernameField = arguments[0];
            var passwordField = arguments[1];

            // 1. Force autocomplete off (standard approach)
            usernameField.setAttribute('autocomplete', 'off');
            passwordField.setAttribute('autocomplete', 'new-password'); // More specific for password fields

            // 2. Clear values immediately
            usernameField.value = '';
            passwordField.value = '';

            // 3. Add a small delay and then re-clear and dispatch events
            //    This helps if autofill re-populates very quickly after initial load
            setTimeout(function() {
                usernameField.value = '';
                passwordField.value = '';

                // Dispatch 'change' and 'blur' events to notify browser of changes
                // This can sometimes trigger autofill to re-evaluate or clear itself.
                usernameField.dispatchEvent(new Event('change', { bubbles: true }));
                usernameField.dispatchEvent(new Event('blur', { bubbles: true }));
                passwordField.dispatchEvent(new Event('change', { bubbles: true }));
                passwordField.dispatchEvent(new Event('blur', { bubbles: true }));
            }, 100); // 100ms delay
        ";

            js.ExecuteScript(script, usernameElement, passwordElement);

            // Optional: A small C# side sleep after the JS executes to ensure
            // the browser has processed everything.
            Thread.Sleep(200); // Wait a bit for the script to finish
        }

        /// <summary>
        /// Enters the username into the input field.
        /// </summary>
        public void EnterUsername(string username)
        {
            TypeText(UsernameInput, username);
        }

        /// <summary>
        /// Enters the password into the input field.
        /// </summary>
        public void EnterPassword(string password)
        {
            TypeText(PasswordInput, password);
        }

        /// <summary>
        /// Clears Username field.
        /// </summary>
        public void ClearUsername()
        {
            ClearField(UsernameInput);
        }

        /// <summary>
        /// Clears Password field.
        /// </summary>
        public void ClearPassword()
        {
            ClearField(PasswordInput);
        }

        /// <summary>
        /// Clicks the Login button.
        /// </summary>
        public void ClickLogin()
        {
            Click(LoginButton);
        }

        /// <summary>
        /// Gets the error message text displayed after a failed login.
        /// </summary>
        public string GetErrorMessage()
        {
            return GetElementText(ErrorMessage);
        }

        /// <summary>
        /// Fills in username and password and clicks login.
        /// </summary>
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        /// <summary>
        /// Uses JavaScript to forcibly clear inputs, defeating autofill.
        /// </summary>
        public void ForceClearWithJS()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            js.ExecuteScript("document.querySelector('#user-name').value = '';");
            js.ExecuteScript("document.querySelector('#password').value = '';");
        }

        // Disable browser autocomplete and autofill on inputs using JS
        public void DisableAutoFill()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            IWebElement passwordElement = Driver.FindElement(PasswordInput);
            js.ExecuteScript("arguments[0].setAttribute('readonly', 'readonly');", passwordElement);
            js.ExecuteScript("arguments[0].value = '';", passwordElement);
            js.ExecuteScript("arguments[0].removeAttribute('readonly');", passwordElement);

        }
    }
}
