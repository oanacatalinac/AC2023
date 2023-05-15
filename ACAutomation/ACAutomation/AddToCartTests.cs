using System.Threading;
using ACAutomation.PageObjects;
using ACAutomation.PageObjects.InputDataBO;
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
            homePage = new HomePage(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
        }

        [TestMethod]
        public void Should_AddToCartAndPlaceOrder_When_UserIsNotLoggedIn()
        {
            var addressData = new ShippingAddressBO
            {
                EmailAddress = "email@email.ro",
                FirstName = "John",
                LastName = "Doe",
                StreetAddress = "AC address1",
                City = "Iasi",
                State = "Hawaii",
                ZipCode = "12345",
                Telephone = "1234567890",
                ShippingMethods = 1
            };

            var navigatePage = homePage.menuItemControl.NavigateToWatchesPage()
                .NavigateToFirstWatchProduct()
                .AddProductToCart()
                .GoToShoppingCart()
                .ProceedToCheckoutPage()
                .AddShippingAddress(addressData)
                .PlaceOrder();

            navigatePage.WaitForElement();
            Assert.AreEqual("Thank you for your purchase!", navigatePage.PageTitle.Text);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
