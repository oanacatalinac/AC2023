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

            //assert
            var homePage = new HomePage(driver);
            homePage.menuItemControlLoggedIn.WaitForElement();

            var expectedResult = "Welcome, Test Firstname Test Lastname!";
            Assert.AreEqual(expectedResult, homePage.menuItemControlLoggedIn.WelcomeUserLabel.Text);
        }

        [TestMethod]
        public void Should_NotLoginUser_When_WrongEmailIsUsed()
        {
            login.SignInTheApplication("test4@outlook.ro", "Test!123");

            //assert
            login.WaitForElement();
            var expectedResult = "The account sign-in was incorrect or your account is disabled temporarily. Please wait and try again later.";
            Assert.AreEqual(expectedResult, login.FailedLoginLabel.Text);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}