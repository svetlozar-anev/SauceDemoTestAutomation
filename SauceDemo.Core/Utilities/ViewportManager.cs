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
            SetViewport(driver, 390, 844);
        }
        
        /// <summary>
        /// Sets tablet viewport.
        /// </summary>
        public static void SetTablet(IWebDriver driver)
        {
            SetViewport(driver, 768, 1024);
        }
        
        /// <summary>
        /// Sets desktop viewport.
        /// </summary>
        public static void SetDesktop(IWebDriver driver)
        {
            SetViewport(driver, 1920, 1080);
        }
        
        private static void SetViewport(
            IWebDriver driver,
            int width,
            int height)
        {
            var js = (IJavaScriptExecutor)driver;

            js.ExecuteScript(
                $"window.resizeTo({width},{height});");
        }
    }
}