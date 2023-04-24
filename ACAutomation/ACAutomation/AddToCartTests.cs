using ACAutomation.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ACAutomation
{
    [TestClass]
    public class AddToCartTests
    {
        private IWebDriver driver;
        private HomePage homePage;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            homePage=new HomePage(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
        }

        [TestMethod]
        public void Should_AddToCartAndPlaceOrder_When_UserIsNotLoggedIn()
        {
            var navigatePage= homePage.menuItemControl.NavigateToWatchesPage()
                .NavigateToFirstWatchProduct()
                .AddProductToCart()
                .GoToShoppingCart()
                .ProceedToCheckoutPage();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
