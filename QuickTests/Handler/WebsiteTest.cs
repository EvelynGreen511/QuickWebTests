using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    class WebsiteTest
    {
        private enum Identifier
        {
            CLASS,
            ID,
            XPATH,
        }

        private List<string> ErrorMessage = new List<string>
        {
            { "Oops, an error occurred!" }
        };

        private string resultPath;
        private List<WebTestTask> list;

        public WebsiteTest(List<WebTestTask> list, string resultPath)
        {
            this.list = list;
            this.resultPath = resultPath;
        }

                
        public List<WebTestResult> TestWebsiteAvailablility()
        {

            List<WebTestTask> availabilityTest = list.FindAll(t => (t.Task == WebTestTask.TestType.AVAILABILTIY));
            List<WebTestResult> resultList = new List<WebTestResult>();
            
            var driver = new ChromeDriver();

            foreach(WebTestTask site in availabilityTest)
            {
                WebTestResult r = new WebTestResult();
                try
                {
                    driver.Navigate().GoToUrl(site.Url);
                    AcceptIfCookieBanner(driver);
                    r.HttpStatus = GetHttpStatus(site.Url);
                    r.ScreenshotPath = SaveScreenshot(driver, resultPath);
                    r.TestTask = site;
                    r.Typo3ErrorText = SearchForTypo3ErrorText(driver);
                }
                catch (Exception e)
                {
                    r.Message = "An Error occured";
                    r.ErrorMessage = e.Message;
                }

                resultList.Add(r);
            }
    
            driver.Close();
            return resultList;
        }

        private void AcceptIfCookieBanner(ChromeDriver driver)
        {
            if(CheckIfElementIsShownOnPage(driver, Identifier.CLASS, "cookie-accept"))
            {
                IWebElement element = driver.FindElementByClassName("cookie-btn");
                element.Click();
            }
        }

        private string SaveScreenshot(ChromeDriver driver, string resultPath)
        {
            Screenshot shot = ((ITakesScreenshot)driver).GetScreenshot();
            string path = resultPath + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + ".png";
            shot.SaveAsFile(path);

            return path;
        }


        private HttpStatusCode GetHttpStatus(String url)
        {
            HttpStatusCode result = default(HttpStatusCode);

            var request = HttpWebRequest.Create(url);
            request.Method = "HEAD";
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response != null)
                {
                    result = response.StatusCode;
                    response.Close();
                }
            }

            return result;
        }


        private string SearchForTypo3ErrorText(ChromeDriver driver)
        {
            foreach(string error in this.ErrorMessage)
            {
                
            }

            return string.Empty;
        }

        private bool CheckIfElementIsShownOnPage(ChromeDriver driver, Identifier type, string elementIdentifier)
        {
            try
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
                bool elementExisting = wait.Until(condition =>
                {
                    try
                    {
                        IWebElement loadedElement = null;

                        if (type == Identifier.CLASS)
                        {
                            loadedElement = driver.FindElementByClassName(elementIdentifier);
                        }
                        else if (type == Identifier.ID)
                        {
                            loadedElement = driver.FindElementById(elementIdentifier);
                        }
                        else if (type == Identifier.XPATH)
                        {
                            loadedElement = driver.FindElementByXPath(elementIdentifier);
                        }
                        else
                        {
                            return false;
                        }

                        return loadedElement.Displayed;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
                return elementExisting;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CheckForTypo3ErrorTextOnPage(ChromeDriver driver)
        {

            return false;
        }
    }
}
