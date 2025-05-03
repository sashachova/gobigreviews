using Gobigreviews.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Gobigreviews.Pages.Components
{
    public class SignUpPageComponents
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SignUpPageComponents(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestSettings.DefaultTimeOut));
        }
        private By Logo => By.CssSelector("img[src*='GoBigReviews-logo.png']");
        private By NameField => By.Name("name");
        private By EmailField => By.Name("email");
        private By PasswordField => By.Name("password");
        private By RepeatPasswordField => By.Name("confirm-password");
        private By TermsAndConditionsCheckbox => By.Name("toc");
        private By SubcribeForNewsletterCheckbox => By.CssSelector("input.form-check-input.font-weight-light");
        private By SignUpButton => By.CssSelector("button.btn.btn-primary");
        private By UserProfileButton => By.CssSelector("img[alt='GoBigReview-user']");
        private By EmailTakenValidationError => By.CssSelector("div.text-danger.errors-field");

        public void WaitForPageToLoad()
        {
            wait.Until(driver => ((IJavaScriptExecutor)driver)
                .ExecuteScript("return document.readyState").Equals("complete"));
        }

        public bool IsLogoDisplayed() => driver.FindElement(Logo).Displayed;
        public string GetLogoHeight() => driver.FindElement(Logo).GetAttribute("height");
        public string GetLogoWidth() => driver.FindElement(Logo).GetAttribute("width");
        public bool IsNameDisplayed() => driver.FindElement(NameField).Displayed;
        public string GetNamePlaceholder() => driver.FindElement(NameField).GetAttribute("placeholder");
        public bool IsEmailDisplayed() => driver.FindElement(EmailField).Displayed;
        public bool IsPasswordDisplayed() => driver.FindElement(PasswordField).Displayed;
        public bool IsRepeatPasswordDisplayed() => driver.FindElement(RepeatPasswordField).Displayed;
        public bool IsTermAndConditionsDisplayed() => driver.FindElement(TermsAndConditionsCheckbox).Displayed;
        public bool IsSubcribeForNewsletterDisplayed() => driver.FindElement(SubcribeForNewsletterCheckbox).Displayed;
        public bool IsSignUpButtonDisplayed() => driver.FindElement(SignUpButton).Displayed;
        public bool IsUserProfileButtonDisplayed() => driver.FindElement(UserProfileButton).Displayed;
        public bool IsEmailTakenErrorDisplayed() => driver.FindElement(EmailTakenValidationError).Displayed;

        public void EnterUserName(string name)
        {
            driver.FindElement(NameField).Clear();
            driver.FindElement(NameField).SendKeys(name);
        }
        public void EnterUserEmail(string email)
        {
            driver.FindElement(EmailField).Clear();
            driver.FindElement(EmailField).SendKeys(email);
        }
        public void EnterUserPassword(string password)
        {
            driver.FindElement(PasswordField).Clear();
            driver.FindElement(PasswordField).SendKeys(password);
        }
        public void EnterUserPassword_Repeate(string passwordRepeate)
        {
            driver.FindElement(RepeatPasswordField).Clear();
            driver.FindElement(RepeatPasswordField).SendKeys(passwordRepeate);
        }
        public void SelectTermsAndConditions()
        {
            
            if (!driver.FindElement(TermsAndConditionsCheckbox).Selected)
                {
                    driver.FindElement(TermsAndConditionsCheckbox).Click(); 
                }
        }
        public void ClickSignUpButton()
        {
            if(driver.FindElement(SignUpButton).Enabled)
            {
                driver.FindElement(SignUpButton).Click();
                Thread.Sleep(5000);
            }
            else{ Console.WriteLine("Sign Up button is disabled");}
        }
    }
}