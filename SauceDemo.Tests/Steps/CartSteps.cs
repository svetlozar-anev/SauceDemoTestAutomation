// <copyright file="CartSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Steps
{
    using SauceDemo.UI.Components;
    
    public class CartSteps
    {
        private readonly CartComponent cart;

        public CartSteps(CartComponent cart)
        {
            this.cart = cart;
        }

        public bool IsLoaded()
        {
            return cart.IsLoaded();
        }

        public List<string> GetItems()
        {
            return cart.GetCartItems().ToList();
        }
    }
}
#pragma warning restore SA1600