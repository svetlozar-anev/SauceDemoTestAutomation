// <copyright file="BaseTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using OpenQA.Selenium.DevTools.V141.Emulation;

namespace SauceDemo.Tests.Tests.Base
{
    using System.Diagnostics;
    using OpenQA.Selenium;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using SauceDemo.UI.Pages;

    /// <summary>
    /// Provides common setup and teardown logic for NUnit-based UI tests.
    /// Handles WebDriver lifecycle, logging, and performance timing.
    /// </summary>
    public abstract class BaseTest
    {
        private Stopwatch? stopwatch;

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
        /// Initializes the logging infrastructure before the entire test run begins.
        /// This method is executed only once.
        /// </summary>
        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            Logger.Init();
        }
        
        /// <summary>
        /// Ensures all pending logs are flushed after the test suite completes.
        /// </summary>
        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            Serilog.Log.CloseAndFlush();
        }

        /// <summary>
        /// Initializes the WebDriver and page objects before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            stopwatch = Stopwatch.StartNew();
            Logger.NUnitLog?.Information("=== SETUP for test: {TestName} ===", TestContext.CurrentContext.Test.Name);

            WebDriverFactory.QuitDriver();
            WebDriverFactory.InitDriver(TestConfig.Browser);

            InitializePages();
        }

        /// <summary>
        /// Cleans up the WebDriver after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            stopwatch?.Stop();
            var duration = stopwatch?.ElapsedMilliseconds ?? 0;
            var testName = TestContext.CurrentContext.Test.Name;
            var result = TestContext.CurrentContext.Result.Outcome.Status;

            if (result == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                Logger.NUnitLog?.Information("=== Test PASSED: {TestName} in {Duration}ms ===\n", testName, duration);
            }
            else
            {
                Logger.NUnitLog?.Error(
                    "=== Test FAILED: {TestName} in {Duration}ms ===\nMessage: {Message}",
                    testName,
                    duration,
                    TestContext.CurrentContext.Result.Message);
            }

            WebDriverFactory.QuitDriver();
            ResetPages();
        }
        
        /// <summary>
        /// Initializes all page objects with the current WebDriver instance.
        /// </summary>
        private void InitializePages()
        {
            LoginPage = new LoginPage(Driver);
            DashboardPage = new DashboardPage(Driver);
            CartPage = new CartPage(Driver);
        }

        /// <summary>
        /// Resets page object references after teardown.
        /// </summary>
        private void ResetPages()
        {
            LoginPage = null!;
            DashboardPage = null!;
            CartPage = null!;
        }

        protected IWebDriver Driver => WebDriverFactory.Driver;
    }
}