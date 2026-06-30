// <copyright file="SauceLabsComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600 
namespace SauceDemo.UI.Components
{
    using OpenQA.Selenium;
    using SauceDemo.UI.Base;

    public class SauceLabsComponent : BaseComponent
    {
        public SauceLabsComponent(IWebDriver driver)
            : base(driver)
        {
        }

        public bool IsLoaded()
        {
            return Driver.Url.Contains("saucelabs.com");
        }

        public void WaitForPageToLoad()
        {
            Wait.Until(d => d.Url.Contains("saucelabs.com"));
        }
    }
}
#pragma warning disable SA1600