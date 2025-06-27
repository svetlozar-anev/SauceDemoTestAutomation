//// <copyright file="TestConfig.cs" company="PlaceholderCompany">
//// Copyright (c) PlaceholderCompany. All rights reserved.
//// </copyright>

//namespace SauceDemo.Tests.Config
//{
//    //using Microsoft.Extensions.Configuration;

//    /// <summary>
//    /// Provides access to test configuration settings from the appsettings.json file.
//    /// </summary>
//    public static class TestConfig
//    {
//        /// <summary>
//        /// The root configuration loaded from appsettings.json.
//        /// </summary>
//        //private static readonly IConfigurationRoot Config = new ConfigurationBuilder()
//        //    .SetBasePath(AppContext.BaseDirectory)
//        //    .AddJsonFile("Config/appsettings.json", optional: false)
//        //    .Build();

//        /// <summary>
//        /// Gets the browser name specified in the configuration.
//        /// Defaults to "chrome" if not found.
//        /// </summary>
//        public static string Browser => Config["TestSettings:Browser"] ?? "chrome";

//        /// <summary>
//        /// Gets the base URL for the test application.
//        /// Defaults to "https://www.saucedemo.com/" if not found.
//        /// </summary>
//        public static string BaseUrl => Config["TestSettings:BaseUrl"] ?? "https://www.saucedemo.com/";

//        /// <summary>
//        /// Gets the number of seconds to wait for WebDriver operations.
//        /// Defaults to 10 seconds if not specified or invalid.
//        /// </summary>
//        public static int WebDriverWaitSeconds => int.TryParse(Config["TestSettings:WebDriverWaitSeconds"], out var seconds) ? seconds : 10;
//    }
//}
