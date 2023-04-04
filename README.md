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
