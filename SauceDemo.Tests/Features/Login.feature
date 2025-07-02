Feature: Login Functionality

  Scenario: UC-1 Attempt to login with empty username and password
    Given I am on the login page
    And I enter "some_user" in the username field
    And I enter "some_password" in the password field
    And I clear the username field
    And I clear the password field
    When I click the login button
    Then I should see the error message "Epic sadface: Username is required"

  Scenario: UC-2 Attempt to login with missing password
    Given I am on the login page
    And I enter "standard_user" in the username field
    And I enter "secret_sauce" in the password field
    And I clear the password field
    When I click the login button
    Then I should see the error message "Epic sadface: Password is required"

  Scenario: UC-3 Successful login with valid credentials
    Given I am on the login page
    And I enter "<username>" in the username field
    And I enter "<password>" in the password field
    When I click the login button
    Then I should be redirected to the dashboard
    And the page title should be "Swag Labs"

    Examples:
      | username        | password       |
      | standard_user   | secret_sauce   |
      | problem_user    | secret_sauce   |
      | performance_glitch_user | secret_sauce |
      | error_user      | secret_sauce   |
      | visual_user      | secret_sauce   |

  Scenario: UC-5 Login fails with incorrect password
    Given I am on the login page
    And I enter "standard_user" in the username field
    And I enter "wrong_password" in the password field
    When I click the login button
    Then I should see the error message "Epic sadface: Username and password do not match any user in this service"

  Scenario: UC-6 Login fails with empty username and password
    Given I am on the login page
    When I click the login button
    Then I should see the error message "Epic sadface: Username is required"

  Scenario: UC-7 Login fails with empty username
    Given I am on the login page
    And I enter "secret_sauce" in the password field
    When I click the login button
    Then I should see the error message "Epic sadface: Username is required"

  Scenario: UC-8 Login with special characters in username and password
    Given I am on the login page
    And I enter "!@#$%^&*()" in the username field
    And I enter "!@#$%^&*()" in the password field
    When I click the login button
    Then I should see the error message "Epic sadface: Username and password do not match any user in this service"

  Scenario: UC-9 Login with whitespace-only username and password
    Given I am on the login page
    And I enter "    " in the username field
    And I enter "    " in the password field
    When I click the login button
    Then I should see the error message "Epic sadface: Username and password do not match any user in this service"