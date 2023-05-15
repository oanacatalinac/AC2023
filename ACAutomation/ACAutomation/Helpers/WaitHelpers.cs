using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ACAutomation.Helpers
{
    public static class WaitHelpers
    {
        public static void WaitForElementToBeVisible(IWebDriver driver, By by, int timeSpan = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        public static void WaitForElementToBeClickable(IWebDriver driver, By by, int timeSpan = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        public static void WaitForElementToBePresent(IWebDriver driver, By by, int timeSpan=10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
        }

        public static void WaitForElementToNotBeDisplayed(IWebDriver driver, By by, int timeSpan=20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public static void WaitForElementToBeVisibleCustom(IWebDriver driver, By by, int timeSpan=10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
            wait.Until(webDriver => webDriver.FindElement(by).Displayed && webDriver.FindElement(by).Enabled);
        }
    }
}
