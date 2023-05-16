# AC2023

### **Session 1 - 27.03.2023**

We have discussed about automation:
  1. What it is
  2. Why it is important
  3. Types of automation testing

The presentation was sent via email after the session.

### **Session 2 - 03.04.2023 - Let's write our first UI automation test**

**Scope:** This session scope was to create a unit test project, install all the dependencies and write a simple test.

How to create a unit test project:
  1. Open Visual studio
  2. Click File > New > Project
  3. Search for unit test: Unit Test Project(.NET Framework)
  4. Add project Name
  5. Click "Create" button


At this moment, we have created a unit test project and should have a default test class: UnitTest1. 
The class has the following particularities: 
1. It has [TestClass] annotation that identifies the class to be a test one. Without this annotation, the test under this class cannot be recognized and there for cannot run the tests within it.
2. The class contains a test method that has a [TestMethod] annotation. This help the method to be recognized as an test method and run it accordingly.

Find more info on: https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022

Let's import the needed packages in order to open a chrome browser using our test. Do a right click on the created project and click Manage Nuget Packages.
On the opened window, select Browse tab and search for:

* **Selenium.Webdriver** 
* **Selenium.Webdriver.ChromeDriver**
* **Selenium.Support**

and install the latest version of each package in your project.


Be aware that if the local installed Chrome is not the same with the installed package, it will trigger a compatibility error when running the test.
  
**REMEMBER: THE TEST DOES WHAT YOU TELL IT TO DO.**
<br>
That's why, selenium manipulates the elements in DOM as a human would do. For now, we will use click and sendkeys.

Our first automated test case will try to login into application. For this, we will need to write code for the next steps:

1. Open the browser
2. Maximize the browser window
3. Navigate to the application URL
4. Click Sign in button from header
5. Fill user email
6. Fill user password
7. Click Sign in button 
8. Assert that the login was successful by checking user details displayed in the header

```csharp  
    var driver = new ChromeDriver(); //open chrome browser
    driver.Manage().Window.Maximize(); //maximize browser window
    driver.Navigate().GoToUrl("OUR URL"); //access the SUT(System Under Test) url. In our case https://magento.softwaretestingboard.com/
```

Until now, we have covered the first 3 steps of our test. Let's complete our test:

```csharp
    driver.FindElement(By.XPath("//div[@class='panel header'] //a[contains(text(), 'Sign In')]")).Click(); //click sign in button from header
    driver.FindElement(By.Id("email")).SendKeys("<email that was used for creating the account>"); //fill in user email
    driver.FindElement(By.Name("login[password]")).SendKeys("<password used to create the account>"); //fill in user password
    driver.FindElement(By.CssSelector("button[name='send']")).Click(); //click sign in button
    Thread.Sleep(2000); //sleep
    Assert.AreEqual("Welcome, <Firstname and Lastname that were used for creating the account>!",
                driver.FindElement(By.XPath("//div[@class='panel header']//li[@class='greet welcome']/span[@class='logged-in']")).Text); //assert
    driver.Quit(); //close the browser
```

Clarification :)
1.	WebElement represents a DOM element. WebElements can be found by searching from the document root using a WebDriver instance. WebDriver API provides built-in methods to find the WebElements which are based on different properties like ID, Name, Class, XPath, CSS Selectors, link Text, etc.
2.	There are some ways of optimizing our selectors used to identify the elements in page.
  a. For the CssSelector, the simplest way is to use the following format: tagname[attribute='attributeValue'].
  b. For the XPath, the simplest way is to use the following format: //tagname[@attribute='attributeValue'].

Let's take for example the Email input:
        
```html
    <input name="login[username]" value="" autocomplete="off" id="email" type="email" class="input-text" title="Email" data-validate="{required:true, 'validate-email':true}" aria-required="true" style="">
```

<br>                               

<input(this is the **tagname**)

* **type**(this is the attribute)="email"(this is the value) &rarr; The CssSelector would be: **input[type='email']** Or xpath &rarr; **//input[@type= 'email']**
* **name**(this is the attribute)=" login[username]"(this is the value) &rarr; The XPath would be: **//input[@name= 'login[username]']** or css selector &rarr; **input[name= 'login[username]']**
* **title** (this is the attribute)=’Email’ &rarr; The CSS selector would be: **input[title= 'Email']** or the xpath &rarr; **//input[@title= 'Email']**
* **class**(this is the attribute)=" input-text "(this is the value) &rarr; The XPath would be: **//input[@class='input-text']** or the css selector &rarr; **input[class='input-text']**


### **Session 3 - 10.04.2023 - Locators. TestInitialize. TestCleanup. Page Object Model**

**Scope:** This session scope was to build reliable web element locators and to use MSTest methods to initialize/clean up our test data and to get rid of our duplicate code.

Xpath - Most flexible in order to build reliable web element locators.

Absolute XPath (direct way, select the element from the root node) /

Relative XPath (anywhere at the webpage) //

	//input[@id="email"]

    //*[@name="login[username]"]

    //input[starts-with(@name, 'login')]

    //input[contains(@name, 'login')]

<br>

If we don't find anything unique we can use:

**AND**

    //input[contains(@name, 'login') and @type='email']
**OR**

    //input[contains(@name, 'login') or @type='email']


<br>
Using Xpath we can also make use of the family of an element:

For example this element **class='field email required '** has a big family:

Following - all following elements of the current node

    //div[@class='field email required']//following::div

Child - all children elements of the current node

    //div[@class='field email required']//child::div

Preceding - all nodes that come before the current node

    //div[@class='field email required']//preceding::fieldset

Following-sibling - following siblings of the context node

    //div[@class='field email required']//following-sibling::label

Parent - parent of the current node

    //div[@class='field email required']//parent::fieldset

Descendant - descendants of the current node

    //div[@class='field email required']//descendant::div

Try to use these elements in order if possible in order to consistently have good tests which will reduce brittleness and increase maintainability.. 


<br>

One of a test case component is the prerequisite: conditions that must be met before the test case can be run.
Our code is testing login scenarios and we need to see what are the prerequisites.
We have identified the following steps that need to be execute before running the test:


```csharp  
    var driver = new ChromeDriver(); //open chrome browser
    driver.Manage().Window.Maximize(); //maximize browser window
    driver.Navigate().GoToUrl("OUR URL"); //access the SUT(System Under Test) url. In our case https://magento.softwaretestingboard.com/
```

<br>

Also, after the test has finished running, we need to clean up the operations that we made in our test in order to not impact further test. Remember, each test is independent and should not influence the result of other tests. In our test, the clean up would mean to close the browser.

```csharp
    driver.Quit();
```

MSTest provides a way to declare methods to be called by the test runner before or after running a test.

```csharp
    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
```

The method decorated by [TestInitialize] is called before running each test of the class. The method decorated by [TestCleanup] is called after running each test of the class.

First, we need to remove the init/clean up steps and to move it the according method.
Then, we have to organize the elements in such way, if the login page layout needs to be changed, also the maintenance of the tests should not be very time consuming.

A better approach to script maintenance is to create a separate class file which would find web elements, fill them or verify them. This class can be reused in all the scripts using that element. In future, if there is a change in the web element, we need to make the change in just 1 class file and not 10 different scripts.

This approach is called Page Object Model (POM). It helps make the code more readable, maintainable, and reusable.

Page Object model is an object design pattern in Selenium, where web pages are represented as classes, and the various elements on the page are defined as variables on the class. All possible user interactions can then be implemented as methods on the class.

Right click on the project > Add > Folder and name it PageObjects

Right click on the PageObjects folder > Add > New Item... > Add a class with name: LoginPage.cs

We need to add the objects that we use in our script in this class: email input, password input, sign in button and create a method to login the user.

Our LoginPge.cs will look like this in the end:

```csharp
public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement EmailInput()
        {
            return driver.FindElement(By.Id("email"));
        }

        private IWebElement PasswordInput()
        {
            return driver.FindElement(By.Name("login[password]"));
        }

        private IWebElement SignInButton()
        {
            return driver.FindElement(By.CssSelector("button[name='send']"));
        }

        public void SignInTheApplication(string email, string password)
        {
            EmailInput().SendKeys(email);
            PasswordInput().SendKeys(password);
            SignInButton().Click();
        }
    }
```
### **Session 4 - 24.04.2023 - MenuItemControl. Adding a product to cart**

**Scope:** This session's scope was to change the methods for declaring the elements from LoginPage using LambdaExpression, to handle the page menu and to create new test classes for adding a product to cart without being logged in.

Let's start with the refactoring:

**LoginPage.cs**:

Change the methods for declaring the elements using Lambda expression.

At this point, the changes look like this:

```csharp
    private IWebElement EmailInput => driver.FindElement(By.Id("email")); 

    private IWebElement PasswordInput => driver.FindElement(By.Name("login[password]")); 

    private IWebElement SignInButton => driver.FindElement(By.CssSelector("button[name='send']")); 

    public void SignInTheApplication(string email, string password)
    {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            SignInButton.Click();
    }
```

Now, we will continue working with the menu. It is present in all the app pages, and we need to create a single base class where the menu elements can be stored. This is a shared component and we need to call it in all of our page objects. The first step is to create a new „Shared” folder and, after that, a class named MenuItemControl.cs. This class will contain all menu elements.

```csharp

     public class MenuItemControl
     {
        private IWebDriver driver;

        public MenuItemControl(IWebDriver browser)
        {
            driver = browser;
        }
     }
```

The application has 2 contexts, but this menu cannot be used from both perspectives: logged out and logged in. Let's identify the elements used in these contexts:

 1. logged out: Sign in, Create an Account
 2. logged in: Sign out, My Account, My Wish List

For the moment we will create another class that will handle the context when a user is logged in: MenuItemControlLoggedOut that will inherit the MenuItemControlClass.

```csharp

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

```

We will continue by making the changes in the LoginPage.cs and LoginTests.cs:

```csharp
    public class LoginPage
    {
        private IWebDriver driver;

        public MenuItemControlLoggedOut menuItemControlLoggedOut => new MenuItemControlLoggedOut(driver);

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        ....
    }

```

```csharp
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
```

**Next we will move to the main scope of the session which is to write a test that will add a product to cart and place order while user is not logged in**

In order to do this, we will need to write code for the next steps:

  1. Open the browser
  2. Maximize the window
  3. Navigate to the application URL
  4. Hover over the Watches option from menu and choose Gear option from the displayed list
  5. Choose the first product from the page (the first watch)
  6. Add the product to the cart
  7. Go to the shopping cart
  8. Choose 'Proceed to checkout'
  9. Complete the mandatory fields for delivery address
  10. Place the order 
  11. Check the message *Thank you for your purchase!* is displayed


In order to navigate through all the pages we will consider a base class HomePage.cs that will be the context of the user after he enters in the application:


```csharp

    public class HomePage
    {
        private IWebDriver driver;

        //reference the menu item control
        public MenuItemControl menuItemControl => new MenuItemControl(driver);

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }
    }

```

After executing **Step 4** user is redirected to a page where all watches are displayed. For this we need to create another page object **WatchesPage.cs**:

```csharp

    public class WatchesPage
    {
        private IWebDriver driver;

        public WatchesPage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```

Having the previous page object created we can update the MenuItemControl with the necessary elements declaration and method to hover over Gear option menu and click on Watches option from **Step 4**:

```csharp

    public class MenuItemControl
    {
        private IWebDriver driver;

        public MenuItemControl(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement MenuGearOption => driver.FindElement(By.XPath("//div[@id='store.menu']//span[text()='Gear']"));

        public IWebElement MenuWatchesOption => driver.FindElement(By.XPath("//div[@id='store.menu']//span[text()='Gear']/../following-sibling::ul[@role='menu']//span[text()='Watches']"));

        public WatchesPage NavigateToWatchesPage()
        {
            Thread.Sleep(2000);
            // hover on menu > gear element
            new Actions(driver).MoveToElement(MenuGearOption).Perform();
            MenuWatchesOption.Click();

            return new WatchesPage(driver);
        }
    }

```

After executing **Step 5** user is redirected to a page where details for the chosen watch are displayed. For this we need to create another page object **WatcheDetailsPage.cs**:

```csharp

    public class WatchDetailsPage
    {
        private IWebDriver driver;

        public WatchDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```

Having the previous page object created we can update the WatchesPage.cs with the necessary elements declaration and method to click on the first watch from the list accordingly with **Step 5**:

```csharp

    public class WatchesPage
    {
        private IWebDriver driver;

        public WatchesPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IList<IWebElement> WatchesProductsList => driver.FindElements(By.XPath("//li[contains(@class, 'product-item')]"));

        public WatchDetailsPage NavigateToFirstWatchProduct()
        {
            WatchesProductsList.First().Click();

            return new WatchDetailsPage(driver);
        }
    }

```

To complete **Step 6** we need to update WatchDetailsPage.cs with the 'Add to cart' button declaration and method to click on the element:

```csharp

    public class WatchDetailsPage
    {
        private IWebDriver driver;

        public WatchDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnAddToCart => driver.FindElement(By.Id("product-addtocart-button"));

        public WatchDetailsPage AddProductToCart()
        {
            BtnAddToCart.Click();

            return this;
        }
    }

```

Next, to complete **Step 7** we need to create the page object for the shopping cart page ShoppingCartPage.cs:

```csharp

    public class ShoppingCartPage
    {
        private IWebDriver driver;

        public ShoppingCartPage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```

Having ShoppingCartPage.cs we can update WatchDetailsPage.cs with the button declaration and method to click on the shopping cart link:

```csharp

    public class WatchDetailsPage
    {
        private IWebDriver driver;

        public WatchDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnAddToCart => driver.FindElement(By.Id("product-addtocart-button"));

        public WatchDetailsPage AddProductToCart()
        {
            BtnAddToCart.Click();

            return this;
        }

        public IWebElement ShoppingCartLink => driver.FindElement(By.LinkText("shopping cart"));

        public ShoppingCartPage GoToShoppingCart()
        {
            Thread.Sleep(2000);
            ShoppingCartLink.Click();

            return new ShoppingCartPage(driver);
        }
    }

```

In order to complete **Step 8** we need to create the page object of the page we will be redirected after clicking on 'Proceed to checkout' button ShippingAddressPage.cs:

```csharp

     public class ShippingAddressPage
     {
        private IWebDriver driver;

        public ShippingAddressPage(IWebDriver browser)
        {
            driver = browser;
        }

     }

```

Having the next page object we will be redirected after executing step 8, we can complete ShoppingCartPage.cs with the button declaration and method to click on the 'Proceed to checkout' element:

```csharp

    public class ShoppingCartPage
    {
        private IWebDriver driver;

        public ShoppingCartPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnProceedToCheckout => driver.FindElement(By.XPath("//button[@title='Proceed to Checkout']/span"));

        public ShippingAddressPage ProceedToCheckoutPage()
        {
            BtnProceedToCheckout.Click();

            return new ShippingAddressPage(driver);
        }
    }

```
### **Session 5 - 08.05.2023 - BusinessObject. Placing an order**

**Scope**: This session's scope is to continue with the test we detailed in Session 4.

We will continue with step 9. In order for **Step 9** to be completed, we need to declare all the mandatory fields elements including the Next button:

```csharp

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
    }

```

Before clicking on Next button and reaching to the **Step 10** we will create the page object for the 2nd step of placing an order PaymentMethodPage.cs:

```csharp

    public class PaymentMethodPage
    {
        private IWebDriver driver;

        public PaymentMethodPage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```
**Parametrize AddShippingAddress method in an efficient way**

To parametrize the details for adding a shipping address in an efficient way, we can create a business object class called ShippingAddressBO.cs which will contain the objects needed in the process of adding a shipping address. We will create this class in a new solution folder named InputDataBO.

```csharp

    public class ShippingAddressBO
    {
        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Telephone { get; set; }

        public int ShippingMethods { get; set; }
    }

```

After this we can complete ShippingAddressPage.cs with the method that will fill declared elements with data from the business object and with clicking on the Next button:

```csharp

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

```

In order to get to the correct page object specific for **Step 11** we will create PlacedOrderPage.cs:

```csharp

   public class PlacedOrderPage
    {
        public IWebDriver driver;

        public PlacedOrderPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement PageTitle => driver.FindElement(By.XPath("//h1[@class='page-title']/span"));
    }

```

Now we can fully complete **Step 10** by clicking on the 'Place order' button in PaymentMethodPage.cs:

```csharp

    public class PaymentMethodPage
    {
        private IWebDriver driver;

        public PaymentMethodPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnPlaceOrder => driver.FindElement(By.CssSelector("button[title='Place Order']"));

        public PlacedOrderPage PlaceOrder()
        {
            Thread.Sleep(5000);
            BtnPlaceOrder.Click();

            return new PlacedOrderPage(driver);
        }
    }
```

We completed adding all page objects necessary for the test we want to perform so now we can create AddToCartTests.cs that will call the declared methods for navigating through steps:

```csharp

[TestClass]
    public class AddToCartTests
    {
        private IWebDriver driver;
        private HomePage homePage;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            homePage = new HomePage(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
        }

        [TestMethod]
        public void Should_AddToCartAndPlaceOrder_When_UserIsNotLoggedIn()
        {
            var addressData = new ShippingAddressBO
            {
                EmailAddress = "email@email.ro",
                FirstName = "John",
                LastName = "Doe",
                StreetAddress = "AC address1",
                City = "Iasi",
                State = "Hawaii",
                ZipCode = "12345",
                Telephone = "1234567890",
                ShippingMethods = 1
            };

            var navigatePage = homePage.menuItemControl.NavigateToWatchesPage()
                .NavigateToFirstWatchProduct()
                .AddProductToCart()
                .GoToShoppingCart()
                .ProceedToCheckoutPage()
                .AddShippingAddress(addressData)
                .PlaceOrder();

            Thread.Sleep(2000);
            Assert.AreEqual("Thank you for your purchase!", navigatePage.PageTitle.Text);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
```

As you can observe the business object was instantiated with data in the test using an efficient parametrization.

At the end an assert was added to check the correct message is displayed after placing an order.

### **Session 6 - 15.05.2023 - Wait methods**

**Scope**: This session's scope is to add wait methods and replace Thread.Sleep from methods and tests.

*Wait strategy*

There are explicit and implicit waits in Selenium Web Driver. Waiting is having the automated task execution elapse a certain amount of time before continuing with the next step.

You should choose to use Explicit or Implicit Waits.

**• Thread.Sleep**

In particular, this pattern of sleep is an example of explicit waits. So this isn’t actually a feature of Selenium WebDriver, it’s common in most programming languages.

Thread.Sleep() does exactly what you think it does, it sleeps the thread.

Example:

```
        Thread.Sleep(2000);
```

Warning! Using Thread.Sleep() can leave to random failures (server is sometimes slow), you don't have full control of the test and the test could take longer than it should. It is a good practice to use other types of waits.

**• Implicit Wait**

WebDriver will poll the DOM for a certain amount of time when trying to find an element or elements if they are not immediately available

Example:
```
         driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
```

**• Explicit Wait** - Wait for a certain condition to occur before proceeding further in the code

In practice, we recommend that you use Web Driver Wait in combination with methods of the Expected Conditions class that reduce the wait. If the element appeared earlier than the time specified during Web Driver wait initialization, Selenium will not wait but will continue the test execution.

Example 1:

``` 
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        wait.Until(ExpectedConditions.ElementIsVisible(firstName));

```

Example 2:

```
        public By ShoppingCartSelector => By.LinkText("shopping cart");

        public IWebElement ShoppingCartLink => driver.FindElement(ShoppingCartSelector);

        public void GoToShoppingCart(IWebDriver browser)
        {
            driver = browser;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(FirstName));
        }
```

In order to implement explicit waits in our solution we add in a shared folder named Helpers the class WaitHelpers.cs that treats multiple wait conditions:

```

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

```

We update the selectors in MenuItemControl.cs and use the wait methods specific for the menu elements:

```
        public By MenuGearSelector => By.XPath("//div[@id='store.menu']//span[text()='Gear']");

        public IWebElement MenuGearOption => driver.FindElement(MenuGearSelector);

        public By MenuWatchesSelector => By.XPath("//div[@id='store.menu']//span[text()='Gear']/../following-sibling::ul[@role='menu']//span[text()='Watches']");

        public IWebElement MenuWatchesOption => driver.FindElement(MenuWatchesSelector);

        public WatchesPage NavigateToWatchesPage()
        {
            WaitHelpers.WaitForElementToBeVisible(driver, MenuGearSelector);
            WaitHelpers.WaitForElementToBePresent(driver, MenuWatchesSelector);

            // hover on menu > gear element
            new Actions(driver).MoveToElement(MenuGearOption).Perform();
            MenuWatchesOption.Click();

            return new WatchesPage(driver);
        }
```

After this we update selectors and add wait methods in WatchDetailsPage.cs:

```

        public By ShoppingCartSelector => By.LinkText("shopping cart");

        public IWebElement ShoppingCartLink => driver.FindElement(ShoppingCartSelector);

        public ShoppingCartPage GoToShoppingCart()
        {
            WaitHelpers.WaitForElementToBeClickable(driver, ShoppingCartSelector);
            ShoppingCartLink.Click();

            return new ShoppingCartPage(driver);
        }
```

We update selectors and add wait methods for ShippingAddressPage.cs:

```
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
```

After this we update the selectors and add wait methods in PaymentMethodPage.cs:

```
        public By LoaderSelector => By.XPath("//div[@class='loader']");

        public By BtnPlaceOrderSelector => By.CssSelector("button[title='Place Order']");

        public IWebElement BtnPlaceOrder => driver.FindElement(BtnPlaceOrderSelector);

        public PlacedOrderPage PlaceOrder()
        {
            WaitHelpers.WaitForElementToNotBeDisplayed(driver, LoaderSelector);
            WaitHelpers.WaitForElementToBeClickable(driver, BtnPlaceOrderSelector);
            BtnPlaceOrder.Click();

            return new PlacedOrderPage(driver);
        }
```

We update PlacedOrderPage.cs and add wait methods in PlacedOrderPage.cs:

```
        public By PageTitleSelector => By.XPath("//h1[@class='page-title']/span[.='Thank you for your purchase!']");

        public IWebElement PageTitle => driver.FindElement(PageTitleSelector);

        public void WaitForElement()
        {
            WaitHelpers.WaitForElementToBePresent(driver, PageTitleSelector);
        }
```

We uppdate the assert section in the AddToCartTests.cs:

```
        [TestMethod]
        public void Should_AddToCartAndPlaceOrder_When_UserIsNotLoggedIn()
        {
            ...

            navigatePage.WaitForElement();
            Assert.AreEqual("Thank you for your purchase!", navigatePage.PageTitle.Text);
        }
```

We add MenuControlLoggedIn.cs class to contain those specific elements for a logged in user in the Shared folder. We move the initial selector from the first test from LoginTests.cs in this class and use wait methods along with a try catch to handle the StaleElement exception that Selenium throws in specific circumstances.

```

    public class MenuItemControlLoggedIn : MenuItemControl
    {
        public IWebDriver driver;

        public MenuItemControlLoggedIn(IWebDriver browser):base(browser)
        {
            driver = browser;
        }

        public By WelcomeUserSelector => By.XPath("//div[@class='panel header']//li[@class='greet welcome']/span[@class='logged-in']");

        public IWebElement WelcomeUserLabel => driver.FindElement(WelcomeUserSelector);

        public void WaitForElement()
        {
            try
            {
                WaitHelpers.WaitForElementToBeVisibleCustom(driver, WelcomeUserSelector);
            }
            catch (StaleElementReferenceException)
            {
                WaitHelpers.WaitForElementToBeVisibleCustom(driver, WelcomeUserSelector);
            }        
        }
    }

```

We update HomePage.cs with the reference for MenuControlLoggedIn.cs:

```
    public class HomePage
    {
        private IWebDriver driver;

        //reference the menu item control
        public MenuItemControl menuItemControl => new MenuItemControl(driver);

        public MenuItemControlLoggedIn menuItemControlLoggedIn => new MenuItemControlLoggedIn(driver);

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }
    }
```

For the selector that was mentioned in the second test from LoginTests.cs we move it in LoginPage.cs and use the right wait method for it:

```
    public class LoginPage
    {
        private By FailedLoginSelector => By.XPath("//div[@role = 'alert']/div/div");

        public IWebElement FailedLoginLabel => driver.FindElement(FailedLoginSelector);

        public void WaitForElement()
        {
            WaitHelpers.WaitForElementToBeVisibleCustom(driver, FailedLoginSelector);
        }
     }
```

We refactor the two tests from LoginTests.cs:

```
        ... 

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

        ...
```

### **References**

Getting started:

 • https://www.automatetheplanet.com/getting-started-webdriver/
 • official documentation: https://www.selenium.dev/documentation/en/

Page object model

  • https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/

Waits:

  • https://www.toolsqa.com/selenium-webdriver/c-sharp/advance-explicit-webdriver-waits-in-c/