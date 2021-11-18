using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using Xunit.Abstractions;
using MobileAutoTest.Utilities;
using Xunit;

namespace MobileAutoTest.IntegrationTest.Regression.Android
{
    public class VisualDevice_LoanMessageBoardSendData:IntegrationTestBase
    {
        public VisualDevice_LoanMessageBoardSendData(ITestOutputHelper output, Setup testSetup) : base(output, testSetup)
        {
        }
        protected AndroidDriver<AndroidElement> androidDriver;
        protected AppiumOptions appiumOptions = new AppiumOptions();

        [Fact]
        [Obsolete]
        public void 房貸留言版_填表單_AndroidDevice()
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
           
            androidDriver.Navigate().GoToUrl("https://www.esunbank.com.tw/bank/personal/loan/tools/apply/house-loan");
            androidDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(600); //600秒內載完網頁內容, 否則報錯, 載完提早進下一步.


            CreateReport(" AndroidDevice_房貸留言版填表單", "York", androidDriver);


            /// 欲申貸額度, 貸款用途
            androidDriver.FindElement(By.XPath("//*[@id='loanAmount']")).SendKeys("40"); // 1. 額度
            androidDriver.FindElementByXPath("//*[@id='loanPurpose']").Click(); // 2. 點貸款用途下拉選單
            Random ran = new Random();
            var ranLoanUse = ran.Next(2, 12); // random 選單選項
            androidDriver.FindElementByXPath($"//*[@id='loanPurpose']/option[{ranLoanUse}]").Click(); // 隨機選選項
            INFO(TestBase.PageSnapshotToReport_Android(androidDriver));


            /// 申請人姓名,身分證,行動電話, 駐地電話,電子郵件
            androidDriver.PressKeyCode(new KeyEvent(AndroidKeyCode.Keycode_PAGE_DOWN));
            androidDriver.FindElementByXPath("//*[@id='name']").SendKeys("AutoTest"); // 3. 申請人姓名
            androidDriver.FindElementByXPath("//*[@id='citizenId']").SendKeys("a123456789"); // 4. 身份證字號
            androidDriver.FindElementByXPath("//*[@id='cellphone']").SendKeys("0987654321"); // 5. 行動電話
            INFO(TestBase.PageSnapshotToReport_Android(androidDriver));


            ///通訊地址, 房屋位置, 服務據點, 方便聯絡時間, 備註
            androidDriver.PressKeyCode(new KeyEvent(AndroidKeyCode.Keycode_PAGE_DOWN));
            System.Threading.Thread.Sleep(300);
            androidDriver.FindElementByXPath("//*[@id='addressCity']").Click(); // 6. 通訊地址-縣市-下拉選單
            var ranCity = ran.Next(2, 24); // random 縣市選單選項範圍
            androidDriver.FindElementByXPath($"//*[@id='addressCity']/option[{ranCity }]").Click(); // 隨機選選項
            androidDriver.FindElementByXPath("//*[@id='addressDistrict']").Click(); // 通訊地址-鄉鎮市區-下拉選單
            androidDriver.FindElementByXPath("//*[@id='addressDistrict']/option[2]").Click(); // 第一個鄉鎮

            androidDriver.FindElementByXPath("//*[@id='address']").SendKeys("中山路一路2段3巷4弄5號"); // 7. 詳細地址

            androidDriver.FindElementByXPath("//*[@id='houseCity']").Click(); // 8. 房屋位置-縣市-下拉選單
            var ranHouse = ran.Next(2, 24); // random 縣市選單選項範圍
            androidDriver.FindElementByXPath($"//*[@id='houseCity']/option[{ranHouse}]").Click(); // 隨機選選項
            androidDriver.FindElementByXPath("//*[@id='houseDistrict']").Click(); // 房屋位置-鄉鎮市區-下拉選單
            androidDriver.FindElementByXPath("//*[@id='houseDistrict']/option[2]").Click(); // 第一個鄉鎮

            androidDriver.FindElementByXPath("/html/body/section[2]/div/div[1]/div[11]/div/div[1]/label").Click(); // 9. 點服務據點-靠近申貸房屋位置

            androidDriver.FindElementByXPath("//*[@id='contactTime']").Click(); // 10. 方便聯絡時間下拉選單
            var ranContactTime = ran.Next(2, 5); // 隨機時段
            androidDriver.FindElementByXPath("//*[@id='contactTime']/option[2]").Click(); // 隨機選選項
            INFO(TestBase.PageSnapshotToReport_Android(androidDriver));


            ///我已閱讀, 送出
            androidDriver.PressKeyCode(new KeyEvent(AndroidKeyCode.Keycode_PAGE_DOWN));
            androidDriver.FindElementByXPath("/html/body/section[2]/div/div[2]/label").Click(); // 11. 我已閱讀並同意

            //androidDriver.FindElementByXPath("/html/body/section[2]/div/div[3]/div/div/div/a[2]").Click(); // 12. 送出

            INFO(TestBase.PageSnapshotToReport_Android(androidDriver));

            

            // Close the browser.
            CloseTest();
            androidDriver.Close();
            androidDriver.Quit();

        }
    }
}