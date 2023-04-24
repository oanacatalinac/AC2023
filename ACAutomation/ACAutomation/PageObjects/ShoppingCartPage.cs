using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class ShoppingCartPage
    {
        private IWebDriver driver;

        public ShoppingCartPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnProceedToCheckout => driver.FindElement(By.XPath("//button[@title='Proceed to Checkout']/span"));

        public ShippingAddressPage ProceedToCheckoutPage()
        {
            BtnProceedToCheckout.Click();

            return new ShippingAddressPage(driver);
        }
    }
}
