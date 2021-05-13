using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class ModelCard
    {
        private IWebElement Root { get; }

        public string Title => Root.FindElement(By.CssSelector("h3")).Text;
        public IWebElement Image => Root.FindElement(By.CssSelector("div[class='col-lg-6'] a"));
        public string MakerTitle => Root.FindElement(By.CssSelector("div[class='col-lg-2'] h4")).Text;
        public IWebElement MakerImage => Root.FindElement(By.CssSelector("div[class='col-lg-2'] a"));
        public string VoteString => Root.FindElement(By.CssSelector("div[class='col-lg-4'] p")).Text;
        public IWebElement VoteButton => Root.FindElement(By.CssSelector("div[class='col-lg-4'] button"));
        public int VoteCount => int.Parse(Root.FindElement(By.CssSelector("div[class='col-lg-4'] h4 strong")).Text);
        public InputText VoteComment => new InputText(Root.FindElement(By.CssSelector("div[class='col-lg-4'] textarea")));

        public ModelCard(IWebElement root)
        {
            Root = root;
    }
    }
}
