// <copyright file="LoginTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Tests
{
    using FluentAssertions;
    using SauceDemo.Core.Utilities;
    using SauceDemo.Tests.Tests.Base;
    using SauceDemo.UI.Pages;

    /// <summary>
    /// Contains automated UI tests related to login functionality for SauceDemo.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class LoginTests : BaseTest
    {
        private const string LogScope = "LoginTests";
        private LoginPage? loginPage;
        private DashboardPage? dashboardPage;

        /// <summary>
        /// Initializes the required page objects and navigates to the login page before each test.
        /// </summary>
        [SetUp]
        public void TestSetUp()
        {
            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
            loginPage?.Open();
        }

        /// <summary>
        /// Verifies that submitting the login form with both username and password empty
        /// shows the appropriate 'Username is required' error message.
        /// </summary>
        [Test]
        [Description("UC-001: Login fails with empty credentials")]
        public void UC_001_Login_WithEmptyCredentials_ShowsUsernameRequiredError()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-001: Login fails with empty credentials", LogScope);

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
        [Description("UC-002: Login fails with missing password")]
        public void UC_002_Login_WithMissingPassword_ShowsPasswordRequiredError()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-002: Login fails with missing password", LogScope);

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
        [Description("UC-003: Login with valid credentials shows Dashboard")]
        public void UC_003_Login_WithValidCredentials_NavigatesToDashboard(string username, string password)
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-003: Valid login with user: {Username} shows Dashboard", LogScope, username);

            loginPage?.Login(username, password);
            Logger.NUnitLog?.Information("[{Scope}] Login submitted", LogScope);

            var isAtDashboard = dashboardPage?.IsAtDashboard() ?? false;
            Logger.NUnitLog?.Information("[{Scope}] Is at dashboard: {Result}", LogScope, isAtDashboard);

            isAtDashboard.Should().BeTrue("the user should be redirected to the dashboard");
            dashboardPage?.GetPageTitle().Should().Be("Swag Labs");
        }

        /// <summary>
        /// Verifies that login with a locked out user shows the appropriate error message.
        /// </summary>
        [Test]
        [Description("UC-004: Login fails with locked out user")]
        public void UC_004_Login_WithLockedOutUser_ShowsLockedOutError()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-004: Login fails with locked out user", LogScope);

            loginPage?.Login("locked_out_user", "secret_sauce");

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Sorry, this user has been locked out.");
        }

        /// <summary>
        /// Verifies that login with incorrect password shows the appropriate error message.
        /// </summary>
        [Test]
        [Description("UC-005: Login fails with wrong password")]
        public void UC_005_Login_WithIncorrectPassword_ShowsNoMatchInServiceError()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-005: Login fails with wrong password", LogScope);

            loginPage?.Login("standard_user", "wrong_password");

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username and password do not match any user in this service");
        }

        /// <summary>
        /// Verifies that login with empty username and valid password shows 'Username is required'.
        /// </summary>
        [Test]
        [Description("UC-006 Login fails with missing username")]
        public void UC_006_Login_WithEmptyUsername_ShowsUsernameRequiredError()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-006 Login fails with missing username", LogScope);

            loginPage?.EnterPassword("secret_sauce");
            loginPage?.ClickLogin();

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username is required");
        }

        /// <summary>
        /// Verifies that login with special characters in username and password shows invalid credentials error.
        /// </summary>
        [Test]
        [Description("UC-007: Login fails with special characters in username and password")]
        public void UC_007_Login_WithSpecialCharacters_ShowsNoMatchInServiceError()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-007: Login fails with special characters in username and password", LogScope);

            loginPage?.Login("!@#$%^&*()", "!@#$%^&*()");

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username and password do not match any user in this service");
        }

        /// <summary>
        /// Verifies that login with whitespace-only credentials fails with invalid credentials error.
        /// </summary>
        [Test]
        [Description("UC-008: Login fails with whitespace-only username and password")]
        public void UC_008_Login_WithWhitespaceOnlyCredentials_ShowsNoMatchInServiceError()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-008: Login fails with whitespace-only username and password", LogScope);

            loginPage?.Login("    ", "    ");

            loginPage?.GetErrorMessage().Should().Be("Epic sadface: Username and password do not match any user in this service");
        }
    }
}
