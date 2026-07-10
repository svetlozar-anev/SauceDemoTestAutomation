// <copyright file="DashboardMenuSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Steps
{
    using SauceDemo.UI.Components;

    public class DashboardMenuSteps
    {
        private readonly DashboardMenu dashboardMenu;

        public DashboardMenuSteps(DashboardMenu dashboardMenu)
        {
            this.dashboardMenu = dashboardMenu;
        }

        public void OpenAboutPage()
        {
            dashboardMenu.Open();
            dashboardMenu.WaitAboutToLoad();
            dashboardMenu.ClickAbout();
        }
    }
}
#pragma warning restore SA1600