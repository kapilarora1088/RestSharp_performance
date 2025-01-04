using RestSharp;
using System;

public class LoadTesting
{
    private static string baseUrl = "http://localhost:5000"; // Replace with your actual server URL

    [Test]
    public void PerformLoadTest()
    {
        var clientRegisterPage = new ClientRegisterPage(baseUrl);

        int numberOfRequests = 100; // Total requests to send
        for (int i = 0; i < numberOfRequests; i++)
        {
            var fullName = "John Doe " + i;  // Dynamic full name for each request
            var username = "testUser" + new Random().Next(1000, 9999);  // Random username
            var email = username + "@example.com";  // Create email based on username
            var password = "testPassword123";  // Predefined password
            var phone = "123456789"+i ;  // Unique phone number for each request
          

            // Send the registration request with the updated payload
            var response = clientRegisterPage.RegisterClient(fullName, username, email, password, phone);

            // Log the status code for each request
            Console.WriteLine($"Request #{i + 1} - Response Code: {response.StatusCode}");
        }
    }
}
