using AutomationFramework;
using AutomationFramework.Generators;
using AutomationFramework.Pages;
using NUnit.Framework;

namespace AirsoftSafeZoneTests
{
    [TestFixture]
    public class SafeZoneTests : TestBase
    {
        [Test]
        public void LoginFailedTest()
        {
            Pages.HomePage.GoTo();
            Pages.LoginPage.GoTo();
            Pages.LoginPage.DoLogin(TextValuesGenerator.EmailGenerator(), TextValuesGenerator.TextGenerator(10));
            Assert.IsFalse(Pages.MainPage.IsAt());
        }

        [Test]
        public void LoginSuccessTest()
        {
            DoLogin();            
        }

        [Test]
        public void LogoutSuccessTest()
        {
            DoLogin();
            Pages.MainPage.Logout();
            Assert.IsTrue(Pages.HomePage.IsAt());
        }

        [Test]
        public void ChangeTeamNameTest()
        {
            DoLogin();
            var teamName = TextValuesGenerator.TextGenerator(8);
            Pages.TeamInfoPage.GoTo();            
            Pages.TeamInfoPage.ChangeTeamName(teamName);
            Pages.MainPage.GoTo();            
            Pages.TeamInfoPage.GoTo();            
            Assert.IsTrue(Pages.TeamInfoPage.IsChangedName(teamName));
        }

        [Test]
        public void ChangeTeamResponsibles()
        {
            DoLogin();            
            Pages.TeamInfoPage.GoTo();
            var inputList = Pages.TeamInfoPage.ChangeResponsibleName();
            Assert.IsNotNull(inputList);
            Pages.TeamInfoPage.Refresh();
            var outputList = Pages.TeamInfoPage.GetTeamInfoData();
            Assert.AreEqual(inputList, outputList);
        }

        [Test]
        public void RemoveAllButOneTeamResponsibles()
        {
            DoLogin();
            Pages.TeamInfoPage.GoTo();
            Pages.TeamInfoPage.RemoveAllButOneResponsible();
            Pages.TeamInfoPage.Refresh();
            var outputList = Pages.TeamInfoPage.GetTeamInfoData();
            Assert.IsTrue(outputList.Count == 1);
        }

        [Test]
        public void ValidateTeamInfoDetailsMessage()
        {
            DoLogin();
            Pages.TeamInfoPage.GoTo();
            var detailsText = Pages.TeamInfoPage.SaveTeamDetails();
            Pages.TeamInfoPage.Refresh();
            var outputText = Pages.TeamInfoPage.GetTeamDetails();
            Assert.AreEqual(detailsText, outputText);
        }

        [Test]
        public void ChangeTeamImage()
        {
            DoLogin();
            Pages.TeamInfoPage.GoTo();
            var currentImagePath = Pages.TeamInfoPage.GetCurrentImagePath();
            Pages.TeamInfoPage.ChangeTeamImage();
        }

        #region Privates

        private void DoLogin()
        {
            Pages.HomePage.GoTo();
            Pages.LoginPage.GoTo();            
            Pages.LoginPage.DoLogin(TextValuesGenerator.GetTeamAdministratorName(), TextValuesGenerator.GetUserPassword());
            
            Assert.IsTrue(Pages.MainPage.IsAt());
        }

        #endregion
    }
}
