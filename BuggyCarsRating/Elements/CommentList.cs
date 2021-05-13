using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class CommentList
    {
        private IWebElement Root { get; }

        public int Count => Root.FindElements(By.CssSelector("tr")).Count;
        public CommentListItem this[int position] => new CommentListItem(Root.FindElement(By.CssSelector($"tr:nth-of-type({position})")));

        public CommentList(IWebElement root)
        {
            Root = root;
        }
    }
}
