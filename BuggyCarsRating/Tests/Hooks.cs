using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using BuggyCarsRating.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BuggyCarsRating.Tests
{
    [Binding]
    public sealed class Hooks
    {
        public static IWebDriver Driver { get; set; }
        public static DateTime SttTime { get; set; }
        public static DateTime EndTime { get; set; }

        private static ExtentReports Report => new ExtentReports();
        private static ExtentTest Feature { get; set; }
        private static ExtentTest Scenario { get; set; }

        private IObjectContainer Container { get; }
        private string Username { get; set; }

        public Hooks(IObjectContainer container)
        {
            Container = container;

            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            Report.AttachReporter(new ExtentHtmlReporter($"{path}\\ExtentReport\\report.html"));
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            SttTime = DateTime.Now;

            switch (ConfigurationManager.AppSettings["Browser"])
            {
                case "Chrome":
                    Driver = new ChromeDriver(); break;
                case "Firefox":
                    Driver = new FirefoxDriver(); break;
                default:
                    throw new ConfigurationErrorsException("Unsupported/Invalid browser defined in app.config");
            }

            Driver.Manage().Window.Maximize();
            Utils.EnsureTestUserIsCreated();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            EndTime = DateTime.Now;
            Utils.FillTestReport(Report);

            if (Driver != null)
                Driver.Quit();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Feature = Report.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeFeature("loggedin")]
        public static void BeforeFeatureLoggedIn()
        {
            Utils.EnsureUsersLoggedIn();
        }

        [BeforeFeature("loggedout")]
        public static void BeforeFeatureLoggedOut()
        {
            Utils.EnsureUsersLoggedOut();
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            // set up dependency injection
            Container.RegisterInstanceAs(Driver, typeof(IWebDriver));
            
            var scenarioHeading = scenarioContext.ScenarioInfo.Title;
            if (scenarioContext.ScenarioInfo.Arguments.Count > 0)
                scenarioHeading += $" - {scenarioContext.ScenarioInfo.Arguments.Cast<DictionaryEntry>().First().Value}";
            Scenario = Feature.CreateNode<Scenario>(scenarioHeading);
        }

        [BeforeScenario("uniqueid", Order = 30)]
        public void BeforeScenarioUniqueId()
        {
            Utils.EnsureUsersLoggedOut();

            var page = new RegisterPage(Driver);
            page.Header.Register.Click();

            Username = ConfigurationManager.AppSettings["Username"] + "-" + Guid.NewGuid().ToString("N");

            page.SetRegisterInfo(Username,
                                 ConfigurationManager.AppSettings["FirstName"],
                                 ConfigurationManager.AppSettings["LastName"],
                                 ConfigurationManager.AppSettings["Password"],
                                 ConfigurationManager.AppSettings["Password"]);
            page.CommitChanges();
            page.Login(Username, ConfigurationManager.AppSettings["Password"]);
        }

        [BeforeScenario("loggedin", Order = 50)]
        public void BeforeScenarioLoggedIn()
        {
            Utils.EnsureUsersLoggedIn();
        }

        [BeforeScenario("loggedout", Order = 50)]
        public void BeforeScenarioLoggedOut()
        {
            Utils.EnsureUsersLoggedOut();
        }

        [AfterScenario("uniqueid")]
        public void AfterScenarioUniqueId()
        {
            var page = new BasePage(Driver);
            page.Logout();
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            var scenarioStepContext = ScenarioStepContext.Current;
            ExtentTest node = null;

            switch (scenarioStepContext.StepInfo.StepDefinitionType)
            {
                case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                    node = Scenario.CreateNode<Given>($"Given {scenarioStepContext.StepInfo.Text}"); break;
                case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                    node = Scenario.CreateNode<When>($"When {scenarioStepContext.StepInfo.Text}"); break;
                case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                    node = Scenario.CreateNode<Then>($"Then {scenarioStepContext.StepInfo.Text}"); break;
            }

            if (scenarioContext.TestError != null)
            {
                var screenshotString = ((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString;
                var screenshotModel = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotString, "Error Screenshot").Build();
                if (node != null)
                {
                    node.Fail(scenarioContext.TestError);
                    node.Fail(scenarioContext.TestError.Message, screenshotModel);
                }
            }
            else if (scenarioStepContext.StepInfo.Table != null)
            {
                var table = scenarioStepContext.StepInfo.Table;
                var header = table.Header.ToArray();
                var builder = new StringBuilder();
                foreach (var row in table.Rows)
                {
                    if (builder.Length > 0)
                        builder.AppendFormat("\n");
                    for (var i = 0; i < row.Count; i++)
                    {
                        if (i > 0) builder.Append(", ");
                        builder.AppendFormat("{0}: {1}", header[i], row[i]);
                    }
                }
                node.Info(builder.ToString());
            }
        }
    }
}
