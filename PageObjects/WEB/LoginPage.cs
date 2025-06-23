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
using TechTalk.SpecFlow;

namespace KARE.E2E.AUTOMATION.PageObjects.WEB
{
    public class LoginPage
    {
        #region Declaration
        private IWebDriver _driver;
        private SeleniumActions seleniumActions;
        public Utilities utility;
        #endregion

        #region Constructor
        public LoginPage(IWebDriver driver)
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
        By imgProfile => By.XPath("//div[contains(@class,'UserName')]");
        By btnLogout => By.XPath("(//div[contains(@class,'dropdown-menu-right')]//button)[last()]");
        By img_NowMoney => By.XPath("//div[contains(@class,'NowMoneyLogo')]");
        By lbl_WelcomeToNowMoney => By.XPath("//div[text()='Welcome to NOW Money!']");
        By lbl_NiceSeeingYouHere => By.XPath("//div[text()='Nice seeing you here']");
        By lbl_Login => By.XPath("//section[text()='Login']");
        By lbl_Username => By.XPath("//label[text()='Username']");
        By lbl_Password => By.XPath("//label[text()='Password']");
        By lnk_ForgotYourPassword => By.XPath("//a[contains(@href,'forgot')]");
        By lbl_DontHaveAnAccYet => By.XPath("//span[contains(text(),'an account')]");
        By lnk_BookADemo => By.XPath("//a[contains(@href,'demo')]");
        By btn_SubmitDisabled => By.XPath("//button[@type='submit' and @disabled]");
        By lbl_RestorePassword => By.XPath("//section[text()='Restore password']");
        By lnk_Login => By.XPath("//span[text()='Login']");
        By lbl_BookADemo => By.XPath("//span[text()='Book a demo']");
        By lbl_WhatAreYouInterestedIn => By.XPath("//span[text()='What are you interested in?']");
        By lbl_DigitalPayroll => By.XPath("//span[text()='Digital payroll']");
        By lbl_BanAccForEmployees => By.XPath("//span[text()='Bank accounts for my employees']");
        By lbl_BenefitsForEmployees => By.XPath("//span[text()='Benefits for my employees']");
        By lbl_FirstName => By.XPath("//span[text()='First name']");
        By inp_FirstName => By.XPath("//input[@name='firstname']");
        By lbl_LastName => By.XPath("//span[text()='Last name']");
        By inp_LastName => By.XPath("//input[@name='lastname']");
        By lbl_BusinessEmail => By.XPath("//span[text()='Business email']");
        By inp_BusinessEmail => By.XPath("//input[@name='email']");
        By lbl_PhoneNumber => By.XPath("//span[text()='Phone number']");
        By drp_CountryCode => By.XPath("//select[contains(@id,'phone')]");
        By inp_PhoneNumber => By.XPath("//input[contains(@id,'phone')]");
        By lbl_CompanyName => By.XPath("//span[text()='Company name']");
        By inp_CompanyName => By.XPath("//input[@name='company']");
        By lbl_NumberOfEmployees => By.XPath("//span[text()='Number of employees']");
        By drp_NumberOfEmployees => By.XPath("//select[@name='numemployees']");
        By btn_Submit => By.XPath("//input[@type='submit']");
        By lblAlert_InvalidUsernameOrPassword => By.XPath("//div[@role='alert' and text()='Invalid username or password']");

        #endregion

        #region pageActions

        /// <summary>
        /// Access the NowMoney Portal by utilizing the provided "URL" for NowMoney.
        /// </summary>
        public void NavigateToURL(string url)
        {
            seleniumActions.NavigateToUrl(url);
        }

        /// <summary>
        /// Upon entering the email and password and clicking the login button, the application automatically directs the user to the default shift page
        /// </summary>
        /// <param name="Email">email store as String</param>
        /// <param name="Password">Password store as String</param>
        public void LoginToApplication(String Email, String Password)
        {
            seleniumActions.SendKeys(txtEmail, Email);
            seleniumActions.SendKeys(txtPassword, Password);
            seleniumActions.Click(btnLogin);
        }

        /// <summary>
        /// clicks the profile icon and clicks the logout button to logout of the application
        /// </summary>
        public void LogoutApplication()
        {
            seleniumActions.Wait(2);
            seleniumActions.Click(imgProfile);
            Assert.IsTrue(seleniumActions.IsElementPresent(btnLogout));
            seleniumActions.Click(btnLogout);
        }

        // ****************** Start of TC 01 ************** //

        /// <summary>
        /// verifies the user lands at login page
        /// </summary>
        public void VerifyUserLandsAtLoginPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_Login);
        }

        /// <summary>
        /// validates the ui elements in login page
        /// </summary>
        public void ValidateLoginPage()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(img_NowMoney));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_WelcomeToNowMoney));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_NiceSeeingYouHere));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Login));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Username));
            Assert.IsTrue(seleniumActions.IsElementPresent(txtEmail));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Password));
            Assert.IsTrue(seleniumActions.IsElementPresent(txtPassword));
            Assert.IsTrue(seleniumActions.IsElementPresent(btnLogin));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnk_ForgotYourPassword));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_DontHaveAnAccYet));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnk_BookADemo));
        }

        // ****************** End of TC 01 ************** //

        // ****************** Start of TC 02 ************** //

        /// <summary>
        /// Clicks the forgot password link in login page
        /// </summary>
        public void ClickForgotPasswordLink()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnk_ForgotYourPassword));
            seleniumActions.Click(lnk_ForgotYourPassword);
        }

        /// <summary>
        /// Validates the ui elements in forgot password page
        /// </summary>
        public void ValidateForgotPasswordPage()
        {
            Assert.IsTrue(seleniumActions.GetURL().Contains(Constants.endpoint_ForgotPassword)); 
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_Username));
            Assert.IsTrue(seleniumActions.IsElementPresent(txtEmail));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_RestorePassword));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_SubmitDisabled));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnk_Login));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_DontHaveAnAccYet));
            Assert.IsTrue(seleniumActions.IsElementPresent(lnk_BookADemo));
        }

        // ****************** End of TC 02 ************** //

        // ****************** Start of TC 03 ************** //

        /// <summary>
        /// Clicks the book a demo link in login page
        /// </summary>
        public void ClickBookADemoLink()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(lnk_BookADemo));
            seleniumActions.Click(lnk_BookADemo);
        }

        /// <summary>
        /// Validates the ui elements in book a demo page
        /// </summary>
        public void ValidateBookADemoPage()
        {
            seleniumActions.GetURL().Contains(Constants.endpoint_BookADemo);
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_BookADemo));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_WhatAreYouInterestedIn));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_DigitalPayroll));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_BanAccForEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_BenefitsForEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_FirstName));
            Assert.IsTrue(seleniumActions.IsElementPresent(inp_FirstName));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_LastName));
            Assert.IsTrue(seleniumActions.IsElementPresent(inp_LastName));

            seleniumActions.ScrollToPosition(0, 800);
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_BusinessEmail));
            Assert.IsTrue(seleniumActions.IsElementPresent(inp_BusinessEmail));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_PhoneNumber));
            Assert.IsTrue(seleniumActions.IsElementPresent(drp_CountryCode));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_CompanyName));
            Assert.IsTrue(seleniumActions.IsElementPresent(inp_CompanyName));
            Assert.IsTrue(seleniumActions.IsElementPresent(lbl_NumberOfEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(drp_NumberOfEmployees));
            Assert.IsTrue(seleniumActions.IsElementPresent(btn_Submit));
        }

        // ****************** End of TC 03 ************** //

        // ****************** Start of TC 04 ************** //

        /// <summary>
        /// validates the ui elements in login page with invalid credentials
        /// </summary>
        public void ValidateLoginPageWithInvalidCredential()
        {
            Assert.IsTrue(seleniumActions.IsElementPresent(txtEmail));
            Assert.IsTrue(seleniumActions.IsElementPresent(txtPassword));
            seleniumActions.SendKeys(txtEmail, Constants.endpoint_Dashboard);
            seleniumActions.SendKeys(txtPassword, Constants.endpoint_Employees);
            seleniumActions.Click(btnLogin);
            Assert.IsTrue(seleniumActions.IsElementPresent(lblAlert_InvalidUsernameOrPassword));
        }

        // ****************** End of TC 04 ************** //

        #endregion
    }
}
