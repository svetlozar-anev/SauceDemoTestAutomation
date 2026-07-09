// <copyright file="DashboardMenuSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Steps
{
    using SauceDemo.UI.Components;

    public class DashboardMenuSteps
    {
        private readonly DashboardMenu DashboardMenu;

        public DashboardMenuSteps(DashboardMenu DashboardMenu)
        {
            this.DashboardMenu = DashboardMenu;
        }

        public void OpenAboutPage()
        {
            DashboardMenu.Open();
            DashboardMenu.WaitAboutToLoad();
            DashboardMenu.ClickAbout();
        }
    }
}
#pragma warning restore SA1600