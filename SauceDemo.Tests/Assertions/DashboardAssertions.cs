// <copyright file="DashboardAssertions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentAssertions;

#pragma warning disable SA1600
namespace SauceDemo.Tests.Assertions
{
    using SauceDemo.UI.Components;
    
    public class DashboardAssertions
    {
        private readonly DashboardComponent dashboard;

        public DashboardAssertions(DashboardComponent dashboard)
        {
            this.dashboard = dashboard;
        }

        public void ShouldBeDisplayed()
        {
            dashboard.IsAtDashboard().Should().BeTrue();
            dashboard.GetPageTitle().Should().Be("Swag Labs");
        }
    }
}
#pragma warning restore SA1600