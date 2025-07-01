using SauceDemo.Core.Utilities;
using TechTalk.SpecFlow;

namespace SauceDemo.Tests.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            // You can later make this dynamic via appsettings/env variables
            WebDriverFactory.InitDriver("chrome");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            WebDriverFactory.QuitDriver();
        }
    }
}
