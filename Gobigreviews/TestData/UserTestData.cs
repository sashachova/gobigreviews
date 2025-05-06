using System.Collections;
using Bogus;

namespace Gobigreviews.TestData
{
    public static class UserTestData
    {
        public const string validUserName = "John";
        public static readonly string  validUserEmail = $"john.doe+{Guid.NewGuid()}@example.com";
        public const string validUserPassword = "Asd1234!";
        public const string usedEmail = "test@new.com";

        public static IEnumerable ValidUsers
        {
            get
            {
                var faker = new Faker("en");
                for (int i = 0; i < 1; i++) 
                {
                    var name = faker.Name.FullName();
                    var email = faker.Internet.Email();
                    var password = faker.Internet.Password(12, true, "", "@1Aa"); 

                    yield return new TestCaseData(name, email, password)
                        .SetName($"SignUp_ValidUser_{i + 1}");
                }
            }
        }
    }

}