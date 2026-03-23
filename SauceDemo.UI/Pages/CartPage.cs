// <copyright file="CartPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace SauceDemo.UI.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;
    using SauceDemo.UI.Base;

    /// <summary>
    /// Page Object Model for the Cart Page of saucedemo.com
    /// Contains locators and methods for interacting with login elements.
    /// </summary>
    public class CartPage : BasePage
    {
        /// <summary>
        /// Verifies that the cart page is loaded by checking the URL and presence of cart items.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the cart page URL contains 'cart.html' and at least one cart item is present; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLoaded()
        {
            return Driver.Url.Contains("cart.html") &&
                   Driver.FindElements(By.ClassName("cart_item")).Any();
        }

        /// <summary>
        /// Returns a list of product names currently in the cart.
        /// </summary>
        /// <returns>
        /// A <see cref="IList{String}"/> containing the names of all products in the cart, trimmed of whitespace.
        /// If no products are in the cart, the list will be empty.
        /// </returns>
        public IList<string> GetCartItems()
        {
            return Driver.FindElements(By.ClassName("inventory_item_name"))
                .Select(x => x.Text.Trim())
                .ToList();
        }
    }
}