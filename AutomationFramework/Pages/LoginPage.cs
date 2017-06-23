using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace AutomationFramework.Pages
{
    public class LoginPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement userNameTextField;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordTextField;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement loginButton;

        public override void GoTo()
        {
            Pages.RouteControl.Login();
            WaitForIt();
        }

        public override bool IsAt()
        {
            throw new NotImplementedException();
        }

        public override void WaitForIt()
        {
            Browser.RetrySearchElement(userNameTextField);
        }


        public void DoLogin(string userName, string password)
        {
            userNameTextField.SendKeys(userName);
            passwordTextField.SendKeys(password);
            loginButton.Click();
            Pages.MainPage.WaitForIt();
        }

        
    }
}