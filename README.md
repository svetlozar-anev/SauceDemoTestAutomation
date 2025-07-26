# SauceDemo Testing

This C# test automation project verifies different functionalities on [https://www.saucedemo.com](https://www.saucedemo.com) using Selenium WebDriver, SpecFlow, NUnit and Fluent Assertions.

---

## ✅ Use Cases

The project covers way too many scenarios and validations. 
For the full list of use cases and step-by-step breakdowns, check the [Use Cases Documentation](./docs/use-cases.md).

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
| ThreadLocal WebDriver | Parallel test execution              |
| Serilog               | Structured logging with file output  |

> 🔄 Both **NUnit tests** and **SpecFlow scenarios** are implemented side-by-side.

---

## 🔧 Solution Structure and Design

This solution is organized into **three separate projects** to follow clean architecture and separation of concerns:

### 1. `SauceDemo.Core`
- Shared utilities and configuration
- Serilog Logging (`Logger.cs`)
- WebDriver lifecycle (`WebDriverFactory.cs`)
- Common config (`TestConfig`, `appsettings.json`)
- 📦 No dependencies on other projects.

### 2. `SauceDemo.Tests`
- All NUnit and SpecFlow tests
- Step definitions (`Steps/`)
- Test classes (`Tests/`)
- 📦 Depends on both `SauceDemo.UI` and `SauceDemo.Core`

### 3. `SauceDemo.UI`
- Page Object Model classes (e.g., `LoginPage`, `DashboardPage`)
- Base page functionality (`BasePage.cs`)
- 📦 Depends **only on** `SauceDemo.Core`


### 🔁 Dependency Direction
- Core -> (no dependencies)
- Tests -> UI, Core
- UI -> Core

This layout supports scalability, test isolation, and reuse of logic across different test suites.

## 🗂️ Solution Layout

```text
🧰 SauceDemo.Core/
├── Config/
│   ├── appsettings.json
│   └── TestConfig.cs
├── Utilities/
│   └── Logger.cs
│   └── WebDriverFactory.cs
└── SauceDemo.Core.csproj

🧪 SauceDemo.Tests/
├── Features/
│   └── Login.feature
├── Steps/
│   ├── Base/
│   │   └── BaseSteps.cs
│   └── LoginSteps.cs
├── Tests/
│   ├── Base/
│   │   └── BaseTest.cs
│   └── LoginTests.cs
└── SauceDemo.Tests.csproj

🖥️ SauceDemo.UI/
├── Base/
│   └── BasePage.cs
├── Pages/
│   ├── DashboardPage.cs
│   └── LoginPage.cs
└── SauceDemo.UI.csproj

📁 Root Solution Files:
├── docs/
│   └── use-cases.md
├── logs/
│   └── NUnit-<date>.log
│   └── Selenium-<date>.log
├── .editorconfig
├── .gitignore
├── README.md
├── stylecop.json
└── SauceDemo.sln
```
---

## 📋 Logging Setup

Serilog is used for structured logging with daily rolling file output.

- `NUnit-<date>.log`  
  Logs activity from **NUnit-based test runs**.

- `Selenium-<date>.log`  
  Logs activity from **SpecFlow scenarios and browser interactions**.

📂 Log files are saved in the `logs/` folder at the root of the solution.

🧱 Two separate logger instances are initialized in [`Logger.cs`](./SauceDemo.Core/Utilities/Logger.cs):
- `NUnitLog` for classic NUnit tests
- `SeleniumLog` for SpecFlow scenarios

---

## 🚀 Running Tests

```bash
# Using NUnit CLI or Visual Studio Test Explorer
dotnet test
