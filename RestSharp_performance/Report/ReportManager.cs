using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class ReportManager
{
    private static ExtentReports _extent;
    private static ExtentTest _test;
    private static Stopwatch _stopwatch = new Stopwatch();
    private static int _numberOfRequests = 0;

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

        // Initialize ExtentReports instance
        _extent = new ExtentReports();

        // Create ExtentSparkReporter with the file path
        var spark = new ExtentSparkReporter(reportFilePath);
        spark.Config.DocumentTitle = "Performance Test Report";
        spark.Config.ReportName = "API Performance Testing";
        //spark.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard; // Uncomment if needed

        // Attach the reporter to ExtentReports
        _extent.AttachReporter(spark);

        _stopwatch.Reset();
        _stopwatch.Start();
        _numberOfRequests = 0;
    }

    public static void StartTest(string testName)
    {
        _test = _extent.CreateTest(testName);
    }

    public static void LogPass(string message)
    {
        _test.Pass(message);
        _numberOfRequests++;
    }

    public static void LogFail(string message)
    {
        _test.Fail(message);
        _numberOfRequests++;
    }

    public static void FinalizeReport()
    {
        _stopwatch.Stop();
        long elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        double throughput = _numberOfRequests / (elapsedMilliseconds / 1000.0);

        _test.Info($"Total Requests: {_numberOfRequests}");
        _test.Info($"Elapsed Time: {elapsedMilliseconds} ms");
        _test.Info($"Throughput: {throughput:F2} requests/second");

        _extent.Flush();
    }
}