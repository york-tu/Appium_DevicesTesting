using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using Xunit;

namespace Xunit_Appium_DevicesTesting_1109
{
    public class XUnit_Appium_DeviceTesting_Samsung
    {
        [Fact]
        [Obsolete]
        public void 房貸留言版()
        {
            // Set the desired capabilities.
            DesiredCapabilities dc = new DesiredCapabilities();
            dc.SetCapability("platformName", "Android");
            dc.SetCapability("platformVersion", "6.0.1");
            dc.SetCapability("deviceName", "SAMSUNG_S6_Edge");
            dc.SetCapability("browserName", "Chrome");
            dc.SetCapability("noReset", true);
            dc.SetCapability("automationName", "UIAutomator2");
            dc.SetCapability("recreateChromeDriverSessions", true);
            dc.SetCapability("noSign", true);
            dc.SetCapability("autoGrantPermissions", true);
            dc.SetCapability("autoAcceptAlerts", true);


            IWebDriver _driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4723/wd/hub"), dc, TimeSpan.FromMinutes(5))
            {
                Url = "https://www.esunbank.com.tw/bank/personal/loan/tools/apply/house-loan?dev=mobile" // 0. Browse 房貸留言版M版
            };

            _driver.FindElement(By.XPath("//*[contains(@name, 'loanAmount')]")).SendKeys("50"); // 1. 額度
            _driver.FindElement(By.XPath("//*[contains(@name, 'loanUse')]")).Click(); // 2. 貸款用途下拉選單
            Random ran = new Random();
            var ranLoanUse = ran.Next(1, 10); // random 選單選項
            Actions fingerAction = new Actions(_driver); // new "action" method
            for (int i = 1; i <= ranLoanUse; i++)
            {
                if (i == ranLoanUse)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // 執行選項
                    _driver.FindElement(By.XPath("//*[contains(@name, 'loanUse')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }

            _driver.FindElement(By.XPath("//*[contains(@name, 'name')]")).SendKeys("AutoTest"); // 3. 申請人姓名
            _driver.FindElement(By.XPath("//*[contains(@name, 'citizenId')]")).SendKeys("a123456789"); // 4. 身份證字號
            _driver.FindElement(By.XPath("//*[contains(@name, 'tel')]")).SendKeys("0987654321"); // 5. 連絡電話
            _driver.FindElement(By.XPath("//*[contains(@name, 'cellPhone')]")).SendKeys("0987654321"); // 6. 行動電話
            _driver.FindElement(By.XPath("//*[contains(@name, 'email')]")).SendKeys("test@gmail.com"); // 7. 電子郵件


            _driver.FindElement(By.XPath("//*[contains(@name, 'city')]")).Click(); // 8. 通訊地址-縣市-下拉選單
            var ranCity = ran.Next(1, 23); // random 縣市選單選項範圍
            for (int i = 1; i <= ranCity; i++)
            {
                if (i == ranCity)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // 執行選項
                    _driver.FindElement(By.XPath("//*[contains(@name, 'city')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }


            _driver.FindElement(By.XPath("//*[contains(@name, 'region')]")).Click(); // 9. 通訊地址-鄉鎮市區-下拉選單
            var ranRegion = ran.Next(1, 2); // random 鄉鎮市區選項範圍
            for (int i = 1; i <= ranRegion; i++)
            {
                if (i == ranRegion)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // 執行選項
                    _driver.FindElement(By.XPath("//*[contains(@name, 'region')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }

            _driver.FindElement(By.XPath("//*[contains(@name, 'addrline')]")).SendKeys("中山路一路2段3巷4弄5號"); // 10. 詳細地址

            fingerAction.SendKeys(Keys.PageDown).Build().Perform(); // scroll down page

            _driver.FindElement(By.XPath("//*[contains(@name, 'locCity')]")).Click(); // 11. 房屋位置-縣市-下拉選單
            var ranLocCity = ran.Next(1, 23); // random 縣市選單選項範圍
            for (int i = 1; i <= ranLocCity; i++)
            {
                if (i == ranLocCity)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // 執行選項
                    _driver.FindElement(By.XPath("//*[contains(@name, 'locCity')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }


            _driver.FindElement(By.XPath("//*[contains(@name, 'locRegion')]")).Click(); // 12. 房屋位置-鄉鎮市區-下拉選單
            var ranLocRegion = ran.Next(1, 2); // random 鄉鎮市區選項範圍
            for (int i = 1; i <= ranLocRegion; i++)
            {

                if (i == ranLocRegion)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // 執行選項
                    _driver.FindElement(By.XPath("//*[contains(@name, 'locRegion')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }

            _driver.FindElement(By.ClassName("last-child")).Click(); // 13. 點服務據點-靠近申貸房屋位置


            _driver.FindElement(By.XPath("//*[contains(@name, 'contactTime')]")).Click(); // 14. 方便聯絡時間下拉選單
            var ranContactTime = ran.Next(1, 5); // random 鄉鎮市區選項範圍
            for (int i = 1; i <= ranContactTime; i++)
            {
                if (i == ranLocRegion)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // 執行選項
                    _driver.FindElement(By.XPath("//*[contains(@name, 'contactTime')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }

            _driver.FindElement(By.ClassName("trans-element-checkbox")).Click(); // 15. 我已閱讀並同意

            //_driver.FindElement(By.Id("submit")).Click(); // 16. 送出

            // Close the browser.
            _driver.Quit();


        }
    }
}
