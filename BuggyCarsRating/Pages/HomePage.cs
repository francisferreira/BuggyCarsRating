using OpenQA.Selenium;

namespace BuggyCarsRating.Pages
{
    public class HomePage : BasePage
    {
        public IWebElement PopularMake => Driver.FindElement(By.XPath("//main//h2[text()='Popular Make']/following-sibling::a/img"));
        public IWebElement PopularModel => Driver.FindElement(By.XPath("//main//h2[text()='Popular Model']/following-sibling::a/img"));
        public IWebElement OverallRating => Driver.FindElement(By.XPath("//main//h2[text()='Overall Rating']/following-sibling::a/img"));

        public HomePage(IWebDriver driver) : base(driver)
        { }
    }
}
