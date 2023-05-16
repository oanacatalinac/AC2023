using System.Collections.Generic;
using System.Linq;
using ACAutomation.Helpers;
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

        public By EmailAddressSelector => By.Id("customer-email");

        public IWebElement EmailAddressInput => driver.FindElement(EmailAddressSelector);

        public By FirstNameInputSelector => By.CssSelector("input[name='firstname']");

        public IWebElement FirstNameInput => driver.FindElement(FirstNameInputSelector);

        public By LastNameInputSelector => By.CssSelector("input[name = 'lastname']");

        public IWebElement LastNameInput => driver.FindElement(LastNameInputSelector);

        public By StreetAddressInputSelector => By.CssSelector("input[name = 'street[0]']");

        public IWebElement StreetAddressInput => driver.FindElement(StreetAddressInputSelector);

        public By CityInputSelector => By.CssSelector("input[name = 'city']");

        public IWebElement CityInput => driver.FindElement(CityInputSelector);

        public By StateDropdownSelector => By.CssSelector("select[name = 'region_id']");

        public IWebElement StateDropdown => driver.FindElement(StateDropdownSelector);

        public By ZipCodeInputSelector => By.CssSelector("input[name = 'postcode']");

        public IWebElement ZipCodeInput => driver.FindElement(ZipCodeInputSelector);

        public By TelephoneInputSelector => By.CssSelector("input[name = 'telephone']");

        public IWebElement TelephoneInput => driver.FindElement(TelephoneInputSelector);

        public By ShippingMethodsOptionsSelector => By.CssSelector("input[type= 'radio']");

        public IList<IWebElement> ShippingMethodsOptions => driver.FindElements(ShippingMethodsOptionsSelector);

        public By BtnNextSelector => By.CssSelector("button[class= 'button action continue primary']");

        public IWebElement BtnNext => driver.FindElement(BtnNextSelector);

        public PaymentMethodPage AddShippingAddress(ShippingAddressBO shippingAddress)
        {
            WaitHelpers.WaitForElementToBeClickable(driver, EmailAddressSelector);
            EmailAddressInput.SendKeys(shippingAddress.EmailAddress);

            WaitHelpers.WaitForElementToBeClickable(driver, FirstNameInputSelector);
            FirstNameInput.SendKeys(shippingAddress.FirstName);

            WaitHelpers.WaitForElementToBeClickable(driver, LastNameInputSelector);
            LastNameInput.SendKeys(shippingAddress.LastName);

            WaitHelpers.WaitForElementToBeClickable(driver, StreetAddressInputSelector);
            StreetAddressInput.SendKeys(shippingAddress.StreetAddress);

            WaitHelpers.WaitForElementToBeClickable(driver, CityInputSelector);
            CityInput.SendKeys(shippingAddress.City);

            //select from dropdown
            WaitHelpers.WaitForElementToBeClickable(driver, StateDropdownSelector);
            var selectState = new SelectElement(StateDropdown);
            selectState.SelectByText(shippingAddress.State);

            WaitHelpers.WaitForElementToBeClickable(driver, ZipCodeInputSelector);
            ZipCodeInput.SendKeys(shippingAddress.ZipCode);

            WaitHelpers.WaitForElementToBeClickable(driver, TelephoneInputSelector);
            TelephoneInput.SendKeys(shippingAddress.Telephone);

            //select radio button value
            WaitHelpers.WaitForElementToBeClickable(driver, ShippingMethodsOptionsSelector);
            ShippingMethodsOptions.ElementAt(shippingAddress.ShippingMethods).Click();

            WaitHelpers.WaitForElementToBeClickable(driver, BtnNextSelector);
            BtnNext.Click();

            return new PaymentMethodPage(driver);
        }
    }
}
