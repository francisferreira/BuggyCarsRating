using AventStack.ExtentReports;
using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BuggyCarsRating.Tests
{
    public class Utils
    {
        public static void NavigateAwayAndBack(IWebElement goAway, IWebElement goBack, By waitAway, By waitBack)
        {
            var wait = new WebDriverWait(Hooks.Driver, TimeSpan.FromSeconds(5));
            Func<IWebDriver, IWebElement> func;

            goAway.Click();
            func = ExpectedConditions.ElementToBeClickable(waitAway);
            wait.Until(func);

            goBack.Click();
            func = ExpectedConditions.ElementToBeClickable(waitBack);
            wait.Until(func);
        }

        public static void AssertLoginsOutcome(BasePage page, string username, string password, string condition)
        {
            page.Login(username, password);

            switch (condition)
            {
                case "can":
                    Assert.IsTrue(page.IsLogged(ConfigurationManager.AppSettings["FirstName"]), "Login failed when it should have succeeded"); break;
                case "cannot":
                    Assert.IsFalse(page.IsLogged(ConfigurationManager.AppSettings["FirstName"]), "Login succeeded when it should have failed"); break;
                default:
                    throw new InvalidOperationException($"Invalid condition called by SpecFlow step: {condition}");
            }
        }

        public static void EnsureTestUserIsCreated()
        {
            Hooks.Driver.Url = ConfigurationManager.AppSettings["Website"] + "register";
            WaitPageToLoad();

            var page = new RegisterPage(Hooks.Driver);
            page.SetRegisterInfo(ConfigurationManager.AppSettings["Username"],
                                 ConfigurationManager.AppSettings["FirstName"],
                                 ConfigurationManager.AppSettings["LastName"],
                                 ConfigurationManager.AppSettings["Userpass"],
                                 ConfigurationManager.AppSettings["Userpass"]);
            page.CommitChanges();
        }

        public static void EnsureUsersLoggedIn()
        {
            var page = new BasePage(Hooks.Driver);
            try
            {
                if (page.Header.Logout.Displayed)
                    return;
            }
            catch { }
            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Userpass"];
            page.Login(username, password);
        }

        public static void EnsureUsersLoggedOut()
        {
            // NOTE: Logout from overall rating page is broken.
            //       I've worked around that by navigating to
            //       home first, then logging out from there.
            if (Hooks.Driver.Url == ConfigurationManager.AppSettings["Website"] + "overall")
            {
                Hooks.Driver.Url = ConfigurationManager.AppSettings["Website"];
                WaitPageToLoad();
            }
            //       The hack above would be removed once that issue is fixed.

            var page = new BasePage(Hooks.Driver);
            try
            {
                if (page.Header.Login.Displayed)
                    return;
            }
            catch { }
            page.Logout();
        }

        public static void WaitPageToLoad()
        {
            var func = ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("img[src='/img/spin.gif']"));
            var wait = new WebDriverWait(Hooks.Driver, TimeSpan.FromSeconds(5));
            wait.Until(func);
        }

        public static void FillTestReport(ExtentReports report)
        {
            var browser = (string)((IJavaScriptExecutor)Hooks.Driver).ExecuteScript("return navigator.userAgent;");
            browser = Regex.Match(browser, @"((?:Firefox|Chrome)\/(?:\d|\.)*)").Groups[1].Value;

            report.AddSystemInfo("Test Object", "Buggy Cars Rating");
            report.AddSystemInfo("Run Duration", (Hooks.EndTime - Hooks.SttTime).ToString("c"));
            report.AddSystemInfo("Test Browser", browser.Replace("/", " "));
            report.AddSystemInfo("Test Machine", Environment.MachineName);
            report.AddSystemInfo("Session User", Environment.UserName);

            report.Flush();
        }
    }
}
