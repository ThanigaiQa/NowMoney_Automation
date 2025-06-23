using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace KARE.E2E.AUTOMATION.Helpers
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;
        public static ExtentTest _feature;
        public static ExtentTest _scenario;

        //public static void ExtentReportInit()
        //{
        //    var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
        //    var actualPath = path.Substring(0, path.LastIndexOf("bin"));
        //    var projectPath = new Uri(actualPath).LocalPath;
        //    Directory.CreateDirectory(projectPath.ToString() + "Reports");
        //    var Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        //    var reportPath = projectPath + "Reports\\NowMoney.html";

        //    var htmlReporter = new ExtentSparkReporter(reportPath);
        //    htmlReporter.Config.ReportName = "NowMoney Automation Report";
        //    htmlReporter.Config.DocumentTitle = "NowMoney Automation Report";
        //    htmlReporter.Config.Theme = Theme.Dark;

        //    _extentReports = new ExtentReports();
        //    _extentReports.AttachReporter(htmlReporter);
        //    _extentReports.AddSystemInfo("Application", "NowMoney Portal");
        //    _extentReports.AddSystemInfo("Browser", ConfigHelper.GetBrowser());
        //    _extentReports.AddSystemInfo("OS", "Windows - " + Environment.OSVersion.Version.ToString());
        //    _extentReports.AddSystemInfo("Environment", ConfigHelper.GetEnvironment());
        //}

        //public static void ExtentReportTearDown()
        //{
        //    _extentReports.Flush();
        //}

        public static void ExtentReportInit()
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            // Create a directory named Reports with current date-time format
            var currentDate = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var reportsDirectory = Path.Combine(projectPath, "Reports", currentDate);
            Directory.CreateDirectory(reportsDirectory);

            var reportPath = Path.Combine(reportsDirectory, "NowMoney.html");

            var htmlReporter = new ExtentSparkReporter(reportPath);
            htmlReporter.Config.ReportName = "NowMoney Automation Report";
            htmlReporter.Config.DocumentTitle = "NowMoney Automation Report";
            htmlReporter.Config.Theme = Theme.Dark;

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            _extentReports.AddSystemInfo("Application", "NowMoney Portal");
            _extentReports.AddSystemInfo("Browser", ConfigHelper.GetBrowser());
            _extentReports.AddSystemInfo("OS", "Windows - " + Environment.OSVersion.Version.ToString());
            _extentReports.AddSystemInfo("Environment", ConfigHelper.GetEnvironment());
        }

        public static void ExtentReportTearDown()
        {
            _extentReports.Flush();
        }
    }
}
