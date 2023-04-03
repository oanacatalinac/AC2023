using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ACAutomation
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void Login_ValidCredentials_DisplaysAccountName()
        {
            //open browser
            var driver = new ChromeDriver();

            //maximize browser window
            driver.Manage().Window.Maximize();

            //navigate to application URL
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");

            //click sign in button from header
            driver.FindElement(By.XPath("//div[@class='panel header']//a[contains(text(), 'Sign In')]")).Click();

            //fill in user email
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

            // clean-up
            driver.Quit();
        }
    }
}