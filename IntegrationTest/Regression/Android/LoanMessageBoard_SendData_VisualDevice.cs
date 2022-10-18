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
        public void �жU�d����_����_AndroidDevice()
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
            androidDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(600); //600�������������e, �_�h����, ���������i�U�@�B.


            CreateReport(" AndroidDevice_�жU�d��������", "York", androidDriver);


            /// ���ӶU�B��, �U�ڥγ~
            androidDriver.FindElement(By.XPath("//*[@id='loanAmount']")).SendKeys("40"); // 1. �B��
            androidDriver.FindElementByXPath("//*[@id='loanPurpose']").Click(); // 2. �I�U�ڥγ~�U�Կ��
            Random ran = new Random();
            var ranLoanUse = ran.Next(2, 12); // random ���ﶵ
            androidDriver.FindElementByXPath($"//*[@id='loanPurpose']/option[{ranLoanUse}]").Click(); // �H����ﶵ
            INFO(TestBase.PageSnapshotToReport_Android(androidDriver));


            /// �ӽФH�m�W,������,��ʹq��, �n�a�q��,�q�l�l��
            androidDriver.PressKeyCode(new KeyEvent(AndroidKeyCode.Keycode_PAGE_DOWN));
            androidDriver.FindElementByXPath("//*[@id='name']").SendKeys("AutoTest"); // 3. �ӽФH�m�W
            androidDriver.FindElementByXPath("//*[@id='citizenId']").SendKeys("a123456789"); // 4. �����Ҧr��
            androidDriver.FindElementByXPath("//*[@id='cellphone']").SendKeys("0987654321"); // 5. ��ʹq��
            INFO(TestBase.PageSnapshotToReport_Android(androidDriver));


            ///�q�T�a�}, �ЫΦ�m, �A�Ⱦ��I, ��K�p���ɶ�, �Ƶ�
            androidDriver.PressKeyCode(new KeyEvent(AndroidKeyCode.Keycode_PAGE_DOWN));
            System.Threading.Thread.Sleep(300);
            androidDriver.FindElementByXPath("//*[@id='addressCity']").Click(); // 6. �q�T�a�}-����-�U�Կ��
            var ranCity = ran.Next(2, 24); // random �������ﶵ�d��
            androidDriver.FindElementByXPath($"//*[@id='addressCity']/option[{ranCity }]").Click(); // �H����ﶵ
            androidDriver.FindElementByXPath("//*[@id='addressDistrict']").Click(); // �q�T�a�}-�m����-�U�Կ��
            androidDriver.FindElementByXPath("//*[@id='addressDistrict']/option[2]").Click(); // �Ĥ@�Ӷm��

            androidDriver.FindElementByXPath("//*[@id='address']").SendKeys("���s���@��2�q3��4��5��"); // 7. �ԲӦa�}

            androidDriver.FindElementByXPath("//*[@id='houseCity']").Click(); // 8. �ЫΦ�m-����-�U�Կ��
            var ranHouse = ran.Next(2, 24); // random �������ﶵ�d��
            androidDriver.FindElementByXPath($"//*[@id='houseCity']/option[{ranHouse}]").Click(); // �H����ﶵ
            androidDriver.FindElementByXPath("//*[@id='houseDistrict']").Click(); // �ЫΦ�m-�m����-�U�Կ��
            androidDriver.FindElementByXPath("//*[@id='houseDistrict']/option[2]").Click(); // �Ĥ@�Ӷm��

            androidDriver.FindElementByXPath("/html/body/section[2]/div/div[1]/div[11]/div/div[1]/label").Click(); // 9. �I�A�Ⱦ��I-�a��ӶU�ЫΦ�m

            androidDriver.FindElementByXPath("//*[@id='contactTime']").Click(); // 10. ��K�p���ɶ��U�Կ��
            var ranContactTime = ran.Next(2, 5); // �H���ɬq
            androidDriver.FindElementByXPath("//*[@id='contactTime']/option[2]").Click(); // �H����ﶵ
            INFO(TestBase.PageSnapshotToReport_Android(androidDriver));


            ///�ڤw�\Ū, �e�X
            androidDriver.PressKeyCode(new KeyEvent(AndroidKeyCode.Keycode_PAGE_DOWN));
            androidDriver.FindElementByXPath("/html/body/section[2]/div/div[2]/label").Click(); // 11. �ڤw�\Ū�æP�N

            //androidDriver.FindElementByXPath("/html/body/section[2]/div/div[3]/div/div/div/a[2]").Click(); // 12. �e�X

            INFO(TestBase.PageSnapshotToReport_Android(androidDriver));

            

            // Close the browser.
            CloseTest();
            androidDriver.Close();
            androidDriver.Quit();

        }
    }
}