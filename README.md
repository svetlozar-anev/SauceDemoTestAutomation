# SauceDemo Automated UI Test Suite

This test automation project verifies login functionality on [https://www.saucedemo.com](https://www.saucedemo.com) using Selenium WebDriver with NUnit and Fluent Assertions.

---

## ✅ Use Cases Covered

### UC-1: Login with empty credentials
- Fill in **any** text in username and password fields
- Clear both fields
- Click **Login**
- Assert error message: `"Username is required"`

### UC-2: Login with missing password
- Fill in a username
- Fill in password
- Clear only the password
- Click **Login**
- Assert error message: `"Password is required"`

### UC-3: Valid login
- Fill in valid username (e.g., `standard_user`)
- Enter password: `secret_sauce`
- Click **Login**
- Assert dashboard title: `"Swag Labs"`

---

## 🛠️ Tech Stack

| Tool / Library        | Purpose                     |
|-----------------------|-----------------------------|
| C#                    | Main language               |
| Selenium WebDriver    | UI interaction              |
| NUnit                 | Test framework              |
| FluentAssertions      | Clean assertions            |
| Chrome & Edge         | Cross-browser support       |
| CSS Selectors         | Element location strategy   |
| ThreadLocal WebDriver | Parallel test support       |
| Log4Net *(optional)*  | Logging                     |
| Design Patterns       | Singleton, Builder, Decorator (optional) |
| [Optional] BDD        | Gherkin-style specs         |

---

## 🚀 Running Tests

```bash
# Using NUnit CLI or Visual Studio Test Explorer
dotnet test
