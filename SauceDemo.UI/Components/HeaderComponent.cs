// <copyright file="HeaderComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.UI.Components
{
    using OpenQA.Selenium;
    using SauceDemo.UI.Base;

#pragma warning disable SA1600 // ElementsMustBeDocumented
    public class HeaderComponent : BaseComponent
    {
        private readonly By cartBadge = By.CssSelector(".shopping_cart_badge");

        public HeaderComponent(IWebDriver driver)
            : base(driver)
        {
        }

        /// <summary>
        /// Gets the current numerical value of the cart badge.
        /// </summary>
        /// <returns>
        /// The cart count, or <c>0</c> if no badge is displayed.
        /// </returns>
        public int GetCartCount()
        {
            var badges = FindAll(cartBadge);
            
            return badges.Count == 0
                ? 0
                : int.Parse(badges.First().Text.Trim());
        }

        /// <summary>
        /// Clicks Cart Icon.
        /// </summary>
        public void ClickCartIcon()
        {
            Click(By.CssSelector(".shopping_cart_link"));
        }
    }
#pragma warning restore SA1600 // ElementsMustBeDocumented
}