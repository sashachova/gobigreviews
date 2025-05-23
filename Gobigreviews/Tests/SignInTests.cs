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

    public class SignInTests

    {

        private IWebDriver driver;

        private HomePage homePage;

        private SignInPageComponents signIn;

        public SignUpPageComponents signUp { get; private set; }
 
        [SetUp]

        public void SetUp()

        {

            driver = WebDriverFactory.Create();

            homePage = new HomePage(driver);

            signUp = new SignUpPageComponents(driver);

            signIn = new SignInPageComponents(driver);

            homePage.Open();

            homePage.ClickSignInLink();

        }
 
        [Test]

        [AllureName("Successful User Sign In")]

        [AllureDescription("Verifies that a user can sign in  with valid data.")]

        [AllureSeverity(SeverityLevel.critical)] // Uses Allure.Net.Commons.SeverityLevel

        public void SignIn_UserIsLoggedInWithAllValidData()

        {

            signIn.EnterUserEmail(UserTestData.usedEmail);

            signIn.EnterUserPassword(UserTestData.validUserPassword);

            signIn.ClickSignInButton();

            signUp.UserRegistrationSuccessful();
 
            Assert.That(signUp.IsUserProfileButtonDisplayed(), "User profile button is not displayed");

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
 