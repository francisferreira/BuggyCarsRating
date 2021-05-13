using OpenQA.Selenium;
using System.Linq;

namespace BuggyCarsRating.Elements
{
    public class CarTableItem
    {
        private IWebElement Root { get; }
        private bool ShiftHeads { get; }

        public IWebElement Image => Root.FindElement(By.CssSelector("td:nth-of-type(1) a"));
        public IWebElement Maker => Root.FindElement(By.CssSelector("td:nth-of-type(2) a"));
        public IWebElement Model => Root.FindElement(By.CssSelector($"td:nth-of-type({(ShiftHeads ? 3 : 2)}) a"));
        public string Rank => Root.FindElement(By.CssSelector($"td:nth-of-type({(ShiftHeads ? 4 : 3)})")).Text;
        public string Votes => Root.FindElement(By.CssSelector($"td:nth-of-type({(ShiftHeads ? 5 : 4)})")).Text;
        public string Engine => Root.FindElement(By.CssSelector("td:nth-last-of-type(2)")).Text;
        public string[] Comments => Root.FindElements(By.CssSelector("td:nth-last-of-type(1) p")).Select(x => x.Text).ToArray();
        public IWebElement ViewMoreLink => Root.FindElement(By.CssSelector("td:nth-last-of-type(1) a"));

        public CarTableItem(IWebElement root, bool shiftHeads = false)
        {
            Root = root;
            ShiftHeads = shiftHeads;
        }
    }
}