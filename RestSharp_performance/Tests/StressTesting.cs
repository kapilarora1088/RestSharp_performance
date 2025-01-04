using RestSharp;
using System;
using System.Threading.Tasks;

public class StressTesting
{
    private static string baseUrl = "http://localhost:5000"; // Replace with your actual server URL

    [Test]
    public void PerformStressTestOnLogin()
    {

        ReportManager.InitializeReport();
        ReportManager.StartTest("Client Login Stress Test");

        var clientLoginPage = new ClientLoginPage(baseUrl);

        int totalRequests = 1000; // Total number of requests for stress testing
        int concurrentUsers = 50; // Number of concurrent threads simulating multiple users

        Parallel.For(0, concurrentUsers, user =>
        {
            for (int i = 0; i < totalRequests / concurrentUsers; i++)
            {
                try
                {
                    var username = "testUser" + new Random().Next(1000, 9999); // Random username
                    var email = username + "@example.com";  // Generate email based on username
                    var password = "testPassword123";  // Sample password for login

                    var response = clientLoginPage.LoginClient(username, email, password);

                    // Log only failed requests for stress testing
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ReportManager.LogPass($"Login Request succeeded for {username}");
                    }
                    else
                    {
                        ReportManager.LogFail($"Login Request failed for {username} with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during login attempt: {ex.Message}");
                }
            }
        });

        Console.WriteLine("Stress Test on Login Completed.");
        ReportManager.FinalizeReport();
    }
}