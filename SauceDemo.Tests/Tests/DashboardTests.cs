// <copyright file="DashboardTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Tests
{
    using FluentAssertions;
    using OpenQA.Selenium;
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
            loginPage?.OpenLoginPage();
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
            productNames.Should().OnlyContain(
                name => !string.IsNullOrWhiteSpace(name),
                because: "product names should not be empty");

            // Assert: Each product has a valid price
            productPrices.Should().NotBeNullOrEmpty(because: "each product should have a price");
            productPrices.Should().OnlyContain(
                price => price.StartsWith("$"),
                because: "product prices should be formatted correctly");

            // Assert: Each product has an image displayed
            productImages.Should().NotBeNullOrEmpty("each product should have an image element");
            foreach (var img in productImages)
            {
                img.GetAttribute("src").Should()
                    .NotBeNullOrEmpty("each product image should have a valid src attribute");
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
            Logger.NUnitLog?.Information(
                "[{Scope}] Executing UC-011: Clicking a product opens its detail page",
                LogScope);

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
            dashboardPage.GetCartCount().Should()
                .Be(cartBefore + 1, because: "adding one item should increment the counter");

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

                // Add to cart by name
                dashboardPage.ClickAddToCartByName(productName);

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

        /// <summary>
        /// UC-014: Verifies that the burger menu is visible and contains expected options.
        /// </summary>
        [Test]
        [Description("UC-014: Navigation menu (burger menu) appears and functions")]
        public void UC_014_BurgerMenu_WorksCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-014: Burger menu validation", LogScope);

            dashboardPage!.WaitForDashboardToLoad();

            dashboardPage.Menu.Open();

            dashboardPage.Menu.IsMenuVisible();
            
            var options = dashboardPage.Menu.GetOptions();

            options.Should().Contain("About");
            options.Should().Contain("Logout");

            Logger.NUnitLog?.Information("[{Scope}] UC-014 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-015: Verifies removing an item from the cart updates UI correctly.
        /// </summary>
        [Test]
        [Description("UC-015: Removing an item from the cart via Dashboard page")]
        public void UC_015_RemoveItemFromCart_WorksCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-015: Remove item from cart", LogScope);

            dashboardPage!.WaitForDashboardToLoad();

            const int index = 0;

            dashboardPage.ClickAddToCartByIndex(index);
            dashboardPage.WaitForButtonLabel(index, "Remove");

            var cartAfterAdd = dashboardPage.GetCartCount();

            dashboardPage.ClickRemoveByIndex(index);
            dashboardPage.WaitForButtonLabel(index, "Add to cart");

            dashboardPage.GetCartCount()
                .Should().Be(cartAfterAdd - 1, because: "removing item should decrement cart");

            dashboardPage.GetAddToCartButtonLabel(index)
                .Should().Be("Add to cart", because: "item should return to initial state");

            Logger.NUnitLog?.Information("[{Scope}] UC-015 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-016: Verifies that cart icon navigates to the cart page.
        /// </summary>
        [Test]
        [Description("UC-016: Cart icon navigates to Cart page")]
        public void UC_016_CartNavigation_WorksCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-016: Cart navigation", LogScope);

            dashboardPage!.WaitForDashboardToLoad();

            var product = dashboardPage.GetAllProductNames().First();

            dashboardPage.ClickAddToCartByName(product);

            dashboardPage.ClickCartIcon();

            var cartPage = new CartPage();

            cartPage.IsLoaded().Should().BeTrue();
            cartPage.GetCartItems()
                .Should().Contain(product);

            Logger.NUnitLog?.Information("[{Scope}] UC-016 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-017: Verifies logout functionality from dashboard.
        /// </summary>
        [Test]
        [Description("UC-017: Logout from the Dashboard page")]
        public void UC_017_Logout_WorksCorrectly()
        {
            Logger.NUnitLog?.Information("[{Scope}] Executing UC-017: Logout", LogScope);
        
            dashboardPage!.WaitForDashboardToLoad();
        
            dashboardPage.Menu.Open();
            dashboardPage.Menu.Logout();
        
            loginPage!.IsLoaded().Should().BeTrue();
        
            Logger.NUnitLog?.Information("[{Scope}] UC-017 completed successfully", LogScope);
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
        
            var images = dashboardPage.GetAllProductImagesLoaded();
        
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

            var prices = dashboardPage.GetAllProductPrices();

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

            dashboardPage.WaitSaucelabsToLoad();

            // Assert the URL
            Driver.Url.Should().Contain("https://saucelabs.com", because: "clicking About should navigate to Sauce Labs site");

            Logger.NUnitLog?.Information("[{Scope}] UC-020 completed successfully", LogScope);
        }

        /// <summary>
        /// UC-021: Verifies cart is reset after logout and login.
        /// </summary>
        [Test]
        [Description("UC-021: Cart badge persists after logout and login")]
        public void UC_021_CartPersistsAfterLogout()
        {
            dashboardPage!.WaitForDashboardToLoad();

            // Add item
            dashboardPage.ClickAddToCartByIndex(0);
            dashboardPage.WaitForButtonLabel(0, "Remove");

            var oldCartCount = dashboardPage.GetCartCount();

            // Logout
            dashboardPage.Menu.Open();
            dashboardPage.Menu.Logout();

            // Login again
            loginPage!.Login("standard_user", "secret_sauce");

            dashboardPage.WaitForDashboardToLoad();

            // Assert cart reset
            dashboardPage.GetCartCount().Should().Be(oldCartCount);
        }

        /// <summary>
        /// UC-022: Verifies cart persists after page refresh.
        /// </summary>
        [Test]
        [Description("UC-022: Cart persists after refresh")]
        public void UC_022_CartPersistsAfterRefresh()
        {
            dashboardPage!.WaitForDashboardToLoad();

            dashboardPage.ClickAddToCartByIndex(0);
            dashboardPage.WaitForButtonLabel(0, "Remove");

            var cartBefore = dashboardPage.GetCartCount();

            dashboardPage.Refresh();

            dashboardPage.WaitForDashboardToLoad();

            dashboardPage.GetCartCount().Should().Be(cartBefore);
            dashboardPage.GetAddToCartButtonLabel(0).Should().Be("Remove");
        }

        /// <summary>
        /// UC-023: Verifies multiple sorting combinations.
        /// </summary>
        [Test]
        [Description("UC-023: Multiple sorting combinations")]
        public void UC_023_MultipleSorting_WorksCorrectly()
        {
            dashboardPage!.WaitForDashboardToLoad();

            dashboardPage.SelectSortOption("Name (A to Z)");
            dashboardPage.GetAllProductNames().Should().BeInAscendingOrder();

            dashboardPage.SelectSortOption("Price (high to low)");
            dashboardPage.GetProductPricesAsDecimal().Should().BeInDescendingOrder();

            dashboardPage.SelectSortOption("Name (Z to A)");
            dashboardPage.GetAllProductNames().Should().BeInDescendingOrder();

            dashboardPage.SelectSortOption("Price (low to high)");
            dashboardPage.GetProductPricesAsDecimal().Should().BeInAscendingOrder();
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

            var before = dashboardPage.GetCartCount();
            
            // Only click if item is not yet in cart
            if (dashboardPage.GetAddToCartButtonLabel(index) == "Add to cart")
            {
                dashboardPage.ClickAddToCartByIndex(index);
                dashboardPage.WaitForButtonLabel(index, "Remove");
            }

            var afterFirst = dashboardPage.GetCartCount();

            // Second click attempt should not change cart
            if (dashboardPage.GetAddToCartButtonLabel(index) == "Add to cart")
            {
                dashboardPage.ClickAddToCartByIndex(index); 
            }

            var afterSecond = dashboardPage.GetCartCount();

            afterFirst.Should().Be(before + 1);
            afterSecond.Should().Be(afterFirst);

            dashboardPage.GetAddToCartButtonLabel(index)
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
            dashboardPage.GetAllProductCards()
                .Should().NotBeEmpty("products should remain visible on mobile layout");

            // Verify burger menu works
            dashboardPage.Menu.Open();
            
            var options = dashboardPage.Menu.GetOptions();

            options.Should().Contain("About");
            options.Should().Contain("Logout");

            Logger.NUnitLog?.Information("[{Scope}] UC-025 completed successfully", LogScope);
        }
    }
}