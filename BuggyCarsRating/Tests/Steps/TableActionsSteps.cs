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
        private int listPage;

        public TableActionsSteps(IWebDriver driver)
        {
            page = new MakerPage(driver);
        }

        [Given(@"the ""(.*)"" page is reached this way")]
        public void GivenThePageIsReachedThisWay(string position)
        {
            if (position == "first")
            {
                while (listPage > 1)
                {
                    page.GoToPrevPage();
                    Assert.AreEqual(--listPage, page.CurrentPage, $"The expected list page is not displayed: {listPage}");
                }
                return;
            }
            if (position == "last")
            {
                var last = page.TotalPages;
                while (listPage < last)
                {
                    page.GoToNextPage();
                    Assert.AreEqual(++listPage, page.CurrentPage, $"The expected list page is not displayed: {listPage}");
                }
                return;
            }
            throw new InvalidOperationException($"Invalid position called by SpecFlow step: {position}");
        }

        [When(@"the mid-page number ""(.*)"" is entered")]
        public void WhenTheMid_PageNumberIsEntered(string method)
        {
            if (page.TotalPages % 2 != 0)
            {
                listPage = (int)Math.Ceiling(page.TotalPages / 2d);
            }
            else
            {
                switch (method)
                {
                    case "(round up)":
                        listPage = page.TotalPages / 2 + 1; break;
                    case "(round down)":
                        listPage = page.TotalPages / 2; break;
                    default:
                        throw new InvalidOperationException($"Invalid method called by SpecFlow step: {method}");
                }
            }
            page.GoToPage(listPage.ToString());
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
                    throw new InvalidOperationException($"Invalid button called by SpecFlow step: {button}");
            }
        }

        [Then(@"the expected page is displayed")]
        public void ThenTheExpectedPageIsDisplayed()
        {
            Assert.AreEqual(listPage, page.CurrentPage, $"The expected list page is not displayed: {listPage}");
        }

        [Then(@"the current page number ""(.*)"" (?:by one|as is)")]
        public void ThenTheCurrentPageNumberByOne(string action)
        {
            switch (action)
            {
                case "increases":
                    Assert.AreEqual(++listPage, page.CurrentPage, $"The expected list page is not displayed: {listPage}"); break;
                case "decreases":
                    Assert.AreEqual(--listPage, page.CurrentPage, $"The expected list page is not displayed: {listPage}"); break;
                case "remains":
                    Assert.AreEqual(listPage, page.CurrentPage, $"The expected list page is not displayed: {listPage}"); break;
                default:
                    throw new InvalidOperationException($"Invalid action called by SpecFlow step: {action}");
            }
        }
    }
}
