using NUnit.Framework;
using ProjectName.Services; // Import the RestClientService
using ProjectName.Utils;    // Import the RandomDataGenerator

[TestFixture]
public class RestSharpTests
{
    private static string baseUrl = "http://localhost:5000"; // Replace with your actual server URL
    private RestClientService _restClientService;

    [SetUp]
    public void SetUp()
    {
        // Initialize RestClientService
        _restClientService = new RestClientService(baseUrl);
    }

    [Test]
    public void TestClientRegister()
    {
        // Generate random data for testing registration
        var randomData = RandomDataGenerator.GenerateRegistrationData();

        // Register the client using random data
        var response = _restClientService.RegisterClient(
            randomData.FullName,
            randomData.UserName,
            randomData.Email,
            "testPassword123", // Fixed password for simplicity
            randomData.Phone
        );

        // Assert that the response status code is 200 OK
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode,
            $"Failed to register client. Response: {response.Content}");
    }

    [Test]
    public void TestClientLogin()
    {
        // Generate random data for login (assuming the account exists)
        var randomData = RandomDataGenerator.GenerateLoginData();

        // Attempt to log in
        var response = _restClientService.LoginClient(
            randomData.UserName,
            randomData.Email,
            "testPassword123" // Assuming the same fixed password
        );

        // Assert that the response status code is 200 OK
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode,
            $"Failed to log in. Response: {response.Content}");
    }
}
