using Gobigreviews.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Gobigreviews.Pages.Components
{
    public class SignInPageComponents
    {
         private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SignInPageComponents(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestSettings.DefaultTimeOut));
        }

        private By SignUpLink => By.CssSelector("a.link-primary.fw-semibold");
        public void ClickSignUpLink()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestSettings.DefaultTimeOut));
            wait.Until(driver => driver.FindElement(SignUpLink).Displayed);
            driver.FindElement(SignUpLink).Click();
        }

    }

}