// <copyright file="ViewportManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Core.Utilities
{
    using System.Drawing;
    using OpenQA.Selenium;
    
    /// <summary>
    /// Provides predefined viewport sizes for responsive testing.
    /// </summary>
    public static class ViewportManager
    {
        /// <summary>
        /// Sets mobile viewport (iPhone-like).
        /// </summary>
        public static void SetMobile(IWebDriver driver)
        {
            driver.Manage().Window.Size = new Size(375, 812);
        }
        
        /// <summary>
        /// Sets tablet viewport.
        /// </summary>
        public static void SetTablet(IWebDriver driver)
        {
            driver.Manage().Window.Size = new Size(768, 1024);
        }
        
        /// <summary>
        /// Sets desktop viewport.
        /// </summary>
        public static void SetDesktop(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }
    }
}