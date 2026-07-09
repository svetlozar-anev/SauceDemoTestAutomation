// <copyright file="LoginSteps.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
namespace SauceDemo.Tests.Steps
{
    using SauceDemo.UI.Components;

    public class LoginSteps
    {
        private readonly LoginComponent login;

        public LoginSteps(LoginComponent login)
        {
            this.login = login;
        }

        public void OpenLoginPage()
        {
            login.Open();
        }

        public void SubmitCredentials(string username, string password)
        {
            login.EnterUsername(username);
            login.EnterPassword(password);
            login.ClickLogin();
        }

        public void SubmitEmptyCredentials(string username, string password)
        {
            login.EnterUsername(username);
            login.EnterPassword(password);
            login.ClearUsername();
            login.ClearPassword();
            login.ClickLogin();
        }

        public void LoginWithEmptyPassword(string username)
        {
            login.EnterUsername(username);
            login.ClickLogin();
        }

        public void LoginWithEmptyUsername(string password)
        {
            login.EnterPassword(password);
            login.ClickLogin();
        }

        public void RefreshPage()
        {
            login.Refresh();
        }

        public void NavigateToInventoryPage()
        {
            login.NavigateToInventory();
        }
    }
}
#pragma warning restore SA1600