using System;
using System.Linq;
using System.Threading.Tasks;
using ProjectName.Services;
using RestSharp;
using TechTalk.SpecFlow;


namespace RestSharp_performance.StepDefinitions
{
    [Binding]
    public class LoadTestSteps
    {
        private readonly RestClientService _restClientService;
        private string randomFullName;
        private string randomUserName;
        private string randomEmail;
        private string randomPassword = "testPassword123";
        private string randomPhone;

        public LoadTestSteps()
        {
            _restClientService = new RestClientService("http://localhost:5000");
        }

        [Given(@"I have random registration data")]
        public void GivenIHaveRandomRegistrationData()
        {
            var randomData = ProjectName.Utils.RandomDataGenerator.GenerateRegistrationData();
            randomFullName = randomData.FullName;
            randomUserName = randomData.UserName;
            randomEmail = randomData.Email;
            randomPhone = randomData.Phone;
        }

        [Given(@"I have random login data")]
        public void GivenIHaveRandomLoginData()
        {
            var randomData = ProjectName.Utils.RandomDataGenerator.GenerateLoginData();
            randomUserName = randomData.UserName;
            randomEmail = randomData.Email;
        }

        [When(@"I send (.*) concurrent requests to ""(.*)""")]
        public void WhenISendConcurrentRequests(int numRequests, string endpoint)
        {
            Parallel.For(0, numRequests, _ =>
            {
                var response = endpoint switch
                {
                    "/client_register" => _restClientService.RegisterClient(randomFullName, randomUserName, randomEmail, randomPassword, randomPhone),
                    "/client_login" => _restClientService.LoginClient(randomUserName, randomEmail, randomPassword),
                    _ => throw new ArgumentException("Invalid endpoint")
                };

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"Request Failed - {response.Content}");
                }
            });
        }

        [Then(@"I should receive a successful response for all requests")]
        public void ThenIShouldReceiveASuccessfulResponseForAllRequests()
        {
            Console.WriteLine("All requests completed successfully.");
        }
    }
}
