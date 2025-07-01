

namespace SauceDemo.Tests.Steps
{
    using FluentAssertions;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Pages;
    using TechTalk.SpecFlow;

    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage loginPage;
        private readonly DashboardPage dashboardPage;

        public LoginSteps()
        {
            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            WebDriverFactory.Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        [Given(@"I enter ""(.*)"" in the username field")]
        public void GivenIEnterInTheUsernameField(string username)
        {
            loginPage.EnterUsername(username);
        }

        [Given(@"I enter ""(.*)"" in the password field")]
        public void GivenIEnterInThePasswordField(string password)
        {
            loginPage.EnterPassword(password);
        }

        [Given(@"I clear the username field")]
        public void GivenIClearTheUsernameField()
        {
            loginPage.ClearUsername();
        }

        [Given(@"I clear the password field")]
        public void GivenIClearThePasswordField()
        {
            loginPage.ClearPassword();
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            loginPage.ClickLogin();
        }

        [Then(@"I should see the error message ""(.*)""")]
        public void ThenIShouldSeeTheErrorMessage(string expectedMessage)
        {
            loginPage.GetErrorMessage().Should().Be(expectedMessage);
        }

        [Then(@"I should be redirected to the dashboard")]
        public void ThenIShouldBeRedirectedToTheDashboard()
        {
            dashboardPage.IsAtDashboard().Should().BeTrue();
        }

        [Then(@"the page title should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string title)
        {
            dashboardPage.GetPageTitle().Should().Be(title);
        }
    }
}
