// <copyright file="LoginTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Tests
{
    using FluentAssertions;
    using SauceDemo.Tests.Pages;
    using SauceDemo.Tests.Utilities;

    /// <summary>
    /// Contains automated UI tests related to login functionality for SauceDemo.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class LoginTests
    {
        private LoginPage? loginPage;
        private DashboardPage? dashboardPage;

        /// <summary>
        /// Initializes the WebDriver and page objects before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            WebDriverFactory.InitDriver(Config.TestConfig.Browser);
            this.loginPage = new LoginPage();
            this.dashboardPage = new DashboardPage();
            this.loginPage.Open();
        }

        /// <summary>
        /// Cleans up the WebDriver after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            WebDriverFactory.QuitDriver();
        }

        /// <summary>
        /// Verifies that submitting the login form with both username and password empty
        /// shows the appropriate 'Username is required' error message.
        /// </summary>
        [Test]
        [Description("UC-1: Login attempt with empty credentials shows 'Username is required' error")]
        public void Login_WithEmptyCredentials_ShowsUsernameRequiredError()
        {
            this.loginPage?.EnterUsername("standard_user");
            this.loginPage?.EnterPassword("secret_sauce");
            this.loginPage?.ClearUsername();
            this.loginPage?.ClearPassword();

            this.loginPage?.ClickLogin();

            this.loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username is required");
        }

        /// <summary>
        /// Verifies that submitting the login form without a password
        /// shows the appropriate 'Password is required' error message.
        /// </summary>
        [Test]
        [Description("UC-2: Login attempt without password shows 'Password is required' error")]
        public void Login_WithMissingPassword_ShowsPasswordRequiredError()
        {
            this.loginPage?.EnterUsername("standard_user");
            this.loginPage?.EnterPassword("secret_sauce");
            this.loginPage?.ClearPassword();

            this.loginPage?.ClickLogin();

            this.loginPage?.GetErrorMessage().Should().Be("Epic sadface: Password is required");
        }

        /// <summary>
        /// Verifies that valid login credentials navigate the user to the dashboard page.
        /// </summary>
        /// <param name="username">The username to be used for login.</param>
        /// <param name="password">The password to be used for login.</param>
        [Test]
        [TestCase("standard_user", "secret_sauce")]
        [TestCase("problem_user", "secret_sauce")]
        [TestCase("performance_glitch_user", "secret_sauce")]
        [Description("UC-3: Successful login with accepted credentials navigates to dashboard")]
        public void Login_WithValidCredentials_NavigatesToDashboard(string username, string password)
        {
            this.loginPage?.Login(username, password);

            this.dashboardPage?.IsAtDashboard().Should().BeTrue("the user should be redirected to the dashboard");
            this.dashboardPage?.GetPageTitle().Should().Be("Swag Labs");
        }
    }
}
