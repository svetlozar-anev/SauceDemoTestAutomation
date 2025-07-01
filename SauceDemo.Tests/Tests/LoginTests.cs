// <copyright file="LoginTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Tests
{
    using FluentAssertions;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Pages;

    /// <summary>
    /// Contains automated UI tests related to login functionality for SauceDemo.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
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
            WebDriverFactory.InitDriver(TestConfig.Browser);
            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
            loginPage.Open();
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
            loginPage?.EnterUsername("standard_user");
            loginPage?.EnterPassword("secret_sauce");
            loginPage?.ClearUsername();
            loginPage?.ClearPassword();

            loginPage?.ClickLogin();

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username is required");
        }

        /// <summary>
        /// Verifies that submitting the login form without a password
        /// shows the appropriate 'Password is required' error message.
        /// </summary>
        [Test]
        [Description("UC-2: Login attempt without password shows 'Password is required' error")]
        public void Login_WithMissingPassword_ShowsPasswordRequiredError()
        {
            loginPage?.EnterUsername("standard_user");
            loginPage?.EnterPassword("secret_sauce");
            loginPage?.ClearPassword();

            loginPage?.ClickLogin();

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Password is required");
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
        [TestCase("error_user", "secret_sauce")]
        [TestCase("visual_user", "secret_sauce")]
        [Description("UC-3: Successful login with accepted credentials navigates to dashboard")]
        public void Login_WithValidCredentials_NavigatesToDashboard(string username, string password)
        {
            loginPage?.Login(username, password);

            dashboardPage?.IsAtDashboard().Should().BeTrue("the user should be redirected to the dashboard");
            dashboardPage?.GetPageTitle().Should().Be("Swag Labs");
        }
    }
}
