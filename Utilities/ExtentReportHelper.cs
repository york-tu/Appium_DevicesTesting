﻿using System;
using System.Collections.Generic;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium.Appium.Android;


namespace MobileAutoTest.Utilities
{
    public class ExtentReportHelper
    {
        private ExtentReports _extentReportsObject;
        private string _testName;
        private string _tester;
        private string _environment;
        private string _driverNameVersion;
        private string _deviceName;
        private string _deviceOSNameVersion;
        private string _deviceScreenSize;

        public ExtentTest ExtentTestObjects;

        List<TestResult> testReport = new List<TestResult>();

        public ExtentReportHelper(string testName, string tester, AndroidDriver<AndroidElement> androidDriver)
        {
            _tester = tester;
            _testName = testName;

        }

        public void Init()
        {
            _extentReportsObject = new ExtentReports();
            var filename = String.Format("{0}_{1}_{2}.html", _testName, _tester, DateTime.Now.ToString("yyyy-MM-dd_HHmm"));
            var _htmlReporter = new ExtentV3HtmlReporter($@"{TestBase.Upperfolderpath}\{filename}");
            _extentReportsObject.AttachReporter(_htmlReporter);
            _extentReportsObject.AddSystemInfo("測試案例", _testName);
            _extentReportsObject.AddSystemInfo("測試人員", _tester);
            _htmlReporter.Config.Theme = Theme.Dark; // theme - standard, dark
            _htmlReporter.Config.Encoding = "UTF-8"; // encoding, default = UTF-8
        }

        public void CreateTestCase(string caseName, string typeName = "")
        {
            if (_extentReportsObject == null)
            {
                Init();
            }

            string setName = string.IsNullOrEmpty(typeName) ? caseName : string.Format("{0}.{1}", typeName, caseName);
            ExtentTestObjects = _extentReportsObject.CreateTest(setName);
        }

        public void GetTestEnvironment(string environment)
        {
            _environment = environment;
            _extentReportsObject.AddSystemInfo("測試環境", _environment);
        }

        public void GetBrowserNameVersion(string driverNameVersion)
        {
            _driverNameVersion = driverNameVersion;
            _extentReportsObject.AddSystemInfo("瀏覽器", _driverNameVersion);
        }
        public void GetDeviceName(string deviceName)
        {
            _deviceName = deviceName;
            _extentReportsObject.AddSystemInfo("裝置", _deviceName);
        }
        public void GetOSNameVersion(string deviceOSNameVersion)
        {
            _deviceOSNameVersion = deviceOSNameVersion;
            _extentReportsObject.AddSystemInfo("作業系統", _deviceOSNameVersion);
        }
        public void GetDeviceScreenSize(string deviceScreenSize)
        {
            _deviceScreenSize = deviceScreenSize;
            _extentReportsObject.AddSystemInfo("螢幕解析度", _deviceScreenSize);
        }

        public void ExportReport()
        {
            _extentReportsObject.Flush();
        }

        public void AddTestResult (string testcasename)
        {
            List<ResultLog> resultLogs = new List<ResultLog>();
            testReport.Add(new TestResult { TestcaseName = testcasename, Result = resultLogs });
        }

        public class TestResult
        {
            public string TestcaseName { get; set; }
            public List<ResultLog> Result { get; set; }
        }

        public class ResultLog
        {
            public string Status { get; set; }
            public string Information { get; set; }
        }

    }
   
}
