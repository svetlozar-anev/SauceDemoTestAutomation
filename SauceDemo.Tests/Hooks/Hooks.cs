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
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Logger.Init();
            //Logger.Log.Information("=== Test Run started ===");
        }

        /// <summary>
        /// Initializes the WebDriver instance before each scenario runs.
        /// The browser type is read from the test configuration.
        /// </summary>
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenario)
        {
           // Logger.Init();
            Logger.Log.Information("=== Scenario started: {scenario}", scenario.ScenarioInfo.Title);
            WebDriverFactory.InitDriver(TestConfig.Browser);
        }

        /// <summary>
        /// Quits and disposes the WebDriver instance after each scenario runs.
        /// Ensures clean browser sessions between tests.
        /// </summary>
        [AfterScenario]
        public void AfterScenario(ScenarioContext scenario)
        {
            if (scenario.TestError != null)
            {
                Logger.Log.Error(scenario.TestError, "Scenario failed: {ScenarioTitle} \n", scenario.ScenarioInfo.Title);
            }
            else
            {
                Logger.Log.Information("Scenario finished successfully: {ScenarioTitle} \n", scenario.ScenarioInfo.Title);
            }

            WebDriverFactory.QuitDriver();
        }
    }
}
