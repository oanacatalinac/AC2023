using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace ACAutomation
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
        }

        [TestMethod]
        public void Should_LoginUser_When_ValidCredentialsAreUsed()
        {
            //click sign in button from header
            driver.FindElement(By.XPath("//div[@class='panel header']//a[contains(text(), 'Sign In')]")).Click();

            //fill in valid user email
            driver.FindElement(By.Id("email")).SendKeys("test@email.ro");

            //fill in user password
            driver.FindElement(By.Name("login[password]")).SendKeys("Test!123");

            //click sign in button
            driver.FindElement(By.CssSelector("button[name='send']")).Click();

            // sleep
            Thread.Sleep(2000);

            //assert
            Assert.AreEqual("Welcome, Test Firstname Test Lastname!",
                driver.FindElement(By.XPath("//div[@class='panel header']//li[@class='greet welcome']/span[@class='logged-in']")).Text);
        }

        [TestMethod]
        public void Should_NotLoginUser_When_WrongEmailIsUsed()
        {
            //click sign in button from header
            driver.FindElement(By.XPath("//div[@class='panel header']//a[contains(text(), 'Sign In')]")).Click();

            //fill in wrong user email
            driver.FindElement(By.Id("email")).SendKeys("tester@outlook.com");

            //fill in user password
            driver.FindElement(By.Name("login[password]")).SendKeys("Test!123");

            //click sign in button
            driver.FindElement(By.CssSelector("button[name='send']")).Click();

            // sleep
            Thread.Sleep(2000);

            //assert
            Assert.AreEqual("The account sign-in was incorrect or your account is disabled temporarily. Please wait and try again later.",
                driver.FindElement(By.XPath("//div[@role = 'alert']/div/div")).Text);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}