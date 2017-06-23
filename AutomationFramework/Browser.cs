using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Collections.Generic;

namespace AutomationFramework
{
    public static class Browser
    {
        private static string baseUrl = "http://homolog.airsoftsafezone.com/";
        private static IWebDriver webDriver = new ChromeDriver();

        public static void Initialize()
        {            
            GoTo("");
        }

        public static string Title { get { return webDriver.Title; } }

        public static ISearchContext Driver { get { return webDriver; } }

        public static void GoTo(string url)
        {
            webDriver.Url = baseUrl + url;
            //Thread.Sleep(TimeSpan.FromSeconds(2));
        }        

        public static void EndBrowser()
        {
            if (webDriver != null)
            {
                webDriver.Close();
                webDriver.Dispose();
            }
        }

        public static string GetCurrentUrl()
        {
            return webDriver.Url;
        }

        public static void GoToActiveElement()
        {
            webDriver.SwitchTo().ActiveElement();
        }

        public static void RetrySearchElement(IWebElement element)
        {                        
            var tries = 0;
            while (tries < 10)
            {
                try
                {
                    if(element.Displayed && element.Enabled)
                        break;
                }
                catch (ElementNotVisibleException)
                {
                    Thread.Sleep(500);
                }
                catch (NoSuchElementException)
                {
                    Thread.Sleep(500);
                }
                tries++;
            }
        }

        public static void Refresh()
        {
            webDriver.Navigate().Refresh();
        }

        public static void RetrySearchElementList(IList<IWebElement> elementList)
        {
            var tries = 0;
            while(tries < 10)
            {
                if (elementList != null && elementList.Count != 0)
                    break;
                Thread.Sleep(500);
                tries++;
            }
        }

        public static void BuildChainActionMouseOverShowElement(IWebElement target, string xpath)
        {
            Actions action = new Actions(webDriver);
            action.MoveToElement(target);
            Thread.Sleep(1000);
            var secondElement = webDriver.FindElement(By.XPath(xpath));
            action.MoveToElement(secondElement);
            Thread.Sleep(1000);
            action.Click().Build().Perform();
                //.Click().Build().Perform();
        }
    }
}