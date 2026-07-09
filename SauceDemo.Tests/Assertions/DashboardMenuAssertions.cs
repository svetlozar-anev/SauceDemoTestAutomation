// <copyright file="DashboardMenuAssertions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Assertions
{
    
    using FluentAssertions;
    using SauceDemo.UI.Components;

    public class DashboardMenuAssertions
    {
        private readonly DashboardMenu dashboardMenu;

        public DashboardMenuAssertions(DashboardMenu dashboardMenu)
        {
            this.dashboardMenu = dashboardMenu;
        }
        
        public void ShouldShowMenu()
        {
            dashboardMenu.IsMenuVisible().Should().BeTrue();
        }

        public void ShouldContainAboutOption()
        {
            dashboardMenu.GetOptions().Should().Contain("About");
        }

        public void ShouldContainLogoutOption()
        {
            dashboardMenu.GetOptions().Should().Contain("Logout");
        }
    }
}
#pragma warning restore SA1600