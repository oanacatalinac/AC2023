using System.Threading;
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

        public IWebElement BtnPlaceOrder => driver.FindElement(By.CssSelector("button[title='Place Order']"));

        public PlacedOrderPage PlaceOrder()
        {
            Thread.Sleep(5000);
            BtnPlaceOrder.Click();

            return new PlacedOrderPage(driver);
        }
    }
}
