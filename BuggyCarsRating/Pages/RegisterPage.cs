using BuggyCarsRating.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BuggyCarsRating.Pages
{
    public class RegisterPage : BasePage
    {
        public string Title => Driver.FindElement(By.CssSelector("my-register h2")).Text;
        public InputText LoginName => new InputText(Driver.FindElement(By.Id("username")));
        public InputText FirstName => new InputText(Driver.FindElement(By.Id("firstName")));
        public InputText LastName => new InputText(Driver.FindElement(By.Id("lastName")));
        public InputText Password => new InputText(Driver.FindElement(By.Id("password")));
        public InputText ConfirmPassword => new InputText(Driver.FindElement(By.Id("confirmPassword")));

        public IWebElement Register => Driver.FindElement(By.XPath("//button[text()='Register']"));
        public IWebElement Cancel => Driver.FindElement(By.XPath("//a[text()='Cancel']"));

        public RegisterPage(IWebDriver driver) : base(driver)
        { }

        public void SetRegisterInfo(string loginName = null, string firstName = null, string lastName = null, string password = null, string confirm = null)
        {
            if (loginName != null)
                LoginName.SetText(loginName);
            if (firstName != null)
                FirstName.SetText(firstName);
            if (lastName != null)
                LastName.SetText(lastName);
            if (password != null)
                Password.SetText(password);
            if (confirm != null)
                ConfirmPassword.SetText(confirm);
        }

        public void CommitChanges()
        {
            Register.Click();

            var func = ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Register']"));
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(func);
        }
    }
}
