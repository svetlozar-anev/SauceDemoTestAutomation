# 🧪 SauceDemo Use Cases

This document details the main test automation use cases covered by the SauceDemo test suite.

---

## ✅ Use Cases Covered

### UC-001: Login fails with empty credentials
- Fill in **any** text in username and password fields
- Clear both fields
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### UC-002: Login fails with missing password
- Fill in a username
- Fill in password
- Clear only the password
- Click **Login**
- Assert error message: `"Epic sadface: Password is required"`

### UC-003: Valid login
- Fill in valid username (`standard_user`, `problem_user`, `performance_glitch_user`, `error_user`, `visual_user`)
- Enter password: `secret_sauce`
- Click **Login**
- Assert dashboard title: `"Swag Labs"`

### UC-004: Login fails with locked out user
- Fill in locked out username: `locked_out_user`
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Sorry, this user has been locked out."`

### UC-005: Login fails with wrong password
- Fill in valid username: `standard_user`
- Enter wrong password: `wrong_password`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### UC-006: Login fails with missing username
- Enter password: `secret_sauce`
- Click **Login**
- Assert error message: `"Epic sadface: Username is required"`

### UC-007: Login fails with special characters in username and password
- Fill in username: `!@#$%^&*()`
- Enter password: `!@#$%^&*()`
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

### UC-008: Login fails with whitespace-only username and password
- Fill in empty username: `    `
- Enter empty password: `    `
- Click **Login**
- Assert error message: `"Epic sadface: Username and password do not match any user in this service"`

---