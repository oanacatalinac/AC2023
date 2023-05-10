using System.Threading;
using ACAutomation.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ACAutomation
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;
        private LoginPage login;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            login = new LoginPage(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            login.menuItemControlLoggedOut.NavigateToLoginPage();
        }

        [TestMethod]
        public void Should_LoginUser_When_ValidCredentialsAreUsed()
        {
            login.SignInTheApplication("test@email.ro", "Test!123");

            // sleep
            Thread.Sleep(2000);

            //assert
            var expectedResult = "Welcome, Test Firstname Test Lastname!";
            var actualResult = driver.FindElement(By.XPath("//div[@class='panel header']//li[@class='greet welcome']/span[@class='logged-in']")).Text;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Should_NotLoginUser_When_WrongEmailIsUsed()
        {
            login.SignInTheApplication("test@outlook.ro", "Test!123");

            // sleep
            Thread.Sleep(2000);

            //assert
            var expectedResult = "The account sign-in was incorrect or your account is disabled temporarily. Please wait and try again later.";
            var actualResult = driver.FindElement(By.XPath("//div[@role = 'alert']/div/div")).Text;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}