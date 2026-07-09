// <copyright file="DashboardTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using SauceDemo.Core.TestData;

#pragma warning disable SA1600
namespace SauceDemo.Tests.Tests
{
    using FluentAssertions;
    using SauceDemo.Core.Utilities;
    using SauceDemo.Tests.Base;
    using SauceDemo.Tests.Steps;
    using SauceDemo.UI.Components;
    using SauceDemo.UI.Pages;

    /// <summary>
    /// Contains automated UI tests related to Dashboard page of SauceDemo.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class DashboardTests : BaseTest
    {
        private const string LogScope = "DashboardTests";
        private LoginComponent? loginPage;
        private DashboardComponent? dashboardPage;
        private LoginSteps? loginSteps;

        /// <summary>
        /// Initializes the required page objects and navigates to the dashboard page before each test.
        /// </summary>
        [SetUp]
        public void TestSetUp()
        {
            Context.LoginSteps.OpenLoginPage();
            Context.LoginSteps.SubmitCredentials("standard_user", "secret_sauce");
        }

        [Test]
        [Description("DASHBOARD001: All product items are displayed")]
        public void DASHBOARD001_AllProductItemsAreDisplayed()
        {
            Context.DashboardAssertions.ShouldDisplaySixProducts();
            Context.DashboardAssertions.ShouldDisplayProductNames();
            Context.DashboardAssertions.ShouldDisplayProductPrices();
            Context.DashboardAssertions.ShouldDisplayProductImages();
        }

        [Test]
        [Description("DASHBOARD002: Product sort dropdown is present and functional")]
        public void DASHBOARD002_ProductSorting_WorksCorrectly()
        {
            Context.DashboardSteps.SortProductsByNameAscending();
            Context.DashboardAssertions.ShouldBeSortedByNameAscending();

            Context.DashboardSteps.SortProductsByNameDescending();
            Context.DashboardAssertions.ShouldBeSortedByNameDescending();

            Context.DashboardSteps.SortProductsByPriceAscending();
            Context.DashboardAssertions.ShouldBeSortedByPriceAscending();

            Context.DashboardSteps.SortProductsByPriceDescending();
            Context.DashboardAssertions.ShouldBeSortedByPriceDescending();
        }

        [Test]
        [Description("DASHBOARD003: Clicking a product opens its detail page")]
        public void DASHBOARD003_AllProductDetailPage_OpensCorrectly()
        {
            const int index = 0;

            var expectedName =
                Context.DashboardComponent.Products
                    .GetNameByIndex(index); // think about moving this to DashboardSteps..?

            Context.DashboardSteps.OpenProductDetailPage(index);

            Context.ProductDetailAssertions.ShouldBeDisplayed();

            Context.ProductDetailAssertions.ShouldDisplayProduct(expectedName);

            Context.ProductDetailSteps.ReturnToDashboard();

            Context.DashboardAssertions.ShouldBeDisplayed();
        }

        [Test]
        [Description("DASHBOARD004: Add to cart button works per item")]
        public void DASHBOARD004_AddToCart_WorksPerItem()
        {
            const int index = 0;

            Context.DashboardAssertions.ShouldNotContainProductInCart(index);

            var cartCountBefore = Context.DashboardSteps.GetCartCount();

            Context.DashboardSteps.AddProductToCart(index);

            Context.DashboardAssertions.ShouldContainProductInCart(index);
            Context.DashboardAssertions.ShouldIncreaseCartCount(cartCountBefore);
        }

        [Test]
        [Description("DASHBOARD005: Multiple products can be added to cart")]
        public void DASHBOARD005_MultipleProductsCanBeAddedToCart()
        {
            const int numberOfProducts = 3;

            var products = Context.DashboardSteps.GetFirstProducts(numberOfProducts);
            var previousCartCount = Context.DashboardSteps.GetCartCount();

            foreach (var product in products)
            {
                Context.DashboardAssertions.ShouldNotContainProductInCart(product);

                Context.DashboardSteps.AddProductToCart(product);

                Context.DashboardAssertions.ShouldContainProductInCart(product);

                previousCartCount++;

                Context.DashboardAssertions.ShouldHaveCartCount(previousCartCount);
            }
        }

        [Test]
        [Description("DASHBOARD006: Navigation menu (burger menu) appears and functions")]
        public void DASHBOARD006_BurgerMenu_WorksCorrectly()
        {
            Context.DashboardSteps.OpenMenu();

            Context.DashboardMenuAssertions.ShouldShowMenu();
            Context.DashboardMenuAssertions.ShouldContainAboutOption();
            Context.DashboardMenuAssertions.ShouldContainLogoutOption();
        }

        [Test]
        [Description("DASHBOARD007: Removing item from cart updates UI correctly")]
        public void DASHBOARD007_RemoveItemFromCart_WorksCorrectly()
        {
            const int index = 0;
            this.Context.DashboardSteps.AddProductToCart(index);

            var cartAfterAdd = Context.DashboardSteps.GetCartCount();

            Context.DashboardSteps.RemoveProductFromCart(index);

            Context.DashboardAssertions.ShouldDecrementCartCount(cartAfterAdd);
            Context.DashboardAssertions.ShouldShowProductAsNotInCart(index);
        }

        [Test]
        [Description("DASHBOARD008: Cart icon navigates to Cart page")]
        public void DASHBOARD008_CartNavigation_WorksCorrectly()
        {
            var product = Context.DashboardSteps.AddFirstProductToCart();

            Context.DashboardSteps.OpenCart();

            Context.CartAssertions.ShouldBeDisplayed();
            Context.CartAssertions.ShouldContainProduct(product);
        }

        [Test]
        [Description("DASHBOARD009: Logout from the Dashboard page")]
        public void DASHBOARD009_Logout_WorksCorrectly()
        {
            Context.DashboardAssertions.ShouldBeDisplayed();
            Context.DashboardSteps.Logout();

            Context.LoginAssertions.ShouldBeOnLoginPage();
        }

        [Test]
        [Description("DASHBOARD010: Card badge persists after logout and login")]
        public void DASHBOARD010_CardPersistsAfterLogout()
        {
            Context.DashboardAssertions.ShouldBeDisplayed();

            var product = Context.DashboardSteps.AddFirstProductToCart();
            var cartCount = Context.DashboardSteps.GetCartCount();

            Context.DashboardSteps.Logout();

            Context.LoginAssertions.ShouldBeOnLoginPage();

            Context.LoginSteps.SubmitCredentials(TestUsers.Standard, TestUsers.Password);

            Context.CartAssertions.ShouldContainProduct(product);
            Context.DashboardAssertions.ShouldHaveCartCount(cartCount);
        }

        [Test]
        [Description("DASHBOARD011: Product images are loaded correctly")]
        public void DASHBOARD011_ProductImages_AreLoaded()
        {
            Context.DashboardAssertions.ShouldDisplayValidProductImages();
        }

        [Test]
        [Description("DASHBOARD012: Price format and currency consistency")]
        public void DASHBOARD012_Prices_AreFormattedCorrectly()
        {
            Context.DashboardAssertions.ShouldDisplayValidPriceFormat();
        }

        [Test]
        [Description("DASHBOARD013: About menu option navigates to Sauce Labs")]
        public void DASHBOARD013_AboutLink_NavigatesToSauceLabs()
        {
            Context.DashboardMenuSteps.OpenAboutPage();

            Context.SauceLabsAssertions.ShouldBeDisplayed();
        }

        [Test]
        [Description("DASHBOARD014: Cart persists after refresh")]
        public void DASHBOARD014_CartPersistsAfterRefresh()
        {
            const int index = 0;

            Context.DashboardSteps.AddProductToCart(index);

            Context.DashboardAssertions.ShouldShowProductInCart(index);

            var cartCount = Context.HeaderComponent.GetCartCount();

            Context.DashboardSteps.RefreshPage();

            Context.DashboardAssertions.ShouldShowCartCount(cartCount);
            Context.DashboardAssertions.ShouldShowProductInCart(index);
        }

        [Test]
        [Description("DASHBOARD015: Product cannot be added multiple times")]
        public void DASHBOARD015_ProductCannotBeAddedMultipleTimes()
        {
            const int index = 0;
            var initCount = Context.DashboardSteps.GetCartCount();
            Context.DashboardSteps.AddProductToCart(index);

            Context.DashboardAssertions.ShouldShowProductInCart(index);
            Context.DashboardAssertions.ShouldShowCartCount(initCount + 1);
            Context.DashboardAssertions.ShouldNotAllowDuplicateAdd(index);
        }
    }
}
#pragma warning restore SA1600