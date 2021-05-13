using BuggyCarsRating.Elements;
using BuggyCarsRating.Pages;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace BuggyCarsRating.Tests
{
    [Binding]
    public sealed class RegisterUserSteps
    {
        private readonly RegisterPage page;
        private string username;

        public RegisterUserSteps(IWebDriver driver)
        {
            page = new RegisterPage(driver);
            username = ConfigurationManager.AppSettings["Username"];
        }

        [Given(@"the fields are filled as per app config")]
        public void GivenTheFieldsAreFilledAsPerAppConfig()
        {
            page.SetRegisterInfo(ConfigurationManager.AppSettings["Username"],
                                 ConfigurationManager.AppSettings["FirstName"],
                                 ConfigurationManager.AppSettings["LastName"],
                                 ConfigurationManager.AppSettings["Password"],
                                 ConfigurationManager.AppSettings["Password"]);
        }

        [Given(@"the unique identifier is appended to login name")]
        public void GivenTheUniqueIdentifierIsAppendedToLoginName()
        {
            username += "-" + Guid.NewGuid().ToString("N");
            page.SetRegisterInfo(loginName: username);
        }

        [When(@"the ""(.*)"" field is cleared")]
        public void WhenTheFieldIsCleared(string name)
        {
            var property = typeof(RegisterPage).GetProperty(name.Replace(" ", "")).GetValue(page);
            ((InputText)property).SetText("");
        }

        [Then(@"login ""(.*)"" be performed as expected")]
        public void ThenLoginBePerformedAsExpected(string condition)
        {
            Utils.AssertLoginsOutcome(page, username, ConfigurationManager.AppSettings["Password"], condition);
        }
    }
}
