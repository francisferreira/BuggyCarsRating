using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BuggyCarsRating.Tests
{
    [Binding]
    public sealed class CommonSteps
    {
        private readonly IWebDriver driver;
        private readonly HomePage page;

        public CommonSteps(IWebDriver driver)
        {
            this.driver = driver;
            page = new HomePage(driver);
        }

        [Given(@"the ""(.*)"" page is displayed")]
        public void GivenThePageIsDisplayed(string page)
        {
            page = page.ToLower();
            var home = ConfigurationManager.AppSettings["Website"];

            if (driver.Url.StartsWith(home + (page != "home" ? page : "")))
                return;
            if (driver.Url != home)
                driver.Url = home;

            Utils.WaitPageToLoad();

            switch (page)
            {
                case "home":
                    break;
                case "maker":
                    this.page.PopularMake.Click(); break;
                case "model":
                    this.page.PopularModel.Click(); break;
                case "overall":
                    this.page.OverallRating.Click(); break;
                case "profile":
                    this.page.Header.Profile.Click(); break;
                case "register":
                    this.page.Header.Register.Click(); break;
                default:
                    throw new InvalidOperationException($"Invalid page called by SpecFlow step: {page}");
            }

            Utils.WaitPageToLoad();
        }

        [When(@"""(.*)"" is performed successfully")]
        public void WhenIsPerformedSuccessfully(string action)
        {
            switch (action)
            {
                case "login":
                    var username = ConfigurationManager.AppSettings["username"];
                    var password = ConfigurationManager.AppSettings["userpass"];
                    page.Login(username, password); break;
                case "logout":
                    page.Logout(); break;
                default:
                    throw new InvalidOperationException($"Invalid action called by SpecFlow step: {action}");
            }
        }

        [When(@"the ""(.*)"" button is clicked")]
        public void WhenTheButtonIsClicked(string button)
        {
            var path = $"//button[text()='{button}']";

            // cancel are not buttons but anchor links
            if (button == "Cancel")
                path = path.Replace("button", "a");

            driver.FindElement(By.XPath(path)).Click();

            if (button != "Cancel")
            {
                // wait for processing
                var func = ExpectedConditions.ElementToBeClickable(By.XPath(path));
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(func);
            }
        }

        [Then(@"the ""(.*)"" message pops up")]
        public void ThenTheWarningPopsUp(string error)
        {
            var alert = driver.FindElement(By.XPath($"//div[contains(@class,'alert') and contains(text(),'{error}')]"));
            Assert.IsTrue(alert.Displayed, $"The expected warning is not displayed: '{error}'");
        }

        [Then(@"the ""(.*)"" page is displayed")]
        public void ThenThePageIsDisplayed(string page)
        {
            var url = ConfigurationManager.AppSettings["Website"] + (page == "home" ? "" : page);
            Assert.AreEqual(url, driver.Url, $"The expected page is not displayed: {page}");
        }

        [Then(@"the ""(.*)"" button is disabled")]
        public void ThenTheButtonIsDisabled(string button)
        {
            var element = driver.FindElement(By.XPath($"//button[text()='{button}']"));
            Assert.IsFalse(element.Enabled, $"The '{button}' button is enabled when it is expected to be disabled");
        }
    }
}
