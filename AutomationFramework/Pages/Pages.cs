using OpenQA.Selenium.Support.PageObjects;


namespace AutomationFramework.Pages
{
    public static class Pages
    {
        private static T GetPages<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(Browser.Driver, page);
            
            return page;
        }

        public static RoutingPage RouteControl { get { return GetPages<RoutingPage>(); } }

        public static HomePage HomePage { get { return GetPages<HomePage>(); } }

        public static LoginPage LoginPage { get { return GetPages<LoginPage>(); } }

        public static MainPage MainPage { get { return GetPages<MainPage>(); } }

        public static TeamInfoPage TeamInfoPage { get { return GetPages<TeamInfoPage>(); } }
        
    }

    
}