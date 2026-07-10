# SauceDemoTestAutomation

A clean and maintainable C# Selenium WebDriver automation framework for testing the [SauceDemo](https://www.saucedemo.com) e-commerce website using NUnit and the Page Object Model pattern.

---

## 📌 Use Cases

The project covers a wide range of user scenarios and validations, including login functionality, product browsing, sorting, cart operations, and navigation flows.

For the complete list of implemented use cases with detailed descriptions, please refer to the [Use Cases Documentation](./docs/use-cases.md).

---

## 🧰 Tech Stack

| Tool / Library          | Purpose                              |
|-------------------------|--------------------------------------|
| C#                      | Main programming language            |
| Selenium WebDriver      | Browser automation                   |
| NUnit                   | Test framework                       |
| FluentAssertions        | Readable assertions                  |
| Chrome & Firefox        | Cross-browser testing                |
| Serilog                 | Structured logging                   |

---

## 🔧 Solution Structure

The solution is organized into three projects with clear separation of concerns:

- **`SauceDemo.Core`** – Shared configuration, utilities, logging, and test data  
- **`SauceDemo.UI`** – Page Object Model classes and base functionality  
- **`SauceDemo.Tests`** – All NUnit test classes  

**Dependency direction**: Core ← UI ← Tests

---

## 🗂️ Solution Layout

```text

🛠️ SauceDemo.Core/
├── Config/
│   ├── appsettings.json
│   └── TestConfig.cs
├── TestData/
│   ├── LoginErrors.cs
│   ├── ProductSort.cs
│   └── TestUsers.cs
├── Utilities/
│   ├── Logger.cs
│   ├── ViewportManager.cs
│   └── WebDriverFactory.cs
└── SauceDemo.Core.csproj

🧪 SauceDemo.Tests/
├── Assertions/
│   ├── CartAssertions.cs
│   ├── DashboardAssertions.cs
│   ├── DashboardMenuAssertions.cs
│   ├── LoginAssertions.cs
│   ├── ProductDetailAssertions.cs
│   └── SauceLabsAssertions.cs
├── Base/
│   ├── BaseTest.cs
│   └── TestContext.cs
├── Steps/
│   ├── CartSteps.cs
│   ├── DashboardMenuSteps.cs
│   ├── DashboardSteps.cs
│   ├── LoginSteps.cs
│   └── ProductDetailSteps.cs
├── Tests/
│   ├── DashboardTests.cs
│   ├── ...Tests.cs
│   └── LoginTests.cs
├── SauceDemo.Tests.csproj
└── appsettings.json

🖥️ SauceDemo.UI/
├── Base/
│   └── BaseComponent.cs
├── Components/
│   ├── CartComponent.cs
│   ├── DashboardComponent.cs
│   ├── DashboardMenu.cs
│   ├── DashboardProducts.cs
│   ├── HeaderComponent.cs
│   ├── LoginComponent.cs
│   ├── ProductDetailComponent.cs
│   └── SauceLabsComponent.cs
└── SauceDemo.UI.csproj

📁 Root Solution Files:
├── docs/
│   └── use-cases.md
├── logs/
│   └── NUnit-<date>.log
├── .editorconfig
├── .gitignore
├── README.md
├── global.json
├── stylecop.json
└── SauceDemo.sln
```
---

## 📋 Logging

Serilog provides structured logging with daily rolling files.

📂 All logs are saved in the logs/ folder as NUnit-<date>.log.

---

## 🚀 Running Tests

```bash
# Using NUnit CLI or Visual Studio Test Explorer
dotnet test
```
