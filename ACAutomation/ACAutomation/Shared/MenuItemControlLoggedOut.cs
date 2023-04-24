using ACAutomation.PageObjects;
using OpenQA.Selenium;

namespace ACAutomation.Shared
{
    public class MenuItemControlLoggedOut : MenuItemControl
    {
        private IWebDriver driver;

        public MenuItemControlLoggedOut(IWebDriver browser) : base(browser)
        {
            driver = browser;
        }

        public IWebElement BtnSignIn => driver.FindElement(By.XPath("//div[@class='panel header']//a[contains(text(), 'Sign In')]"));

        public LoginPage NavigateToLoginPage()
        {
            BtnSignIn.Click();

            return new LoginPage(driver);
        }
    }
}
