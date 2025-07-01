# SauceDemo Testing

This test automation project verifies login functionality on [https://www.saucedemo.com](https://www.saucedemo.com) using Selenium WebDriver with NUnit and Fluent Assertions.

---

## ✅ Use Cases Covered

### UC-1: Login with empty credentials
- Fill in **any** text in username and password fields
- Clear both fields
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### UC-2: Login with missing password
- Fill in a username
- Fill in password
- Clear only the password
- Click **Login**
- Assert error message: `"Epic sadface: Password is required"`

### UC-3: Valid login
- Fill in valid username (`standard_user`, `problem_user`, `performance_glitch_user`, `error_user`, `visual_user`)
- Enter password: `secret_sauce`
- Click **Login**
- Assert dashboard title: `"Swag Labs"`

---

## 🛠️ Tech Stack

| Tool / Library        | Purpose                              |
|-----------------------|---------------------------------------|
| C#                    | Main language                        |
| Selenium WebDriver    | UI interaction                       |
| NUnit                 | Test framework (used for core tests) |
| SpecFlow              | Gherkin-based BDD test support       |
| FluentAssertions      | Clean assertions                     |
| Chrome & Edge         | Cross-browser support                |
| CSS Selectors         | Element location strategy            |
| ThreadLocal WebDriver | Parallel test support                |

> 🔄 Both **NUnit tests** and **SpecFlow scenarios** are implemented side-by-side.

---

## 🔧 Project Structure and Design

This solution is organized into **three separate projects** to follow clean architecture and separation of concerns:

### 1. `SauceDemo.Core`
- Shared utilities and configuration
- Base classes like `WebDriverFactory`
- Common config (`TestConfig`, `appsettings.json`)
- 💡 Does **not** depend on any other project.

### 2. `SauceDemo.UI`
- Page Object Model classes (`BasePage` `LoginPage`, `DashboardPage`)
- Encapsulates all UI-specific interactions
- Depends **only on** `SauceDemo.Core`

### 3. `SauceDemo.Tests`
- NUnit test classes like `LoginTests`
- Test definitions and assertions
- Depends on both `SauceDemo.UI` and `SauceDemo.Core`

### 🔁 Dependency Direction
- Core -> (no dependencies)
- UI -> Core
- Tests -> UI, Core

This layout supports scalability, test isolation, and reuse of logic across different test suites.

## 🗂️ Project Layout

```text
🧰 SauceDemo.Core/
├── Config/
│   ├── appsettings.json
│   └── TestConfig.cs
├── Utilities/
│   └── WebDriverFactory.cs
└── SauceDemo.Core.csproj

🖥️ SauceDemo.UI/
├── Base/
│   └── BasePage.cs
├── Pages/
│   ├── DashboardPage.cs
│   └── LoginPage.cs
└── SauceDemo.UI.csproj

🧪 SauceDemo.Tests/
├── Features/
│   └── Login.feature
├── Hooks/
│   └── Hooks.cs
├── Steps/
│   └── LoginSteps.cs
├── Tests/
│   └── LoginTests.cs
├── specflow.json
└── SauceDemo.Tests.csproj

📁 Root Solution Files:
├── .editorconfig
├── .gitignore
├── README.md
├── stylecop.json
└── SauceDemo.sln
```


---

## 🚀 Running Tests

```bash
# Using NUnit CLI or Visual Studio Test Explorer
dotnet test
