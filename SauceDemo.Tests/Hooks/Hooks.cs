// <copyright file="Hooks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Hooks
{
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using TechTalk.SpecFlow;

    /// <summary>
    /// SpecFlow hooks to initialize and clean up the WebDriver before and after each scenario.
    /// </summary>
    [Binding]
    public class Hooks
    {
        /// <summary>
        /// Initializes the WebDriver instance before each scenario runs.
        /// The browser type is read from the test configuration.
        /// </summary>
        [BeforeScenario]
        public void BeforeScenario()
        {
            WebDriverFactory.InitDriver(TestConfig.Browser);
        }

        /// <summary>
        /// Quits and disposes the WebDriver instance after each scenario runs.
        /// Ensures clean browser sessions between tests.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            WebDriverFactory.QuitDriver();
        }
    }
}
