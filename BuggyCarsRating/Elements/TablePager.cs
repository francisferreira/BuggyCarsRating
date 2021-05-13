using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class TablePager
    {
        private IWebElement Root { get; }

        public IWebElement Prev => Root.FindElement(By.XPath(".//a[text()='«']"));
        public IWebElement Next => Root.FindElement(By.XPath(".//a[text()='»']"));
        public InputText PageNumber => new InputText(Root.FindElement(By.CssSelector("input")));
        public string TableFooter => Root.FindElement(By.CssSelector("div[class='pull-xs-right']")).Text.Trim();

        public TablePager(IWebElement root)
        {
            Root = root;
        }
    }
}
