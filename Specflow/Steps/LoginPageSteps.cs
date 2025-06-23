using AventStack.ExtentReports;
using KARE.E2E.AUTOMATION.Helpers;
using KARE.E2E.AUTOMATION.PageObjects.WEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace KARE.E2E.AUTOMATION.Specflow.Steps
{
    [Binding]
    public  class LoginPageSteps : ExtentReports
    {
        #region Declaration
        private DriverContext driverContext;
        private LoginPage loginPage;
        private DashboardPage dashboardPage;
        public ScenarioContext scenarioContext;
        public FeatureContext featureContext;
        #endregion

        #region Constructor
        public LoginPageSteps(DriverContext driverContext, ScenarioContext scenariocontext, FeatureContext featurecontext)
        {
            this.driverContext = driverContext;
            scenarioContext = scenariocontext;
            featureContext = featurecontext;
            loginPage = new LoginPage(driverContext.WebDriver);
            dashboardPage = new DashboardPage(driverContext.WebDriver);
        }
        #endregion

        #region LoginSteps

        [Given(@"I navigate to the application URL")]
        public void GivenINavigateToTheApplicationURL()
        {
            loginPage.NavigateToURL(ConfigHelper.GetURL());
        }

        [When(@"I verify user lands at login page")]
        public void WhenIVerifyUserLandsAtLoginPage()
        {
            loginPage.VerifyUserLandsAtLoginPage();
        }

        [Then(@"I validate the '([^']*)' page")]
        public void ThenIValidateThePage(string page)
        {
            switch (page)
            {
                case "login":
                    loginPage.ValidateLoginPage();
                    break;
                case "forgot password":
                    loginPage.ValidateForgotPasswordPage();
                    break;
                case "book a demo":
                    loginPage.ValidateBookADemoPage();
                    break;
            }
        }

        [Then(@"I navigate to '([^']*)' page")]
        public void ThenINavigateToPage(string page)
        {
            switch (page)
            {
                case "forgot password":
                    loginPage.ClickForgotPasswordLink();
                    break;
                case "book a demo":
                    loginPage.ClickBookADemoLink();
                    break;
            }
        }

        [Then(@"I validate the login with invalid credential")]
        public void ThenIValidateTheLoginWithInvalidCredential()
        {
            loginPage.ValidateLoginPageWithInvalidCredential();
        }

        #endregion

    }
}
