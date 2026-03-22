Feature: Dashboard Functionality
    As a standard user
    I want to see products and add them to my cart
    So that I can browse and shop easily

    Scenario: UC-009: All product names are displayed
        Given I am logged in as a standard user
        And I am on the dashboard page
        When the dashboard finishes loading
        Then all product names are visible and not empty
        And all product prices are displayed and formatted correctly
        And all product images are visible
        
    Scenario: UC-010: Product sort dropdown works
        Given I am logged in as a standard user
        And I am on the dashboard page
        When I sort products by "Name (A to Z)"
        Then products are sorted by name ascending
        When I sort products by "Name (Z to A)"
        Then products are sorted by name descending
        When I sort products by "Price (low to high)"
        Then products are sorted by price ascending
        When I sort products by "Price (high to low)"
        Then products are sorted by price descending
        
    Scenario: UC-011: Clicking a product opens its detail page
        Given I am logged in as a standard user
        And I am on the dashboard page
        When I click the first product
        Then the product detail page should display the correct product
        And I return to the dashboard page
        
    Scenario: UC-012: Add to cart for a single item
        Given I am logged in as a standard user
        And I am on the dashboard page
        When I add the first product to the cart
        Then the button should change to "Remove"
        And the cart badge should increment by 1

    Scenario: UC-013: Add to cart for multiple items
        Given I am logged in as a standard user
        And I am on the dashboard page
        When I add the first three products to the cart
        Then each button should change to "Remove"
        And the cart badge should reflect the total items added