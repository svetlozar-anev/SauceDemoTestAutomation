// <copyright file="TestContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>


#pragma warning disable SA1600
namespace SauceDemo.Tests.Base
{
    
    using OpenQA.Selenium;
    using SauceDemo.Tests.Assertions;
    using SauceDemo.Tests.Steps;
    using SauceDemo.UI.Components;
    using SauceDemo.UI.Pages;

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
       public CartSteps CartSteps { get; }
       public DashboardMenuSteps DashboardMenuSteps { get;  }
       public DashboardSteps DashboardSteps { get; }
       public LoginSteps LoginSteps { get; }
       public ProductDetailSteps ProductDetailSteps { get; }
       
       // Assertions
       public CartAssertions CartAssertions { get; }
       public DashboardAssertions DashboardAssertions { get; }
       public DashboardMenuAssertions DashboardMenuAssertions { get; }
       public LoginAssertions LoginAssertions { get; }
       public ProductDetailAssertions ProductDetailAssertions { get; }
       public SauceLabsAssertions SauceLabsAssertions { get; }
       
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
           CartSteps = new CartSteps(CartComponent);
           DashboardMenuSteps = new DashboardMenuSteps(DashboardMenu);
           DashboardSteps = new DashboardSteps(DashboardComponent);
           LoginSteps = new LoginSteps(LoginComponent);
           ProductDetailSteps = new ProductDetailSteps(ProductDetailComponent);
           
           // Assertions
           CartAssertions = new CartAssertions(CartComponent);
           DashboardAssertions = new DashboardAssertions(DashboardComponent);
           DashboardMenuAssertions = new DashboardMenuAssertions(DashboardMenu);
           LoginAssertions = new LoginAssertions(LoginComponent);
           ProductDetailAssertions = new ProductDetailAssertions(ProductDetailComponent);
           SauceLabsAssertions = new SauceLabsAssertions(SauceLabsComponent);
       }
       
    }
}
#pragma warning restore SA1600