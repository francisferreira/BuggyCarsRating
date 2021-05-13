using BuggyCarsRating.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BuggyCarsRating.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver { get; }

        public PageHeader Header => new PageHeader(Driver.FindElement(By.TagName("header")));
        public PageFooter Footer => new PageFooter(Driver.FindElement(By.TagName("footer")));

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void Login(string username, string password)
        {
            Header.Username.SetText(username);
            Header.Password.SetText(password);
            Header.Login.Click();

            var func = ExpectedConditions.ElementIsVisible(By.CssSelector("header span"));
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(func);
        }

        public void Logout()
        {
            Header.Logout.Click();

            var func = ExpectedConditions.ElementToBeClickable(By.XPath("//header//button[text()='Login']"));
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(func);
        }

        public bool IsLogged(string firstname)
        {
            try
            {
                return Header.Greeting == $"Hi, {firstname}";
            }
            catch
            {
                return false;
            }
        }
    }
}
