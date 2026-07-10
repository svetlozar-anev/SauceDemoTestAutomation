// <copyright file="ProductDetailAssertions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Assertions
{
    using FluentAssertions;
    using SauceDemo.UI.Pages;
    
    public class ProductDetailAssertions
    {
        private readonly ProductDetailComponent productDetail;

        public ProductDetailAssertions(ProductDetailComponent productDetail)
        {
            this.productDetail = productDetail;
        }

        public void ShouldBeDisplayed()
        {
            productDetail.IsLoaded().Should().BeTrue();
        }

        public void ShouldDisplayProduct(string expectedName)
        {
            productDetail.GetTitle().Should().Be(expectedName);
        }
    }
}
#pragma warning restore SA1600