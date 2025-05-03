using OpenQA.Selenium;
using Gobigreviews.Config;
using Gobigreviews.Pages.Components;
using OpenQA.Selenium.Support.UI;

namespace Gobigreviews.Pages
{
    public class HomePage
    {
            private readonly IWebDriver driver;

            public HomePage(IWebDriver driver)
            {
                this.driver = driver;
            }

            public void Open() => driver.Navigate().GoToUrl(TestSettings.BaseUrl);
            private By SignInLink => By.CssSelector("#main_menu > div > ul > li:nth-child(2) > a");
            public void ClickSignInLink()
                {
                 var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestSettings.DefaultTimeOut));
                 wait.Until(driver => driver.FindElement(SignInLink).Displayed);
                 driver.FindElement(SignInLink).Click();
                }
    }
}