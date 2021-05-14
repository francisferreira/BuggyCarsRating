using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace BuggyCarsRating.Tests
{
    [Binding]
    public sealed class LoginLogoutSteps
    {
        private readonly HomePage page;

        public LoginLogoutSteps(IWebDriver driver)
        {
            page = new HomePage(driver);
        }

        [When(@"login is done with wrong ""(.*)""")]
        public void WhenLoginIsDoneWithWrong(string field)
        {
            var username = Hooks.Username;
            var password = Hooks.Password;
            switch (field)
            {
                case "Username":
                    username = "@" + username; break;
                case "Password":
                    password = "@" + password; break;
                default:
                    throw new InvalidOperationException($"Invalid regex match in SpecFlow step: {field} != [Username|Password]");
            }
            page.Login(username, password);
        }

        [Then(@"""(.*)"" is displayed")]
        public void ThenIsDisplayed(string error)
        {
            Assert.AreEqual(error, page.Header.Greeting, $"The expected error message was not displayed: {error}");
        }

        [Then(@"the login button is displayed")]
        public void ThenTheLoginButtonIsDisplayed()
        {
            Assert.IsTrue(page.Header.Login.Displayed, "Logout was not performed successfully");
        }

        [Then(@"the expected greeting is displayed")]
        public void ThenTheExpectedGreetingIsDisplayed()
        {
            var firstname = ConfigurationManager.AppSettings["FirstName"];
            Assert.IsTrue(page.IsLogged(firstname), "Login was not performed successfully");
        }
    }
}
