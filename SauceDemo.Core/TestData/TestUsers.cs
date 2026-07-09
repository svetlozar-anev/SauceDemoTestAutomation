// <copyright file="TestUsers.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable SA1600
namespace SauceDemo.Core.TestData
{
    public static class TestUsers
    {
        // Standard login user
        public const string Standard = "standard_user";

        // Other demo users
        public const string LockedOut = "locked_out_user";
        public const string Problem = "problem_user";
        public const string PerformanceGlitch = "performance_glitch_user";
        public const string Error = "error_user";
        public const string Visual = "visual_user";
        public const string Unknown = "this_user_does_not_exist";

        // Common password for all demo users
        public const string Password = "secret_sauce";
        public const string WrongPassword = "wrong_password";

        public const string SpecialChars = "!@#$%^&*()";
        public const string EmptyString = "       ";
    }
}
#pragma warning restore SA1600