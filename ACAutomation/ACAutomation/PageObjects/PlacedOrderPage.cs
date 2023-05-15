using ACAutomation.Helpers;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class PlacedOrderPage
    {
        public IWebDriver driver;

        public PlacedOrderPage(IWebDriver browser)
        {
            driver = browser;
        }

        public By PageTitleSelector => By.XPath("//h1[@class='page-title']/span[.='Thank you for your purchase!']");

        public IWebElement PageTitle => driver.FindElement(PageTitleSelector);

        public void WaitForElement()
        {
            WaitHelpers.WaitForElementToBePresent(driver, PageTitleSelector);
        }
    }
}