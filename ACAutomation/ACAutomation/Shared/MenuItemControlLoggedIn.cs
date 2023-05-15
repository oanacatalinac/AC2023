using ACAutomation.Helpers;
using OpenQA.Selenium;

namespace ACAutomation.Shared
{
    public class MenuItemControlLoggedIn : MenuItemControl
    {
        public IWebDriver driver;

        public MenuItemControlLoggedIn(IWebDriver browser):base(browser)
        {
            driver = browser;
        }

        public By WelcomeUserSelector => By.XPath("//div[@class='panel header']//li[@class='greet welcome']/span[@class='logged-in']");

        public IWebElement WelcomeUserLabel => driver.FindElement(WelcomeUserSelector);

        public void WaitForElement()
        {
            try
            {
                WaitHelpers.WaitForElementToBeVisibleCustom(driver, WelcomeUserSelector);
            }
            catch (StaleElementReferenceException)
            {
                WaitHelpers.WaitForElementToBeVisibleCustom(driver, WelcomeUserSelector);
            }        
        }
    }
}
