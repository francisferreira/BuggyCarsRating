using BuggyCarsRating.Elements;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace BuggyCarsRating.Pages
{
    public class MakerPage : BasePage
    {
        public MakerCard MakerCard => new MakerCard(Driver.FindElement(By.XPath("(//div[@class='container'][.//table])[2]")));
        public CarTable ModelList => new CarTable(Driver.FindElement(By.XPath("(//div[@class='container'][.//table])[2]")));
        public int CurrentPage => int.Parse(Regex.Match(ModelList.TablePager.TableFooter, "page (.*) of .*").Groups[1].Value);
        public int TotalPages => int.Parse(Regex.Match(ModelList.TablePager.TableFooter, "page .* of (.*)").Groups[1].Value);

        public MakerPage(IWebDriver driver) : base(driver)
        { }

        public void GoToNextPage()
        {
            try
            {
                ModelList.TablePager.Next.Click();
            }
            catch (ElementClickInterceptedException) { }
        }

        public void GoToPrevPage()
        {
            try
            {
                ModelList.TablePager.Prev.Click();
            }
            catch (ElementClickInterceptedException) { }
        }

        public void GoToPage(string page)
        {
            ModelList.TablePager.PageNumber.SetText(page + Keys.Enter);
        }
    }
}
