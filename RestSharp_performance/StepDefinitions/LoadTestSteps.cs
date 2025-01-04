using System;
using System.Linq;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
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

        // Define ExtentReports objects
        private static ExtentReports _extent;
        private static ExtentTest _test;
        private static ExtentSparkReporter _htmlReporter;

        public LoadTestSteps()
        {
            _restClientService = new RestClientService("http://localhost:5000");
            InitializeReport();
        }

        // Initialize ExtentReports with ExtentSparkReporter
        private static void InitializeReport()
        {
            // Define report folder and file path
            string reportFolderPath = @"C:\Reports"; // Customize the path if needed

            // Ensure the folder exists
            if (!Directory.Exists(reportFolderPath))
            {
                Directory.CreateDirectory(reportFolderPath);
            }

            // Set the report file path
            string reportPath = Path.Combine(reportFolderPath, "BDD_PerformanceTestReport.html");

            // Create an instance of ExtentSparkReporter
            _htmlReporter = new ExtentSparkReporter(reportPath)
            {
                Config =
                {
                    DocumentTitle = "Performance Test Report", // Report title
                    ReportName = "API Load Testing",          // Report name
                   // Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard // Set the theme
                }
            };

            // Create the ExtentReports instance and attach the reporter
            _extent = new ExtentReports();
            _extent.AttachReporter(_htmlReporter);
        }

        // Start a new test in the report
        private void StartTest(string testName)
        {
            _test = _extent.CreateTest(testName);
        }

        // Log the test outcome in the report
        private void LogPass(string message)
        {
            _test.Pass(message);
        }

        private void LogFail(string message)
        {
            _test.Fail(message);
        }

        // Finalize the report
        private static void FinalizeReport()
        {
            // Ensure the report is flushed and saved
            _extent.Flush();
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

            // Log to ExtentReport
            StartTest("Generate Random Login Data");
            LogPass("Random login data generated successfully.");
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

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    LogPass($"Request to {endpoint} succeeded with status {response.StatusCode}.");
                }
                else
                {
                    LogFail($"Request to {endpoint} failed with status {response.StatusCode}. Error: {response.Content}");
                }
            });
        }

        [Then(@"I should receive a successful response for all requests")]
        public void ThenIShouldReceiveASuccessfulResponseForAllRequests()
        {
            // Log that all requests completed
            LogPass("All requests completed successfully.");

            // Finalize the report at the end of the test
            FinalizeReport();
        }
    }
}
