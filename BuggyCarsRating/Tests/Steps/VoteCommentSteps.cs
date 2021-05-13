using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace BuggyCarsRating.Tests
{
    [Binding]
    public sealed class VoteCommentSteps
    {
        private readonly ModelPage page;
        private int totalVotes;
        private int totalComments;
        private string timestamp;
        private string feedback;
        private string contents;

        public VoteCommentSteps(IWebDriver driver)
        {
            page = new ModelPage(driver);
        }

        [Given(@"the ""(.*)"" is saved")]
        public void GivenTheIsSaved(string stat)
        {
            switch (stat)
            {
                case "first comment date":
                    timestamp = page.GetLatestComment().Date; break;
                case "number of comments":
                    totalComments = page.TotalComments; break;
                case "number of votes":
                    totalVotes = page.TotalVotes; break;
                default:
                    throw new InvalidOperationException($"Invalid stat called by SpecFlow step: {stat}");
            }
        }

        [When(@"vote is cast without comment")]
        public void WhenVoteIsCastWithoutComment()
        {
            page.CastVote(out feedback);
        }

        [When(@"vote is cast with comment ""(.*)""")]
        public void WhenVoteIsCastWithComment(string comment)
        {
            contents = comment;
            page.CastVote(out feedback, comment);
        }

        [Then(@"the ""(.*)"" remains unaltered")]
        public void ThenTheRemainsUnaltered(string stat)
        {
            switch (stat)
            {
                case "first comment date":
                    Assert.AreEqual(timestamp, page.GetLatestComment().Date, "Last comment timestamp is not the same"); break;
                case "number of comments":
                    Assert.AreEqual(totalComments, page.TotalComments, "Number of comments is not the same"); break;
                default:
                    throw new InvalidOperationException($"Invalid stat called by SpecFlow step: {stat}");
            }
        }

        [Then(@"the ""(.*)"" increases by one")]
        public void ThenTheIncreasesByOne(string stat)
        {
            switch (stat)
            {
                case "number of comments":
                    Assert.AreEqual(totalComments + 1, page.TotalComments, "Number of comments did not increase by one"); break;
                case "number of votes":
                    Assert.AreEqual(totalVotes + 1, page.TotalVotes, "Number of votes did not increase by one"); break;
                default:
                    throw new InvalidOperationException($"Invalid stat called by SpecFlow step: {stat}");
            }
        }

        [Then(@"the first comment ""(.*)"" (?:is|are) correct")]
        public void ThenTheIsCorrect(string stat)
        {
            var topComment = page.GetLatestComment();
            var dateDiff = (DateTime.Now - DateTime.Parse(topComment.Date)).Duration();
            var author = string.Join(" ", ConfigurationManager.AppSettings["FirstName"], ConfigurationManager.AppSettings["LastName"]);

            switch (stat)
            {
                case "date":
                    Assert.IsTrue(dateDiff < TimeSpan.FromSeconds(5), "Comment date is not as expected"); break;
                case "author":
                    Assert.AreEqual(author, topComment.Author, "Comment author is not as expected"); break;
                case "contents":
                    Assert.AreEqual(contents, topComment.Contents, "Comment contents are not as expected"); break;
                default:
                    throw new InvalidOperationException($"Invalid stat called by SpecFlow step: {stat}");
            }
        }

        [Then(@"the vote cannot be cast")]
        public void ThenTheVoteCannotBeCast()
        {
            Assert.IsFalse(page.IsVotePossible(out feedback), "Voting is allowed when it should be denied");
        }

        [Then(@"the page says ""(.*)""")]
        public void ThenThePageSays(string feedback)
        {
            Assert.AreEqual(feedback, this.feedback, $"The expected message is not displayed: {feedback}");
        }
    }
}
