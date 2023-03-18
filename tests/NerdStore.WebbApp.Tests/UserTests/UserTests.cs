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
            var partOfUrl = "/Identity/Account/Register";
            var initialResponse = await _testsFixture.Client.GetAsync(partOfUrl);
            initialResponse.EnsureSuccessStatusCode();

            var antiForgeryToken = _testsFixture.GetAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            var email = "teste@teste.com";

            var formData = new List<KeyValuePair<string, string>>
            {
                KeyValuePair.Create("email", email),
                KeyValuePair.Create("Input.Password", "Teste@123"),
                KeyValuePair.Create("Input.ConfirmPassword", "Teste@123"),
                KeyValuePair.Create(_testsFixture.AntiForgeryFieldName, antiForgeryToken)
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, partOfUrl)
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            var responseString = await postResponse.Content.ReadAsStringAsync();
            postResponse.EnsureSuccessStatusCode();

            //responseString.
            //Assert.Contains($"<a id=\"confirm-link\" href=\"https://localhost:7195/Identity/Account/ConfirmEmail?userId=b84e220e-695b-4c3a-aaa6-82c7cf8da39f&amp;code=Q2ZESjhMT2p3T2JMeDFSS205M2loNjdqeFBoUjZweGxMV1p6OERrT2pQak8xbkc0c1JkTjlGb0hldTRWMWNoZTVXUEthSUprcHZDNzg3WXp3SGlQMkFWaHBUajgzYUNEVHFrcU5jZ1RLYU1ZUGRZMGZiZnJuNzlRenlHUmNBTEJ6cno3aittczhGQUF5cjdKTVNLeVl2cUc5bHRjVElkUUt1YkE1ZFBRNVNzNGVlSmd0M3hKZG5NNkVYanJBeWcyVk8vY1JzdWI3dXlNYkVxKzhnTXJXY00xZnZ1dSs2bUpZMHpUOWs0OWxtcXQ0dHBsNDgzQzVMM1gvaGtjYUJWY2dPVkx4UT09&amp;returnUrl=%2F\">Click here to confirm your account</a>");

            Assert.Contains($"Hello {email}!", responseString);
        }
    }
}
