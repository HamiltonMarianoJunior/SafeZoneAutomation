
namespace AutomationFramework.Pages
{
    public abstract class BasePage
    {
        public abstract bool IsAt();
        public abstract void WaitForIt();
        public abstract void GoTo();

    }
}