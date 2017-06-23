using NUnit.Framework;
using System;
using System.Threading;

namespace AutomationFramework
{
    [SetUpFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            Browser.Initialize();     
        }
        
        [OneTimeTearDown]
        public void EndBrowser()
        {            
            Browser.EndBrowser();
        }
    }
}