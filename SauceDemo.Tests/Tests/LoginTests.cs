using FluentAssertions;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.Utilities;

namespace SauceDemo.Tests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class LoginTests
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;

        [SetUp]
        public void Setup()
        {
            WebDriverFactory.InitDriver(); // TODO: or read from config
            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
            loginPage.Open();
        }

        [TearDown]
        public void TearDown()
        {
            WebDriverFactory.QuitDriver();
        }

        [Test]
        [Description("UC-1: Login attempt with empty credentials shows 'Username is required' error")]
        public void Login_WithEmptyCredentials_ShowsUsernameRequiredError()
        {
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClearUsername();
            loginPage.ClearPassword();

            loginPage.ClickLogin();

            loginPage.GetErrorMessage().Should().Be("Epic sadface: Username is required");
        }

        [Test]
        [Description("UC-2: Login attempt without password shows 'Password is required' error")]
        public void Login_WithMissingPassword_ShowsPasswordRequiredError()
        {
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClearPassword();

            loginPage.ClickLogin();

            loginPage.GetErrorMessage().Should().Be("Epic sadface: Password is required");
        }

        [Test]
        [TestCase("standard_user", "secret_sauce")]
        [TestCase("problem_user", "secret_sauce")]
        [TestCase("performance_glitch_user", "secret_sauce")]
        [Description("UC-3: Successful login with accepted credentials navigates to dashboard")]
        public void Login_WithValidCredentials_NavigatesToDashboard(string username, string password)
        {
            loginPage.Login(username, password);

            dashboardPage.IsAtDashboard().Should().BeTrue("the user should be redirected to the dashboard");
            dashboardPage.GetPageTitle().Should().Be("Swag Labs");
        }
    }
}
