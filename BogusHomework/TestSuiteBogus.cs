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
            var expectedUser = User.GenerateUser();            

            driver.FindElement(emailField).SendKeys(expectedUser.email);
            driver.FindElement(nameField).SendKeys(expectedUser.name);
            driver.FindElement(surnameField).SendKeys(expectedUser.surname);
            driver.FindElement(companyField).SendKeys(expectedUser.company);
            driver.FindElement(passwordField).SendKeys(expectedUser.password);
            driver.FindElement(confirmPasswordField).SendKeys(expectedUser.password);
            driver.FindElement(registerButton).Click();

            //Check if the Hello button text is valid
            var helloButtonElement = driver.FindElement(helloButton);
            helloButtonElement.Text.Should().Contain(expectedUser.email);

            //Save id given after registration
            helloButtonElement.Click();

            var actualUser = new User()
            {
                name = driver.FindElement(nameLabel).Text,
                surname = driver.FindElement(surnameLabel).Text,
                company = driver.FindElement(companyLabel).Text,
                email = driver.FindElement(emailLabel).Text
            };

            //Check that data is the same as we entered during registration process
            actualUser.Should().BeEquivalentTo(expectedUser, options => options.Excluding(u => u.password).Excluding(u => u.id));

            //Logout and login
            driver.FindElement(logOffButtonHeader).Click();

            driver.FindElement(loginButtonHeader).Click();
            driver.FindElement(emailField).SendKeys(expectedUser.email);
            driver.FindElement(passwordField).SendKeys(expectedUser.password);
            driver.FindElement(loginButton).Click();

            //Check that the Hello button text is valid after login
            helloButtonElement = driver.FindElement(helloButton);
            helloButtonElement.Text.Should().Contain(expectedUser.email);

            helloButtonElement.Click();

            actualUser = new User()
            {
                name = driver.FindElement(nameLabel).Text,
                surname = driver.FindElement(surnameLabel).Text,
                company = driver.FindElement(companyLabel).Text,
                email = driver.FindElement(emailLabel).Text
            };

            //Check that the same user data are displayed on page
            actualUser.Should().BeEquivalentTo(expectedUser, options => options.Excluding(u => u.password).Excluding(u => u.id));

        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

    }
}
