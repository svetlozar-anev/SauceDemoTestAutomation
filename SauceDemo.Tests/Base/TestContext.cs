// <copyright file="TestContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using SauceDemo.Tests.Assertions;
using SauceDemo.Tests.Steps;
using SauceDemo.UI.Components;
using SauceDemo.UI.Pages;

#pragma warning disable SA1600
namespace SauceDemo.Tests.Base
{
    using OpenQA.Selenium;

    public class TestContext
    {
       #pragma warning disable SA1516
       // Page Objects
       public CartComponent CartComponent { get; }
       public DashboardComponent DashboardComponent { get; }
       public DashboardMenu DashboardMenu { get; }
       public DashboardProducts DashboardProducts { get; }
       public HeaderComponent HeaderComponent { get; }
       public LoginComponent LoginComponent { get; }
       public ProductDetailComponent ProductDetailComponent { get; }
       public SauceLabsComponent SauceLabsComponent { get; }
       
       // Steps
       public LoginSteps LoginSteps { get; }
       
       // Assertions
       public DashboardAssertions DashboardAssertions { get; }
       public LoginAssertions LoginAssertions { get; }

       private IWebDriver Driver { get; }
       #pragma warning restore SA1516
        
       public TestContext(IWebDriver driver)
       {
           Driver = driver;
           
           // Page Objects
           CartComponent = new CartComponent(Driver);
           DashboardComponent = new DashboardComponent(Driver);
           DashboardMenu = new DashboardMenu(Driver);
           DashboardProducts = new DashboardProducts(Driver);
           HeaderComponent = new HeaderComponent(Driver);
           LoginComponent = new LoginComponent(Driver);
           ProductDetailComponent = new ProductDetailComponent(Driver);
           SauceLabsComponent = new SauceLabsComponent(Driver);
           
           // Steps
           LoginSteps = new LoginSteps(LoginComponent);
           
           // Assertions
           LoginAssertions = new LoginAssertions(LoginComponent);
           DashboardAssertions = new DashboardAssertions(DashboardComponent);
       }
       
    }
}
#pragma warning restore SA1600