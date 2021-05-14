using AventStack.ExtentReports;
using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BuggyCarsRating.Tests
{
    public class Utils
    {
        public static void NavigateAwayAndBack(IWebElement goAway, IWebElement goBack)
        {
            goAway.Click();
            WaitPageToLoad();
            goBack.Click();
            WaitPageToLoad();
        }

        public static void AssertLoginOutcome(string username, string password, string condition)
        {
            var page = new BasePage(Hooks.Driver);
            page.Login(username, password);

            switch (condition)
            {
                case "works":
                    Assert.IsTrue(page.IsLogged(ConfigurationManager.AppSettings["FirstName"]), "Login failed when it should have succeeded"); break;
                case "fails":
                    Assert.IsFalse(page.IsLogged(ConfigurationManager.AppSettings["FirstName"]), "Login succeeded when it should have failed"); break;
                default:
                    throw new InvalidOperationException($"Invalid regex match in SpecFlow step: {condition} != [works|fails]");
            }
        }

        public static void ViewModelByRating(int rank)
        {
            Hooks.Driver.Url = ConfigurationManager.AppSettings["Website"] + "overall";
            WaitPageToLoad();

            var tablePage = rank / 5 + (rank % 5 == 0 ? 0 : 1);
            var posInPage = rank % 5 + (rank % 5 == 0 ? 5 : 0);

            var page = new MakerPage(Hooks.Driver, true);
            page.GoToPage(tablePage.ToString());

            var watch = Stopwatch.StartNew();
            try
            {
                while (watch.Elapsed < TimeSpan.FromSeconds(5))
                    if (page.CarTable[posInPage].Rank == rank.ToString())
                        break;
            }
            catch (StaleElementReferenceException)
            { }
            if (page.CarTable[posInPage].Rank != rank.ToString())
                throw new ApplicationException($"Model in Rank #{rank} was not found");

            page.CarTable[posInPage].Image.Click();
            WaitPageToLoad();
        }

        public static void EnsureUserIsCreated()
        {
            Hooks.Driver.Url = ConfigurationManager.AppSettings["Website"] + "register";
            WaitPageToLoad();

            Hooks.Username = ConfigurationManager.AppSettings["LoginName"] + "-" + Guid.NewGuid().ToString("N");
            Hooks.Password = ConfigurationManager.AppSettings["OldPassword"];

            var page = new RegisterPage(Hooks.Driver);
            page.SetRegisterInfo(Hooks.Username,
                                 ConfigurationManager.AppSettings["FirstName"],
                                 ConfigurationManager.AppSettings["LastName"],
                                 Hooks.Password,
                                 Hooks.Password);
            page.CommitChanges();
        }

        public static void EnsureUserLoggedIn()
        {
            var page = new BasePage(Hooks.Driver);
            try
            {
                if (page.Header.Logout.Displayed)
                    return;
            }
            catch { }
            page.Login(Hooks.Username, Hooks.Password);
        }

        public static void EnsureUserLoggedOut()
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
