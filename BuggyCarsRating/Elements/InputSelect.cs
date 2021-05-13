using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BuggyCarsRating.Elements
{
    public class InputSelect
    {
        private SelectElement Input { get; }

        public InputSelect(IWebElement root)
        {
            Input = new SelectElement(root);
        }

        public string GetSelection()
        {
            return Input.SelectedOption.Text;
        }

        public void SetSelection(string text)
        {
            Input.SelectByText(text);
        }

        public void SetSelection(int index)
        {
            Input.SelectByIndex(index);
        }
    }
}
