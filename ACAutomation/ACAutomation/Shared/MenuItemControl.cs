using System.Threading;
using ACAutomation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace ACAutomation.Shared
{
    public class MenuItemControl
    {
        private IWebDriver driver;

        public MenuItemControl(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement MenuGearOption => driver.FindElement(By.XPath("//div[@id='store.menu']//span[text()='Gear']"));

        public IWebElement MenuWatchesOption => driver.FindElement(By.XPath("//div[@id='store.menu']//span[text()='Gear']/../following-sibling::ul[@role='menu']//span[text()='Watches']"));

        public WatchesPage NavigateToWatchesPage()
        {
            Thread.Sleep(2000);
            // hover on menu > gear element
            new Actions(driver).MoveToElement(MenuGearOption).Perform();
            MenuWatchesOption.Click();

            return new WatchesPage(driver);
        }
    }
}
