// <copyright file="BaseTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Base
{
    using OpenQA.Selenium;
    using SauceDemo.Core.Config;
    using SauceDemo.Core.Utilities;
    
    /// <summary>
    /// Provides common setup and teardown logic for NUnit-based UI tests.
    /// Handles WebDriver lifecycle, logging, and performance timing.
    /// </summary>
    public class BaseTest
    {
        protected IWebDriver driver;
        protected TestContext context;

        [SetUp]
        public void Setup()
        {
            WebDriverFactory.InitDriver(TestConfig.Browser);

            driver = WebDriverFactory.Driver;
            context = new TestContext(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
#pragma warning restore SA1600