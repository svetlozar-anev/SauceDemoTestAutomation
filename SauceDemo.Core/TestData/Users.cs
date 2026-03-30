// <copyright file="Users.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Core.TestData
{
    public static class Users
    {
        // Standard login user
        public const string Standard = "standard_user";

        // Other demo users
        public const string LockedOut = "locked_out_user";
        public const string Problem = "problem_user";
        public const string PerformanceGlitch = "performance_glitch_user";
        public const string Error = "error_user";
        public const string Visual = "visual_user";

        // Common password for all demo users
        public const string Password = "secret_sauce";
        public const string WrongPassword = "wrong_password";
    }
}