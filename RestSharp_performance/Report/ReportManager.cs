using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;

public class ReportManager
{
    private static ExtentSparkReporter spark = new ExtentSparkReporter("Spark.html");
    private static ExtentReports _extent;
    private static ExtentTest _test;

    public static void InitializeReport()
    {
        // Define the report path
        string reportDirectory = @"C:\Reports";
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string reportFilePath = Path.Combine(reportDirectory, $"PerformanceTestReport_{timestamp}.html");

        // Check if the directory exists, if not, create it
        if (!Directory.Exists(reportDirectory))
        {
            Directory.CreateDirectory(reportDirectory);
        }

        // Initialize the ExtentSparkReporter with the file path
        spark = new ExtentSparkReporter(reportFilePath);
        spark.Config.DocumentTitle = "Performance Test Report";
        spark.Config.ReportName = "API Performance Testing";
        //spark.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard; // Uncomment if needed

        _extent = new ExtentReports();
        _extent.AttachReporter(spark);
    }

    public static void StartTest(string testName)
    {
        _test = _extent.CreateTest(testName);
    }

    public static void LogPass(string message)
    {
        _test.Pass(message);
    }

    public static void LogFail(string message)
    {
        _test.Fail(message);
    }

    public static void FinalizeReport()
    {
        _extent.Flush();
    }
}
