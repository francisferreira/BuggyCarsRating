using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class PageFooter
    {
        private IWebElement Root { get; }

        public string Copyright => Root.FindElement(By.CssSelector("p")).Text;
        public IWebElement Facebook => Root.FindElement(By.CssSelector("a[title='Facebook']"));
        public IWebElement Twitter => Root.FindElement(By.CssSelector("a[title='Twitter']"));

        public PageFooter(IWebElement root)
        {
            Root = root;
        }
    }
}
