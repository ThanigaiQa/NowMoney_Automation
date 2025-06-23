using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Extras = SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using OpenDialogWindowHandler;

namespace KARE.E2E.AUTOMATION.Helpers
{
    public class SeleniumActions
    {
        #region Declaration
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private IWebElement element;
        private IList<IWebElement> elements;
        private Actions _actions;
        private IJavaScriptExecutor javaScriptExecutor;
        #endregion

        #region Constructor
        public SeleniumActions(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            _actions = new Actions(_driver);
            javaScriptExecutor = (IJavaScriptExecutor)_driver;
        }
        #endregion

        #region NavigationActions
        /// <summary>
        /// Navigates the browser to the specified Url 
        /// </summary>
        /// <param name="url">Url that needs to be opened</param>
        public void NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Returns the current URL from the browser 
        /// </summary>
        /// <returns>URL as string</returns>
        public string GetURL()
        {
            return _driver.Url;
        }

        /// <summary>
        /// Refresh the current page 
        /// </summary>
        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }

        /// <summary>
        /// Navigates back to the previous page
        /// </summary>
        public void NavigateBack()
        {
            _driver.Navigate().Back();
        }

        /// <summary>
        /// Navigates forward to the previous page
        /// </summary>
        public void NavigateForward()
        {
            _driver.Navigate().Forward();
        }
        #endregion

        #region FindActions
        /// <summary>
        /// Waits for an element until it exists and returns the found element
        /// </summary>
        /// <param name="by"></param>
        /// <returns>WebElement</returns>
        public IWebElement FindElement(By by)
        {
            return _wait.Until(Extras.ExpectedConditions.ElementExists(by));
        }
        public IWebElement FindElement(String xpath)
        {
            return _driver.FindElement(By.XPath(xpath));
        }

        /// <summary>
        /// Finds a list of elements that matches the locator specified
        /// </summary>
        /// <param name="by"></param>
        /// <returns>List of WebElements</returns>
        public IList<IWebElement> FindElements(By by)
        {
            _wait.Until(Extras.ExpectedConditions.ElementIsVisible(by));
            return _driver.FindElements(by);
        }

        /// <summary>
        /// Finds a list of elements that matches the locator specified and clicks a specific element based on index provided
        /// </summary>
        /// <param name="by"></param>
        /// <param name="index"></param>
        public void FindElementsAndClick(By by, int index)
        {
            IList<IWebElement> elements = FindElements(by);
            elements[index].Click();
        }

        /// <summary>
        /// Finds a list of elements and gets the text of element based on index provided
        /// </summary>
        /// <param name="by"></param>
        /// <param name="index"></param>
        /// <returns>Text of an WebElement as String</returns>
        public string FindElementsAndGetText(By by, int index)
        {
            IList<IWebElement> elements = FindElements(by);
            return elements[index].Text;
        }

        /// <summary>
        /// Finds a list of elements and gets the text of all the elements
        /// </summary>
        /// <param name="by"></param>
        /// <returns>Text of all the WebElements Identified as List<String></returns>
        public List<string> FindElementsAndGetText(By by)
        {
            List<string> text = new List<string>();
            IList<IWebElement> elements = FindElements(by);
            for (int i = 0; i < elements.Count; i++)
            {
                text.Add(elements[i].Text);
            }
            return text;
        }
        #endregion

        #region SendKeysActions
        /// <summary>
        /// Waits for an element to be visible, clear the text already present and sends the text to the element
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text"></param>
        public void SendKeys(By by, string text)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementIsVisible(by));
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Waits for an element to be visible, clear the text and sends the text for a no.of times the loop is mentioned
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text"></param>
        /// <param name="loop"></param>
        public void SendKeys(By by, string text, int loop)
        {
            for (int l = 1; l <= loop; l++)
            {
                element = _wait.Until(Extras.ExpectedConditions.ElementIsVisible(by));
                element.SendKeys(text);
            }
        }
        #endregion

        #region ClickActions
        /// <summary>
        /// Waits for an element to be clickable and clicks the element
        /// </summary>
        /// <param name="by"></param>
        public void Click(By by)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementToBeClickable(by));
            element.Click();
        }

        /// <summary>
        /// Waits for an element to be clickable and clicks the element
        /// </summary>
        /// <param name="element"></param> 
        public void ClickElement(IWebElement element)
        {
            _wait.Until(Extras.ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }
        public void Click(String xpath)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
            element.Click();
        }

        /// <summary>
        /// Waits for an element to be visible and clicks the element
        /// </summary>
        /// <param name="by"></param>
        public void ClickIfPresent(By by)
        {
            try
            {
                element = _wait.Until(Extras.ExpectedConditions.ElementIsVisible(by));
                element.Click();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Waits for a link to be visible and clicks on the link || Pass only the text of the link
        /// </summary>
        /// <param name="link"></param>
        public void ClickOnLink(string link)
        {
            try
            {
                element = _wait.Until(Extras.ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='" + link + "']")));
                element.Click();
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Waits for an element to be clickable and clicks the element using Actions Class
        /// </summary>
        /// <param name="by"></param>
        public void ActionsClick(By by)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementToBeClickable(by));
            _actions.Click(element).Build().Perform();
        }

        /// <summary>
        /// Waits for an element to be clickable and perform right click on the element
        /// </summary>
        /// <param name="by"></param>
        public void ClickThroughJavaScript(By by)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementToBeClickable(by));
            javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Waits for an element to be clickable and perform right click on the element
        /// </summary>
        /// <param name="by"></param>
        public void ContextClick(By by)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementToBeClickable(by));
            Actions actionProvider = new Actions(_driver);
            actionProvider.ContextClick(element).Build().Perform();
        }

        /// <summary>
        /// Waits for an element to be clickable and double click on the element using Actions Class
        /// </summary>
        /// <param name="by"></param>
        public void DoubleClick(By by)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementToBeClickable(by));
            _actions.DoubleClick(element).Build().Perform();
        }

        /// <summary>
        /// Double click on an element using Actions Class from a group of elements
        /// </summary>
        /// <param name="by"></param>
        /// <param name="index"></param>
        public void DoubleClick(By by, int index)
        {
            IList<IWebElement> elements = FindElements(by);
            _actions.DoubleClick(elements[index]).Build().Perform();
        }
        #endregion

        #region OtherActions

        /// <summary>
        /// Selects an element from Dropdown by index or value or text
        /// </summary>
        /// <param name="by">Locator of type By</param>
        /// <param name="type">'index' or 'value' or 'type'</param>
        /// <param name="value"></param>
        public void SelectDropDown(By by, string type, string value)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementIsVisible(by));
            SelectElement select = new SelectElement(element);
            switch (type)
            {
                case "value":
                    select.SelectByValue(value);
                    break;
                case "index":
                    select.SelectByIndex(0);
                    break;
                case "text":
                    select.SelectByText(value);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Switch to an IFrame by using its locator as input
        /// </summary>
        /// <param name="by"></param>
        public void SwitchToFrame(By by)
        {
            _wait.Until(Extras.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(by));
        }

        /// <summary>
        /// Switch to the default parent frame
        /// </summary>
        public void SwitchToParentFrame()
        {
            _driver.SwitchTo().ParentFrame();
        }
        /// <summary>
        /// Waits for an element to exist and scroll to the element using JavaScriptExecutor 
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        public void ScrollToElement(By by, int timeout = 20)
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
            element = _wait.Until(Extras.ExpectedConditions.ElementExists(by));
            javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(false);", element);
        }

        /// <summary>
        /// scrolls to the specific position using JavaScriptExecutor 
        /// </summary>
        public void ScrollToPosition(int xAxis, int yAxis)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(" + xAxis + "," + yAxis + ");");
        }

        /// <summary>
        /// Gets a list of elements and move to a particular element based on index provided
        /// </summary>
        /// <param name="by"></param>
        /// <param name="cnt"></param>
        public void MoveToElement(By by, int cnt = 0)
        {
            elements = _driver.FindElements(by);
            Actions actions = new Actions(_driver);
            Thread.Sleep(1000);
            actions.MoveToElement(elements[cnt]);
            actions.Perform();
        }
        /// <summary>
        /// Clears the cache of the browser
        /// </summary>
        public void ClearCache()
        {
            try
            {
                Thread.Sleep(5000);
                //Delete all cookies
                _driver.Manage().Cookies.DeleteAllCookies();
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                //get the browser & Browse to the Settings
                String siteLink = "chrome://settings/clearBrowserData";
                _driver.Navigate().GoToUrl(siteLink);
                //wait for setting window to load
                Wait(3);
                //get clear cache button
                IWebElement clearBtn = (IWebElement)js.ExecuteScript
                    ("return document.querySelector(\"body > settings-ui\").shadowRoot.querySelector(\"#main\")" +
                    ".shadowRoot.querySelector(\"settings-basic-page\").shadowRoot.querySelector(\"#basicPage > " +
                    "settings-section:nth-child(9) > settings-privacy-page\").shadowRoot.querySelector" +
                    "(\"settings-clear-browsing-data-dialog\").shadowRoot.querySelector(\"#clearBrowsingDataConfirm\")");
                //Click
                clearBtn.Click();
            }
            catch (Exception ex)
            {
                Thread.Sleep(5000);
                //Delete all cookies
                _driver.Manage().Cookies.DeleteAllCookies();
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                //get the browser & Browse to the Settings
                String siteLink = "chrome://settings/clearBrowserData";
                _driver.Navigate().GoToUrl(siteLink);
                //wait for setting window to load
                Wait(3);
                //get clear cache button
                IWebElement clearBtn = (IWebElement)js.ExecuteScript
                    ("return document.querySelector(\"body > settings-ui\").shadowRoot.querySelector(\"#main\")" +
                    ".shadowRoot.querySelector(\"settings-basic-page\").shadowRoot.querySelector(\"#basicPage > " +
                    "settings-section:nth-child(10) > settings-privacy-page\").shadowRoot.querySelector" +
                    "(\"settings-clear-browsing-data-dialog\").shadowRoot.querySelector(\"#clearBrowsingDataConfirm\")");
                //Click
                clearBtn.Click();
            }
        }

        /// <summary>
        /// Upload File by browsing
        /// </summary>
        public void UploadFile(String path, String fileName)
        {   
            HandleOpenDialog hndOpen = new HandleOpenDialog();
            hndOpen.fileOpenDialog(path, fileName);
        }

        #endregion        

        #region WaitActions
        /// <summary>
        /// Verify if the element is displayed
        /// </summary>
        /// <param name="by"></param>
        /// <returns>Boolean true or false</returns>
        public bool VerifyElementIsDisplayed(By by)
        {
            return _driver.FindElement(by).Displayed;
        }
        /// <summary>
        /// Verify if the element is clickable
        /// </summary>
        /// <param name="by"></param>
        /// <returns>Boolean true or false</returns>
        public bool VerifyElementIsEnabled(By by)
        {
            return _driver.FindElement(by).Enabled;
        }

        /// <summary>
        /// Wait Statement to wait for an element to exist in DOM
        /// </summary>
        /// <param name="by"></param>
        public void WaitForElementToExists(By by)
        {
            _wait.Until(Extras.ExpectedConditions.ElementExists(by));
        }

        /// <summary>
        /// Boolean condition that waits until an element exist
        /// </summary>
        /// <param name="by"></param>
        /// <param name="seconds"></param>
        /// <returns>True or False based on WebElement Existance</returns>
        public bool IsElementPresent(By by, int seconds = 40)
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            try
            {
                element = _wait.Until(Extras.ExpectedConditions.ElementExists(by));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Basically Thread.Sleep
        /// </summary>
        /// <param name="time"></param>
        public void Wait(int time)
        {
            Thread.Sleep(TimeSpan.FromSeconds(time));
        }

        #endregion

        #region ActionsThatReturnValues
        /// <summary>
        /// Get the count of elements from a group of elements and returns the index of a random element from the group
        /// </summary>
        /// <param name="by"></param>
        /// <returns>Index of an element from a group</returns>
        public int SelectRandomElement(By by)
        {
            elements = _wait.Until(Extras.ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            elements = _driver.FindElements(by);
            Random random = new Random();
            return random.Next(elements.Count);
        }

        /// <summary>
        /// To get the attribute value of any WebElement
        /// </summary>
        /// <param name="by"></param>
        /// <param name="attribute"></param>
        /// <returns>Value of attribute as String</returns>
        public string GetAttributeValue(By by, string attribute)
        {
            element = _wait.Until(Extras.ExpectedConditions.ElementExists(by));
            return element.GetAttribute(attribute);
        }

        /// <summary>
        /// To get the text of an element by providing locator and index as input
        /// </summary>
        /// <param name="by"></param>
        /// <param name="index"></param>
        /// <returns>Text of an WebElement as String</returns>
        public string GetText(By by, int index = 0)
        {
            IList<IWebElement> elements = FindElements(by);
            return elements[index].Text;
        }

        /// <summary>
        /// To get the count of elements with matching locator criteria 
        /// </summary>
        /// <param name="by"></param>
        /// <returns>Count of WebElements as Integer</returns>
        public int ElementCount(By by)
        {
            int count = _driver.FindElements(by).Count;
            return count;
        }


        /// <summary>
        /// To return ManageCensusPage Column Header count || Pass header value as input and it returns its column index value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Column index value of header</returns>
        public int returnHeaderCount(String value)
        {
            int i;
            for (i = 2; i < _driver.FindElements(By.XPath("//table/thead/tr/th//div/span")).Count; i++)
            {
                if (_driver.FindElement(By.XPath("//table/thead/tr/th[" + i + "]//div/span")).Text.Contains(value))
                    break;
            }
            return i;
        }
        
        #endregion
    }
}
