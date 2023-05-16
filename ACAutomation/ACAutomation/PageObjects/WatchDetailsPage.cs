using ACAutomation.Helpers;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class WatchDetailsPage
    {
        private IWebDriver driver;

        public WatchDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnAddToCart => driver.FindElement(By.Id("product-addtocart-button"));

        public WatchDetailsPage AddProductToCart()
        {
            BtnAddToCart.Click();

            return this;
        }

        public By ShoppingCartSelector => By.LinkText("shopping cart");

        public IWebElement ShoppingCartLink => driver.FindElement(ShoppingCartSelector);

        public ShoppingCartPage GoToShoppingCart()
        {
            WaitHelpers.WaitForElementToBeClickable(driver, ShoppingCartSelector);
            ShoppingCartLink.Click();

            return new ShoppingCartPage(driver);
        }
    }
}
