Feature: Dashboard

@TC-01
Scenario: TC_01_Login to Portal
Given I login to Portal
When I land at Dashboard
Then I validate Dashboard
Then I logout the portal

@TC-02
Scenario: TC_02_Validate side menu in dashboard
Given I login to Portal
When I land at Dashboard
Then I validate side menu in dashboard
Then I logout the portal
	
@TC-03
Scenario: TC_03_Validate your balance tab in dashboard
Given I login to Portal
When I land at Dashboard
Then I validate 'Your Balance' Tab in dashboard
Then I logout the portal

@TC-04
Scenario: TC_04_Validate balance history tab in dashboard
Given I login to Portal
When I land at Dashboard
Then I validate 'Balance History' Tab in dashboard
Then I logout the portal

@TC-05
Scenario: TC_05_Validate Your transactions tab in dashboard
Given I login to Portal
When I land at Dashboard
Then I validate 'Your Transactions' Tab in dashboard
Then I logout the portal

@TC-06
Scenario: TC_06_Validate total value tab in dashboard
Given I login to Portal
When I land at Dashboard
Then I validate 'Total Value' Tab in dashboard
Then I logout the portal

@TC-07
Scenario: TC_07_Validate total transactions tab in dashboard
Given I login to Portal
When I land at Dashboard
Then I validate 'Total Transactions' Tab in dashboard
Then I logout the portal

@TC-08
Scenario: TC_08_Validate Transactions status page by clicking View all transfers link in dashboard
Given I login to Portal
When I land at Dashboard
When I click on 'View all transfers' link
Then I validate 'Transaction status' page
Then I logout the portal

@TC-09
Scenario: TC_09_Validate Transactions status page by clicking View all trasactions link in dashboard
Given I login to Portal
When I land at Dashboard
When I click on 'View all transactions' link
Then I validate 'Transaction status' page
Then I logout the portal

@TC-10
Scenario: TC_10_Validate Your employees tab in dashboard
Given I login to Portal
When I land at Dashboard
Then I validate 'Your Employees' Tab in dashboard
Then I logout the portal

@TC-11
Scenario: TC_11_Validate Employees page by clicking View all employees link in dashboard
Given I login to Portal
When I land at Dashboard
When I click on 'View all Employees' link
Then I validate 'employees' page
Then I logout the portal

@TC-12
Scenario: TC_12_Validate Show All filter and Display filter in employees page by clicking employees link side menu
Given I login to Portal
When I land at Dashboard
Then I click on 'Employees' in side menu
Then I validate 'Show All filter and Display filter in employees' page
Then I refresh the page
Then I logout the portal

@TC-13
Scenario: TC_13_Validate Show All filter and Display filter in employees page by clicking 'View all employees' link
Given I login to Portal
When I land at Dashboard
When I click on 'View all Employees' link
Then I validate 'Show All filter and Display filter in employees' page
Then I refresh the page
Then I logout the portal

@TC-14
Scenario: TC_14_Validate Load corporate account page by clicking Load corporate account side menu
Given I login to Portal
When I land at Dashboard
Then I click on 'Load corporate account' in side menu
Then I validate 'Load corporate account' page
Then I logout the portal

@TC-15
Scenario: TC_15_Validate Review company details page by clicking company info side menu
Given I login to Portal
When I land at Dashboard
Then I click on 'Company info' in side menu
Then I validate 'Review company details' page
Then I logout the portal

@TC-16
Scenario: TC_16_Validate Payments page by clicking view payments in reports side menu
Given I login to Portal
When I land at Dashboard
Then I click on 'View Payments' in side menu
Then I validate 'Payments' page
Then I logout the portal

@TC-17
Scenario: TC_17_Validate WPS Payments page by clicking WPS status in reports side menu
Given I login to Portal
When I land at Dashboard
Then I click on 'WPS Status' in side menu
Then I validate 'WPS Payments' page
Then I logout the portal

@TC-18
Scenario: TC_18_Validate Transactions status page by clicking Status report in reports side menu
Given I login to Portal
When I land at Dashboard
Then I click on 'Status Report' in side menu
Then I validate 'Transaction status' page
Then I logout the portal

@TC-19
Scenario: TC_19_Validate Users page by clicking Users in Portal admins side menu
Given I login to Portal
When I land at Dashboard
Then I click on 'Users' in side menu
Then I validate 'Users' page
Then I logout the portal

@TC-20
Scenario: TC_20_Validate Permissions page by clicking Permissions in Portal admins side menu
Given I login to Portal
When I land at Dashboard
Then I click on 'Permissions' in side menu
Then I validate 'Permissions' page
Then I logout the portal

@TC-21
Scenario: TC_21_Validate Add Employee screen after clicking add employee button in Employee page
Given I login to Portal
When I land at Dashboard
Then I click on 'Employees' in side menu
Then I click on 'Add Employee' button
Then I verify the Add employee screen
Then I logout the portal

@TC-22
Scenario: TC_22_Validate UI elements in "Non WPS Payments Bulk" inside Transactions -> Create Payments
Given I login to Portal
When I land at Dashboard
Then I click on 'Create Payments - Non WPS Payments Bulk' in side menu
Then I validate 'Create Bulk Payment' page
Then I refresh the page
Then I logout the portal

@TC-23
Scenario: TC_23_Validate UI elements in "Non WPS Payments Individual" inside Transactions -> Create Payments
Given I login to Portal
When I land at Dashboard
Then I click on 'Create Payments - Non WPS Payments Individual' in side menu
Then I validate 'Create Individual Payment' page
Then I refresh the page
Then I logout the portal

@TC-24
Scenario: TC_24_Validate UI elements in "WPS Payments" inside Transactions -> Create Payments
Given I login to Portal
When I land at Dashboard
Then I click on 'Create Payments - WPS Payments' in side menu
Then I validate 'Create WPS payments' page
Then I refresh the page
Then I logout the portal

@TC-25
Scenario: TC_25_Validate UI elements in "Non WPS Payments Bulk" inside Transactions -> Authorize Payments
Given I login to Portal
When I land at Dashboard
Then I click on 'Authorize Payments - Non WPS Payments Bulk' in side menu
Then I validate 'Authorize Bulk Payment' page
Then I refresh the page
Then I logout the portal

@TC-26
Scenario: TC_26_Validate UI elements in "Non WPS Payments Individual" inside Transactions -> Authorize Payments
Given I login to Portal
When I land at Dashboard
Then I click on 'Authorize Payments - Non WPS Payments Individual' in side menu
Then I validate 'Authorize Individual Payment' page
Then I refresh the page
Then I logout the portal

@TC-27
Scenario: TC_27_Validate UI elements in "WPS Payments" inside Transactions -> Authorize Payments
Given I login to Portal
When I land at Dashboard
Then I click on 'Authorize Payments - WPS Payments' in side menu
Then I validate 'Authorize WPS payments' page
Then I refresh the page
Then I logout the portal