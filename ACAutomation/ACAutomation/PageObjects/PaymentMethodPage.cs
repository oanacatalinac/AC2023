using ACAutomation.Helpers;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class PaymentMethodPage
    {
        private IWebDriver driver;

        public PaymentMethodPage(IWebDriver browser)
        {
            driver = browser;
        }

        public By LoaderSelector => By.XPath("//div[@class='loader']");

        public By BtnPlaceOrderSelector => By.CssSelector("button[title='Place Order']");

        public IWebElement BtnPlaceOrder => driver.FindElement(BtnPlaceOrderSelector);

        public PlacedOrderPage PlaceOrder()
        {
            WaitHelpers.WaitForElementToNotBeDisplayed(driver, LoaderSelector);
            WaitHelpers.WaitForElementToBeClickable(driver, BtnPlaceOrderSelector);
            BtnPlaceOrder.Click();

            return new PlacedOrderPage(driver);
        }
    }
}
