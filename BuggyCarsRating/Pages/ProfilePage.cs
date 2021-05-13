using BuggyCarsRating.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BuggyCarsRating.Pages
{
    public class ProfilePage : BasePage
    {
        // basic info fields
        public InputText LoginName => new InputText(Driver.FindElement(By.Id("username")));
        public InputText FirstName => new InputText(Driver.FindElement(By.Id("firstName")));
        public InputText LastName => new InputText(Driver.FindElement(By.Id("lastName")));

        // additional info fields
        public InputText Gender => new InputText(Driver.FindElement(By.Id("gender")));
        public InputText Age => new InputText(Driver.FindElement(By.Id("age")));
        public InputText Address => new InputText(Driver.FindElement(By.Id("address")));
        public InputText Phone => new InputText(Driver.FindElement(By.Id("phone")));
        public InputSelect Hobby => new InputSelect(Driver.FindElement(By.Id("hobby")));

        // password + language fields
        public InputText CurrentPassword => new InputText(Driver.FindElement(By.Id("currentPassword")));
        public InputText NewPassword => new InputText(Driver.FindElement(By.Id("newPassword")));
        public InputText ConfirmPassword => new InputText(Driver.FindElement(By.Id("newPasswordConfirmation")));
        public InputSelect Language => new InputSelect(Driver.FindElement(By.Id("language")));

        public IWebElement Save => Driver.FindElement(By.XPath("//button[text()='Save']"));
        public IWebElement Cancel => Driver.FindElement(By.XPath("//a[text()='Cancel']"));

        public ProfilePage(IWebDriver driver) : base(driver)
        { }

        public void SetBasicInfo(string firstName = null, string lastName = null)
        {
            if (firstName != null)
                FirstName.SetText(firstName);
            if (lastName != null)
                LastName.SetText(lastName);
        }

        public void SetAdditionalInfo(string gender = null, string age = null, string address = null, string phone = null, string hobby = null)
        {
            if (gender != null)
                Gender.SetText(gender);
            if (age != null)
                Age.SetText(age);
            if (address != null)
                Address.SetText(address);
            if (phone != null)
                Phone.SetText(phone);
            if (hobby != null)
                Hobby.SetSelection(hobby);
        }

        public void SetPasswordInfo(string currentPassword = null, string newPassword = null, string confirmPassword = null)
        {
            if (currentPassword != null)
                CurrentPassword.SetText(currentPassword);
            if (newPassword != null)
                NewPassword.SetText(newPassword);
            if (confirmPassword != null)
                ConfirmPassword.SetText(confirmPassword);
        }

        public void SetLanguageInfo(string language = null)
        {
            if (language != null)
                Language.SetSelection(language);
        }

        public void CommitChanges()
        {
            Save.Click();

            var func = ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Save']"));
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(func);
        }
    }
}
