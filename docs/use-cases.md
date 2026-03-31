# 🧪 SauceDemo Use Cases

This document details the main test automation use cases covered by the SauceDemo test suite.

---

## Login Scenarios

### UC-001: Login fails with empty credentials
- Fill in **any** text in username and password fields
- Clear both fields
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### UC-002: Login fails with missing password
- Fill in a username
- Fill in password
- Clear only the password
- Click **Login**
- Assert error message: `"Epic sadface: Password is required"`

### UC-003: Login with valid credentials shows Dashboard
- Fill in valid username (`standard_user`, `problem_user`, `performance_glitch_user`, `error_user`, `visual_user`)
- Enter password: `secret_sauce`
- Click **Login**
- Assert URL is `/inventory.html`
- Assert dashboard title: `"Swag Labs"`
- Assert page header contains `"Products"`

### UC-004: Login fails with locked out user
- Fill in locked out username: `locked_out_user`
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Sorry, this user has been locked out."`

### UC-005: Login fails with wrong password
- Fill in valid username: `standard_user`
- Enter wrong password: `wrong_password`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### UC-006: Login fails with missing username
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### UC-007: Login fails with special characters in username and password
- Fill in username: `!@#$%^&*()`
- Enter password: `!@#$%^&*()`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### UC-008: Login fails with whitespace-only username and password
- Fill in empty username: `    `
- Enter empty password: `    `
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

---

## Dashboard Scenarios

### UC-009: All product items are displayed
- Verify presence of all six product cards
- Each product has a name, price, and image

### UC-010: Product sort dropdown is present and functional
- Validate options: Name (A to Z), Name (Z to A), Price (low to high), Price (high to low)
- Select each and verify correct order

### UC-011: Clicking a product opens its detail page
- Click title or image of a product
- Assert product detail page loads with correct info

### UC-012: Add to cart button works per item
- Click “Add to cart” for one item
- Button changes to “Remove”
- Cart icon updates with item count

### UC-013: Multiple items can be added to cart
- Add multiple products to cart
- Cart count matches number of items added

### UC-014: Navigation menu (burger menu) appears and functions
- Click ☰ menu
- Verify options: All Items, About, Logout

### UC-015: Removing an item from the cart via Dashboard page
- Add a product to the cart
- Click “Remove” button for that product
- Verify cart count updates correctly
- Verify product returns to “Add to cart” state

### UC-016: Cart icon navigates to Cart page
- Add at least one item to the cart
- Click cart icon
- Assert navigation to `/cart.html`
- Verify correct items are displayed in the cart

### UC-017: Logout from the Dashboard page
- Open burger menu
- Click “Logout”
- Verify redirection to login page
- Assert previous session is ended (cannot access `/inventory.html` without logging in)

### UC-018: Product images load correctly
- Verify all product images have valid `src` attributes
- Check if `naturalWidth` > 0 to ensure no broken images

### UC-019: Price format and currency consistency
- Verify all product prices follow $\d+\.\d{2} format
- Assert all prices use USD currency symbol

### UC-020: About link in menu navigates correctly
- Open burger menu
- Click “About”
- Verify navigation to Sauce Labs About page

<<<<<<< tests-26-35
### UC-021: Adding items to cart persists on Dashboard logout
=======
### UC-021: Cart badge resets after logout and login
>>>>>>> main
- Add items to cart
- Logout from Dashboard page
- Login again with valid credentials
- Verify cart badge shows same number of items

### UC-022: Adding items to cart persists on page refresh
- Add one or more items to cart
- Refresh the Dashboard page
- Verify cart count and “Remove” buttons persist correctly

### UC-023: Sorting products updates display order correctly with multiple sorts
- Sort by Name (A to Z), verify order
- Sort by Price (high to low), verify order
- Sort by Name (Z to A), verify order
- Sort by Price (low to high), verify order

### UC-024: Prevent adding same item multiple times (idempotency)
- Click “Add to cart” for an item multiple times
- Verify cart count increments only once per unique item
- Button text remains “Remove” after first click

<<<<<<< tests-26-35
### UC-025: Responsive layout check for Dashboard page
=======
### UC-025: Clicking logo navigates to Dashboard
- From any page (e.g., product detail or cart)
- Click the Sauce Labs logo
- Verify navigation back to `/inventory.html`

### UC-026: Responsive layout check for Dashboard page
>>>>>>> main
- Resize browser window to mobile size
- Verify product grid adjusts correctly
- Verify burger menu functions properly on small screen

<<<<<<< tests-26-35
### UC-026: Error handling for product loading failure
- Simulate failure to load products (mock/fail API)
- Verify appropriate error message or fallback UI is shown

### UC-027: Cart icon updates when removing items from Cart page
=======
### UC-027: Error handling for product loading failure
- Simulate failure to load products (mock/fail API)
- Verify appropriate error message or fallback UI is shown

### UC-028: Cart icon updates when removing items from Cart page
>>>>>>> main
- Add multiple items to cart
- Go to Cart page
- Remove an item
- Verify cart icon badge updates on Dashboard

<<<<<<< tests-26-35
### UC-028: Cart contents persist across sessions
=======
### UC-029: Cart contents persist across sessions
>>>>>>> main
- Add items to cart
- Logout and close browser
- Reopen browser and login
- Verify cart contains previously added items

<<<<<<< tests-26-35
### UC-029: Performance check on Dashboard load
=======
### UC-030: Performance check on Dashboard load
>>>>>>> main
- Measure load time for Dashboard page
- Assert load time is within acceptable limits (e.g., < 2 seconds)

### UC-030: Keyboard navigation works for all interactive elements
- Log in with valid credentials
- Use **Tab** to move through product cards (Add to cart / Remove), burger menu, cart icon, and sort dropdown
- Verify a visible focus indicator on each element
- Press **Enter** or **Space** on focused buttons/links and assert correct actions trigger

### UC-031: Screen reader-friendly attributes
- Log in with valid credentials
- Verify product images have meaningful `alt` text
- Verify buttons/controls expose useful labels (e.g., `aria-label`)
- Confirm page landmark roles (e.g., `<main>`, `<nav>`) exist where applicable

### UC-032: Sorting preserves cart state
- Log in and add **two** specific products to the cart
- Apply a sort order (e.g., **Price (low to high)**)
- Verify cart count and “Remove” state persist
- Change sort again and re-verify persistence

### UC-033: Cart state after logout/login (no leakage)
- Log in and add **one** item to the cart
- Log out via burger menu
- Log in again as the **same** user → assert cart count is **0** (or expected app behavior)
- Log in as a **different** user → assert no cart leakage

### UC-034: Product name overflow handling
- Log in and inspect all product titles
- If a title is long, verify it wraps or truncates (ellipsis) without breaking layout

### UC-035: Price edge cases render correctly
- Log in and verify all prices are > $0.00
- (If test data allows) validate formatting and sorting for extreme values (e.g., $0.00, > $10,000)

### UC-XXX: Rapid add/remove clicking is robust
- Log in, pick one product
- Alternate **Add to cart** / **Remove** rapidly (~10 times)
- Assert no duplicate entries, final cart count is consistent, and button state matches cart

### UC-XXX: Multiple detail pages in new tabs
- Log in and open **3** product detail pages in new tabs
- From one detail page, add the item to cart
- Return to Dashboard
- Verify cart count reflects the change (with/without refresh depending on app behavior)

### UC-XXX: Direct dashboard access blocked without auth
- Log out
- Navigate directly to `/inventory.html`
- Assert redirect to login and no dashboard content visible

### UC-XXX: Invalid product ID URL is handled gracefully
- Log in and open a product detail page
- Manually alter the product ID in the URL to a non-existent one
- Assert the app shows an error or redirects cleanly (no broken layout/crash)

---