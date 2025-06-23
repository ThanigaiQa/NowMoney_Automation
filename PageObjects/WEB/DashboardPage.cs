using KARE.E2E.AUTOMATION.Data.API;
using KARE.E2E.AUTOMATION.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.PageObjects.WEB
{
    public class DashboardPage
    {
        #region Declaration
        private IWebDriver _driver;
        private SeleniumActions seleniumActions;
        public Utilities utility;
        #endregion

        #region Constructor
        public DashboardPage(IWebDriver driver)
        {
            _driver = driver;
            seleniumActions = new SeleniumActions(_driver);
            utility = new Utilities();
        }
        #endregion

        #region pageLocators
        By txtEmail => By.XPath("//input[@name='username']");
        By txtPassword => By.XPath("//input[@name='password']");
        By btnLogin => By.XPath("//button[@type='submit']");
        By txtHeader => By.XPath("//div[contains(text(),'Welcome to Now Money, ')]");
        By lblYourBalance => By.XPath("//a[text()='Your Balance']//parent::li");
        By lblBalanceHistory => By.XPath("//a[text()='Balance History']//parent::li");
        By lblYourTransaction => By.XPath("//a[text()='Your Transactions']//parent::li");
        By lblTotalValue => By.XPath("//a[text()='Total Value']//parent::li");
        By lblTotalTransactions => By.XPath("//a[text()='Total Transactions']//parent::li");
        By lblCurrentBalanceHeld => By.XPath("//div[text()='Current Balance Held With NOW Money']");
        By lblTotalMoneyTransfered => By.XPath("//div[text()='Total Money Transferred']");
        By lblCurrentBalance => By.XPath("//span[contains(text(),'CURRENT BALANCE: AED')]");
        By lnkDashboard_SideMenu => By.XPath("//a[@activeclassname='active']//span[contains(text(),'Dashboard')]");
        By lnkEmployees_SideMenu => By.XPath("//a[@activeclassname='active']//span[contains(text(),'Employees')]");
        By lnkLoadCorporateAccount_SideMenu => By.XPath("//a[@activeclassname='active']//span[contains(text(),'Load corporate account')]");
        By lnkCompanyInfo_SideMenu => By.XPath("//a[@activeclassname='active']//span[contains(text(),'Company info')]");
        By lnkTransactions_SideMenu => By.XPath("//li[contains(@class,'nav-item')]//span[contains(text(),'Transactions')]");
        By lnkPortalAdmins_SideMenu => By.XPath("//li[contains(@class,'nav-item')]//span[contains(text(),'Portal Admins')]");
        By lnkReports_SideMenu => By.XPath("//li[contains(@class,'nav-item')]//span[contains(text(),'Reports')]");
        By tblComponents_YourTransactions => By.XPath("//div[contains(@class,'ChartComponents_YourTransactions')]");
        By lblValueOfYourTransactions => By.XPath("//div[text()='Value Of Your Transactions']");
        By lblNumberOfPayments => By.XPath("//div[text()='Number of payments']");
        By lnkViewAllTransfers => By.XPath("//a[@href='/payroll/statusReport' and text()='View all transfers']");
        By lnkViewAllTransactions => By.XPath("//a[@href='/payroll/statusReport' and text()='View all Transactions']");
        By lblTransactionStatus => By.XPath("//h3[text()='Transaction Status']");
        By btnDownloadAll => By.XPath("//button//span[text()='Download all']");
        By btnAccountStatement => By.XPath("//button//span[text()='Account statement']");
        By inp_Search => By.XPath("//input[@name='search']");
        By displayFilter => By.XPath("//div[@id='demo-mutiple-checkbox']");
        By lblYourEmployees => By.XPath("//a[text()='Your Employees']");
        By lblTotalEnrolledEmployees => By.XPath("//div[text()='Total Enrolled Employees']");
        By lnkViewAllEmployees => By.XPath("//a[@href='/payroll/employees' and text()='View all Employees']");
        By lblEmployees => By.XPath("//h3[text()='Employees']");
        By btnUploadEmployees => By.XPath("//button//span[text()='Upload employees']");
        By btnAddEmployees => By.XPath("//span[normalize-space()='Add employee(s)']//parent::button");
        By btnDownloadXLSX => By.XPath("//button//span[text()='Download XLSX']");
        By tbl_ShowAll => By.XPath("//ul[@role='listbox']");
        By tbl_Display => By.XPath("//ul[@role='listbox']");
        By drp_ShowAllFilter => By.XPath("//div[@role='button' and text()='Show All']");
        By drp_DisplayFilter => By.XPath("//div[@role='button' and text()='Display']");
        By img_NowMoneyLogoSidebar => By.XPath("//div[contains(@class,'sidebarHeader')]//img");
        By lbl_LoadCorporateAccountHeader => By.XPath("//div[contains(@class,'Payroll') and contains(@class,'CustomHeader')]");
        By lbl_NowMoneyAccDetails => By.XPath("//h5[text()='NOW Money account details']");
        By lbl_BankName => By.XPath("//div[contains(text(),'Bank name')]");
        By lbl_BankAddress => By.XPath("//div[contains(text(),'Bank address')]");
        By lbl_Swift => By.XPath("//div[contains(text(),'SWIFT')]");
        By lbl_AccName => By.XPath("//div[contains(text(),'Account name')]");
        By lbl_AccNum => By.XPath("//div[contains(text(),'Account number')]");
        By lbl_Iban => By.XPath("//div[contains(text(),'IBAN')]");
        By tbl_UploadArea => By.XPath("//div[contains(@class,'Payroll') and contains(@class,'UploadArea')]");
        By inp_FileUpload => By.XPath("//div[@role='button']//input");
        By txt_DepositValue => By.XPath("//input[@name='amount']");
        By btn_Upload => By.XPath("//button[@type='submit']");
        By btn_Save => By.XPath("//button[@type='submit']");
        By lnkViewPayments_SideMenu => By.XPath("//span[text()='View Payments']");
        By lnkWpsStatus_SideMenu => By.XPath("//span[text()='WPS Status']");
        By lnkStatusReport_SideMenu => By.XPath("//span[text()='Status Report']");
        By lblPayments => By.XPath("//h3[text()='Payments']");
        By inpSearch => By.XPath("//input[@name='search']");
        By btn_DownloadCsv => By.XPath("//button//span[text()='Download CSV']//parent::button");
        By datepicker_InputDate => By.XPath("//input[@name='daterange']");
        By lnkUsers_SideMenu => By.XPath("//span[text()='Users']");
        By lblUsers => By.XPath("//h3[text()='Users']");
        By btnAddUser => By.XPath("//button//span[text()='Add user']");
        By lnkPermissions_SideMenu => By.XPath("//span[text()='Permissions']");
        By lblPermissions => By.XPath("//h3[text()='Permissions']");
        By adminTab_Active => By.XPath("//a[text()='ADMIN']//parent::li[contains(@class,'Active')]");
        By financeTab_Inactive => By.XPath("//a[text()='FINANCE']");
        By financeTab_Active => By.XPath("//a[text()='FINANCE']//parent::li[contains(@class,'Active')]");
        By btn_createUser => By.XPath("//span[text()='Create user']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_businessAccess => By.XPath("//span[text()='Business access']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_ExportEmployeeData => By.XPath("//span[text()='Export employee data']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_AuthorizeWPSSalary => By.XPath("//span[text()='Authorize WPS salary']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_AuthorizeBulkSalary => By.XPath("//span[text()='Authorize bulk salary']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_UploadTransferProof => By.XPath("//span[text()='Upload transfer proof']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_UploadSalaryFile => By.XPath("//span[text()='Upload salary file']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_UploadWPSSalaryFile => By.XPath("//span[text()='Upload WPS salary file']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_ViewPaymentDetails => By.XPath("//span[text()='View payment details']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_AuthorizeIndividualSalary => By.XPath("//span[text()='Authorize Individual Salary']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_AddEmployees => By.XPath("//span[text()='Add employees']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_UpdateKybDetails => By.XPath("//span[text()='Update Kyb Details']//preceding-sibling::div[contains(@class,'checked')]");
        By btn_Update => By.XPath("//button//span[text()='Update']");
        By lblWPSPayments => By.XPath("//h3[text()='WPS Payments']");
        By lbl_totalBalanceWithheldNowMoney => By.XPath("//span[text()='Total balance held with NOW Money : AED']");
        By lbl_AddEmployee => By.XPath("//h5[@class='modal-title']");
        By btn_CloseAddEmployee => By.XPath("//button[@class='close']//*[name()='svg']");
        By txt_EmployeeName => By.XPath("//input[@name='name']");
        By txt_EmiratedIDNumber => By.XPath("//input[@name='id']");
        By txt_MobileNumber => By.XPath("//input[@name='mobile']");
        By txt_EmpDesignationNumber => By.XPath("//input[@name='position']");
        By txt_EmpDept => By.XPath("//input[@name='department']");
        By txt_ReferenceID => By.XPath("//input[@name='refId']");
        By btn_Submit => By.XPath("//span[normalize-space()='Submit']");
        By lnkCreatePayments_SideMenu => By.XPath("//span[text()='Create Payments']");
        By lnkNonWPSPaymentsBulk_CreatePaymentSideMenu => By.XPath("(//span[text()='Non WPS Payments (Bulk)'])[1]");
        By lnkNonWPSPaymentsIndividual_CreatePaymentSideMenu => By.XPath("(//span[text()='Non WPS Payments (Individual)'])[1]");
        By lnkWPSPayments_CreatePaymentSideMenu => By.XPath("(//span[text()='WPS payments'])[1]");
        By lnkAuthorizePayments_SideMenu => By.XPath("//span[normalize-space()='Authorize Payments']");
        By lnkNonWPSPaymentsBulk_AuthorizePaymentSideMenu => By.XPath("(//span[text()='Non WPS Payments (Bulk)'])[2]");
        By lnkNonWPSPaymentsIndividual_AuthorizePaymentSideMenu => By.XPath("(//span[text()='Non WPS Payments (Individual)'])[2]");
        By lnkWPSPayments_AuthorizePaymentSideMenu => By.XPath("(//span[text()='WPS payments'])[2]");
        By lbl_CreateBulkPayment => By.XPath("//div[text()='Create Bulk Payment']");
        By lbl_UploadSalaryFileEmiratedID => By.XPath("//h6[text()='Upload salary file with Emirates ID and salary amount']");
        By btn_DownloadTemplate => By.XPath("//span[normalize-space()='Download template']");
        By input_File => By.XPath("//div[contains(@role,'button')]//input");
        By lbl_CreateIndividualPayment => By.XPath("//div[text()='Create Individual Payment']");
        By lbl_SelectEmployee => By.XPath("//p[text()='Select employee to transfer create payment']");
        By lbl_Benificiary => By.XPath("//div[text()='Benificiary *']");
        By img_ChevronDownBenificiary => By.XPath("//div[contains(@class,'indicatorContainer')]");
        By lbl_Amount => By.XPath("//label[text()='Amount']");
        By inp_Amount => By.XPath("//input[@placeholder='Amount in AED']");
        By lbl_PaymentType => By.XPath("//label[text()='Payment type']");
        By inp_PaymentType => By.XPath("//input[@placeholder='Payment type']");
        By lbl_Description => By.XPath("//label[text()='Description']");
        By inp_Description => By.XPath("//input[@placeholder='Description']");
        By lbl_CreateWPSPayment => By.XPath("//div[text()='Create WPS payments']");
        By lbl_TotalAmountAED => By.XPath("//label[text()='Total amount (AED)']");
        By inp_TotalAmountAED => By.XPath("//input[@name='totalAmount']");
        By lbl_NoOfEmployees => By.XPath("//label[text()='Number of employees']");
        By inp_NoOfEmployees => By.XPath("//input[@name='numOfRecords']");
        By lbl_AuthorizeBulkPayments => By.XPath("//h3[text()='Authorize bulk payments']");
        By btn_ApproveFilesDisabled => By.XPath(" //span[text()='Approve  Files']//parent::button[contains(@class,'disabled')]");
        By lbl_AuthorizeIndividualPayments => By.XPath("//h3[text()='Authorize individual payments']");
        By lbl_AuthorizeWPSPayments => By.XPath("//h3[text()='Authorize WPS payments']");

        #endregion

        #region pageActions

        // ****************** Start of TC 01 ************** //

        /// <summary>
        /// Make sure the users lands at Dashboard
        /// </summary>
        public void ValidateUserLandsAtDashboard()
        {
            Thread.Sleep(5000);
            seleniumActions.GetURL().Contains(Constants.endpoint_Dashboard);
            string header = seleniumActions.GetText(txtHeader).ToLower();
            //Assert.IsTrue(header.Contains(ConfigHelper.GetEmail()));
        }

        /// <summary>
        /// Validates the ui elements present in dashboard
        /// </summary>
        public void ValidateUIElementsInDashboard()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(txtHeader));
            Assert.IsTrue(seleniumActions.IsElementPresent(lblCurrentBalance));
            Assert.IsTrue(seleniumActions.IsElementPresent(lblYourBalance));
            Assert.IsTrue(seleniumActions.IsElementPresent(lblBalanceHistory));
            Assert.IsTrue(seleniumActions.IsElementPresent(lblCurrentBalance));
            Assert.IsTrue(seleniumActions.IsElementPresent(lblYourTransaction));
            Assert.IsTrue(seleniumActions.IsElementPresent(lblTotalValue));
            Assert.IsTrue(seleniumActions.IsElementPresent(lblTotalTransactions));
        }

        // ****************** End of TC 01 ************** //

        // ****************** Start of TC 02 ************** //

        /// <summary>
        /// Validates the ui elements in side menu
        /// </summary>
        public void ValidateSideMenuInDashboard()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Dashboard);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkDashboard_SideMenu));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkEmployees_SideMenu));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkLoadCorporateAccount_SideMenu));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkTransactions_SideMenu));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkPortalAdmins_SideMenu));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkReports_SideMenu));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkCompanyInfo_SideMenu));
        }

        // ****************** End of TC 02 ************** //

        // ****************** Start of TC 03 ************** //

        /// <summary>
        /// Validates the ui elements in your balance tab 
        /// </summary>
        public void ValidateYourBalanceTabInDashboard()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Dashboard);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblYourBalance));
            Assert.IsTrue(seleniumActions.IsElementPresent(lblCurrentBalanceHeld));
        }

        // ****************** End of TC 03 ************** //

        // ****************** Start of TC 04 ************** //

        /// <summary>
        /// Validates the ui elements in balance history tab
        /// </summary>
        public void ValidateBalanceHistoryTabInDashboard()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Dashboard);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblBalanceHistory));
            seleniumActions.Click(lblBalanceHistory);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblTotalMoneyTransfered,10));
        }

        // ****************** End of TC 04 ************** //

        // ****************** Start of TC 05 ************** //

        /// <summary>
        /// Validates the ui elements in your transactions tab
        /// </summary>
        public void ValidateYourTransactionsTabInDashboard()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Dashboard);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblYourTransaction));
            Assert.IsTrue(seleniumActions.IsElementPresent(tblComponents_YourTransactions));
        }

        // ****************** End of TC 05 ************** //

        // ****************** Start of TC 06 ************** //

        /// <summary>
        /// Validates the ui elements in total value tab
        /// </summary>
        public void ValidateTotalValueTabInDashboard()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Dashboard);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblTotalValue));
            seleniumActions.Click(lblTotalValue);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblValueOfYourTransactions));
        }

        // ****************** End of TC 06 ************** //

        // ****************** Start of TC 07 ************** //

        /// <summary>
        /// Validates the ui elements in total transactions tab
        /// </summary>
        public void ValidateTotalTransactionsTabInDashboard()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Dashboard);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblTotalTransactions));
            seleniumActions.Click(lblTotalTransactions);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblNumberOfPayments));
        }

        // ****************** End of TC 07 ************** //

        // ****************** Start of TC 08 ************** //

        /// <summary>
        /// Clicks the view all transfers link
        /// </summary>
        public void ClickViewAllTransfersLink()
        {
            seleniumActions.WaitForElementToExists(lnkViewAllTransfers);
            seleniumActions.Click(lnkViewAllTransfers);
        }

        /// <summary>
        /// Validates the ui elements of transaction status page
        /// </summary>
        public void ValidateTransactionStatusPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_TransactionStatus);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblTransactionStatus));
            Assert.IsTrue(seleniumActions.IsElementPresent(btnDownloadAll));
            Assert.IsTrue(seleniumActions.IsElementPresent(btnAccountStatement));
            Assert.IsTrue(seleniumActions.IsElementPresent(inp_Search));
            Assert.IsTrue(seleniumActions.IsElementPresent(displayFilter));
        }

        // ****************** End of TC 08 ************** //

        // ****************** Start of TC 09 ************** //

        /// <summary>
        /// Clicks the view all transactions link
        /// </summary>
        public void ClickViewAllTransactionsLink()
        {
            seleniumActions.WaitForElementToExists(lnkViewAllTransactions);
            seleniumActions.Click(lnkViewAllTransactions);
        }

        // ****************** End of TC 09 ************** //

        // ****************** Start of TC 10 ************** //

        /// <summary>
        /// Validates the ui elements in employees tab in dashboard
        /// </summary>
        public void ValidateEmployeesTabInDashboard()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Dashboard);
            seleniumActions.ScrollToPosition(0, 1000);
            seleniumActions.Wait(2);
            seleniumActions.Click(lblYourEmployees);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblTotalEnrolledEmployees));
        }

        // ****************** End of TC 10 ************** //

        // ****************** Start of TC 11 ************** //

        /// <summary>
        /// Clicks the view all employees link
        /// </summary>
        public void ClickViewAllEmployeesLink()
        {
            seleniumActions.ScrollToPosition(0, 1000);
            seleniumActions.Wait(2);
            seleniumActions.WaitForElementToExists(lnkViewAllEmployees);
            seleniumActions.Click(lnkViewAllEmployees);
        }

        /// <summary>
        /// Validates the ui elements in employees page
        /// </summary>
        public void ValidateEmployeesPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Employees);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(btnUploadEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(btnAddEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(btnDownloadXLSX));
        }

        // ****************** End of TC 11 ************** //

        // ****************** Start of TC 12 ************** //

        /// <summary>
        /// Clicks employees in side menu
        /// </summary>
        public void ClickEmployeesInSideMenu()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkEmployees_SideMenu));
            seleniumActions.Click(lnkEmployees_SideMenu);
        }

        /// <summary>
        /// Verifies the ui elements inside show all filter in employees page
        /// </summary>
        public void VerifyShowAllFilter()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Employees);
            Assert.IsTrue(seleniumActions.IsElementPresent(drp_ShowAllFilter));
            seleniumActions.Click(drp_ShowAllFilter);
            seleniumActions.Wait(2);
            IList<IWebElement> showAllFilterValuesCount = seleniumActions.FindElements(By.XPath("//ul[@role='listbox']//li"));

            if (showAllFilterValuesCount.Count != Constants.showAllFilterValues.Count)
            {
                Console.WriteLine("Dropdown option count mismatch.");
            }

            for (int i = 0; i < showAllFilterValuesCount.Count; i++)
            {
                string actualText = showAllFilterValuesCount[i].Text.Trim();
                string expectedText = Constants.showAllFilterValues[i];

                if (actualText.Equals(expectedText))
                    Console.WriteLine($"Option {i + 1} matched: {actualText}");
                else
                    Console.WriteLine($"Mismatch at option {i + 1}: Expected = {expectedText}, Actual = {actualText}");
            }
        }

        /// <summary>
        /// Verifies the ui elements inside Display filter in employees page
        /// </summary>
        public void VerifyDisplayFilter()
        {
            Refresh();
            seleniumActions.GetURL().Contains(Constants.endpoint_Employees);
            Assert.IsTrue(seleniumActions.IsElementPresent(drp_DisplayFilter));
            seleniumActions.Click(drp_DisplayFilter);
            seleniumActions.Wait(2);
            IList<IWebElement> displayFilterValuesCount = seleniumActions.FindElements(By.XPath("//ul[@role='listbox']//li"));

            if (displayFilterValuesCount.Count != Constants.displayFilterValues.Count)
            {
                Console.WriteLine("Dropdown option count mismatch");
            }

            for (int i = 0; i < displayFilterValuesCount.Count; i++)
            {
                string actualText = displayFilterValuesCount[i].Text.Trim();
                string expectedText = Constants.displayFilterValues[i];

                if (actualText.Equals(expectedText))
                    Console.WriteLine($"Option {i + 1} matched: {actualText}");
                else
                    Console.WriteLine($"Mismatch at option {i + 1}: Expected = {expectedText}, Actual = {actualText}");
            }
        }

        /// <summary>
        /// Refreshes the page
        /// </summary>
        public void Refresh()
        { 
           seleniumActions.Refresh();
        }

        // ****************** End of TC 12 ************** //

        // ****************** Start of TC 14 ************** //

        /// <summary>
        /// Clicks Load corporate account in side menu
        /// </summary>
        public void ClickLoadCorporateAccountInSideMenu()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkLoadCorporateAccount_SideMenu));
            seleniumActions.Click(lnkLoadCorporateAccount_SideMenu);
        }

        /// <summary>
        /// Validates the ui elements in Load corporate account page
        /// </summary>
        public void ValidateLoadCorporateAccountPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_LoadCorporateAccount);
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_LoadCorporateAccountHeader));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_NowMoneyAccDetails));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_BankName));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_BankAddress));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Swift));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_AccName));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_AccNum));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Iban));
            seleniumActions.ScrollToPosition(0, 800);
            Assert.IsTrue(seleniumActions.IsElementPresent(tbl_UploadArea));
            Assert.IsTrue(seleniumActions.IsElementPresent(inp_FileUpload));
            Assert.IsTrue(seleniumActions.IsElementPresent(txt_DepositValue));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_Upload));
        }

        // ****************** End of TC 14 ************** //

        // ****************** Start of TC 15 ************** //
        // ****************** End of TC 15 ************** //

        // ****************** Start of TC 16 ************** //

        /// <summary>
        /// Clicks view payments inside reports in side menu
        /// </summary>
        public void ClickViewPayments()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkReports_SideMenu));
            seleniumActions.Click(lnkReports_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkViewPayments_SideMenu));
            seleniumActions.Click(lnkViewPayments_SideMenu);
        }

        /// <summary>
        /// Validates the ui elements in Payments page inside reports in side menu
        /// </summary>
        public void ValidatePaymentsPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Payments);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblPayments));
            Assert.IsTrue(seleniumActions.IsElementPresent(datepicker_InputDate));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_DownloadCsv));
            Assert.IsTrue(seleniumActions.IsElementPresent(inpSearch));
            Assert.IsTrue(seleniumActions.IsElementPresent(displayFilter));
        }

        // ****************** End of TC 16 ************** //

        // ****************** Start of TC 17 ************** //

        /// <summary>
        /// Clicks view payments inside reports in side menu
        /// </summary>
        public void ClickWPSStatus()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkReports_SideMenu));
            seleniumActions.Click(lnkReports_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkWpsStatus_SideMenu));
            seleniumActions.Click(lnkWpsStatus_SideMenu);
        }

        /// <summary>
        /// Validates the ui elements in WPSPayments page inside reports in side menu
        /// </summary>
        public void ValidateWPSPaymentsPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_WpsPayments);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblWPSPayments));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_totalBalanceWithheldNowMoney));
            Assert.IsTrue(seleniumActions.IsElementPresent(displayFilter));
        }

        // ****************** End of TC 17 ************** //

        // ****************** Start of TC 18 ************** //

        /// <summary>
        /// Validates the ui elements in WPS status page inside reports in side menu
        /// </summary>
        public void ClickStatusReport()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkReports_SideMenu));
            seleniumActions.Click(lnkReports_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkStatusReport_SideMenu));
            seleniumActions.Click(lnkStatusReport_SideMenu);
        }

        // ****************** End of TC 18 ************** //

        // ****************** Start of TC 19 ************** //

        /// <summary>
        /// Clicks users inside portal admins in side menu
        /// </summary>
        public void ClickUsers()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkPortalAdmins_SideMenu));
            seleniumActions.Click(lnkPortalAdmins_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkUsers_SideMenu));
            seleniumActions.Click(lnkUsers_SideMenu);
        }

        /// <summary>
        /// Validates the ui elements in users page inside portal admins in side menu
        /// </summary>
        public void ValidateUsersPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Users);
            seleniumActions.GetURL().Contains("users");
            Assert.IsTrue(seleniumActions.IsElementPresent(lblUsers));
            Assert.IsTrue(seleniumActions.IsElementPresent(btnAddUser));
        }

        // ****************** End of TC 19 ************** //

        // ****************** Start of TC 20 ************** //

        /// <summary>
        /// Clicks permissions inside portal admins in side menu
        /// </summary>
        public void ClickPermissions()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkPortalAdmins_SideMenu));
            seleniumActions.Click(lnkPortalAdmins_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkPermissions_SideMenu));
            seleniumActions.Click(lnkPermissions_SideMenu);
        }

        /// <summary>
        /// Validates the ui elements in permissions page inside portal admins in side menu
        /// </summary>
        public void ValidatePermissionsPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Permissions);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblPermissions));
            Assert.IsTrue(seleniumActions.IsElementPresent(adminTab_Active));

            // -- Admin -- //
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_createUser));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_businessAccess));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_ExportEmployeeData));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_AuthorizeWPSSalary));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_AuthorizeBulkSalary));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_UploadTransferProof));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_UploadSalaryFile));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_UploadWPSSalaryFile));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_ViewPaymentDetails));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_AuthorizeIndividualSalary));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_AddEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_UpdateKybDetails));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_Update));

            Assert.IsTrue(seleniumActions.IsElementPresent(financeTab_Inactive));
            seleniumActions.Click(financeTab_Inactive);
            Assert.IsTrue(seleniumActions.IsElementPresent(financeTab_Active));

            // -- finance -- //
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_createUser));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_businessAccess));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_ExportEmployeeData));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_AuthorizeWPSSalary));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_AuthorizeBulkSalary));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_UploadTransferProof));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_UploadSalaryFile));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_UploadWPSSalaryFile));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_ViewPaymentDetails));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_AuthorizeIndividualSalary));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_AddEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_UpdateKybDetails));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_Update));
        }

        // ****************** End of TC 20 ************** //

        // ****************** Start of TC 21 ************** //

        /// <summary>
        /// clicks add employee button in employees page
        /// </summary>
        public void ClickAddEmployeeButton()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(btnAddEmployees));
            seleniumActions.Click(btnAddEmployees);
        }

        /// <summary>
        /// verifies add employee screen
        /// </summary>
        public void VerifyAddEmployeeScreen()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_AddEmployee));
            Assert.IsTrue(seleniumActions.IsElementPresent(txt_EmployeeName));
            Assert.IsTrue(seleniumActions.IsElementPresent(txt_EmiratedIDNumber));
            Assert.IsTrue(seleniumActions.IsElementPresent(txt_MobileNumber));
            Assert.IsTrue(seleniumActions.IsElementPresent(txt_EmpDesignationNumber));
            Assert.IsTrue(seleniumActions.IsElementPresent(txt_EmpDept));
            Assert.IsTrue(seleniumActions.IsElementPresent(txt_ReferenceID));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_CloseAddEmployee));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_Submit));
            seleniumActions.Click(btn_CloseAddEmployee);
        }

        // ****************** End of TC 21 ************** //

        // ****************** Start of TC 22 ************** //

        /// <summary>
        /// clicks non wps payments bulk in Transactions - create payments
        /// </summary>
        public void ClickNonWpsPaymentsBulk_CreatePayments()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkTransactions_SideMenu));
            seleniumActions.Click(lnkTransactions_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkCreatePayments_SideMenu));
            seleniumActions.Click(lnkCreatePayments_SideMenu);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkNonWPSPaymentsBulk_CreatePaymentSideMenu));
            seleniumActions.Click(lnkNonWPSPaymentsBulk_CreatePaymentSideMenu);
        }

        /// <summary>
        /// validates ui elements of non wps payments bulk in Transactions - create payments
        /// </summary>
        public void ValidateNonWpsPaymentsBulk_CreatePayments()
        {
            Assert.IsTrue(seleniumActions.GetURL().Contains(Constants.endpoint_CreatePayments_NonWPSPaymentsBulk));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_CreateBulkPayment));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_UploadSalaryFileEmiratedID));
            Assert.IsTrue(seleniumActions.IsElementPresent(input_File));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_DownloadTemplate));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_Save));
        }

        // ****************** End of TC 22 ************** //

        // ****************** Start of TC 23 ************** //

        /// <summary>
        /// clicks non wps payments individual in Transactions - create payments
        /// </summary>
        public void ClickNonWpsPaymentsIndividual_CreatePayments()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkTransactions_SideMenu));
            seleniumActions.Click(lnkTransactions_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkCreatePayments_SideMenu));
            seleniumActions.Click(lnkCreatePayments_SideMenu);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkNonWPSPaymentsIndividual_CreatePaymentSideMenu));
            seleniumActions.Click(lnkNonWPSPaymentsIndividual_CreatePaymentSideMenu);
        }

        /// <summary>
        /// validates ui elements of non wps payments individual in Transactions - create payments
        /// </summary>
        public void ValidateNonWpsPaymentsIndividual_CreatePayments()
        {
            Assert.IsTrue(seleniumActions.GetURL().Contains(Constants.endpoint_CreatePayments_NonWPSPaymentsIndividual));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_CreateIndividualPayment));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_SelectEmployee));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Benificiary));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Amount));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_PaymentType));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Description));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_Save));
        }

        // ****************** End of TC 23 ************** //

        // ****************** Start of TC 24 ************** //

        /// <summary>
        /// clicks wps payments in Transactions - create payments
        /// </summary>
        public void ClickWpsPayments_CreatePayments()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkTransactions_SideMenu));
            seleniumActions.Click(lnkTransactions_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkCreatePayments_SideMenu));
            seleniumActions.Click(lnkCreatePayments_SideMenu);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkWPSPayments_CreatePaymentSideMenu));
            seleniumActions.Click(lnkWPSPayments_CreatePaymentSideMenu);
        }

        /// <summary>
        /// validates ui elements of wps payments in Transactions - create payments
        /// </summary>
        public void ValidateWpsPayments_CreatePayments()
        {
            Assert.IsTrue(seleniumActions.GetURL().Contains(Constants.endpoint_CreatePayments_WPSPayment));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_CreateWPSPayment));
            Assert.IsTrue(seleniumActions.IsElementPresent(input_File));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_TotalAmountAED));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_NoOfEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_DownloadTemplate));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_Save));
        }

        // ****************** End of TC 24 ************** //

        // ****************** Start of TC 25 ************** //

        /// <summary>
        /// clicks non wps payments bulk in Transactions - authorize payments
        /// </summary>
        public void ClickNonWpsPaymentsBulk_AuthorizePayments()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkTransactions_SideMenu));
            seleniumActions.Click(lnkTransactions_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkAuthorizePayments_SideMenu));
            seleniumActions.Click(lnkAuthorizePayments_SideMenu);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkNonWPSPaymentsBulk_AuthorizePaymentSideMenu));
            seleniumActions.Click(lnkNonWPSPaymentsBulk_AuthorizePaymentSideMenu);
        }

        /// <summary>
        /// validates ui elements of non wps payments bulk in Transactions - authorize payments
        /// </summary>
        public void ValidateNonWpsPaymentsBulk_AuthorizePayments()
        {
            Assert.IsTrue(seleniumActions.GetURL().Contains(Constants.endpoint_AuthorizePayments_NonWPSPaymentsBulk));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_AuthorizeBulkPayments));
            Assert.IsTrue(seleniumActions.IsElementPresent(inpSearch));
            Assert.IsTrue(seleniumActions.IsElementPresent(drp_DisplayFilter));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_ApproveFilesDisabled));

            seleniumActions.Click(drp_DisplayFilter);
            seleniumActions.Wait(2);
            IList<IWebElement> displayFilterValuesCount = seleniumActions.FindElements(By.XPath("//ul[@role='listbox']//li"));

            if (displayFilterValuesCount.Count != Constants.authorizeBulkDisplayFilterValues.Count)
            {
                Console.WriteLine("Dropdown option count mismatch");
            }

            for (int i = 0; i < displayFilterValuesCount.Count; i++)
            {
                string actualText = displayFilterValuesCount[i].Text.Trim();
                string expectedText = Constants.authorizeBulkDisplayFilterValues[i];

                if (actualText.Equals(expectedText))
                    Console.WriteLine($"Option {i + 1} matched: {actualText}");
                else
                    Console.WriteLine($"Mismatch at option {i + 1}: Expected = {expectedText}, Actual = {actualText}");
            }
        }

        // ****************** End of TC 25 ************** //

        // ****************** Start of TC 26 ************** //

        /// <summary>
        /// clicks non wps payments individual in Transactions - authorize payments
        /// </summary>
        public void ClickNonWpsPaymentsIndividual_AuthorizePayments()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkTransactions_SideMenu));
            seleniumActions.Click(lnkTransactions_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkAuthorizePayments_SideMenu));
            seleniumActions.Click(lnkAuthorizePayments_SideMenu);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkNonWPSPaymentsIndividual_AuthorizePaymentSideMenu));
            seleniumActions.Click(lnkNonWPSPaymentsIndividual_AuthorizePaymentSideMenu);
        }

        /// <summary>
        /// validates ui elements of non wps payments individual in Transactions - authorize payments
        /// </summary>
        public void ValidateNonWpsPaymentsIndividual_AuthorizePayments()
        {
            Assert.IsTrue(seleniumActions.GetURL().Contains(Constants.endpoint_AuthorizePayments_NonWPSPaymentsIndividual));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_AuthorizeIndividualPayments));
            Assert.IsTrue(seleniumActions.IsElementPresent(inpSearch));
            Assert.IsTrue(seleniumActions.IsElementPresent(drp_DisplayFilter));

            seleniumActions.Click(drp_DisplayFilter);
            seleniumActions.Wait(2);
            IList<IWebElement> displayFilterValuesCount = seleniumActions.FindElements(By.XPath("//ul[@role='listbox']//li"));

            if (displayFilterValuesCount.Count != Constants.authorizeIndividualDisplayFilterValues.Count)
            {
                Console.WriteLine("Dropdown option count mismatch");
            }

            for (int i = 0; i < displayFilterValuesCount.Count; i++)
            {
                string actualText = displayFilterValuesCount[i].Text.Trim();
                string expectedText = Constants.authorizeIndividualDisplayFilterValues[i];

                if (actualText.Equals(expectedText))
                    Console.WriteLine($"Option {i + 1} matched: {actualText}");
                else
                    Console.WriteLine($"Mismatch at option {i + 1}: Expected = {expectedText}, Actual = {actualText}");
            }
        }

        // ****************** End of TC 26 ************** //

        // ****************** Start of TC 27 ************** //

        /// <summary>
        /// clicks wps payments in Transactions - authorize payments
        /// </summary>
        public void ClickWpsPayments_AuthorizePayments()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkTransactions_SideMenu));
            seleniumActions.Click(lnkTransactions_SideMenu);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkAuthorizePayments_SideMenu));
            seleniumActions.Click(lnkAuthorizePayments_SideMenu);
            seleniumActions.Wait(2);
            Assert.IsTrue(seleniumActions.IsElementPresent(lnkWPSPayments_AuthorizePaymentSideMenu));
            seleniumActions.Click(lnkWPSPayments_AuthorizePaymentSideMenu);
        }

        /// <summary>
        /// validates ui elements of wps payments in Transactions - authorize payments
        /// </summary>
        public void ValidateWpsPayments_AuthorizePayments()
        {
            Assert.IsTrue(seleniumActions.GetURL().Contains(Constants.endpoint_AuthorizePayments_WPSPayment));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_AuthorizeWPSPayments));
            Assert.IsTrue(seleniumActions.IsElementPresent(inpSearch));
            Assert.IsTrue(seleniumActions.IsElementPresent(drp_DisplayFilter));

            seleniumActions.Click(drp_DisplayFilter);
            seleniumActions.Wait(2);
            IList<IWebElement> displayFilterValuesCount = seleniumActions.FindElements(By.XPath("//ul[@role='listbox']//li"));

            if (displayFilterValuesCount.Count != Constants.authorizeBulkDisplayFilterValues.Count)
            {
                Console.WriteLine("Dropdown option count mismatch");
            }

            for (int i = 0; i < displayFilterValuesCount.Count; i++)
            {
                string actualText = displayFilterValuesCount[i].Text.Trim();
                string expectedText = Constants.authorizeBulkDisplayFilterValues[i];

                if (actualText.Equals(expectedText))
                    Console.WriteLine($"Option {i + 1} matched: {actualText}");
                else
                    Console.WriteLine($"Mismatch at option {i + 1}: Expected = {expectedText}, Actual = {actualText}");
            }
        }

        // ****************** End of TC 27 ************** //


        #endregion
    }
}
