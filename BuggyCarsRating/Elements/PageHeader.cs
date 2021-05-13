using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class PageHeader
    {
        private IWebElement Root { get; }

        public IWebElement HomeLink => Root.FindElement(By.CssSelector("a[class='navbar-brand']"));

        // elements displayed only when logged in
        public string Greeting => Root.FindElement(By.CssSelector("span")).Text;
        public IWebElement Profile => Root.FindElement(By.XPath(".//a[text()='Profile']"));
        public IWebElement Logout => Root.FindElement(By.XPath(".//a[text()='Logout']"));

        // elements displayed only when logged out
        public InputText Username => new InputText(Root.FindElement(By.CssSelector("input[name='login']")));
        public InputText Password => new InputText(Root.FindElement(By.CssSelector("input[name='password']")));
        public IWebElement Login => Root.FindElement(By.XPath(".//button[text()='Login']"));
        public IWebElement Register => Root.FindElement(By.XPath(".//a[text()='Register']"));

        public PageHeader(IWebElement root)
        {
            Root = root;
        }
    }
}
