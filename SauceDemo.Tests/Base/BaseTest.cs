// <copyright file="BaseTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using SauceDemo.Core.Config;

#pragma warning disable SA1600
namespace SauceDemo.Tests.Base
{
    using OpenQA.Selenium;
    using SauceDemo.Core.Utilities;
    
    /// <summary>
    /// Provides common setup and teardown logic for NUnit-based UI tests.
    /// Handles WebDriver lifecycle, logging, and performance timing.
    /// </summary>
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected TestContext Context;

        [SetUp]
        public void Setup()
        {
            WebDriverFactory.InitDriver(TestConfig.Browser);

            Driver = WebDriverFactory.Driver;
            Context = new TestContext(Driver);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
#pragma warning restore SA1600