using BuggyCarsRating.Elements;
using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BuggyCarsRating.Tests
{
    [Binding]
    public sealed class EditProfileSteps
    {
        private readonly ProfilePage page;
        private string age;

        public EditProfileSteps(IWebDriver driver)
        {
            page = new ProfilePage(driver);
        }

        [Given(@"the user password is changed")]
        public void GivenTheUserPasswordIsChanged()
        {
            page.SetPasswordInfo(ConfigurationManager.AppSettings["OldPassword"],
                                 ConfigurationManager.AppSettings["NewPassword"],
                                 ConfigurationManager.AppSettings["NewPassword"]);
        }

        [When(@"the ""(.*)"" field is deleted")]
        public void WhenTheFieldIsDeleted(string name)
        {
            var property = typeof(ProfilePage).GetProperty(name.Replace(" ", "")).GetValue(page);
            ((InputText)property).SetText("");
        }

        [When(@"""(.*)"" is entered for Age")]
        public void WhenIsEnteredForAge(string text)
        {
            age = page.Age.GetText();
            page.Age.SetText(text);
        }

        [When(@"the additional info is edited")]
        public void WhenTheAdditionalInfoIsEdited()
        {
            page.SetAdditionalInfo(ConfigurationManager.AppSettings["InfoGender"],
                                   ConfigurationManager.AppSettings["InfoYears"],
                                   ConfigurationManager.AppSettings["InfoAddress"],
                                   ConfigurationManager.AppSettings["InfoPhone"],
                                   ConfigurationManager.AppSettings["InfoHobby"]);
        }

        [When(@"the basic info is edited as follows")]
        public void WhenTheBasicInfoIsEditedAsFollows(Table table)
        {
            var (firstname, lastname) = table.CreateInstance<(string firstname, string lastname)>();
            page.SetBasicInfo(firstname, lastname);
        }

        [Then(@"the ""(.*)"" remains unchanged")]
        public void ThenTheRemainsUnchanged(string type)
        {
            Utils.NavigateAwayAndBack(page.Header.HomeLink, page.Header.Profile);

            switch (type)
            {
                case "age info":
                    Assert.AreEqual(age, page.Age.GetText(), "Age doesn't match previous data"); break;
                case "basic info":
                    Assert.AreEqual(ConfigurationManager.AppSettings["FirstName"], page.FirstName.GetText(), "First Name doesn't match previous data");
                    Assert.AreEqual(ConfigurationManager.AppSettings["LastName"], page.LastName.GetText(), "Last Name doesn't match previous data"); break;
                default:
                    throw new InvalidOperationException($"Invalid regex match in SpecFlow step: {type} != [age info|basic info]");
            }
        }

        [Then(@"login with the ""(.*)"" password ""(.*)""")]
        public void ThenLoginWithThePassword(string prefix, string condition)
        {
            string password;
            switch (prefix)
            {
                case "new":
                    password = ConfigurationManager.AppSettings["NewPassword"]; break;
                case "old":
                    password = ConfigurationManager.AppSettings["OldPassword"]; break;
                default:
                    throw new InvalidOperationException($"Invalid regex match in SpecFlow step: {prefix} != [new|old]");
            }
            Utils.AssertLoginOutcome(Hooks.Username, password, condition);
        }

        [Then(@"the edited additional info is persisted")]
        public void ThenTheEditedAdditionalInfoIsPersisted()
        {
            Utils.NavigateAwayAndBack(page.Header.HomeLink, page.Header.Profile);
            Assert.AreEqual(ConfigurationManager.AppSettings["InfoGender"], page.Gender.GetText(), "Gender doesn't match");
            Assert.AreEqual(ConfigurationManager.AppSettings["InfoYears"], page.Age.GetText(), "Age doesn't match");
            Assert.AreEqual(ConfigurationManager.AppSettings["InfoAddress"], page.Address.GetText(), "Address doesn't match");
            Assert.AreEqual(ConfigurationManager.AppSettings["InfoPhone"], page.Phone.GetText(), "Phone doesn't match");
            Assert.AreEqual(ConfigurationManager.AppSettings["InfoHobby"], page.Hobby.GetSelection(), "Hobby doesn't match");
        }
    }
}
