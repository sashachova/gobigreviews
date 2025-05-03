using NUnit.Framework;
using OpenQA.Selenium;
using Gobigreviews.Drivers;
using Gobigreviews.Pages;
using Gobigreviews.Pages.Components;
using Gobigreviews.TestData;

namespace Gobigreviews.Tests
{
    public class SignUpTests
    {
        private IWebDriver driver;
        private HomePage homePage;
        private SignInPageComponents signIn;
        public SignUpPageComponents SignUp
            {
                get;
                private set;
            }


        [SetUp]
        public void SetUp()
        {
            driver = WebDriverFactory.Create();
            homePage = new HomePage(driver);
            SignUp = new SignUpPageComponents(driver);
            homePage.Open();
            homePage.ClickSignInLink();
            signIn = new SignInPageComponents(driver);
            signIn.ClickSignUpLink();
            
        }

        [Test]
        public void SignUp_ShouldHaveLogoLink()
        {
            Assert.That(SignUp.IsLogoDisplayed(), "Logo is not displayed");
            Assert.That(SignUp.GetLogoHeight(), Is.EqualTo("81"));
            Assert.That(SignUp.GetLogoWidth(), Is.EqualTo("170"));
        }
        [Test]
        public void SignUp_RegistrationFormIsDisplayed()
        {
            Assert.That(SignUp.IsNameDisplayed(), "Name field is not displayed");
            Assert.That(SignUp.GetNamePlaceholder(), Is.EqualTo("Name"), "Name placeholder is incorrect");
            Assert.That(SignUp.IsEmailDisplayed(), "Email field is not displayed");
            Assert.That(SignUp.IsPasswordDisplayed(), "Password field is not displayed");
            Assert.That(SignUp.IsRepeatPasswordDisplayed(), "Repeate password field is displayed");
            Assert.That(SignUp.IsTermAndConditionsDisplayed(), "Terms and conditions checkbox is not displayed");
            Assert.That(SignUp.IsSubcribeForNewsletterDisplayed(), "Subscribe for newsletter checkbox is not displayed");
            Assert.That(SignUp.IsSignUpButtonDisplayed(), "Sign Up button is not displayed");
        }
        [Test]
        public void SignUp_UserIsRegisteredWithAllValidData()
        {
            SignUp.EnterUserName(UserTestData.validUserName);
            SignUp.EnterUserEmail(UserTestData.validUserEmail);
            SignUp.EnterUserPassword(UserTestData.validUserPassword);
            SignUp.EnterUserPassword_Repeate(UserTestData.validUserPassword);
            SignUp.SelectTermsAndConditions();
            SignUp.ClickSignUpButton();

            Assert.That(SignUp.IsUserProfileButtonDisplayed, "User profile button is not displayed");
        }
        [Test]
        public void SignUp_ValidationErrorIsDisplayedWhenEmailHasAlreadyBeenTaken()
        {
            SignUp.EnterUserName(UserTestData.validUserName);
            SignUp.EnterUserEmail(UserTestData.usedEmail);
            SignUp.EnterUserPassword(UserTestData.validUserPassword);
            SignUp.EnterUserPassword_Repeate(UserTestData.validUserPassword);
            SignUp.SelectTermsAndConditions();
            SignUp.ClickSignUpButton();

            Assert.That(SignUp.IsEmailTakenErrorDisplayed, "Validation error is not displayed when email has already been taken");
        }

        [TearDown]
        public void TearDown()
    {
        try
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during teardown: {ex.Message}");
            }
            finally
            {
                driver = null;
            }
           
        }
    }
}