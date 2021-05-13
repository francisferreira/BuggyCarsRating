using BuggyCarsRating.Elements;
using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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

        [Given(@"the current age is recorded for reference")]
        public void GivenTheCurrentAgeIsRecordedForReference()
        {
            age = page.Age.GetText();
        }

        [Given(@"password is changed from ""(.*)"" to ""(.*)""")]
        public void GivenPasswordIsChangedFromTo(string oldPwd, string newPwd)
        {
            page.SetPasswordInfo(oldPwd, newPwd, newPwd);
        }

        [When(@"the ""(.*)"" field is deleted")]
        public void WhenTheFieldIsDeleted(string name)
        {
            var property = typeof(ProfilePage).GetProperty(name.Replace(" ", "")).GetValue(page);
            ((InputText)property).SetText("");
        }

        [When(@"the text ""(.*)"" is entered for Age")]
        public void WhenTheTextAreEnteredForAge(string text)
        {
            page.Age.SetText(text);
        }

        [When(@"the additional info is changed as per app config")]
        public void WhenTheAdditionalInfoIsChangedAsPerAppConfig()
        {
            page.SetAdditionalInfo(ConfigurationManager.AppSettings["Gender"],
                                   ConfigurationManager.AppSettings["Age"],
                                   ConfigurationManager.AppSettings["Address"],
                                   ConfigurationManager.AppSettings["Phone"],
                                   ConfigurationManager.AppSettings["Hobby"]);
        }

        [When(@"the basic info is edited as follows")]
        public void WhenTheBasicInfoIsEditedAsFollows(Table table)
        {
            var (firstname, lastname) = table.CreateInstance<(string firstname, string lastname)>();
            page.SetBasicInfo(firstname, lastname);
        }

        [Then(@"the age info remains unchanged")]
        public void ThenTheAgeInfoRemainsUnchanged()
        {
            Utils.NavigateAwayAndBack(page.Header.HomeLink, page.Header.Profile, By.XPath("//header//a[text()='Profile']"), By.Id("age"));
            Assert.AreEqual(age, page.Age.GetText(), "Age does not match the previous data");
        }

        [Then(@"the new additional info is persisted")]
        public void ThenTheNewAdditionalInfoIsPersisted()
        {
            Utils.NavigateAwayAndBack(page.Header.HomeLink, page.Header.Profile, By.XPath("//header//a[text()='Profile']"), By.Id("age"));
            Assert.AreEqual(ConfigurationManager.AppSettings["Gender"], page.Gender.GetText(), "Gender does not match");
            Assert.AreEqual(ConfigurationManager.AppSettings["Age"], page.Age.GetText(), "Age does not match");
            Assert.AreEqual(ConfigurationManager.AppSettings["Address"], page.Address.GetText(), "Address does not match");
            Assert.AreEqual(ConfigurationManager.AppSettings["Phone"], page.Phone.GetText(), "Phone does not match");
            Assert.AreEqual(ConfigurationManager.AppSettings["Hobby"], page.Hobby.GetSelection(), "Hobby does not match");
        }

        [Then(@"I ""(.*)"" login using ""(.*)"" as password")]
        public void ThenILoginUsingAsPassword(string condition, string password)
        {
            Utils.AssertLoginsOutcome(page, ConfigurationManager.AppSettings["Username"], password, condition);
        }
    }
}
