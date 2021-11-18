using System;
using Excel = Microsoft.Office.Interop.Excel;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using Xunit.Abstractions;
using MobileAutoTest.Utilities;
using Xunit;
using System.Globalization;

namespace MobileAutoTest.IntegrationTest.Regression.Android
{
    public class VisualDevice_PageContent:IntegrationTestBase
    {
        public VisualDevice_PageContent(ITestOutputHelper output, Setup testSetup) : base(output, testSetup)
        {
        }
        public AndroidDriver<AndroidElement> androidDriver;
        public AppiumOptions appiumOptions = new AppiumOptions();

        [Theory]
        #region 工作表對應網址分類
        /*
        工作表 1:      "/bank/personal",
        工作表 2:      "/bank/personal/deposit",
        工作表 3:      "/bank/personal/loan", 
        工作表 4:      "/bank/personal/credit-card",
        工作表 5:      "/bank/personal/wealth", 
        工作表 6:      "/bank/personal/trust", 
        工作表 7:      "/bank/personal/insurance",
        工作表 8:      "/bank/personal/lifefin", 
        工作表 9:      "/bank/personal/apply", 
        工作表 10:     "/bank/personal/event",
        工作表 11:     "/bank/small-business", 
        工作表 12:     "/bank/corporate", 
        工作表 13:     "/bank/digital", 
        工作表 14:     "/bank/about", 
        工作表 15:     "/bank/marketing",
        工作表 16:     "/bank/iframe/widget", 
        工作表 17:     "/bank/error", 
        工作表 18:     "/bank/bank-en",
        工作表 19:     "/bank/preview";
        */
        #endregion
        [InlineData(new int[] {8})]
        public void 檢查網頁內容_AndroidDevice(int[] sheet)
        {

            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build(); // Creates local Appium server instance
            _appiumLocalService.Start(); // Starts local Appium server instance

            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "OnePlus 5");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.Udid, "emulator-5554");
            appiumOptions.AddAdditionalCapability("browserName", "chrome");
            appiumOptions.AddAdditionalCapability("chromedriverExecutable", @"C:\Users\axn01\Desktop\DeviceAutomation\ChromeDriversPackage\chromedriver.exe");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.NoReset, false);
            androidDriver = new AndroidDriver<AndroidElement>(_appiumLocalService, appiumOptions); // initialize Android driver on local server instance


            CreateReport("網頁內容檢查_Android", "York",androidDriver);

            Excel.Application excel_App = new Excel.Application(); //  新增excel應用程序

            #region step 1: 讀取sitemap excel內容到 excel_WB
            Excel.Workbook excel_WB = excel_App.Workbooks.Open(TestBase.sitemapsExcelPath); // pass指定路徑excel全部內容to "excel_WB"
            #endregion

            #region step 2: 讀取 expect result excel (expectResult.xlsx)第一行內容 (excel_Expect_xxxx) 到 expectResultArray 陣列中
            Excel.Workbook excel_Expect_WB = excel_App.Workbooks.Open($"{TestBase.Upperfolderpath}testdata\\ExpectResult_0323.xlsx"); // open 指定路徑excel
            Excel.Worksheet excel_Expect_WS = (Excel.Worksheet)excel_Expect_WB.Worksheets[1]; // 指定讀取excel 檔第一個工作表
            int sheetRows = 4; // 工作表內行數 
            Excel.Range expectResultRange = (Excel.Range)excel_Expect_WS.UsedRange; // export excel 內容 to Range
            string[] expectResultArray = new string[expectResultRange.Count / sheetRows];
            for (int i = 0; i < expectResultRange.Count / sheetRows; i++)
            {
                expectResultArray[i] = (string)((Excel.Range)expectResultRange.Cells[i + 1, 1]).Value; // 將excel第一行內容丟進expectResultArray陣列中
            }
            #endregion


            foreach (var index in sheet)
            {
                Excel.Worksheet excel_WS = (Excel.Worksheet)excel_WB.Worksheets[index]; //  讀sitemaps excel指定工作表內容
                Excel.Range range = excel_WS.UsedRange; // 撈出工作表內容, pass to "range"
                INFO(excel_WS.Name);

                for (int i = 0; i < range.Count; i++)
                {
                    string strURL = (string)((Excel.Range)range.Cells[i + 1, 1]).Value; // 讀出第 i 行URL
                    int meetExpectResultURLIndex = Array.IndexOf(expectResultArray, strURL); // 搜尋目標URL對應到expect result 陣列中的位置

                    if (meetExpectResultURLIndex == -1) // 該URL在對應表上搜尋不到
                    {
                        WARNING($"[新增/變動] URL:  {strURL}");
                        continue;
                    }
                    else
                    {
                        string cssSelector = (string)((Excel.Range)expectResultRange.Cells[meetExpectResultURLIndex + 1, 2]).Value; // 讀 expect result excel中第二行欄位值
                        string expectString = (string)((Excel.Range)expectResultRange.Cells[meetExpectResultURLIndex + 1, 3]).Value;  // 讀 expect result excel中第三行欄位值

                        ((IJavaScriptExecutor)androidDriver).ExecuteScript("window.open();"); // 另開新頁
                        //androidDriver.SwitchTo().Window(androidDriver); // focus on 新頁上 
                        androidDriver.Navigate().GoToUrl(strURL);
                        #region 當網頁自動切到M版時, click "切換電腦版" 強制切回PC版
                        //if (androidDriver.Url.ToString().Contains("?dev=mobile")) // workaround: 當網頁自動切到M版時, 強制切回PC版
                        //{
                        //    TestBase.ScrollPageUpOrDown(driver, 1500);
                        //    androidDriver.FindElementByClassName("changeTarget").Click();
                        //}
                        #endregion
                        androidDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(600); //600秒內載完網頁內容, 否則報錯, 載完提早進下一步.
                        if (androidDriver.Url.ToString() != strURL) // 檢查網頁開啟當下網址是否為輸入網址 (判斷網頁是否有redirect)
                        {
                            WARNING($"[Page Redirect], {driver.Url.ToString()}  (Expect: {strURL})");
                            WARNING(TestBase.PageSnapshotToReport_Android(androidDriver));
                            continue;
                        }
                        else if (strURL == "https://easyfee.esunbank.com.tw/index.action")
                        {
                            WARNING($"{strURL}, Keyword: {expectString}");
                            WARNING(TestBase.PageSnapshotToReport_Android(androidDriver));
                            continue;
                        }
                        else if (androidDriver.Url.ToString() == "https://www.esunbank.com.tw/bank/personal/event/calendar/events") // Workaround 1 : 活動日曆URL >>> 需抓當天日期
                        {
                            string day_of_week = DateTime.Now.ToString("dddd", new CultureInfo("en-us")); // 英文星期幾(e.g., Wednesday)
                            string day = DateTime.Now.ToString("dd"); // 日期 (e.g., 23)
                            string month = DateTime.Now.ToString("MMMM", new CultureInfo("zh-cn")); // 中文月份 (e.g., 三月)
                            expectString = $"{day}\r\n{day_of_week}\r\n{month}";
                        }
                        else if (androidDriver.Url.ToString().EndsWith("/pages")) // Workaround 2 : 網址為 ".../page" >>> 網頁為空內容
                        {
                            expectString = "";
                        }
                        try
                        {
                            Assert.Contains(expectString, androidDriver.FindElement(By.CssSelector(cssSelector)).Text); // 判斷element 字串是否符合預期
                            PASS($"{strURL}, Keyword: {expectString}");
                            PASS(TestBase.PageSnapshotToReport_Android(androidDriver));
                        }
                        catch (Exception e)
                        {
                            FAIL($"{strURL}, Exception:{e.Message}");
                            FAIL(TestBase.PageSnapshotToReport_Android(androidDriver));
                        }
                        //driver.SwitchTo().Window(driver.WindowHandles.Last()).Close(); // 關掉新頁
                        //driver.SwitchTo().Window(driver.WindowHandles.First()); // 切回原頁
                    }
                }
            }
            #region 關閉 & 釋放文件
            excel_WB.Close();
            excel_Expect_WB.Close();
            excel_App.Quit();
            #endregion

            CloseTest();
            androidDriver.Close();
            androidDriver.Quit();
        }
    }
}