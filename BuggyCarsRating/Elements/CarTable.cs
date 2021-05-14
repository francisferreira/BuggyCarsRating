using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class CarTable
    {
        protected IWebElement Root { get; }
        private bool RatingView { get; }

        public IWebElement MakerHeader => Root.FindElement(By.XPath(".//a[text()='Make']"));
        public IWebElement ModelHeader => Root.FindElement(By.XPath(".//a[text()='Model']"));
        public IWebElement RankHeader => Root.FindElement(By.XPath(".//a[text()='Rank']"));
        public IWebElement VotesHeader => Root.FindElement(By.XPath(".//a[text()='Votes']"));
        public IWebElement EngineHeader => Root.FindElement(By.XPath(".//a[text()='Rank']"));

        public TablePager TablePager => new TablePager(Root.FindElement(By.CssSelector("my-pager")));

        public CarTableItem this[int row] => new CarTableItem(Root.FindElement(By.CssSelector($"tbody tr:nth-of-type({row})")), RatingView);

        public CarTable(IWebElement root, bool ratingView)
        {
            Root = root;
            RatingView = ratingView;
        }
    }
}
