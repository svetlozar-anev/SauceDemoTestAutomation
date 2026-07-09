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

        /// <summary>
        /// UC-018: Verifies product images are valid and not broken.
        /// </summary>
        [Test]
        [Description("UC-018: Product images load correctly")]
        public void UC_018_ProductImages_AreValid()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-018: Image validation", LogScope);

            dashboardPage!.WaitForDashboardToLoad();

            var images = dashboardPage.Products.GetAllImagesLoaded();

            foreach (var img in images)
            {
                img.GetAttribute("src").Should().NotBeNullOrEmpty();
            }

            Logger.NUnitLog?.Information("[{Scope}] UC-018 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-019: Verifies price format and currency consistency.
        /// </summary>
        [Test]
        [Description("UC-019: Price format and currency consistency")]
        public void UC_019_Prices_AreFormattedCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-019: Price format validation", LogScope);

            dashboardPage!.WaitForDashboardToLoad();

            var prices = dashboardPage.Products.GetAllPrices();

            prices.Should().OnlyContain(
                price =>
                    System.Text.RegularExpressions.Regex.IsMatch(price, @"^\$\d+\.\d{2}$"),
                because: "prices should follow $xx.xx format");

            Logger.NUnitLog?.Information("[{Scope}] UC-019 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-020: Verifies About link navigates correctly.
        /// </summary>
        [Test]
        [Description("UC-020: About link in menu navigates correctly")]
        public void UC_020_About_LinkInMenu_WorksCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-020: About navigation", LogScope);

            dashboardPage!.WaitForDashboardToLoad();
            dashboardPage.Menu.Open();

            dashboardPage.Menu.WaitAboutToLoad();
            dashboardPage.Menu.ClickAbout();

            var saucePage = new SauceLabsComponent(Driver);
            saucePage.WaitForPageToLoad();

            // Assert the URL
            Driver.Url.Should().Contain(
                "https://saucelabs.com",
                because: "clicking About should navigate to Sauce Labs site");

            Logger.NUnitLog?.Information("[{Scope}] UC-020 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-022: Verifies cart persists after page refresh.
        /// </summary>
        [Test]
        [Description("UC-022: Cart persists after refresh")]
        public void UC_022_CartPersistsAfterRefresh()
        {
            dashboardPage!.WaitForDashboardToLoad();

            dashboardPage.Products.AddItemToCart(0);
            dashboardPage.Products.WaitForButtonLabel(0, "Remove");

            var cartBefore = dashboardPage.Header.GetCartCount();
            dashboardPage.WaitForDashboardToLoad();

            dashboardPage.Header.GetCartCount().Should().Be(cartBefore);
            dashboardPage.Products.GetAddToCartButtonLabel(0).Should().Be("Remove");
        }

        /// <summary>
        /// UC-023: Verifies multiple sorting combinations.
        /// </summary>
        [Test]
        [Description("UC-023: Multiple sorting combinations")]
        public void UC_023_MultipleSorting_WorksCorrectly()
        {
            dashboardPage!.WaitForDashboardToLoad();

            dashboardPage.Products.SelectSortOption("Name (A to Z)");
            dashboardPage.Products.GetAllNames().Should().BeInAscendingOrder();

            dashboardPage.Products.SelectSortOption("Price (high to low)");
            dashboardPage.Products.GetPricesAsDecimal().Should().BeInDescendingOrder();

            dashboardPage.Products.SelectSortOption("Name (Z to A)");
            dashboardPage.Products.GetAllNames().Should().BeInDescendingOrder();

            dashboardPage.Products.SelectSortOption("Price (low to high)");
            dashboardPage.Products.GetPricesAsDecimal().Should().BeInAscendingOrder();
        }

        /// <summary>
        /// UC-024: Verifies item cannot be added multiple times.
        /// </summary>
        [Test]
        [Description("UC-024: Prevent duplicate add to cart")]
        public void UC_024_AddToCart_IsIdempotent()
        {
            dashboardPage!.WaitForDashboardToLoad();

            var index = 0;

            var before = dashboardPage.Header.GetCartCount();

            // Only click if item is not yet in cart
            if (dashboardPage.Products.GetAddToCartButtonLabel(index) == "Add to cart")
            {
                dashboardPage.Products.AddItemToCart(index);
                dashboardPage.Products.WaitForButtonLabel(index, "Remove");
            }

            var afterFirst = dashboardPage.Header.GetCartCount();

            // Second click attempt should not change cart
            if (dashboardPage.Products.GetAddToCartButtonLabel(index) == "Add to cart")
            {
                dashboardPage.Products.AddItemToCart(index);
            }

            var afterSecond = dashboardPage.Header.GetCartCount();

            afterFirst.Should().Be(before + 1);
            afterSecond.Should().Be(afterFirst);

            dashboardPage.Products.GetAddToCartButtonLabel(index)
                .Should().Be("Remove");
        }

        /// <summary>
        /// UC-025: Verifies dashboard layout adapts to mobile screen size.
        /// </summary>
        [Test]
        [Description("UC-025: Responsive layout check for Dashboard page")]
        public void UC_025_ResponsiveLayout_WorksCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-025: Responsive layout", LogScope);

            dashboardPage!.WaitForDashboardToLoad();

            // Set mobile viewport (clean abstraction)
            ViewportManager.SetMobile(Driver);

            // Wait for layout to stabilize (important!)
            dashboardPage.WaitForDashboardToLoad();

            // Verify products are still visible
            dashboardPage.Products.GetAllCards().Should().NotBeEmpty("products should remain visible on mobile layout");

            // Verify burger menu works
            dashboardPage.Menu.Open();

            var options = dashboardPage.Menu.GetOptions();

            options.Should().Contain("About");
            options.Should().Contain("Logout");

            Logger.NUnitLog?.Information("[{Scope}] UC-025 completed successfully", LogScope);
        }
    }
}
#pragma warning restore SA1600