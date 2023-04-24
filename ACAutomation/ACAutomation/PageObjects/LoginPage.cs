using ACAutomation.Shared;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        public MenuItemControlLoggedOut menuItemControlLoggedOut => new MenuItemControlLoggedOut(driver);

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement EmailInput => driver.FindElement(By.Id("email"));

        private IWebElement PasswordInput => driver.FindElement(By.Name("login[password]"));


        private IWebElement SignInButton => driver.FindElement(By.CssSelector("button[name='send']"));

        public void SignInTheApplication(string email, string password)
        {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            SignInButton.Click();
        }
    }
}
