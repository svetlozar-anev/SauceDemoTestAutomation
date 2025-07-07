// <copyright file="BaseTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Tests.Base
{
    using System.Diagnostics;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Provides common setup and teardown logic for NUnit-based UI tests.
    /// Handles WebDriver lifecycle, logging, and performance timing.
    /// </summary>
    public abstract class BaseTest
    {
        private Stopwatch? stopwatch;

        /// <summary>
        /// Initializes the logging infrastructure before the entire test run begins.
        /// This method is executed only once.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Logger.Init();
            Logger.NUnitLog?.Information("==== Logger initialized...\n");
        }

        /// <summary>
        /// Initializes the WebDriver and page objects before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            stopwatch = Stopwatch.StartNew();
            Logger.NUnitLog?.Information("=== SETUP for test: {TestName} ===", TestContext.CurrentContext.Test.Name);

            WebDriverFactory.InitDriver(TestConfig.Browser);
        }

        /// <summary>
        /// Cleans up the WebDriver after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            stopwatch?.Stop();
            var duration = stopwatch?.ElapsedMilliseconds;
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
        }

        /// <summary>
        /// Ensures all pending logs are flushed after the test suite completes.
        /// </summary>
        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            Logger.NUnitLog?.Information("==== Test run complete. Logger flushing... ====\n");

            Serilog.Log.CloseAndFlush();
        }
    }
}
