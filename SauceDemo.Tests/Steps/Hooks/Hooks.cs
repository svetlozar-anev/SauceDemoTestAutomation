// <copyright file="Hooks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Steps.Hooks
{
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using TechTalk.SpecFlow;

    /// <summary>
    /// SpecFlow hooks to initialize and clean up the WebDriver before and after each scenario.
    /// </summary>
    [Binding]
    public static class Hooks
    {
        /// <summary>
        /// Initializes the logger once before any tests are run in the test suite.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Logger.Init();
            Logger.SeleniumLog?.Information("=== Logger initialized...\n");
        }

        /// <summary>
        /// Initializes the WebDriver instance before each scenario runs.
        /// The browser type is read from the test configuration.
        /// </summary>
        /// <param name="scenario">Provides context information about the currently executing scenario.</param>
        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext scenario)
        {
            Logger.SeleniumLog?.Information("=== Scenario started: {Scenario}", scenario.ScenarioInfo.Title);
            WebDriverFactory.InitDriver(TestConfig.Browser);
        }

        /// <summary>
        /// Quits and disposes the WebDriver instance after each scenario runs.
        /// Ensures clean browser sessions between tests.
        /// </summary>
        /// <param name="scenario">The SpecFlow context containing scenario details and any errors.</param>
        [AfterScenario]
        public static void AfterScenario(ScenarioContext scenario)
        {
            if (scenario.TestError != null)
            {
                Logger.SeleniumLog?.Error(scenario.TestError, "=== Scenario failed: {ScenarioTitle} \n", scenario.ScenarioInfo.Title);
            }
            else
            {
                Logger.SeleniumLog?.Information("=== Scenario finished successfully: {ScenarioTitle} \n", scenario.ScenarioInfo.Title);
            }

            WebDriverFactory.QuitDriver();
        }

        /// <summary>
        /// Flushes and closes the logger after all tests have run.
        /// Prevents log loss and releases file handles.
        /// </summary>
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Logger.SeleniumLog?.Information("=== Test run complete. Logger flushing...\n");
            Serilog.Log.CloseAndFlush(); // Flush both static and custom loggers
        }
    }
}
