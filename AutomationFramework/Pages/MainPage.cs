using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace AutomationFramework.Pages
{
    public class MainPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//span[@ng-click='questionLogoff()']")]
        private IWebElement logoutLink;

        [FindsBy(How = How.XPath, Using = "//button[@ng-click='yes()']")]
        private IWebElement confirmPopUpButton;

        [FindsBy(How = How.XPath, Using = "//button[@ng-click='no()']")]
        private IWebElement denyPopUpButton;

        public override void GoTo()
        {
            Pages.RouteControl.MainPage();
            WaitForIt();
        }

        public override bool IsAt()
        {
            return Browser.GetCurrentUrl().Equals(@"http://homolog.airsoftsafezone.com/HomeSite#/Home");
        }

        public override void WaitForIt()
        {
            Browser.RetrySearchElement(logoutLink);
        }

        public void Logout()
        {
            Browser.RetrySearchElement(logoutLink);
            logoutLink.Click();
            Browser.RetrySearchElement(confirmPopUpButton);
            confirmPopUpButton.Click();
            Pages.HomePage.WaitForIt();
        }

        
    }
}