// <copyright file="BaseSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>


namespace SauceDemo.Tests.Steps.Base
{
    using OpenQA.Selenium;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Pages;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Provides shared setup and teardown logic for Selenium-based SpecFlow steps.
    /// Inherits this class in any SpecFlow step definition file to enable logging and WebDriver lifecycle management.
    /// </summary>
    public class BaseSteps
    {
        // === PAGE PROPERTIES ===
#pragma warning disable SA1516 // Elements should be separated by blank line
#pragma warning disable SA1600 // ElementsMustBeDocumented
        protected LoginPage LoginPage { get; private set; } = null!;
        protected DashboardPage DashboardPage { get; private set; } = null!;
        protected CartPage CartPage { get; private set; } = null!;
        protected ProductDetailPage DetailPage { get; private set; } = null!;
#pragma warning disable SA1516 // Elements should be separated by blank line
#pragma warning disable SA1600 // Elements must be documented

        /// <summary>
        /// Initializes the logger once before any tests are run in the test suite.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Logger.Init();
        }

        /// <summary>
        /// Flushes and closes the logger after all tests have run.
        /// Prevents log loss and releases file handles.
        /// </summary>
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Serilog.Log.CloseAndFlush(); // Flush both static and custom loggers
        }

        /// <summary>
        /// Initializes the WebDriver instance before each scenario runs.
        /// The browser type is read from the test configuration.
        /// </summary>
        /// <param name="scenario">Provides context information about the currently executing scenario.</param>
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenario)
        {
            Logger.SeleniumLog?.Information("=== Scenario started: {Scenario}", scenario.ScenarioInfo.Title);
            WebDriverFactory.QuitDriver();
            System.Threading.Thread.Sleep(300);
            
            WebDriverFactory.InitDriver(TestConfig.Browser);

            InitializePages();
        }

        /// <summary>
        /// Quits and disposes the WebDriver instance after each scenario runs.
        /// Ensures clean browser sessions between tests.
        /// </summary>
        /// <param name="scenario">The SpecFlow context containing scenario details and any errors.</param>
        [AfterScenario]
        public void AfterScenario(ScenarioContext scenario)
        {
            if (scenario.TestError != null)
            {
                Logger.SeleniumLog?.Error(scenario.TestError, "=== Scenario failed: {ScenarioTitle} \n",
                    scenario.ScenarioInfo.Title);
            }
            else
            {
                Logger.SeleniumLog?.Information("=== Scenario finished successfully: {ScenarioTitle} \n",
                    scenario.ScenarioInfo.Title);
            }

            WebDriverFactory.QuitDriver();
            ResetPages();
        }


        private void InitializePages()
        {
            var driver = WebDriverFactory.Driver;
            
            LoginPage = new LoginPage(driver);
            DashboardPage = new DashboardPage(driver);
            CartPage = new CartPage(driver);
            DetailPage = new ProductDetailPage(driver);
        }

        private void ResetPages()
        {
            LoginPage = null!;
            DashboardPage = null!;
            CartPage = null!;
            DetailPage = null!;
        }
    }
}