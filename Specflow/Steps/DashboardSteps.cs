using AventStack.ExtentReports;
using KARE.E2E.AUTOMATION.Data.API;
using KARE.E2E.AUTOMATION.Helpers;
using KARE.E2E.AUTOMATION.PageObjects.API;
using KARE.E2E.AUTOMATION.PageObjects.WEB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AventStack.ExtentReports.Gherkin.Model;
using TechTalk.SpecFlow;
using BoDi;
using System.ComponentModel;
using AventStack.ExtentReports.MarkupUtils;
using static KARE.E2E.AUTOMATION.PageObjects.API.Models.Response.RandomDataAlternate;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium.DevTools.V116.Debugger;

namespace KARE.E2E.AUTOMATION.Specflow.Steps
{
    [Binding]
    public class DashboardSteps : ExtentReport
    {
        #region Declaration
        private DriverContext driverContext;
        private LoginPage loginPage;
        private DashboardPage dashboardPage;
        public ScenarioContext scenarioContext;
        public FeatureContext featureContext;

        #endregion

        #region Constructor
        public DashboardSteps(DriverContext driverContext, ScenarioContext scenariocontext, FeatureContext featurecontext)
        {
            this.driverContext = driverContext;
            scenarioContext = scenariocontext;
            featureContext = featurecontext;
            loginPage = new LoginPage(driverContext.WebDriver);
            dashboardPage = new DashboardPage(driverContext.WebDriver);
        }
        #endregion

        #region stepDefinitions

        [Given(@"I login to Portal")]
        public void GivenILoginToPortal()
        {
            loginPage.NavigateToURL(ConfigHelper.GetURL());
            loginPage.LoginToApplication(ConfigHelper.GetEmail(), ConfigHelper.GetPassword());
        }

        [Then(@"I logout the portal")]
        public void ThenILogoutThePortal()
        {
            loginPage.LogoutApplication();
        }

        [When(@"I land at Dashboard")]
        public void WhenILandAtDashboard()
        {
            dashboardPage.ValidateUserLandsAtDashboard();
        }

        [Then(@"I validate Dashboard")]
        public void ThenIValidateDashboard()
        {
            dashboardPage.ValidateUIElementsInDashboard();
        }

        [Then(@"I validate side menu in dashboard")]
        public void ThenIValidateSideMenuInDashboard()
        {
            dashboardPage.ValidateSideMenuInDashboard();
        }

        [Then(@"I validate '([^']*)' Tab in dashboard")]
        public void ThenIValidateTabInDashboard(string tabName)
        {
            switch(tabName)
            {
                case "Your Balance":
                    dashboardPage.ValidateYourBalanceTabInDashboard();
                    break;
                case "Balance History":
                    dashboardPage.ValidateBalanceHistoryTabInDashboard();
                    break;
                case "Your Transactions":
                    dashboardPage.ValidateYourTransactionsTabInDashboard();
                    break;
                case "Total Value":
                    dashboardPage.ValidateTotalValueTabInDashboard();
                    break;
                case "Total Transactions":
                    dashboardPage.ValidateTotalTransactionsTabInDashboard();
                    break;
                case "Your Employees":
                    dashboardPage.ValidateEmployeesTabInDashboard();
                    break; ;
            }
        }

        [When(@"I click on '([^']*)' link")]
        public void WhenIClickOnLink(string link)
        {
            switch (link)
            {
                case "View all transfers":
                    dashboardPage.ClickViewAllTransfersLink();
                    break;
                case "View all transactions":
                    dashboardPage.ClickViewAllTransactionsLink();
                    break;
                case "View all Employees":
                    dashboardPage.ClickViewAllEmployeesLink();
                    break;
            }
        }

        [Then(@"I validate '([^']*)' page")]
        public void ThenIValidatePage(string page)
        {
            switch (page)
            {
                case "Transaction status":
                    dashboardPage.ValidateTransactionStatusPage();
                    break;
                case "employees":
                    dashboardPage.ValidateEmployeesPage();
                    break;
                case "Show All filter and Display filter in employees":
                    dashboardPage.VerifyShowAllFilter();
                    dashboardPage.VerifyDisplayFilter();
                    break;
                case "Load corporate account":
                    dashboardPage.ValidateLoadCorporateAccountPage();
                    break;
                case "Review company details":
                    break;
                case "Payments":
                    dashboardPage.ValidatePaymentsPage();
                    break;
                case "WPS Payments":
                    dashboardPage.ValidateWPSPaymentsPage();
                    break;
                case "Users":
                    dashboardPage.ValidateUsersPage();
                    break;
                case "Permissions":
                    dashboardPage.ValidatePermissionsPage();
                    break;
                case "Create Bulk Payment":
                    dashboardPage.ValidateNonWpsPaymentsBulk_CreatePayments();
                    break;
                case "Create Individual Payment":
                    dashboardPage.ValidateNonWpsPaymentsIndividual_CreatePayments();
                    break;
                case "Create WPS payments":
                    dashboardPage.ValidateWpsPayments_CreatePayments();
                    break;
                case "Authorize Bulk Payment":
                    dashboardPage.ValidateNonWpsPaymentsBulk_AuthorizePayments();
                    break;
                case "Authorize Individual Payment":
                    dashboardPage.ValidateNonWpsPaymentsIndividual_AuthorizePayments();
                    break;
                case "Authorize WPS payments":
                    dashboardPage.ValidateWpsPayments_AuthorizePayments();
                    break;
            }
        }

        [Then(@"I click on '([^']*)' in side menu")]
        public void ThenIClickOnInSideMenu(string link)
        {
            switch (link)
            {
                case "Employees":
                    dashboardPage.ClickEmployeesInSideMenu();
                    break;
                case "Load corporate account":
                    dashboardPage.ClickLoadCorporateAccountInSideMenu();
                    break;
                case "Company info":
                    break;
                case "View Payments":
                    dashboardPage.ClickViewPayments();
                    break;
                case "WPS Status":
                    dashboardPage.ClickWPSStatus();
                    break;
                case "Status Report":
                    dashboardPage.ClickStatusReport();
                    break;
                case "Users":
                    dashboardPage.ClickUsers();
                    break;
                case "Permissions":
                    dashboardPage.ClickPermissions();
                    break;
                case "Create Payments - Non WPS Payments Bulk":
                    dashboardPage.ClickNonWpsPaymentsBulk_CreatePayments();
                    break;
                case "Create Payments - Non WPS Payments Individual":
                    dashboardPage.ClickNonWpsPaymentsIndividual_CreatePayments();
                    break;
                case "Create Payments - WPS Payments":
                    dashboardPage.ClickWpsPayments_CreatePayments();
                    break;
                case "Authorize Payments - Non WPS Payments Bulk":
                    dashboardPage.ClickNonWpsPaymentsBulk_AuthorizePayments();
                    break;
                case "Authorize Payments - Non WPS Payments Individual":
                    dashboardPage.ClickNonWpsPaymentsIndividual_AuthorizePayments();
                    break;
                case "Authorize Payments - WPS Payments":
                    dashboardPage.ClickWpsPayments_AuthorizePayments();
                    break;
            }
        }

        [Then(@"I refresh the page")]
        public void ThenIRefreshThePage()
        {
            dashboardPage.Refresh();
        }

        [Then(@"I click on '([^']*)' button")]
        public void ThenIClickOnButton(string button)
        {
            switch (button)
            {
                case "Add Employee":
                    dashboardPage.ClickAddEmployeeButton();
                    break;
            }
        }

        [Then(@"I verify the Add employee screen")]
        public void ThenIVerifyTheAddEmployeeScreen()
        {
            dashboardPage.VerifyAddEmployeeScreen();
        }

        #endregion
    }
}