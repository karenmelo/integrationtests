using Microsoft.VisualStudio.TestPlatform.TestHost;
using NerdStore.WebbApp.Tests.Config;

namespace NerdStore.WebbApp.Tests.UserTests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class UserTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;

        public UserTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Register successfully")]
        [Trait("Category", "Web Integration - User")]
        public async void User_Register_MustRunSuccessfully()
        {
            // Arrange
            var initialResponse = await _testsFixture.Client.GetAsync("/Identity/Account/Register");
            initialResponse.EnsureSuccessStatusCode();

            // Act

            // Assert

        }
    }
}
