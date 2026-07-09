// <copyright file="ProductDetailSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.Tests.Steps
{
    using SauceDemo.UI.Pages;

    public class ProductDetailSteps
    {
        private readonly ProductDetailComponent productDetail;

        public ProductDetailSteps(ProductDetailComponent productDetail)
        {
            this.productDetail = productDetail;
        }

        public void ReturnToDashboard()
        {
            productDetail.ReturnToDashboard();
        }
    }
}
#pragma warning restore SA1600