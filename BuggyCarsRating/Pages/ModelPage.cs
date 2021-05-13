using BuggyCarsRating.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BuggyCarsRating.Pages
{
    public class ModelPage : BasePage
    {
        public ModelCard ModelCard => new ModelCard(Driver.FindElement(By.CssSelector("my-model > div[class=container]")));
        public CommentList CommentList => new CommentList(Driver.FindElement(By.CssSelector("my-model > div[class=container] table")));
        public int TotalComments => CommentList.Count;
        public int TotalVotes => ModelCard.VoteCount;

        public ModelPage(IWebDriver driver) : base(driver)
        { }

        public void CastVote(out string feedback, string comment = null)
        {
            if (comment != null)
                ModelCard.VoteComment.SetText(comment);
            
            ModelCard.VoteButton.Click();

            var func = ExpectedConditions.ElementIsVisible(By.CssSelector("div[class='col-lg-4'] p"));
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            feedback = wait.Until(func).Text;
        }

        public bool IsVotePossible(out string feedback)
        {
            try
            {
                if (ModelCard.VoteButton.Displayed)
                {
                    feedback = "Vote is possible!";
                    return true;
                }
            }
            catch { }
            feedback = ModelCard.VoteString;
            return false;
        }

        public CommentListItem GetLatestComment()
        {
            return CommentList[1];
        }

        public CommentListItem GetCommentNumber(int number)
        {
            return CommentList[number];
        }
    }
}
