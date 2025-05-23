using Gobigreviews.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using Gobigreviews.Utils;

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
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
        //Universal methods:
        public bool IsLogoDisplayed() => ElementHelper.IsDisplayed(driver, Logo);
        public string GetLogoHeight() => ElementHelper.GetAttribute(driver, Logo, "height");
        public string GetLogoWidth() => ElementHelper.GetAttribute(driver, Logo, "width");
        public bool IsNameDisplayed() => ElementHelper.IsDisplayed(driver, NameField);
        public string GetNamePlaceholder() => ElementHelper.GetAttribute(driver,NameField, "placeholder");
        public bool IsEmailDisplayed() => ElementHelper.IsDisplayed(driver, EmailField);
        public bool IsPasswordDisplayed() => ElementHelper.IsDisplayed(driver, PasswordField);
        public bool IsRepeatPasswordDisplayed() => ElementHelper.IsDisplayed(driver, RepeatPasswordField);
        public bool IsTermAndConditionsDisplayed() => ElementHelper.IsDisplayed(driver, TermsAndConditionsCheckbox);
        public bool IsSubcribeForNewsletterDisplayed() => ElementHelper.IsDisplayed(driver, SubcribeForNewsletterCheckbox);
        public bool IsSignUpButtonDisplayed() => ElementHelper.IsDisplayed(driver, SignUpButton);
        public bool IsUserProfileButtonDisplayed() => ElementHelper.IsDisplayed(driver, UserProfileButton);
        public bool IsEmailTakenErrorDisplayed() => ElementHelper.IsDisplayed(driver, EmailTakenValidationError);

   
        public void EnterUserName(string name) => ElementHelper.Type(driver, NameField, name);
        public void EnterUserEmail(string email) => ElementHelper.Type(driver, EmailField, email);
        public void EnterUserPassword(string password) => ElementHelper.Type(driver, PasswordField, password);
        public void EnterUserPassword_Repeat(string passwordRepeat) => ElementHelper.Type(driver, RepeatPasswordField, passwordRepeat);
        // public void SelectTermsAndConditions()
        // {
            
        //     if (!driver.FindElement(TermsAndConditionsCheckbox).Selected)
        //         {
        //             driver.FindElement(TermsAndConditionsCheckbox).Click(); 
        //         }
        // }
        public void SelectTermsAndConditions() => ElementHelper.Click(driver, TermsAndConditionsCheckbox);
        // public void ClickSignUpButton()
        // {
        //     if(driver.FindElement(SignUpButton).Enabled)
        //     {
        //         driver.FindElement(SignUpButton).Click();
        //     }
        //     else{ Console.WriteLine("Sign Up button is disabled");}
        // }
        public void ClickSignUpButton() => ElementHelper.Click(driver, SignUpButton);
        public void UserRegistrationSuccessful() =>  ElementHelper.IsDisplayed(driver, UserProfileButton);
        public void UserRegistrationFailed() => ElementHelper.IsDisplayed(driver, EmailTakenValidationError);

        
    }
}