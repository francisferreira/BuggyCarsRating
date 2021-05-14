using BuggyCarsRating.Elements;
using BuggyCarsRating.Pages;
using OpenQA.Selenium;
using System.Configuration;
using TechTalk.SpecFlow;

namespace BuggyCarsRating.Tests
{
    [Binding]
    public sealed class RegisterUserSteps
    {
        private readonly RegisterPage page;
        private readonly string password;
        private string username;

        public RegisterUserSteps(IWebDriver driver)
        {
            page = new RegisterPage(driver);
            username = Hooks.Username.Replace('-', '#');
            password = "S@m3$4Me";
        }

        [Given(@"the registration form is filled out")]
        public void GivenTheRegistrationFormIsFilledOut()
        {
            page.SetRegisterInfo(username, ConfigurationManager.AppSettings["FirstName"], ConfigurationManager.AppSettings["LastName"], password, password);
        }

        [Given(@"the login name ""(.*)"" unique")]
        public void GivenTheLoginNameUnique(string condition)
        {
            if (condition == "is not")
            {
                username = Hooks.Username;
                page.SetRegisterInfo(loginName: username);
            }
        }

        [When(@"the ""(.*)"" field is cleared")]
        public void WhenTheFieldIsCleared(string field)
        {
            var property = typeof(RegisterPage).GetProperty(field.Replace(" ", "")).GetValue(page);
            ((InputText)property).SetText("");
        }

        [Then(@"login ""(.*)"" with those creds")]
        public void ThenLoginWithThoseCreds(string condition)
        {
            Utils.AssertLoginOutcome(username, password, condition);
        }
    }
}
