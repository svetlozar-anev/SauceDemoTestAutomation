// <copyright file="DashboardTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Tests
{
    using SauceDemo.Core.TestData;
    using SauceDemo.Tests.Base;
    using SauceDemo.Tests.Steps;
    using SauceDemo.UI.Components;

    /// <summary>
    /// Contains automated UI tests related to Dashboard page of SauceDemo.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class DashboardTests : BaseTest
    {
        /// <summary>
        /// Initializes the required page objects and navigates to the dashboard page before each test.
        /// </summary>
        [SetUp]
        public void TestSetUp()
        {
            context.LoginSteps.OpenLoginPage();
            context.LoginSteps.SubmitCredentials("standard_user", "secret_sauce");
        }

        [Test]
        [Description("DASHBOARD001: All product items are displayed")]
        public void DASHBOARD001_AllProductItemsAreDisplayed()
        {
            context.DashboardAssertions.ShouldDisplaySixProducts();
            context.DashboardAssertions.ShouldDisplayProductNames();
            context.DashboardAssertions.ShouldDisplayProductPrices();
            context.DashboardAssertions.ShouldDisplayProductImages();
        }

        [Test]
        [Description("DASHBOARD002: Product sort dropdown is present and functional")]
        public void DASHBOARD002_ProductSorting_WorksCorrectly()
        {
            context.DashboardSteps.SortProductsByNameAscending();
            context.DashboardAssertions.ShouldBeSortedByNameAscending();

            context.DashboardSteps.SortProductsByNameDescending();
            context.DashboardAssertions.ShouldBeSortedByNameDescending();

            context.DashboardSteps.SortProductsByPriceAscending();
            context.DashboardAssertions.ShouldBeSortedByPriceAscending();

            context.DashboardSteps.SortProductsByPriceDescending();
            context.DashboardAssertions.ShouldBeSortedByPriceDescending();
        }

        [Test]
        [Description("DASHBOARD003: Clicking a product opens its detail page")]
        public void DASHBOARD003_AllProductDetailPage_OpensCorrectly()
        {
            const int index = 0;

            var expectedName =
                context.DashboardComponent.Products
                    .GetNameByIndex(index); // think about moving this to DashboardSteps..?

            context.DashboardSteps.OpenProductDetailPage(index);

            context.ProductDetailAssertions.ShouldBeDisplayed();

            context.ProductDetailAssertions.ShouldDisplayProduct(expectedName);

            context.ProductDetailSteps.ReturnToDashboard();

            context.DashboardAssertions.ShouldBeDisplayed();
        }

        [Test]
        [Description("DASHBOARD004: Add to cart button works per item")]
        public void DASHBOARD004_AddToCart_WorksPerItem()
        {
            const int index = 0;

            context.DashboardAssertions.ShouldNotContainProductInCart(index);

            var cartCountBefore = context.DashboardSteps.GetCartCount();

            context.DashboardSteps.AddProductToCart(index);

            context.DashboardAssertions.ShouldContainProductInCart(index);
            context.DashboardAssertions.ShouldIncreaseCartCount(cartCountBefore);
        }

        [Test]
        [Description("DASHBOARD005: Multiple products can be added to cart")]
        public void DASHBOARD005_MultipleProductsCanBeAddedToCart()
        {
            const int numberOfProducts = 3;

            var products = context.DashboardSteps.GetFirstProducts(numberOfProducts);
            var previousCartCount = context.DashboardSteps.GetCartCount();

            foreach (var product in products)
            {
                context.DashboardAssertions.ShouldNotContainProductInCart(product);

                context.DashboardSteps.AddProductToCart(product);

                context.DashboardAssertions.ShouldContainProductInCart(product);

                previousCartCount++;

                context.DashboardAssertions.ShouldHaveCartCount(previousCartCount);
            }
        }

        [Test]
        [Description("DASHBOARD006: Navigation menu (burger menu) appears and functions")]
        public void DASHBOARD006_BurgerMenu_WorksCorrectly()
        {
            context.DashboardSteps.OpenMenu();

            context.DashboardMenuAssertions.ShouldShowMenu();
            context.DashboardMenuAssertions.ShouldContainAboutOption();
            context.DashboardMenuAssertions.ShouldContainLogoutOption();
        }

        [Test]
        [Description("DASHBOARD007: Removing an item from cart updates UI")]
        public void DASHBOARD007_RemoveItemFromCart_WorksCorrectly()
        {
            const int index = 0;
            this.context.DashboardSteps.AddProductToCart(index);

            var cartAfterAdd = context.DashboardSteps.GetCartCount();

            context.DashboardSteps.RemoveProductFromCart(index);

            context.DashboardAssertions.ShouldDecrementCartCount(cartAfterAdd);
            context.DashboardAssertions.ShouldShowProductAsNotInCart(index);
        }

        [Test]
        [Description("DASHBOARD008: Cart icon navigates to Cart page")]
        public void DASHBOARD008_CartNavigation_WorksCorrectly()
        {
            var product = context.DashboardSteps.AddFirstProductToCart();

            context.DashboardSteps.OpenCart();

            context.CartAssertions.ShouldBeDisplayed();
            context.CartAssertions.ShouldContainProduct(product);
        }

        [Test]
        [Description("DASHBOARD009: Logout from the Dashboard page")]
        public void DASHBOARD009_Logout_WorksCorrectly()
        {
            context.DashboardAssertions.ShouldBeDisplayed();
            context.DashboardSteps.Logout();

            context.LoginAssertions.ShouldBeOnLoginPage();
        }

        [Test]
        [Description("DASHBOARD010: Card badge persists after logout and login")]
        public void DASHBOARD010_CardPersistsAfterLogout()
        {
            context.DashboardAssertions.ShouldBeDisplayed();

            var product = context.DashboardSteps.AddFirstProductToCart();
            var cartCount = context.DashboardSteps.GetCartCount();

            context.DashboardSteps.Logout();

            context.LoginAssertions.ShouldBeOnLoginPage();

            context.LoginSteps.SubmitCredentials(TestUsers.Standard, TestUsers.Password);

            context.CartAssertions.ShouldContainProduct(product);
            context.DashboardAssertions.ShouldHaveCartCount(cartCount);
        }

        [Test]
        [Description("DASHBOARD011: Product images are loaded correctly")]
        public void DASHBOARD011_ProductImages_AreLoaded()
        {
            context.DashboardAssertions.ShouldDisplayValidProductImages();
        }

        [Test]
        [Description("DASHBOARD012: Price format and currency consistency")]
        public void DASHBOARD012_Prices_AreFormattedCorrectly()
        {
            context.DashboardAssertions.ShouldDisplayValidPriceFormat();
        }

        [Test]
        [Description("DASHBOARD013: About menu option navigates to Sauce Labs")]
        public void DASHBOARD013_AboutLink_NavigatesToSauceLabs()
        {
            context.DashboardMenuSteps.OpenAboutPage();

            context.SauceLabsAssertions.ShouldBeDisplayed();
        }

        [Test]
        [Description("DASHBOARD014: Cart persists after refresh")]
        public void DASHBOARD014_CartPersistsAfterRefresh()
        {
            const int index = 0;

            context.DashboardSteps.AddProductToCart(index);

            context.DashboardAssertions.ShouldShowProductInCart(index);

            var cartCount = context.HeaderComponent.GetCartCount();

            context.DashboardSteps.RefreshPage();

            context.DashboardAssertions.ShouldShowCartCount(cartCount);
            context.DashboardAssertions.ShouldShowProductInCart(index);
        }

        [Test]
        [Description("DASHBOARD015: Product cannot be added multiple times")]
        public void DASHBOARD015_ProductCannotBeAddedMultipleTimes()
        {
            const int index = 0;
            var initCount = context.DashboardSteps.GetCartCount();
            context.DashboardSteps.AddProductToCart(index);

            context.DashboardAssertions.ShouldShowProductInCart(index);
            context.DashboardAssertions.ShouldShowCartCount(initCount + 1);
            context.DashboardAssertions.ShouldNotAllowDuplicateAdd(index);
        }
    }
}
#pragma warning restore SA1600