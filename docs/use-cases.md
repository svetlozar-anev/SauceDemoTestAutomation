# 🧪 SauceDemo Use Cases

This document details the main test automation use cases covered by the SauceDemo test suite.

---

## Login Scenarios

### LOGIN001: Login with valid credentials shows Dashboard
- Fill in valid username (`standard_user`, `problem_user`, `performance_glitch_user`, `error_user`, `visual_user`)
- Enter password: `secret_sauce`
- Click **Login**
- Assert dashboard page should be displayed

### LOGIN002: Login fails with empty credentials
- Fill in **any** text in username and password fields
- Clear both fields
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### LOGIN003: Login fails with missing username
- Fill in a password
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### LOGIN004: Login fails with missing password
- Fill in a username
- Click **Login**
- Assert error message: `"Epic sadface: Password is required"`

### LOGIN005: Login fails with locked out user
- Fill in locked out username: `locked_out_user`
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Sorry, this user has been locked out."`

### LOGIN006: Login fails with wrong password
- Fill in valid username: `standard_user`
- Enter wrong password: `wrong_password`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN007: Login fails with special characters in username and password
- Fill in username: `!@#$%^&*()`
- Enter password: `!@#$%^&*()`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN008: Login fails with whitespace-only username and password
- Fill in empty username: `    `
- Enter empty password: `    `
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN009: Login fails with unknown username
- Fill in unknown username: `unknown_user`
- Enter valid password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN010: Login fails with invalid username and password
- Fill in unknown username: `unknown_user`
- Enter wrong password: `wrong_password`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN011: Login fails when username casing is incorrect
- Fill in username: `STANDARD_USER`
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN012: Login fails when password casing is incorrect
- Fill in username: `standard_user`
- Enter password: `SECRET_SAUCE`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN013: Login fails with leading whitespace in username or password
- Fill in username: ` standard_user`
- Enter password: ` secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN014: Login fails with trailing whitespace in username or password
- Fill in username: `standard_user `
- Enter password: `secret_sauce `
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN015: Successful login after failed login
- Fill in username: `standard_user`
- Enter password: `wrong_password`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`
- Fill in username: `standard_user`
- Enter password: `secret_sauce`
- Click **Login**
- Assert dashboard should be displayed

### LOGIN016: Direct navigation to inventory redirects to login
- Navigate directly to `/inventory.html`
- Assert login page is displayed
- Assert error message `"Epic sadface: You can only access '/inventory.html' when you are logged in."`

### LOGIN017: Refresh after failed login
- Fill in username: `standard_user`
- Enter password: `wrong_password`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`
- Refresh the page
- Assert no error message is displayed

### LOGIN018: Login fails with extremely long username
- Fill in username consisting of 1000 characters
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### LOGIN019: Login fails with extremely long password
- Fill in username: `standard_user`
- Enter password consisting of 1000 characters
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

---

## Dashboard Scenarios

### DASHBOARD001: All product items are displayed
- Verify presence of all six product cards
- Each product has a name, price, and image

### DASHBOARD002: Product sort dropdown is present and functional
- Select **Name (A to Z)**
- Assert products are sorted alphabetically ascending
- Select **Name (Z to A)**
- Assert products are sorted alphabetically descending
- Select **Price (low to high)**
- Assert products are sorted by ascending price
- Select **Price (high to low)**
- Assert products are sorted by descending price

### DASHBOARD003: Clicking a product opens its detail page
- Click a product
- Assert the Product Details page is displayed
- Verify the selected product information is shown
- Return to the Dashboard
- Assert the Dashboard page is displayed

### DASHBOARD004: Add to cart button works per item
- Verify the selected product is not already in the cart
- Click **Add to cart**
- Assert the product is marked as added to the cart
- Assert the cart badge increases by one

### DASHBOARD005: Multiple products can be added to cart
- Add multiple products to the cart
- Verify each product changes to **Remove**
- Verify the cart badge increments after each addition

### DASHBOARD006: Navigation menu (burger menu) appears and functions
- Open the burger menu
- Verify the menu is displayed
- Verify the **About** option is present
- Verify the **Logout** option is present

### DASHBOARD007: Removing an item from the cart updates the UI
- Add a product to the cart
- Click **Remove**
- Verify the cart badge decreases by one
- Verify the product returns to the **Add to cart** state

### DASHBOARD008: Cart icon navigates to Cart page
- Add a product to the cart
- Click the cart icon
- Assert the Cart page is displayed
- Verify the selected product is present in the cart

### DASHBOARD009: Logout from the Dashboard page
- Open the burger menu
- Click **Logout**
- Assert the Login page is displayed

### DASHBOARD010: Cart persists after logout and login
- Add a product to the cart
- Record the cart badge count
- Logout
- Login again with the same user
- Verify the previously added product is still present
- Verify the cart badge count is unchanged

### DASHBOARD011: Product images are loaded correctly
- Verify every product image is displayed
- Verify every image is loaded successfully


### DASHBOARD012: Price format and currency consistency
- Verify every product price follows the expected currency format
- Verify all prices use the dollar (`$`) currency symbol

### DASHBOARD013: About menu option navigates to Sauce Labs
- Open the burger menu
- Click **About**
- Assert the Sauce Labs page is displayed

### DASHBOARD014: Cart persists after page refresh
- Add a product to the cart
- Verify the product is marked as added
- Refresh the page
- Verify the cart badge remains unchanged
- Verify the product is still marked as added

### DASHBOARD015: Product cannot be added multiple times
- Add a product to the cart
- Verify the cart badge increases by one
- Verify the product remains in the **Remove** state
- Verify the same product cannot be added again

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