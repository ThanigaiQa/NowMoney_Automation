using BoDi;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using OpenQA.Selenium;
using AventStack.ExtentReports.MarkupUtils;

namespace KARE.E2E.AUTOMATION.Helpers
{
    [Binding]
    public class SpecflowHooks_Extent : ExtentReport
    {
        #region Declaration
        private readonly IObjectContainer objectContainer;
        private static DriverContext driverContext;
        public static string ReportPath;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        #endregion

        #region Constructor
        public SpecflowHooks_Extent(IObjectContainer container, ScenarioContext scenariocontext, FeatureContext featurecontext)
        {
            this.objectContainer = container;
            _scenarioContext = scenariocontext;
            _featureContext = featurecontext;
        }
        #endregion

        public String addScreenshot(IWebDriver driver, ScenarioContext scenarioContext)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "\\Reports\\Screenshots\\" + scenarioContext.ScenarioInfo.Title + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath);
            return localpath;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReportInit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportTearDown();
        }


        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature()
        {

        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            string linkText = _scenarioContext.ScenarioInfo.Tags[0];
            string linkUrl = "https://dev.azure.com/DoyouKare/Kare/_workitems/edit/" + linkText;
            driverContext = new DriverContext();
            objectContainer.RegisterInstanceAs<DriverContext>(driverContext);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title 
                + " | " + $"Test Case ID : <a href='{linkUrl}' target='_blank'>{linkText}</a>");
        }
        [AfterScenario]
        public static void AfterScenario()
        {
            driverContext.WebDriver.Quit();
            driverContext.WebDriver.Dispose();
        }
        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running after step....");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;
            CodeLanguage lang = new CodeLanguage();
            CodeLanguage failLang = new CodeLanguage();
            var driver = driverContext.WebDriver;

            switch (ConfigHelper.GetLogLanguage())
            {
                case "Json":
                    lang = CodeLanguage.Json;
                    break;
                case "Xml":
                    lang = CodeLanguage.Xml;
                    break;
                default:
                    lang = CodeLanguage.Xml;
                    break;
            }

            switch (ConfigHelper.GetFailLogLanguage())
            {
                case "Json":
                    failLang = CodeLanguage.Json;
                    break;
                case "Xml":
                    failLang = CodeLanguage.Xml;
                    break;
                default:
                    failLang = CodeLanguage.Xml;
                    break;
            }

            //When scenario passed
            if (scenarioContext.TestError == null)
            {
                if (ConfigHelper.GetLogSwitch().Equals("enabled"))
                {
                    if (stepType == "Given")
                    {
                        if (scenarioContext.ContainsKey("APIRequest") && scenarioContext.ContainsKey("APIResponse"))
                        {
                            _scenario.CreateNode<Given>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), lang)).
                                Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), lang));
                            scenarioContext.Remove("APIRequest");
                        }
                        else if (scenarioContext.ContainsKey("APIRequest") || scenarioContext.ContainsKey("APIResponse"))
                        {
                            if (scenarioContext.ContainsKey("APIRequest"))
                                _scenario.CreateNode<Given>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), lang));
                            if (scenarioContext.ContainsKey("APIResponse"))
                                _scenario.CreateNode<Given>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), lang));
                            scenarioContext.Remove("APIResponse");
                        }
                        else
                            _scenario.CreateNode<Given>(stepName);
                    }
                    else if (stepType == "When")
                    {
                        if (scenarioContext.ContainsKey("APIRequest") && scenarioContext.ContainsKey("APIResponse"))
                        {
                            _scenario.CreateNode<When>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), lang)).
                                Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), lang));
                            scenarioContext.Remove("APIRequest");
                        }
                        else if (scenarioContext.ContainsKey("APIRequest") || scenarioContext.ContainsKey("APIResponse"))
                        {
                            if (scenarioContext.ContainsKey("APIRequest"))
                                _scenario.CreateNode<When>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), lang));
                            if (scenarioContext.ContainsKey("APIResponse"))
                                _scenario.CreateNode<When>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), lang));
                            scenarioContext.Remove("APIResponse");
                        }
                        else
                            _scenario.CreateNode<When>(stepName);
                    }
                    else if (stepType == "Then")
                    {
                        if (scenarioContext.ContainsKey("APIRequest") && scenarioContext.ContainsKey("APIResponse"))
                        {
                            _scenario.CreateNode<Then>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), lang)).
                                Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), lang));
                            scenarioContext.Remove("APIRequest");
                        }
                        else if (scenarioContext.ContainsKey("APIRequest") || scenarioContext.ContainsKey("APIResponse"))
                        {
                            if (scenarioContext.ContainsKey("APIRequest"))
                                _scenario.CreateNode<Then>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), lang));
                            if (scenarioContext.ContainsKey("APIResponse"))
                                _scenario.CreateNode<Then>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), lang));
                            scenarioContext.Remove("APIResponse");
                        }
                        else
                            _scenario.CreateNode<Then>(stepName);
                    }
                    else if (stepType == "And")
                    {
                        if (scenarioContext.ContainsKey("APIRequest") && scenarioContext.ContainsKey("APIResponse"))
                        {
                            _scenario.CreateNode<And>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), lang)).
                                Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), lang));
                            scenarioContext.Remove("APIRequest");
                        }
                        else if (scenarioContext.ContainsKey("APIRequest") || scenarioContext.ContainsKey("APIResponse"))
                        {
                            if (scenarioContext.ContainsKey("APIRequest"))
                                _scenario.CreateNode<And>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), lang));
                            if (scenarioContext.ContainsKey("APIResponse"))
                                _scenario.CreateNode<And>(stepName).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), lang));
                            scenarioContext.Remove("APIResponse");
                        }
                        else
                            _scenario.CreateNode<And>(stepName);
                    }                    
                }
                else
                {
                    if (stepType == "Given")
                    {
                        _scenario.CreateNode<Given>(stepName);
                    }
                    else if (stepType == "When")
                    {
                        _scenario.CreateNode<When>(stepName);
                    }
                    else if (stepType == "Then")
                    {
                        _scenario.CreateNode<Then>(stepName);
                    }
                    else if (stepType == "And")
                    {
                        _scenario.CreateNode<And>(stepName);
                    }
                }

            }

            //When scenario fails
            if (scenarioContext.TestError != null)
            {
                if (ConfigHelper.GetLogSwitch().Equals("enabled"))
                {
                    if (stepType == "Given")
                    {
                        if (scenarioContext.ContainsKey("APIRequest") && scenarioContext.ContainsKey("APIResponse"))
                        {
                            _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), failLang)).
                                Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), failLang));
                            scenarioContext.Remove("APIRequest");
                        }
                        else if (scenarioContext.ContainsKey("APIRequest") || scenarioContext.ContainsKey("APIResponse"))
                        {
                            if (scenarioContext.ContainsKey("APIRequest"))
                                _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), failLang));
                            if (scenarioContext.ContainsKey("APIResponse"))
                                _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), failLang));
                            scenarioContext.Remove("APIResponse");
                        }
                        else
                            _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());

                    }
                    else if (stepType == "When")
                    {
                        if (scenarioContext.ContainsKey("APIRequest") && scenarioContext.ContainsKey("APIResponse"))
                        {
                            _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), failLang)).
                                Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), failLang));
                            scenarioContext.Remove("APIRequest");
                        }
                        else if (scenarioContext.ContainsKey("APIRequest") || scenarioContext.ContainsKey("APIResponse"))
                        {
                            if (scenarioContext.ContainsKey("APIRequest"))
                                _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), failLang));
                            if (scenarioContext.ContainsKey("APIResponse"))
                                _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), failLang));
                            scenarioContext.Remove("APIResponse");
                        }
                        else
                            _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                    }
                    else if (stepType == "Then")
                    {
                        if (scenarioContext.ContainsKey("APIRequest") && scenarioContext.ContainsKey("APIResponse"))
                        {
                            _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), failLang)).
                                Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), failLang));
                            scenarioContext.Remove("APIRequest");
                        }
                        else if (scenarioContext.ContainsKey("APIRequest") || scenarioContext.ContainsKey("APIResponse"))
                        {
                            if (scenarioContext.ContainsKey("APIRequest"))
                                _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), failLang));
                            if (scenarioContext.ContainsKey("APIResponse"))
                                _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), failLang));
                            scenarioContext.Remove("APIResponse");
                        }
                        else
                            _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                    }
                    else if (stepType == "And")
                    {
                        if (scenarioContext.ContainsKey("APIRequest") && scenarioContext.ContainsKey("APIResponse"))
                        {
                            _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), failLang)).
                                Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), failLang));
                            scenarioContext.Remove("APIRequest");
                        }
                        else if (scenarioContext.ContainsKey("APIRequest") || scenarioContext.ContainsKey("APIResponse"))
                        {
                            if (scenarioContext.ContainsKey("APIRequest"))
                                _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIRequest"].ToString(), failLang));
                            if (scenarioContext.ContainsKey("APIResponse"))
                                _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build()).Info(MarkupHelper.CreateCodeBlock(scenarioContext["APIResponse"].ToString(), failLang));
                            scenarioContext.Remove("APIResponse");
                        }
                        else
                            _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                    }
                }
                else
                {
                    if (stepType == "Given")
                    {
                        _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                    }
                    else if (stepType == "When")
                    {
                        _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                    }
                    else if (stepType == "Then")
                    {
                        _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                    }
                    else if (stepType == "And")
                    {
                        _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                            MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                    }
                }

            }
        }       
    }
}
