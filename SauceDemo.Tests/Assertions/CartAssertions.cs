// <copyright file="CartAssertions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Assertions
{
    using FluentAssertions;
    using SauceDemo.UI.Components;
    
    public class CartAssertions
    {
        private readonly CartComponent cart;
        
        public CartAssertions(CartComponent cart)
        {
            this.cart = cart;
        }

        public void ShouldBeDisplayed()
        {
            cart.IsLoaded().Should().BeTrue();
        }

        public void ShouldContainProduct(string productName)
        {
            cart.GetCartItems().Should().Contain(productName);
        }
    }
}
#pragma warning restore SA1600