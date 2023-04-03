using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            //fill in user email
            //fill in user password
            //click sign in button
            //assert
        }
    }
}
