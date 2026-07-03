// <copyright file="LoginAssertions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>


using FluentAssertions;
using SauceDemo.Core.TestData;

#pragma warning disable SA1600
namespace SauceDemo.Tests.Assertions
{
    using SauceDemo.UI.Components;
    
    public class LoginAssertions
    {
        private readonly LoginComponent login;
        
        public LoginAssertions(LoginComponent login)
        {
            this.login = login;
        }

        public void ShouldRequireUsername()
        {
            login.GetErrorMessage().Should().Be(LoginErrors.UsernameRequired);
        }
        
        public void ShouldRequirePassword()
        {
            login.GetErrorMessage().Should().Be(LoginErrors.PasswordRequired);
        }

        public void ShouldRejectCredentials()
        {
            login.GetErrorMessage().Should().Be(LoginErrors.InvalidCredentials);
        }

        public void ShouldShowLockedOutUserMessage()
        {
            login.GetErrorMessage().Should().Be(LoginErrors.LockedOut);
        }
    }
}
#pragma warning restore SA1600