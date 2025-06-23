Feature: Login

@TC-01
Scenario: TC_01_Validate UI elements in login page
Given I navigate to the application URL
When I verify user lands at login page
Then I validate the 'login' page

@TC-02
Scenario: TC_02_Validate UI elements in forgot password page
Given I navigate to the application URL
When I verify user lands at login page
Then I navigate to 'forgot password' page
Then I validate the 'forgot password' page

@TC-03
Scenario: TC_03_Validate UI elements in book a demo page
Given I navigate to the application URL
When I verify user lands at login page
Then I navigate to 'book a demo' page
Then I validate the 'book a demo' page

@TC-04
Scenario: TC_04_Validate login with invalid credential
Given I navigate to the application URL
When I verify user lands at login page
Then I validate the login with invalid credential

