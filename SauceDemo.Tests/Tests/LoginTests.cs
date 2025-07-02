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
            Logger.Init();
            Logger.Log.Information("Test Setup started");
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
            var testName = TestContext.CurrentContext.Test.Name;
            var result = TestContext.CurrentContext.Result.Outcome.Status;

            if (result == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                Logger.Log.Error("Test {TestName} FAILED", testName);
            }
            else
            {
                Logger.Log.Information("Test {TestName} PASSED", testName);
            }

            WebDriverFactory.QuitDriver();
        }

        /// <summary>
        /// Verifies that submitting the login form with both username and password empty
        /// shows the appropriate 'Username is required' error message.
        /// </summary>
        [Test]
        [Description("UC-1: Login fails with empty credentials")]
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
        [Description("UC-2: Login Login fails with missing password")]
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

        /// <summary>
        /// Verifies that login with a locked out user shows the appropriate error message.
        /// </summary>
        [Test]
        [Description("UC-4: Login fails with locked out user")]
        public void Login_WithLockedOutUser_ShowsLockedOutError()
        {
            loginPage?.Login("locked_out_user", "secret_sauce");

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Sorry, this user has been locked out.");
        }

        /// <summary>
        /// Verifies that login with incorrect password shows the appropriate error message.
        /// </summary>
        [Test]
        [Description("UC-5: Login fails with incorrect password")]
        public void Login_WithIncorrectPassword_ShowsNoMatchInServiceError()
        {
            loginPage?.Login("standard_user", "wrong_password");

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username and password do not match any user in this service");
        }

        /// <summary>
        /// Verifies that login with empty username and valid password shows 'Username is required'.
        /// </summary>
        [Test]
        [Description("UC-6: Login fails with empty username")]
        public void Login_WithEmptyUsername_ShowsUsernameRequiredError()
        {
            loginPage?.EnterPassword("secret_sauce");
            loginPage?.ClickLogin();

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username is required");
        }

        /// <summary>
        /// Verifies that login with special characters in username and password shows invalid credentials error.
        /// </summary>
        [Test]
        [Description("UC-7: Login fails with special characters as username and password")]
        public void Login_WithSpecialCharacters_ShowsNoMatchInServiceError()
        {
            loginPage?.Login("!@#$%^&*()", "!@#$%^&*()");

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username and password do not match any user in this service");
        }

        /// <summary>
        /// Verifies that login with whitespace-only credentials fails with invalid credentials error.
        /// </summary>
        [Test]
        [Description("UC-8: Login with whitespace-only credentials fails with invalid credentials error")]
        public void Login_WithWhitespaceOnlyCredentials_ShowsNoMatchInServiceError()
        {
            loginPage?.Login("    ", "    ");

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username and password do not match any user in this service");
        }
    }
}
