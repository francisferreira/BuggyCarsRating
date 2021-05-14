using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace BuggyCarsRating.Tests
{
    [Binding]
    public sealed class TableActionsSteps
    {
        private readonly MakerPage page;
        private int tablePage;

        public TableActionsSteps(IWebDriver driver)
        {
            page = new MakerPage(driver);
        }

        [Given(@"the ""(.*)"" page is reached this way")]
        public void GivenThePageIsReachedThisWay(string position)
        {
            if (position == "first")
            {
                while (tablePage > 1)
                {
                    page.GoToPrevPage();
                    Assert.AreEqual(--tablePage, page.CurrentPage, "The expected table page was not displayed");
                }
                return;
            }
            if (position == "last")
            {
                var last = page.TotalPages;
                while (tablePage < last)
                {
                    page.GoToNextPage();
                    Assert.AreEqual(++tablePage, page.CurrentPage, $"The expected table page was not displayed");
                }
                return;
            }
            throw new InvalidOperationException($"Invalid regex match in SpecFlow step: {position} != [first|last]");
        }

        [When(@"the mid-page is reached via textbox")]
        public void WhenTheMidPageIsReachedViaTextbox()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var scenario = ScenarioContext.Current.ScenarioInfo.Title;
#pragma warning restore CS0618 // Type or member is obsolete

            if (page.TotalPages % 2 != 0)
                tablePage = (int)Math.Ceiling(page.TotalPages / 2d);
            else
                switch (scenario)
                {
                    case string x when x.Contains("Backward"):
                        tablePage = page.TotalPages / 2 + 1; break;
                    case string x when x.Contains("Forward"):
                        tablePage = page.TotalPages / 2; break;
                }

            page.GoToPage(tablePage.ToString());
        }

        [When(@"the ""(.*)"" page button is clicked")]
        public void WhenThePageButtonIsClicked(string button)
        {
            switch (button)
            {
                case "next":
                    page.GoToNextPage(); break;
                case "previous":
                    page.GoToPrevPage(); break;
                default:
                    throw new InvalidOperationException($"Invalid regex match in SpecFlow step: {button} != [next|previous]");
            }
        }

        [Then(@"the page number ""(.*)"" (?:by one|as is)")]
        public void ThenThePageNumberByOneAsIs(string action)
        {
            switch (action)
            {
                case "increases":
                    Assert.AreEqual(++tablePage, page.CurrentPage, $"The expected table page was not displayed"); break;
                case "decreases":
                    Assert.AreEqual(--tablePage, page.CurrentPage, $"The expected table page was not displayed"); break;
                case "remains":
                    Assert.AreEqual(tablePage, page.CurrentPage, $"The expected table page was not displayed"); break;
                default:
                    throw new InvalidOperationException($"Invalid regex match in SpecFlow step: {action} != [increases|decreases|remains]");
            }
        }

        [Then(@"the expected page is displayed")]
        public void ThenTheExpectedPageIsDisplayed()
        {
            Assert.AreEqual(tablePage, page.CurrentPage, $"The expected table page was not displayed");
        }
    }
}
