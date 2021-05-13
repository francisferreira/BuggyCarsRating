using OpenQA.Selenium;

namespace BuggyCarsRating.Elements
{
    public class InputText
    {
        private IWebElement Input { get; }

        public InputText(IWebElement input)
        {
            Input = input;
        }

        public string GetText()
        {
            return Input.GetAttribute("value");
        }

        public void SetText(string value)
        {
            Input.SendKeys(Keys.Control + "a");
            Input.SendKeys(Keys.Delete);
            Input.SendKeys(value);
        }
    }
}
