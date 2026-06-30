// <copyright file="CartComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.UI.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;
    using SauceDemo.UI.Base;
    
    public class CartComponent : BaseComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartComponent"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver.</param>
        public CartComponent(IWebDriver driver) 
            : base(driver)
        {
        }
        
        public bool IsLoaded()
        {
            return Driver.Url.Contains("cart.html") &&
                   Driver.FindElements(By.ClassName("cart_item")).Any();
        }
        
        public IList<string> GetCartItems()
        {
            return Driver.FindElements(By.ClassName("inventory_item_name"))
                .Select(x => x.Text.Trim())
                .ToList();
        }
    }
}
#pragma warning restore SA1600