using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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

        [When(@"logging in as ""(.*)"" with ""(.*)""")]
        public void WhenLoggingInAsWith(string username, string password)
        {
            page.Login(username, password);
        }

        [Then(@"""(.*)"" is displayed")]
        public void ThenIsShown(string error)
        {
            Assert.AreEqual(error, page.Header.Greeting, $"The expected error message was not displayed: {error}");
        }

        [Then(@"the expected greeting is displayed")]
        public void ThenTheExpectedGreetingIsDisplayed()
        {
            var firstname = ConfigurationManager.AppSettings["FirstName"];
            Assert.IsTrue(page.IsLogged(firstname), "Login was not performed successfully");
        }

        [Then(@"the login button is displayed")]
        public void ThenTheLoginButtonIsDisplayed()
        {
            Assert.IsTrue(page.Header.Login.Displayed, "Logout was not performed successfully");
        }
    }
}
