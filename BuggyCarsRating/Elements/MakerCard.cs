using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class MakerCard
    {
        private IWebElement Root { get; }

        public string Title => Root.FindElement(By.CssSelector("h3[class='card-header']")).Text;
        public IWebElement Image => Root.FindElement(By.CssSelector("div[class='card-block'] a"));

        public MakerCard(IWebElement root)
        {
            Root = root;
        }
    }
}
