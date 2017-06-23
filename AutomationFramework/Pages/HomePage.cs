using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AutomationFramework.Pages
{
    public class HomePage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//a[@ng-click='startLogin()']")]
        private IWebElement goToLoginLink;

        public override void GoTo()
        {
            Pages.RouteControl.Home();
            WaitForIt();
        }

        public override bool IsAt()
        {
            return Browser.GetCurrentUrl().Equals(@"http://homolog.airsoftsafezone.com/Home");
        }

        public override void WaitForIt()
        {
            Browser.RetrySearchElement(goToLoginLink);
        }

        public void LoginLink()
        {
            WaitForIt();
            goToLoginLink.Click();
            Pages.LoginPage.WaitForIt();
        }
    }
}