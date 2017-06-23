using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;

namespace AutomationFramework.Pages
{
    public class RoutingPage
    {        
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement userNameTextField;

        internal void TeamInfo()
        {
            Browser.GoTo("HomeSite#/Admin/TeamInfo");
        }

        public void Home()
        {
            Browser.GoTo("");
        }

        public void Login()
        {
            Pages.HomePage.LoginLink();            
        }

        public void MainPage()
        {
            Browser.GoTo("HomeSite#/Home");
        }

        public void UserRegister()
        {
            Browser.GoTo("HomeSite#/PlayerRegistration");
        }
    }
}