# 🧪 SauceDemo Use Cases

This document details the main test automation use cases covered by the SauceDemo test suite.

---

## ✅ Use Cases Covered

### UC-1: Login with empty credentials
- Fill in **any** text in username and password fields
- Clear both fields
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### UC-2: Login with missing password
- Fill in a username
- Fill in password
- Clear only the password
- Click **Login**
- Assert error message: `"Epic sadface: Password is required"`

### UC-3: Valid login
- Fill in valid username (`standard_user`, `problem_user`, `performance_glitch_user`, `error_user`, `visual_user`)
- Enter password: `secret_sauce`
- Click **Login**
- Assert dashboard title: `"Swag Labs"`

### UC-4: Login with locked out user
- Fill in locked out username: `locked_out_user`
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Sorry, this user has been locked out."`

### UC-5: Login fails with incorrect password
- Fill in valid username: `standard_user`
- Enter wrong password: `wrong_password`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### UC-6: Login fails with empty username and password
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### UC-7 Login fails with empty username
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### UC-8 Login with special characters in username and password
- Fill in username: `!@#$%^&*()`
- Enter password: `!@#$%^&*()`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### UC-9 Login with whitespace-only username and password
- Fill in empty username: `    `
- Enter empty password: `    `
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

---