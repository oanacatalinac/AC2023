using System.Threading;
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

        public IWebElement ShoppingCartLink => driver.FindElement(By.LinkText("shopping cart"));

        public ShoppingCartPage GoToShoppingCart()
        {
            Thread.Sleep(2000);
            ShoppingCartLink.Click();

            return new ShoppingCartPage(driver);
        }
    }
}
