//using System;
//using BoDi;
//using System.IO;
//using System.Reflection;
//using TechTalk.SpecFlow;
//using System.Text.Json;
//using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Diagnostics;
//using System.Xml.Linq;
//using AventStack.ExtentReports;
//using AventStack.ExtentReports.Reporter;
//using AventStack.ExtentReports.Gherkin.Model;
//using AventStack.ExtentReports.Model;
//using OpenQA.Selenium;
//using System.Drawing.Imaging;

//namespace KARE.E2E.AUTOMATION.Helpers
//{
//    [Binding]
//    public class SpecflowHooks
//    {
//        #region Declaration
//        private readonly IObjectContainer objectContainer;
//        private static DriverContext driverContext;
//        public static string ReportPath;
//        private readonly ScenarioContext _scenarioContext;
//        private readonly FeatureContext _featureContext;
//        private static ExtentTest featureName;
//        private static ExtentTest scenario;
//        private static ExtentReports extent;
//        #endregion

//        #region Constructor
//        public SpecflowHooks(IObjectContainer container, ScenarioContext scenariocontext, FeatureContext featurecontext)
//        {
//            this.objectContainer = container;
//            _scenarioContext = scenariocontext;
//            _featureContext = featurecontext;
//        }
//        #endregion

//        [BeforeTestRun]
//        [Obsolete]
//        public static void BeforeTestRun()
//        {
//            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
//            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
//            var projectPath = new Uri(actualPath).LocalPath;
//            Directory.CreateDirectory(projectPath.ToString() + "Reports");
//            var Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
//            var reportPath = projectPath + "Reports\\KAREReports_" + Timestamp + ".html";
//            ExtentV3HtmlReporter htmlReporter = new ExtentV3HtmlReporter(reportPath);
//            extent = new ExtentReports();
//            extent.AttachReporter(htmlReporter);
//        }

//        [BeforeFeature]
//        public static void BeforeFeature()
//        {

//        }

//        [BeforeScenario]
//        public void BeforeScenario()
//        {
//            featureName = extent.CreateTest<Feature>(_scenarioContext.ScenarioInfo.Tags[0]);
//            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
//            driverContext = new DriverContext();
//            objectContainer.RegisterInstanceAs<DriverContext>(driverContext);
//        }

//        [AfterStep]
//        public void AfterEachStep()
//        {
//            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
//            if (_scenarioContext.TestError == null)
//            {
//                if (stepType == "Given")
//                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
//                else if (stepType == "When")
//                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
//                else if (stepType == "Then")
//                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
//                else if (stepType == "And")
//                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
//            }
//            else if (_scenarioContext.TestError != null)
//            {
//                string screenShotPath = TakeScreenshot(driverContext.WebDriver, "ScreenShot");
//                if (stepType == "Given")
//                {
//                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
//                    scenario.AddScreenCaptureFromPath(screenShotPath, "Error Screenshot");
//                }
//                else if (stepType == "When")
//                {
//                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
//                    scenario.AddScreenCaptureFromPath(screenShotPath, "Error Screenshot");
//                }
//                else if (stepType == "Then")
//                {
//                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
//                    scenario.AddScreenCaptureFromPath(screenShotPath, "Error Screenshot");
//                }
//                else if (stepType == "And")
//                {
//                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
//                    scenario.AddScreenCaptureFromPath(screenShotPath, "Error Screenshot");
//                }
//            }
//        }

//        [AfterScenario]
//        public void AfterScenario()
//        {
//            driverContext.WebDriver.Quit();
//            driverContext.WebDriver.Dispose();
//        }

//        [AfterTestRun]
//        public static void AfterTestRun()
//        {
//            extent.Flush();
//        }

//        public String TakeScreenshot(IWebDriver driver, String sspath)
//        {
//            ITakesScreenshot ts = (ITakesScreenshot)driver;
//            Screenshot screenshot = ts.GetScreenshot();
//            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
//            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "\\Reports\\Screenshots\\" + sspath + ".png";
//            string localpath = new Uri(finalpth).LocalPath;
//            screenshot.SaveAsFile(localpath);
//            return localpath;
//        }
//    }
//}
