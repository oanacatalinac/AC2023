using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ACAutomation.PageObjects.InputDataBO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ACAutomation.PageObjects
{
    public class ShippingAddressPage
    {
        private IWebDriver driver;

        public ShippingAddressPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement EmailAddressInput => driver.FindElement(By.Id("customer-email"));

        public IWebElement FirstNameInput => driver.FindElement(By.CssSelector("input[name = 'firstname']"));

        public IWebElement LastNameInput => driver.FindElement(By.CssSelector("input[name = 'lastname']"));

        public IWebElement StreetAddressInput => driver.FindElement(By.CssSelector("input[name = 'street[0]']"));

        public IWebElement CityInput => driver.FindElement(By.CssSelector("input[name = 'city']"));

        public IWebElement StateDropdown => driver.FindElement(By.CssSelector("select[name = 'region_id']"));

        public IWebElement ZipCodeInput => driver.FindElement(By.CssSelector("input[name = 'postcode']"));

        public IWebElement TelephoneInput => driver.FindElement(By.CssSelector("input[name = 'telephone']"));

        public IList<IWebElement> ShippingMethodsOptions => driver.FindElements(By.CssSelector("input[type= 'radio']"));

        public IWebElement BtnNext
            => driver.FindElement(By.CssSelector("button[class= 'button action continue primary']"));

        public PaymentMethodPage AddShippingAddress(ShippingAddressBO shippingAddress)
        {
            Thread.Sleep(5000);
            EmailAddressInput.SendKeys(shippingAddress.EmailAddress);
            FirstNameInput.SendKeys(shippingAddress.FirstName);
            LastNameInput.SendKeys(shippingAddress.LastName);
            StreetAddressInput.SendKeys(shippingAddress.StreetAddress);
            CityInput.SendKeys(shippingAddress.City);

            //select from dropdown
            var selectState = new SelectElement(StateDropdown);
            selectState.SelectByText(shippingAddress.State);

            ZipCodeInput.SendKeys(shippingAddress.ZipCode);
            TelephoneInput.SendKeys(shippingAddress.Telephone);

            //select radio button value
            ShippingMethodsOptions.ElementAt(shippingAddress.ShippingMethods).Click();

            BtnNext.Click();

            return new PaymentMethodPage(driver);
        }
    }
}
