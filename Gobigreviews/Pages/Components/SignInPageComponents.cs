using Gobigreviews.Config;
using Gobigreviews.Utils;
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
        private By EmailField => By.Name("email");
        private By PasswordField => By.Name("password");
        private By SignInButton => By.Id("kt_sign_up_submit");
        public void ClickSignUpLink()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestSettings.DefaultTimeOut));
            wait.Until(driver => driver.FindElement(SignUpLink).Displayed);
            driver.FindElement(SignUpLink).Click();
        }
        public void EnterUserEmail(string email) => ElementHelper.Type(driver, EmailField, email);
        public void EnterUserPassword(string password) => ElementHelper.Type(driver, PasswordField, password);
        public void ClickSignInButton() => ElementHelper.Click(driver, SignInButton);

    }

}