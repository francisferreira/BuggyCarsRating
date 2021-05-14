using BuggyCarsRating.Elements;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace BuggyCarsRating.Pages
{
    public class MakerPage : BasePage
    {
        private bool RatingView { get; }

        public MakerCard MakerCard => new MakerCard(Driver.FindElement(By.XPath("(//div[@class='container'][.//table])[2]")));
        public CarTable CarTable => new CarTable(Driver.FindElement(By.XPath("(//div[@class='container'][.//table])[2]")), RatingView);
        public int CurrentPage => int.Parse(Regex.Match(CarTable.TablePager.TableFooter, "page (.*) of .*").Groups[1].Value);
        public int TotalPages => int.Parse(Regex.Match(CarTable.TablePager.TableFooter, "page .* of (.*)").Groups[1].Value);

        public MakerPage(IWebDriver driver, bool ratingView = false) : base(driver)
        {
            RatingView = ratingView;
        }

        public void GoToNextPage()
        {
            try
            {
                CarTable.TablePager.Next.Click();
            }
            catch (ElementClickInterceptedException)
            { }
        }

        public void GoToPrevPage()
        {
            try
            {
                CarTable.TablePager.Prev.Click();
            }
            catch (ElementClickInterceptedException)
            { }
        }

        public void GoToPage(string page)
        {
            CarTable.TablePager.PageNumber.SetText(page + Keys.Enter);
        }
    }
}
