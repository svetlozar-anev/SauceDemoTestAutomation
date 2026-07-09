// <copyright file="LoginErrors.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.Core.TestData
{
    public class LoginErrors
    {
        public const string UsernameRequired = "Epic sadface: Username is required";
        public const string PasswordRequired = "Epic sadface: Password is required";
        public const string InvalidCredentials = "Epic sadface: Username and password do not match any user in this service";
        public const string LockedOut = "Epic sadface: Sorry, this user has been locked out.";
        public const string NoAccessToInventory = "Epic sadface: You can only access '/inventory.html' when you are logged in.";
    }
}
#pragma warning restore SA1600