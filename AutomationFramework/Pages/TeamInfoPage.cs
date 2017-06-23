using AutomationFramework.Generators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace AutomationFramework.Pages
{
    public class TeamInfoPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "txtTeamName")]
        private IWebElement teamNameInputField;

        [FindsBy(How = How.XPath, Using = "//div[@ng-repeat='item in model.responsibles']")]
        private IList<IWebElement> lstDivResponsibles;

        [FindsBy(How = How.ClassName, Using = "note-editable")]
        private IWebElement detailsTextAreaField;

        [FindsBy(How = How.Id, Using = "imgPreview")]
        private IWebElement imgPreview;

        [FindsBy(How = How.Id, Using = "fileInput")]
        private IWebElement fileInputField;

        [FindsBy(How = How.ClassName, Using = "player-profile-picture-box")]
        private IWebElement divPlayerPictureBox;

        public object Therad { get; private set; }

        public override void GoTo()
        {
            Pages.RouteControl.TeamInfo();
            WaitForIt();
        }

        public override bool IsAt()
        {
            return Browser.GetCurrentUrl().Equals(@"http://homolog.airsoftsafezone.com/HomeSite#/Admin/TeamInfo");
        }

        public override void WaitForIt()
        {
            Browser.RetrySearchElement(teamNameInputField);
        }

        public List<object> ChangeResponsibleName()
        {
            var lst = new List<object>();
            Browser.RetrySearchElementList(lstDivResponsibles);
            foreach (var element in lstDivResponsibles)
            {
                var repeatedDiv = element.FindElements(By.TagName("div"));
                var inputName = repeatedDiv[0].FindElement(By.XPath("input[@placeholder='Nome']"));
                var inputEmail = repeatedDiv[1].FindElement(By.XPath("input[@placeholder='Email']"));
                var inputPhone = repeatedDiv[2].FindElement(By.XPath("input[@placeholder='Número de Telefone']"));

                inputName.Clear();
                inputEmail.Clear();
                inputPhone.Clear();

                inputName.SendKeys(TextValuesGenerator.TextGenerator(5));
                inputEmail.SendKeys(TextValuesGenerator.EmailGenerator());
                inputPhone.SendKeys(TextValuesGenerator.PhoneGenerator());
                inputPhone.SendKeys(Keys.Tab);

                lst.Add(new
                {
                    name = inputName.GetAttribute("value"),
                    inputEmail = inputEmail.GetAttribute("value"),
                    phone = inputPhone.GetAttribute("value")
                });
            }
            //Must Wait a moment for angular to save changes made
            Thread.Sleep(1000);
            return lst;
        }

        public void RemoveAllButOneResponsible()
        {
            var jump = true;
            Browser.RetrySearchElementList(lstDivResponsibles);
            foreach (var element in lstDivResponsibles)
            {
                if(jump)
                {
                    jump = false;
                    continue;
                }

                var repeatedDiv = element.FindElements(By.TagName("div"));
                var removeButton = repeatedDiv[3].FindElement(By.TagName("button"));
                removeButton.Click();
            }
            //Must Wait a moment for angular to save changes made
            Thread.Sleep(1000);
        }

        public void ChangeTeamImage()
        {
            Browser.BuildChainActionMouseOverShowElement(divPlayerPictureBox, "//div[@ng-click='changeTeamPicture()']");
            var path = AppDomain.CurrentDomain.BaseDirectory;
            
            //fileInputField.SendKeys("");
        }

        public string GetCurrentImagePath()
        {
            try
            {
                return imgPreview.GetAttribute("src");
            }
            catch (NoSuchElementException)
            {
                return "";
            }
        }

        public List<object> GetTeamInfoData()
        {
            var pageDataList = new List<object>();
            Browser.RetrySearchElementList(lstDivResponsibles);
            foreach (var element in lstDivResponsibles)
            {
                var repeatedDiv = element.FindElements(By.TagName("div"));
                var inputName = repeatedDiv[0].FindElement(By.XPath("input[@placeholder='Nome']"));
                var inputEmail = repeatedDiv[1].FindElement(By.XPath("input[@placeholder='Email']"));
                var inputPhone = repeatedDiv[2].FindElement(By.XPath("input[@placeholder='Número de Telefone']"));

                if(!string.IsNullOrEmpty(inputName.GetAttribute("value")) || !string.IsNullOrEmpty(inputEmail.GetAttribute("value")) || !string.IsNullOrEmpty(inputPhone.GetAttribute("value")))
                {
                    pageDataList.Add(new
                    {
                        name = inputName.GetAttribute("value"),
                        inputEmail = inputEmail.GetAttribute("value"),
                        phone = inputPhone.GetAttribute("value")
                    });
                }
            }
            return pageDataList;
        }
        public void Refresh()
        {
            Browser.Refresh();
            WaitForIt();
        }

        public void ChangeTeamName(string changedName)
        {       
            WaitForIt();

            teamNameInputField.Clear();
            teamNameInputField.SendKeys(changedName);
            teamNameInputField.SendKeys(Keys.Tab);
            
            Thread.Sleep(TimeSpan.FromSeconds(6));
        }

        public bool IsChangedName(string changedName)
        {
            WaitForIt();

            var result = teamNameInputField.GetAttribute("value");
            return teamNameInputField.Text.Equals(result);
        }

        public string SaveTeamDetails()
        {
            //Must Wait a moment for angular to load the data
            Thread.Sleep(1000);
            var detailsText = TextValuesGenerator.TextGenerator(150);
            detailsTextAreaField.SendKeys(Keys.Control + "a");
            detailsTextAreaField.SendKeys(Keys.Backspace);
            detailsTextAreaField.SendKeys(detailsText);
            teamNameInputField.Click();
            //Must Wait a moment for angular to save changes made
            Thread.Sleep(1000);
            return detailsText;
        }

        public string GetTeamDetails()
        {
            //Must Wait a moment for angular to load the data
            Thread.Sleep(1000);
            return detailsTextAreaField.GetAttribute("innerHTML");
        }
    }
}