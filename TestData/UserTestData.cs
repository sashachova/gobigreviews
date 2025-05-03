namespace Gobigreviews.TestData
{
    public static class UserTestData
    {
        public const string validUserName = "John";
        public static readonly string  validUserEmail = $"john.doe+{Guid.NewGuid()}@example.com";
        public const string validUserPassword = "Asd1234!";
        public const string usedEmail = "test@new.com";

    }
}