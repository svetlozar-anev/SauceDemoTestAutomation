// <copyright file="DashboardTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace SauceDemo.Tests.Tests
{
    using FluentAssertions;
    using SauceDemo.Core.Utilities;
    using SauceDemo.Tests.Tests.Base;
    using SauceDemo.UI.Pages;

    /// <summary>
    /// Contains automated UI tests related to Dashboard page of SauceDemo.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class DashboardTests : BaseTest
    {
        private const string LogScope = "DashboardTests";
        private LoginPage? loginPage;
        private DashboardPage? dashboardPage;

        /// <summary>
        /// Initializes the required page objects and navigates to the dashboard page before each test.
        /// </summary>
        [SetUp]
        public void TestSetUp()
        {
            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
            loginPage?.Open();
            loginPage?.Login("standard_user", "secret_sauce");

            Logger.NUnitLog?.Information("[{Scope}] Logged in as standard_user for Dashboard tests", LogScope);
        }

        /// <summary>
        /// UC-009: Verifies that all product items are displayed with name, price, and image.
        /// </summary>
        [Test]
        [Description("UC-009: All product items are displayed")]
        public void UC_009_AllProductItemsAreDisplayed()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-009: All product items are displayed", LogScope);

            var productCards = dashboardPage?.GetAllProductCards();
            var productNames = dashboardPage?.GetAllProductNames();
            var productPrices = dashboardPage?.GetAllProductPrices();
            var productImages = dashboardPage?.GetAllProductImages();

            Logger.NUnitLog?.Information("[{Scope}] Found {Count} product cards", LogScope, productCards?.Count ?? 0);

            // Assert: 6 product items are displayed
            productCards?.Count.Should().Be(6, because: "there should be 6 products displayed on the dashboard");

            // Assert: Each product has a name
            productNames.Should().NotBeNullOrEmpty(because: "each product should have a name");
            productNames.Should().OnlyContain(name => !string.IsNullOrWhiteSpace(name), because: "product names should not be empty");

            // Assert: Each product has a valid price
            productPrices.Should().NotBeNullOrEmpty(because: "each product should have a price");
            productPrices.Should().OnlyContain(price => price.StartsWith("$"), because: "product prices should be formatted correctly");

            // Assert: Each product has an image displayed
            productImages.Should().NotBeNullOrEmpty("each product should have an image element");
            foreach (var img in productImages)
            {
                img.GetAttribute("src").Should().NotBeNullOrEmpty("each product image should have a valid src attribute");
            }
        }

        /// <summary>
        /// UC-010: Verifies that sort dropdown is functional.
        /// </summary>
        [Test]
        [Description("UC-010: Product sort dropdown is present and functional")]
        public void UC_010_ProductSortDropdown_WorksCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-010: Product sort dropdown is functional", LogScope);

            // Sort by Name (A to Z)
            dashboardPage?.SelectSortOption("Name (A to Z)");
            var namesAsc = dashboardPage?.GetAllProductNames();
            namesAsc.Should().BeInAscendingOrder(because: "products should be sorted by name A to Z");

            // Sort by Name (Z to A)
            dashboardPage?.SelectSortOption("Name (Z to A)");
            var namesDesc = dashboardPage?.GetAllProductNames();
            namesDesc.Should().BeInDescendingOrder(because: "products should be sorted by name Z to A");

            // Sort by Price (low to high)
            dashboardPage?.SelectSortOption("Price (low to high)");
            var pricesAsc = dashboardPage?.GetProductPricesAsDecimal();
            pricesAsc.Should().BeInAscendingOrder(because: "products should be sorted by price low to high");

            // Sort by Price (high to low)
            dashboardPage?.SelectSortOption("Price (high to low)");
            var pricesDesc = dashboardPage?.GetProductPricesAsDecimal();
            pricesDesc.Should().BeInDescendingOrder(because: "products should be sorted by price high to low");

            Logger.NUnitLog?.Information("[{Scope}] UC-010 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-011: Ensures each product tile opens its corresponding detail page,
        /// and verifies successful navigation back to the dashboard.
        /// </summary>
        [Test]
        [Description("UC-011: Clicking a product opens its detail page")]
        public void UC_011_AllProductDetailPageOpensCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-011: Clicking a product opens its detail page", LogScope);

            // Ensure dashboard is ready
            dashboardPage!.WaitForDashboardToLoad();

            // Pick the first product (reliable, stable)
            int index = 0;

            string expectedName = dashboardPage.GetProductNameByIndex(index);
            Logger.NUnitLog?.Information("[{Scope}] Clicking product: {Name}", LogScope, expectedName);

            // Click the product title (or image; title is the most stable)
            dashboardPage.ClickProductTitleByIndex(index);

            // Create detail page object and let it handle its own wait
            var detailPage = new ProductDetailPage();
            detailPage.IsLoaded().Should().BeTrue(because: "the detail page must load before validation");

            // Validate content
            detailPage.GetTitle().Should().Be(expectedName, "the detail page must show the correct product name");

            Logger.NUnitLog?.Information("[{Scope}] Verified detail page for: {Name}", LogScope, expectedName);

            // Return to dashboard safely
            detailPage.ReturnToDashboard();

            // Make sure we're back where we think we are
            dashboardPage.WaitForDashboardToLoad();
            Logger.NUnitLog?.Information("[{Scope}] UC-011 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-012: Verifies that clicking "Add to cart" for a specific item
        /// changes the button label to "Remove" and increments the cart badge.
        /// </summary>
        [Test]
        [Description("UC-012: Add to cart button works per item")]
        public void UC_012_AddToCart_WorksPerItem()
        {
            Logger.NUnitLog?.Information("[{Scope}] Running UC-012: Add to cart button works per item", LogScope);

            // Ensure the dashboard is fully loaded before interacting.
            dashboardPage!.WaitForDashboardToLoad();

            // Choose an item by index. Item 0 is good enough.
            int index = 0;

            // Before clicking, the button label must be "Add to cart"
            dashboardPage.GetAddToCartButtonLabel(index)
                .Should().Be("Add to cart", because: "items should initially be un-added");

            int cartBefore = dashboardPage.GetCartCount();

            // Action: click the button and wait for the label to change.
            dashboardPage.ClickAddToCartByIndex(index);
            dashboardPage.WaitForButtonLabel(index, "Remove");

            // Verify that the cart badge incremented by exactly one.
            dashboardPage.GetCartCount().Should().Be(cartBefore + 1, because: "adding one item should increment the counter");

            Logger.NUnitLog?.Information("[{Scope}] UC-012 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-013: Verifies that multiple products can be added to the cart
        /// and that the cart badge correctly reflects the number of items added.
        /// </summary>
        [Test]
        [Description("UC-013: Multiple items can be added to cart")]
        public void UC_013_MultipleItemsCanBeAddedToCart()
        {
            Logger.NUnitLog?.Information("[{Scope}] Running UC-013: Multiple items can be added to cart", LogScope);

            // Ensure the dashboard is fully loaded
            dashboardPage!.WaitForDashboardToLoad();
            dashboardPage!.DismissPasswordPopupIfPresent();

            // Pick the first 3 product names from the UI
            var productsToAdd = dashboardPage.GetAllProductNames().Take(3).ToList();

            if (productsToAdd.Count < 3)
            {
                Assert.Fail("Not enough products loaded on the page to run UC-013.");
            }

            int expectedCartCount = dashboardPage.GetCartCount();

            foreach (var productName in productsToAdd)
            {
                // Ensure the button starts as "Add to cart"
                dashboardPage.GetButtonLabel(productName)
                    .Should().Be("Add to cart", because: $"{productName} should be un-added initially");

                // Add to cart
                dashboardPage.ClickAddToCart(productName);

                // Wait until button shows "Remove"
                dashboardPage.WaitForButtonLabel(productName, "Remove");

                // Confirm button updated
                dashboardPage.GetButtonLabel(productName)
                    .Should().Be("Remove", because: $"{productName} must show 'Remove' after adding");

                // Increment expected cart count
                expectedCartCount++;

                // Confirm cart badge updated
                dashboardPage.GetCartCount()
                    .Should().Be(expectedCartCount, because: "cart badge should increment after each item");
            }

            Logger.NUnitLog?.Information("[{Scope}] UC-013 completed successfully", LogScope);
        }
    }
}