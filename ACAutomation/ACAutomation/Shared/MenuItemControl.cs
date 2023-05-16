using ACAutomation.Helpers;
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

        public By MenuGearSelector => By.XPath("//div[@id='store.menu']//span[text()='Gear']");

        public IWebElement MenuGearOption => driver.FindElement(MenuGearSelector);

        public By MenuWatchesSelector => By.XPath("//div[@id='store.menu']//span[text()='Gear']/../following-sibling::ul[@role='menu']//span[text()='Watches']");

        public IWebElement MenuWatchesOption => driver.FindElement(MenuWatchesSelector);

        public WatchesPage NavigateToWatchesPage()
        {
            WaitHelpers.WaitForElementToBeVisible(driver, MenuGearSelector);
            WaitHelpers.WaitForElementToBePresent(driver, MenuWatchesSelector);

            // hover on menu > gear element
            new Actions(driver).MoveToElement(MenuGearOption).Perform();
            MenuWatchesOption.Click();

            return new WatchesPage(driver);
        }
    }
}
