// <copyright file="DashboardSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests.Steps
{
    using FluentAssertions;
    using SauceDemo.Core.TestData;
    using SauceDemo.Tests.Steps.Base;
    using SauceDemo.UI.Pages;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Step definitions for dashboard feature scenarios.
    /// </summary>
    [Binding]
    public class DashboardSteps : BaseSteps
    {
        private string? selectedProductName;
        private int initialCartCount;
        private List<string>? selectedProducts;

        // ========================
        // GIVEN
        // ========================

        /// <summary>
        /// Logs in as a standard user using valid credentials.
        /// </summary>
        [Given(@"I am logged in as a standard user")]
        public void GivenIAmLoggedInAsAStandardUser()
        {
            LoginPage?.Open();
            LoginPage?.Login(Users.Standard, Users.Password);
        }

        /// <summary>
        /// Ensures the user is on the dashboard page.
        /// </summary>
        [Given("I am on the dashboard page")]
        public void GivenIAmOnTheDashboardPage()
        {
            DashboardPage?.Open();
        }

        // ========================
        // WHEN
        // ========================

        /// <summary>
        /// Waits for the dashboard to load.
        /// </summary>
        [When("the dashboard finishes loading")]
        public void WhenTheDashboardFinishesLoading()
        {
            DashboardPage?.WaitForDashboardToLoad();
        }

        /// <summary>
        /// Selects a sort option from the product dropdown.
        /// </summary>
        /// <param name="option">Selects string option.</param>
        [When(@"I sort products by ""(.*)""")]
        public void WhenISortProductsBy(string option)
        {
            DashboardPage?.Products.SelectSortOption(option);
        }

        /// <summary>
        /// Clicks the first product on the dashboard and stores its name.
        /// </summary>
        [When(@"I click the first product")]
        public void WhenIClickTheFirstProduct()
        {
            DashboardPage?.WaitForDashboardToLoad();
            selectedProductName = DashboardPage?.Products.GetNameByIndex(0);
            DashboardPage?.Products.ClickTitleByIndex(0);
        }

        /// <summary>
        /// Adds the first product to the cart and waits for the button to update.
        /// </summary>
        [When(@"I add the first product to the cart")]
        public void WhenIAddTheFirstProductToTheCart()
        {
            DashboardPage?.WaitForDashboardToLoad();

            initialCartCount = DashboardPage.GetCartCount();

            DashboardPage?.ClickAddToCartByIndex(0);
            DashboardPage?.WaitForButtonLabel(0, "Remove");
        }

        /// <summary>
        /// Removes the first product of the cart to appear and waits for the button to update.
        /// </summary>
        [When("I remove the same product from the cart")]
        public void WhenIRemoveTheFirstProductToTheCart()
        {
            DashboardPage?.WaitForDashboardToLoad();

            initialCartCount = DashboardPage.GetCartCount();

            DashboardPage?.ClickRemoveByIndex(0);

            DashboardPage?.WaitForButtonLabel(0, "Add");
        }

        /// <summary>
        /// Adds the first three products to the cart and waits for each button to update.
        /// </summary>
        [When("I add the first three products to the cart")]
        public void WhenIAddTheFirstThreeProductsToTheCart()
        {
            DashboardPage?.WaitForDashboardToLoad();
            
            selectedProducts = DashboardPage?.Products.GetAllNames().Take(3).ToList();
            initialCartCount = DashboardPage.GetCartCount();

            foreach (var product in selectedProducts)
            {
                DashboardPage?.ClickAddToCartByName(product);
                DashboardPage?.WaitForButtonLabel(product, "Remove");
            }
        }

        /// <summary>
        /// We click on the react burger menu button.
        /// </summary>
        [When("the user clicks the burger menu icon")]
        public void WhenTheUserClicksTheBurgerMenuIcon()
        {
            DashboardPage?.WaitForDashboardToLoad();
            DashboardPage?.Menu.Open();
        }

        // ========================
        // THEN
        // ========================

        /// <summary>d
        /// Verifies that all product names are present and non-empty.
        /// </summary>
        [Then("all product names are visible and not empty")]
        public void ThenAllProductNamesAreVisibleAndNotEmpty()
        {
            var names = DashboardPage?.Products.GetAllNames();
            names.Should().NotBeNullOrEmpty();
            names.Should().OnlyContain(name => !string.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// Verifies that all product prices are displayed and formatted with a dollar sign.
        /// </summary>
        [Then("all product prices are displayed and formatted correctly")]
        public void ThenAllProductPricesAreDisplayedAndFormattedCorrectly()
        {
            var prices = DashboardPage?.Products.GetAllPrices();

            prices.Should().NotBeNullOrEmpty();
            prices.Should().OnlyContain(p => p.StartsWith("$"));
        }

        /// <summary>
        /// Verifies that all product images have a valid source.
        /// </summary>
        [Then("all product images are visible")]
        public void ThenAllProductImagesAreVisible()
        {
            var images = DashboardPage?.Products.GetAllImages();

            images.Should().NotBeNullOrEmpty();

            foreach (var image in images)
            {
                image.GetAttribute("src").Should().NotBeNullOrEmpty();
            }
        }

        /// <summary>
        /// Checks that product names are sorted A→Z.
        /// </summary>
        [Then("products are sorted by name ascending")]
        public void ThenProductsAreSortedByNameAscending()
        {
            DashboardPage?.Products.GetAllNames()
                .Should().BeInAscendingOrder();
        }

        /// <summary>
        /// Checks that product names are sorted Z→A.
        /// </summary>
        [Then("products are sorted by name descending")]
        public void ThenProductsAreSortedByNameDescending()
        {
            DashboardPage?.Products.GetAllNames()
                .Should().BeInDescendingOrder();
        }

        /// <summary>
        /// Checks that product prices are sorted low→high.
        /// </summary>
        [Then("products are sorted by price ascending")]
        public void ThenProductsAreSortedByPriceAscending()
        {
            DashboardPage?.Products.GetPricesAsDecimal()
                .Should().BeInAscendingOrder();
        }

        /// <summary>
        /// Checks that product prices are sorted high→low.
        /// </summary>
        [Then("products are sorted by price descending")]
        public void ThenProductsAreSortedByPriceDescending()
        {
            DashboardPage?.Products.GetPricesAsDecimal()
                .Should().BeInDescendingOrder();
        }

        /// <summary>
        /// Verifies the detail page shows the selected product.
        /// </summary>
        [Then("the product detail page should display the correct product")]
        public void ThenTheProductDetailPageShouldDisplayTheCorrectProduct()
        {
            DetailPage.IsLoaded().Should().BeTrue();
            DetailPage.GetTitle().Should().Be(selectedProductName);
        }

        /// <summary>
        /// Returns from the product detail page to the dashboard.
        /// </summary>
        [Then("I return to the dashboard page")]
        public void ThenIReturnToTheDashboardPage()
        {
            DetailPage.ReturnToDashboard();

            DashboardPage?.WaitForDashboardToLoad();
        }

        /// <summary>
        /// Verifies that the add-to-cart button updated its label.
        /// </summary>
        /// <param name="expected">The menu options table - All Items, About, Logout.</param>
        [Then(@"the button should change to ""(.*)""")]
        public void ThenTheButtonShouldChangeTo(string expected)
        {
            DashboardPage?.GetAddToCartButtonLabel(0)
                .Should().Be(expected);
        }

        /// <summary>
        /// Verifies that the add-to-cart button updated its label.
        /// </summary>
        [Then("the cart badge should increment by 1")]
        public void ThenTheCartBadgeShouldIncrementBy1()
        {
            DashboardPage?.GetCartCount()
                .Should().Be(initialCartCount + 1);
        }

        /// <summary>
        /// Verifies that all selected products show the "Remove" button.
        /// </summary>
        [Then("each button should change to \"Remove\"")]
        public void ThenEachButtonShouldChangeToRemove()
        {
            foreach (var product in selectedProducts!)
            {
                DashboardPage?.GetButtonLabel(product)
                    .Should().Be("Remove");
            }
        }

        /// <summary>
        /// Verifies the cart badge matches the total items added.
        /// </summary>
        [Then("the cart badge should reflect the total items added")]
        public void ThenTheCartBadgeShouldReflectTheTotalItemsAdded()
        {
            DashboardPage?.GetCartCount()
                .Should().Be(initialCartCount + selectedProducts!.Count);
        }

        /// <summary>
        /// Verifies the Dashboard page navigation menu is Displayed.
        /// </summary>
        [Then("the navigation menu should be displayed")]
        public void ThenTheNavigationMenuShouldBeDisplayed()
        {
            DashboardPage?.Menu.IsMenuVisible().Should().BeTrue();
        }

        /// <summary>
        /// Verifies the menu contains table of options.
        /// </summary>
        /// <param name="table">The menu options table - All Items, About, Logout.</param>
        [Then(@"the menu should contain the following options:")]
        public void ThenAndTheMenuShouldContainTheOptions(Table table)
        {
            var actualOptions = DashboardPage?.Menu.GetOptions();

            var selectedOptions = table.Rows.Select(r => r[0].ToString()).ToList();

            actualOptions.Should().Contain(selectedOptions);
        }

        /// <summary>
        /// Verifies that the cart badge returns 0 as car count.
        /// </summary>
        [Then("Then the cart badge should decrement to 0")]
        public void ThenTheCartBadgeShouldDecrementTo0()
        {
            DashboardPage?.WaitForButtonLabel(0, "Add to cart");
        }

        /// <summary>
        /// Verify that the Button label on Index 0 is "Add to cart".
        /// </summary>
        [Then("And the product button should change to \"Add to cart\"")]
        public void ThenAndTheProductButtonShouldChangeToAddToCart()
        {
            DashboardPage?.WaitForButtonLabel(0, "Add to cart");
        }
    }
}