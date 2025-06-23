using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Helpers
{
    public class DriverContext
    {
        #region Declaration
        public readonly IWebDriver _driver;
        public IWebDriver WebDriver { get; set; }
        #endregion

        #region Constructor
        public DriverContext()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var projectPath = path.Substring(0, path.IndexOf("bin"));
            var downloadpath = new Uri(projectPath).LocalPath + "Downloads";            
            ChromeOptions options = new ChromeOptions();
            //options.AddExtension("C:\\Users\\Saranya\\source\\repos\\kare-automation\\KARE.E2E.AUTOMATION\\Data\\Extension\\ADBLOCK.crx");
            options.AddArguments("test-type");
            options.AddArguments("--start-maximized");
            //options.AddArguments("--headless");
            options.AddArguments("--window-size=1920,1080");
            options.AddUserProfilePreference("download.default_directory", downloadpath);
            options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            WebDriver = new ChromeDriver(options);
            WebDriver.Manage().Window.Maximize();

            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
            js.ExecuteScript("document.body.style.zoom='50%'");
            var screenWidth = js.ExecuteScript("return screen.width;");
            var screenHeight = js.ExecuteScript("return screen.height;");

            Console.WriteLine("Screen Resolution is : " + screenWidth + "x" + screenHeight);
        }
        #endregion
    }
}
