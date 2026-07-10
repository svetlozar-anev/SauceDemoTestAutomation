// <copyright file="SauceLabsAssertions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Assertions
{
    using FluentAssertions;
    using SauceDemo.UI.Components;
    
    public class SauceLabsAssertions
    {
        private readonly SauceLabsComponent sauceLabs;
        
        public SauceLabsAssertions(SauceLabsComponent sauceLabs)
        {
            this.sauceLabs = sauceLabs;
        }

        public void ShouldBeDisplayed()
        {
            sauceLabs.WaitForPageToLoad();
            sauceLabs.IsLoaded().Should().BeTrue();
        }
    }
}