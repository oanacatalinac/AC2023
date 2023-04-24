using ACAutomation.Shared;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;

        //reference the menu item control
        public MenuItemControl menuItemControl => new MenuItemControl(driver);

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }
    }
}
