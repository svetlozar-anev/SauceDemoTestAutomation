// <copyright file="DashboardAssertions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Assertions
{
    using FluentAssertions;
    using SauceDemo.UI.Components;

    public class DashboardAssertions
    {
        private readonly DashboardComponent dashboard;

        public DashboardAssertions(DashboardComponent dashboard)
        {
            this.dashboard = dashboard;
        }

        public void ShouldBeDisplayed()
        {
            dashboard.IsAtDashboard().Should().BeTrue();
            dashboard.GetPageTitle().Should().Be("Swag Labs");
        }
        
        public void ShouldContainProductInCart(int index)
        {
            this.dashboard.Products.IsItemInCart(index).Should().BeTrue();
        }

        public void ShouldContainProductInCart(string product)
        {
            dashboard.Products.GetButtonLabel(product).Should().Be("Remove");
        }

        public void ShouldNotContainProductInCart(int index)
        {
            dashboard.Products.IsItemInCart(index).Should().BeFalse();
        }
        
        public void ShouldNotContainProductInCart(string product)
        {
            dashboard.Products
                .GetButtonLabel(product)
                .Should()
                .Be("Add to cart");
        }
        
        public void ShouldIncreaseCartCount(int previousCount)
        {
            dashboard.Header.GetCartCount().Should().Be(previousCount + 1);
        }

        public void ShouldDisplaySixProducts()
        {
            dashboard.Products.GetAllCards().Should().HaveCount(6);
        }

        public void ShouldDisplayProductNames()
        {
            dashboard.Products.GetAllNames().Should().NotBeEmpty().And.OnlyContain(name => !string.IsNullOrWhiteSpace(name));
        }

        public void ShouldDisplayProductPrices()
        {
            dashboard.Products.GetAllPrices().Should().NotBeEmpty().And.OnlyContain(price => price.StartsWith("$"));
        }
        
        public void ShouldDisplayProductImages()
        {
            var images = dashboard.Products.GetAllImages();
            images.Should().NotBeEmpty();
            images.Should().OnlyContain(image => !string.IsNullOrWhiteSpace(image.GetAttribute("src")));
        }
        
        public IReadOnlyList<string> GetFirstProducts(int count)
        {
            return dashboard.Products
                .GetAllNames()
                .Take(count)
                .ToList();
        }

        public void CartCountShouldPersist(int expectedCount)
        {
            {
                dashboard.Header
                    .GetCartCount()
                    .Should()
                    .Be(expectedCount, "cart should persist");
            }
        }

        public void ShouldDecrementCartCount(int previousCount)
        {
            dashboard.Header
                .GetCartCount()
                .Should()
                .Be(previousCount - 1);
        }
        
        public void ShouldShowProductAsNotInCart(int index)
        {
            dashboard.Products
                .GetAddToCartButtonLabel(index)
                .Should()
                .Be("Add to cart");
        }
        
        public void ShouldHaveCartCount(int expected)
        {
            dashboard.Header.GetCartCount().Should().Be(expected);
        }
        
        public void ShouldBeSortedByNameAscending()
        {
            dashboard.Products.GetAllNames().Should().BeInAscendingOrder();
        }

        public void ShouldBeSortedByNameDescending()
        {
            dashboard.Products.GetAllNames().Should().BeInDescendingOrder();
        }

        public void ShouldBeSortedByPriceAscending()
        {
            dashboard.Products.GetPricesAsDecimal().Should().BeInAscendingOrder();
        }

        public void ShouldBeSortedByPriceDescending()
        {
            dashboard.Products.GetPricesAsDecimal().Should().BeInDescendingOrder();
        }
        
        public void ShouldShowCartCount(int expectedCount)
        {
            dashboard.Header.GetCartCount().Should().Be(expectedCount);
        }
        
        public void ShouldShowProductInCart(int index)
        {
            dashboard.Products.IsItemInCart(index).Should().BeTrue();
        }
        
        public void ShouldDisplayValidPriceFormat()
        {
            dashboard.Products.ArePricesFormattedCorrectly()
                .Should()
                .BeTrue();
        }

        public void ShouldDisplayValidProductImages()
        {
            var images = dashboard.Products.GetAllImagesLoaded();

            images.Should().NotBeNullOrEmpty();

            foreach (var image in images)
            {
                image.GetAttribute("src")
                    .Should()
                    .NotBeNullOrEmpty();
            }
        }
    }
}
#pragma warning restore SA1600