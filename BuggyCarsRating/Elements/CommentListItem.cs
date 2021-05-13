using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class CommentListItem
    {
        private IWebElement Root { get; }

        public string Date => Root.FindElement(By.CssSelector("td:nth-of-type(1)")).Text;
        public string Author => Root.FindElement(By.CssSelector("td:nth-of-type(2)")).Text;
        public string Contents => Root.FindElement(By.CssSelector("td:nth-of-type(3)")).Text;

        public CommentListItem(IWebElement root)
        {
            Root = root;
        }
    }
}
