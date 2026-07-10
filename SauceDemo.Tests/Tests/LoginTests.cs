// <copyright file="LoginTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.Tests.Tests
{
    using FluentAssertions;
    using SauceDemo.Core.TestData;
    using SauceDemo.Core.Utilities;
    using SauceDemo.Tests.Base;

    /// <summary>
    /// Contains automated UI tests related to login functionality for SauceDemo.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class LoginTests : BaseTest
    {
        private const string LogScope = "LoginTests";

        /// <summary>
        /// Initializes the required page objects and navigates to the login page before each test.
        /// </summary>
        [SetUp]
        public void TestSetUp()
        {
            context.LoginSteps.OpenLoginPage();
        }

        [Test]
        [TestCase(TestUsers.Standard, TestUsers.Password)]
        [TestCase(TestUsers.Problem, TestUsers.Password)]
        [TestCase(TestUsers.PerformanceGlitch, TestUsers.Password)]
        [TestCase(TestUsers.Error, TestUsers.Password)]
        [TestCase(TestUsers.Visual, TestUsers.Password)]
        [Description("LOGIN001: Login with valid credentials shows Dashboard")]
        public void LOGIN001_Login_WithValidCredentials_NavigatesToDashboard(string username, string password)
        {
            context.LoginSteps.SubmitCredentials(username, password);
            
            context.DashboardAssertions.ShouldBeDisplayed();
        }

        [Test]
        [Description("LOGIN002: Login fails with empty credentials")]
        public void LOGIN002_Login_WithEmptyCredentials_ShowsUsernameRequiredError()
        {
            context.LoginSteps.SubmitEmptyCredentials(TestUsers.Standard, TestUsers.Password);
            
            context.LoginAssertions.ShouldRequireUsername();
        }

        [Test]
        [Description("LOGIN003: Login fails with missing username")]
        public void LOGIN003_Login_WithEmptyUsername_ShowsUsernameRequiredError()
        {
            context.LoginSteps.LoginWithEmptyUsername(TestUsers.Password);

            context.LoginAssertions.ShouldRequireUsername();
        }
        
        [Test]
        [Description("LOGIN004: Login fails with missing password")]
        public void LOGIN004_Login_WithMissingPassword_ShowsPasswordRequiredError()
        {
            context.LoginSteps.LoginWithEmptyPassword(TestUsers.Standard);
            
            context.LoginAssertions.ShouldRequirePassword();
        }
        
        [Test]
        [Description("LOGIN005: Login fails with locked out user")]
        public void LOGIN005_Login_WithLockedOutUser_ShowsLockedOutError()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.LockedOut, TestUsers.Password);
            
            context.LoginAssertions.ShouldShowLockedOutUserMessage();
        }

        [Test]
        [Description("LOGIN006: Login fails with wrong password")]
        public void LOGIN006_Login_WithIncorrectPassword_ShowsNoMatchInServiceError()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.Standard, TestUsers.WrongPassword);
            
            context.LoginAssertions.ShouldRejectCredentials();
        }

        [Test]
        [Description("LOGIN007: Login fails with special characters in username and password")]
        public void LOGIN007_Login_WithSpecialCharacters_ShowsNoMatchInServiceError()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.SpecialChars, TestUsers.SpecialChars);
            
            context.LoginAssertions.ShouldRejectCredentials();
        }

        [Test]
        [Description("LOGIN008: Login fails with whitespace-only username and password")]
        public void LOGIN008_Login_WithWhitespaceOnlyCredentials_ShowsNoMatchInServiceError()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.EmptyString, TestUsers.EmptyString);
            
            context.LoginAssertions.ShouldRejectCredentials();
        }

        [Test]
        [Description("LOGIN009: Login fails with unknown username")]
        public void LOGIN009_Login_WithUnknownUsername_ShowsNoMatchInServiceError()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.Unknown, TestUsers.Password);
            
            context.LoginAssertions.ShouldRejectCredentials();
        }
        
        [Test]
        [Description("LOGIN010: Login fails with invalid username and password")]
        public void LOGIN010_Login_WithInvalidUsernameAndPassword_ShowsNoMatchInServiceError()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.Unknown, TestUsers.WrongPassword);

            context.LoginAssertions.ShouldRejectCredentials();
        }
        
        [Test]
        [Description("LOGIN011: Login fails when username casing is incorrect")]
        public void LOGIN011_Login_WithUppercaseUsername_ShowsNoMatchInServiceError()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.Standard.ToUpperInvariant(), TestUsers.Password);

            context.LoginAssertions.ShouldRejectCredentials();
        }
        
        [Test]
        [Description("LOGIN012: Login fails when password casing is incorrect")]
        public void LOGIN012_Login_WithUppercasePassword_ShowsNoMatchInServiceError()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.Standard, TestUsers.Password.ToUpperInvariant());

            context.LoginAssertions.ShouldRejectCredentials();
        }
        
        [Test]
        [TestCase($" {TestUsers.Standard}", TestUsers.Password)]
        [TestCase(TestUsers.Standard, $" {TestUsers.Password}")]
        [TestCase($" {TestUsers.Standard}",$" {TestUsers.Password}")]
        [Description("LOGIN013: Login fails with leading whitespace in username or password")]
        public void LOGIN013_Login_WithLeadingWhitespace_ShowsNoMatchInServiceError(string username, string password)
        {
            context.LoginSteps.SubmitCredentials(username, password);

            context.LoginAssertions.ShouldRejectCredentials();
        }
        
        [Test]
        [TestCase($"{TestUsers.Standard} ", TestUsers.Password)]
        [TestCase(TestUsers.Standard, $"{TestUsers.Password} ")]
        [TestCase($"{TestUsers.Standard} ",$"{TestUsers.Password} ")]
        [Description("LOGIN014: Login fails with trailing whitespace in username or password")]
        public void LOGIN014_Login_TrailingWhitespace_ShowsNoMatchInServiceError(string username, string password)
        {
            context.LoginSteps.SubmitCredentials(username, password);

            context.LoginAssertions.ShouldRejectCredentials();
        }

        [Test]
        [Description("LOGIN015: Successful login after failed login")]
        public void LOGIN015_Login_AfterFailedAttempt_NavigatesToDashboard()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.Standard, TestUsers.WrongPassword);

            context.LoginAssertions.ShouldRejectCredentials();

            context.LoginSteps.SubmitCredentials(TestUsers.Standard, TestUsers.Password);

            context.DashboardAssertions.ShouldBeDisplayed();
        }

        [Test]
        [Description("LOGIN016: Direct navigation to inventory redirects to login")]
        public void LOGIN016_DirectInventoryNavigation_RedirectsToLogin()
        {
            context.LoginSteps.NavigateToInventoryPage();

            context.LoginAssertions.ShouldBeOnLoginPage();
            context.LoginAssertions.ShouldShowNoAccessToInventoryMessage();
        }
        
        [Test]
        [Description("LOGIN017: Refresh after failed login")]
        public void LOGIN017_RefreshAfterFailedLogin_ClearsErrorMessage()
        {
            context.LoginSteps.SubmitCredentials(TestUsers.Standard, TestUsers.WrongPassword);

            context.LoginAssertions.ShouldRejectCredentials();

            context.LoginSteps.RefreshPage();

            context.LoginAssertions.ShouldNotShowError();
        }

        [Test]
        [Description("LOGIN018: Login fails with extremely long username")]
        public void LOGIN018_Login_WithVeryLongUsername_ShowsNoMatchInServiceError()
        {
            var longUsername = new string('a', 1000);
            context.LoginSteps.SubmitCredentials(longUsername, TestUsers.Password);

            context.LoginAssertions.ShouldRejectCredentials();
        }

        [Test]
        [Description("LOGIN019: Login fails with extremely long password")]
        public void LOGIN019_Login_WithVeryLongPassword_ShowsNoMatchInServiceError()
        {
            var longPassword = new string('a', 1000);
            context.LoginSteps.SubmitCredentials(TestUsers.Standard, longPassword);
            
            context.LoginAssertions.ShouldRejectCredentials();
        }
    }
}
#pragma warning restore SA1600