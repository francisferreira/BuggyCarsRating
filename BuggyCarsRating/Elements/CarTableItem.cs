using OpenQA.Selenium;
using System.Linq;

namespace BuggyCarsRating.Elements
{
    public class CarTableItem
    {
        private IWebElement Root { get; }
        private bool RatingView { get; }

        public IWebElement Image => Root.FindElement(By.CssSelector("td:nth-of-type(1) a"));
        public IWebElement Maker => Root.FindElement(By.CssSelector("td:nth-of-type(2) a"));
        public IWebElement Model => Root.FindElement(By.CssSelector($"td:nth-of-type({(RatingView ? 3 : 2)}) a"));
        public string Rank => Root.FindElement(By.CssSelector($"td:nth-of-type({(RatingView ? 4 : 3)})")).Text;
        public string Votes => Root.FindElement(By.CssSelector($"td:nth-of-type({(RatingView ? 5 : 4)})")).Text;
        public string Engine => Root.FindElement(By.CssSelector("td:nth-last-of-type(2)")).Text;
        public string[] Comments => Root.FindElements(By.CssSelector("td:nth-last-of-type(1) p")).Select(x => x.Text).ToArray();
        public IWebElement ViewMoreLink => Root.FindElement(By.CssSelector("td:nth-last-of-type(1) a"));

        public CarTableItem(IWebElement root, bool ratingView)
        {
            Root = root;
            RatingView = ratingView;
        }
    }
}