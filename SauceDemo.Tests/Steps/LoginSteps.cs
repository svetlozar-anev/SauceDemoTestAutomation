// <copyright file="LoginSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Steps
{
    using FluentAssertions;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Pages;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Step definitions for login feature scenarios, implementing user actions and verifications.
    /// </summary>
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage loginPage;
        private readonly DashboardPage dashboardPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSteps"/> class.
        /// Instantiates page objects used in the login scenarios.
        /// </summary>
        public LoginSteps()
        {
            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
        }

        /// <summary>
        /// Navigates the browser to the login page URL specified in configuration.
        /// </summary>
        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            WebDriverFactory.Driver.Navigate().GoToUrl(TestConfig.BaseUrl);
        }

        /// <summary>
        /// Enters the specified username into the username input field.
        /// </summary>
        /// <param name="username">The username to enter.</param>
        [Given(@"I enter ""(.*)"" in the username field")]
        public void GivenIEnterInTheUsernameField(string username)
        {
            Logger.Log.Information("Entering username: {Username}", username);
            loginPage.EnterUsername(username);
        }

        /// <summary>
        /// Enters the specified password into the password input field.
        /// </summary>
        /// <param name="password">The password to enter.</param>
        [Given(@"I enter ""(.*)"" in the password field")]
        public void GivenIEnterInThePasswordField(string password)
        {
            Logger.Log.Information("Entering password: [PROTECTED]");
            loginPage.EnterPassword(password);
        }

        /// <summary>
        /// Clears the username input field.
        /// </summary>
        [Given(@"I clear the username field")]
        public void GivenIClearTheUsernameField()
        {
            loginPage.ClearUsername();
        }

        /// <summary>
        /// Clears the password input field.
        /// </summary>
        [Given(@"I clear the password field")]
        public void GivenIClearThePasswordField()
        {
            loginPage.ClearPassword();
        }

        /// <summary>
        /// Clicks the login button to submit the login form.
        /// </summary>
        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            Logger.Log.Information("Clicking the login button");
            loginPage.ClickLogin();
        }

        /// <summary>
        /// Asserts that the displayed error message matches the expected message.
        /// </summary>
        /// <param name="expectedMessage">The expected error message text.</param>
        [Then(@"I should see the error message ""(.*)""")]
        public void ThenIShouldSeeTheErrorMessage(string expectedMessage)
        {
            Logger.Log.Information("Verifying error message: {ExpectedMessage}", expectedMessage);
            loginPage.GetErrorMessage().Should().Be(expectedMessage);
        }

        /// <summary>
        /// Asserts that the user has been redirected to the dashboard page.
        /// </summary>
        [Then(@"I should be redirected to the dashboard")]
        public void ThenIShouldBeRedirectedToTheDashboard()
        {
            Logger.Log.Information("Verifying redirection to dashboard");
            dashboardPage.IsAtDashboard().Should().BeTrue();
        }

        /// <summary>
        /// Asserts that the page title matches the expected title.
        /// </summary>
        /// <param name="title">The expected page title.</param>
        [Then(@"the page title should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string title)
        {
            dashboardPage.GetPageTitle().Should().Be(title);
        }
    }
}
