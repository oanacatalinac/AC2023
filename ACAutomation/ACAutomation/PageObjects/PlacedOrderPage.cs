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

        public IWebElement PageTitle => driver.FindElement(By.XPath("//h1[@class='page-title']/span"));
    }
}
