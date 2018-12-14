using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BogusHomework
{
    [TestFixture]
    class TestSuiteBogus
    {

        public IWebDriver driver;

        //Header and main page locators
        public By registerButtonHeader = By.LinkText("Register");
        public By loginButtonHeader = By.LinkText("Log in");
        public By logOffButtonHeader = By.LinkText("Log off");
        public By loginButton = By.XPath("//input[@value='Log in']");

        //Registration locators and hello button
        public By emailField = By.Id("Email");
        public By nameField = By.Id("Name");
        public By surnameField = By.Id("Surname");
        public By companyField = By.Id("Company");
        public By passwordField = By.Id("Password");
        public By confirmPasswordField = By.Id("ConfirmPassword");
        public By registerButton = By.XPath("//input[@value='Register']");
        public By helloButton = By.XPath("//form[@id='logoutForm']//li/a[contains(text(),'Hello')]");

        //Profile information page
        public By idLabel = By.Id("userId");
        public By nameLabel = By.Id("userName");
        public By surnameLabel = By.Id("userSurname");
        public By companyLabel = By.Id("UserCompany");
        public By emailLabel = By.Id("userEmail");

        

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void TestRegisterLogin()
        {
            driver.Navigate().GoToUrl("https://homeworkdecoration20181213051012.azurewebsites.net/");
            driver.FindElement(registerButtonHeader).Click();

            //Create user and fill the fields
            var user1 = new User();
            user1.GetRandomDetails();
            driver.FindElement(emailField).SendKeys(user1.email);
            driver.FindElement(nameField).SendKeys(user1.name);
            driver.FindElement(surnameField).SendKeys(user1.surname);
            driver.FindElement(companyField).SendKeys(user1.company);
            driver.FindElement(passwordField).SendKeys(user1.password);
            driver.FindElement(confirmPasswordField).SendKeys(user1.password);
            driver.FindElement(registerButton).Click();

            //Check if the Hello button text is valid
            var helloButtonElement = driver.FindElement(helloButton);
            helloButtonElement.Text.Should().Be("Hello " + user1.email + "!");

            //Save id given after registration
            helloButtonElement.Click();
            string idAfterRegistration = driver.FindElement(idLabel).Text;

            //Check that data is the same as we entered during registration process
            driver.FindElement(nameLabel).Text.Should().Be(user1.name);
            driver.FindElement(surnameLabel).Text.Should().Be(user1.surname);
            driver.FindElement(companyLabel).Text.Should().Be(user1.company);
            driver.FindElement(emailLabel).Text.Should().Be(user1.email);

            //Logout and login
            driver.FindElement(logOffButtonHeader).Click();

            driver.FindElement(loginButtonHeader).Click();
            driver.FindElement(emailField).SendKeys(user1.email);
            driver.FindElement(passwordField).SendKeys(user1.password);
            driver.FindElement(loginButton).Click();

            //Check that the Hello button text is valid after login
            helloButtonElement = driver.FindElement(helloButton);
            helloButtonElement.Text.Should().Be("Hello " + user1.email + "!");

            helloButtonElement.Click();

            //Check that id is the same as after registration
            driver.FindElement(idLabel).Text.Should().Be(idAfterRegistration);

            //Check that the same user data are displayed on page
            driver.FindElement(nameLabel).Text.Should().Be(user1.name);
            driver.FindElement(surnameLabel).Text.Should().Be(user1.surname);
            driver.FindElement(companyLabel).Text.Should().Be(user1.company);
            driver.FindElement(emailLabel).Text.Should().Be(user1.email);

        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

    }
}
