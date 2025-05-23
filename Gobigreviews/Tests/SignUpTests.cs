using Allure.Net.Commons; 

using Allure.NUnit;

using Allure.NUnit.Attributes;

using NUnit.Framework;

using OpenQA.Selenium;

using Gobigreviews.Drivers;

using Gobigreviews.Pages;

using Gobigreviews.Pages.Components;

using Gobigreviews.TestData;

using Gobigreviews.Utils;
 
namespace Gobigreviews.Tests

{

    [AllureNUnit]

    public class SignUpTests

    {

        private IWebDriver driver;

        private HomePage homePage;

        private SignInPageComponents signIn;

        public SignUpPageComponents SignUp { get; private set; }
 
        [SetUp]

        public void SetUp()

        {

            driver = WebDriverFactory.Create();

            homePage = new HomePage(driver);

            SignUp = new SignUpPageComponents(driver);

            signIn = new SignInPageComponents(driver);

            homePage.Open();

            homePage.ClickSignInLink();

            signIn.ClickSignUpLink();

        }
 
        [Test]

        [AllureName("Verify Logo Link on SignUp Page")]

        [AllureDescription("Checks if the logo is displayed with correct dimensions and verifies redirect on click.")]

        [AllureSeverity(SeverityLevel.normal)] // Uses Allure.Net.Commons.SeverityLevel

        public void SignUp_ShouldHaveLogoLink()

        {

            Assert.That(SignUp.IsLogoDisplayed(), "Logo is not displayed");

            Assert.That(SignUp.GetLogoHeight(), Is.EqualTo("81"));

            Assert.That(SignUp.GetLogoWidth(), Is.EqualTo("170"));

            // TO-DO: Verify redirect on logo click

        }
 
        [Test]

        [AllureName("Verify Registration Form Display")]

        [AllureDescription("Ensures all fields and elements in the registration form are displayed.")]

        [AllureSeverity(SeverityLevel.critical)] // Uses Allure.Net.Commons.SeverityLevel

        public void SignUp_RegistrationFormIsDisplayed()

        {

            Assert.That(SignUp.IsNameDisplayed(), "Name field is not displayed");

            Assert.That(SignUp.GetNamePlaceholder(), Is.EqualTo("Name"), "Name placeholder is incorrect");

            Assert.That(SignUp.IsEmailDisplayed(), "Email field is not displayed");

            Assert.That(SignUp.IsPasswordDisplayed(), "Password field is not displayed");

            Assert.That(SignUp.IsRepeatPasswordDisplayed(), "Repeat password field is not displayed");

            Assert.That(SignUp.IsTermAndConditionsDisplayed(), "Terms and conditions checkbox is not displayed");

            Assert.That(SignUp.IsSubcribeForNewsletterDisplayed(), "Subscribe for newsletter checkbox is not displayed");

            Assert.That(SignUp.IsSignUpButtonDisplayed(), "Sign Up button is not displayed");

        }
 
        [Test]

        [AllureName("Successful User Registration")]

        [AllureDescription("Verifies that a user can register with valid data.")]

       [AllureSeverity(SeverityLevel.critical)] // Uses Allure.Net.Commons.SeverityLevel

        public void SignUp_UserIsRegisteredWithAllValidData()

        {

            SignUp.EnterUserName(UserTestData.validUserName);

            SignUp.EnterUserEmail(UserTestData.validUserEmail);

            SignUp.EnterUserPassword(UserTestData.validUserPassword);

            SignUp.EnterUserPassword_Repeat(UserTestData.validUserPassword);

            SignUp.SelectTermsAndConditions();

            SignUp.ClickSignUpButton();

            SignUp.UserRegistrationSuccessful();
 
            Assert.That(SignUp.IsUserProfileButtonDisplayed(), "User profile button is not displayed");

        }
 
        [Test]

        [AllureName("Validation Error for Duplicate Email")]

        [AllureDescription("Checks if validation error is shown when email is already taken.")]

       [AllureSeverity(SeverityLevel.normal)] // Uses Allure.Net.Commons.SeverityLevel

        public void SignUp_ValidationErrorIsDisplayedWhenEmailHasAlreadyBeenTaken()

        {

            SignUp.EnterUserName(UserTestData.validUserName);

            SignUp.EnterUserEmail(UserTestData.usedEmail);

            SignUp.EnterUserPassword(UserTestData.validUserPassword);

            SignUp.EnterUserPassword_Repeat(UserTestData.validUserPassword);

            SignUp.SelectTermsAndConditions();

            SignUp.ClickSignUpButton();

            SignUp.UserRegistrationFailed();
 
            Assert.That(SignUp.IsEmailTakenErrorDisplayed(), "Validation error is not displayed when email has already been taken");

        }
 
        [Test, TestCaseSource(typeof(UserTestData), nameof(UserTestData.ValidUsers))]

        [AllureName("SignUp with Faker-Generated Data")]

        [AllureDescription("Verifies registration with dynamically generated user data.")]

        [AllureSeverity(SeverityLevel.critical)] // Uses Allure.Net.Commons.SeverityLevel

        public void SignUp_WithFakerGeneratedData(string name, string email, string password)

        {

            SignUp.EnterUserName(name);

            SignUp.EnterUserEmail(email);

            SignUp.EnterUserPassword(password);

            SignUp.EnterUserPassword_Repeat(password);

            SignUp.SelectTermsAndConditions();

            SignUp.ClickSignUpButton();

            SignUp.UserRegistrationSuccessful();
 
            Assert.That(SignUp.IsUserProfileButtonDisplayed(), "User profile button is not displayed");

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
 