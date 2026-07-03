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
            Context.LoginSteps.OpenLoginPage();
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
            Context.LoginSteps.SubmitCredentials(username, password);
            
            Context.DashboardAssertions.ShouldBeDisplayed();
        }

        [Test]
        [Description("LOGIN002: Login fails with empty credentials")]
        public void LOGIN002_Login_WithEmptyCredentials_ShowsUsernameRequiredError()
        {
            Context.LoginSteps.SubmitEmptyCredentials(TestUsers.Standard, TestUsers.Password);
            
            Context.LoginAssertions.ShouldRequireUsername();
        }

        [Test]
        [Description("LOGIN003 Login fails with missing username")]
        public void LOGIN003_Login_WithEmptyUsername_ShowsUsernameRequiredError()
        {
            Context.LoginSteps.LoginWithEmptyUsername(TestUsers.Password);

            Context.LoginAssertions.ShouldRequireUsername();
        }
        
        [Test]
        [Description("LOGIN004: Login fails with missing password")]
        public void LOGIN004_Login_WithMissingPassword_ShowsPasswordRequiredError()
        {
            Context.LoginSteps.LoginWithEmptyPassword(TestUsers.Standard);
            
            Context.LoginAssertions.ShouldRequirePassword();
        }
        
        [Test]
        [Description("LOGIN005: Login fails with locked out user")]
        public void LOGIN005_Login_WithLockedOutUser_ShowsLockedOutError()
        {
            Context.LoginSteps.SubmitCredentials(TestUsers.LockedOut, TestUsers.Password);
            
            Context.LoginAssertions.ShouldShowLockedOutUserMessage();
        }

        [Test]
        [Description("LOGIN006: Login fails with wrong password")]
        public void LOGIN006_Login_WithIncorrectPassword_ShowsNoMatchInServiceError()
        {
            Context.LoginSteps.SubmitCredentials(TestUsers.Standard, TestUsers.WrongPassword);
            
            Context.LoginAssertions.ShouldRejectCredentials();
        }

        [Test]
        [Description("LOGIN007: Login fails with special characters in username and password")]
        public void LOGIN007_Login_WithSpecialCharacters_ShowsNoMatchInServiceError()
        {
            Context.LoginSteps.SubmitCredentials(TestUsers.SpecialChars, TestUsers.SpecialChars);
            
            Context.LoginAssertions.ShouldRejectCredentials();
        }

        [Test]
        [Description("LOGIN008: Login fails with whitespace-only username and password")]
        public void LOGIN008_Login_WithWhitespaceOnlyCredentials_ShowsNoMatchInServiceError()
        {
            Context.LoginSteps.SubmitCredentials(TestUsers.EmptyString, TestUsers.EmptyString);
            
            Context.LoginAssertions.ShouldRejectCredentials();
        }
    }
}
#pragma warning restore SA1600