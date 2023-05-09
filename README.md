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
