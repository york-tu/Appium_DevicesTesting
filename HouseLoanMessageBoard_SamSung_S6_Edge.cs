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
        public void �жU�d����()
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
                Url = "https://www.esunbank.com.tw/bank/personal/loan/tools/apply/house-loan?dev=mobile" // 0. Browse �жU�d����M��
            };

            _driver.FindElement(By.XPath("//*[contains(@name, 'loanAmount')]")).SendKeys("50"); // 1. �B��
            _driver.FindElement(By.XPath("//*[contains(@name, 'loanUse')]")).Click(); // 2. �U�ڥγ~�U�Կ��
            Random ran = new Random();
            var ranLoanUse = ran.Next(1, 10); // random ���ﶵ
            Actions fingerAction = new Actions(_driver); // new "action" method
            for (int i = 1; i <= ranLoanUse; i++)
            {
                if (i == ranLoanUse)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // ����ﶵ
                    _driver.FindElement(By.XPath("//*[contains(@name, 'loanUse')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }

            _driver.FindElement(By.XPath("//*[contains(@name, 'name')]")).SendKeys("AutoTest"); // 3. �ӽФH�m�W
            _driver.FindElement(By.XPath("//*[contains(@name, 'citizenId')]")).SendKeys("a123456789"); // 4. �����Ҧr��
            _driver.FindElement(By.XPath("//*[contains(@name, 'tel')]")).SendKeys("0987654321"); // 5. �s���q��
            _driver.FindElement(By.XPath("//*[contains(@name, 'cellPhone')]")).SendKeys("0987654321"); // 6. ��ʹq��
            _driver.FindElement(By.XPath("//*[contains(@name, 'email')]")).SendKeys("test@gmail.com"); // 7. �q�l�l��


            _driver.FindElement(By.XPath("//*[contains(@name, 'city')]")).Click(); // 8. �q�T�a�}-����-�U�Կ��
            var ranCity = ran.Next(1, 23); // random �������ﶵ�d��
            for (int i = 1; i <= ranCity; i++)
            {
                if (i == ranCity)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // ����ﶵ
                    _driver.FindElement(By.XPath("//*[contains(@name, 'city')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }


            _driver.FindElement(By.XPath("//*[contains(@name, 'region')]")).Click(); // 9. �q�T�a�}-�m����-�U�Կ��
            var ranRegion = ran.Next(1, 2); // random �m���Ͽﶵ�d��
            for (int i = 1; i <= ranRegion; i++)
            {
                if (i == ranRegion)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // ����ﶵ
                    _driver.FindElement(By.XPath("//*[contains(@name, 'region')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }

            _driver.FindElement(By.XPath("//*[contains(@name, 'addrline')]")).SendKeys("���s���@��2�q3��4��5��"); // 10. �ԲӦa�}

            fingerAction.SendKeys(Keys.PageDown).Build().Perform(); // scroll down page

            _driver.FindElement(By.XPath("//*[contains(@name, 'locCity')]")).Click(); // 11. �ЫΦ�m-����-�U�Կ��
            var ranLocCity = ran.Next(1, 23); // random �������ﶵ�d��
            for (int i = 1; i <= ranLocCity; i++)
            {
                if (i == ranLocCity)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // ����ﶵ
                    _driver.FindElement(By.XPath("//*[contains(@name, 'locCity')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }


            _driver.FindElement(By.XPath("//*[contains(@name, 'locRegion')]")).Click(); // 12. �ЫΦ�m-�m����-�U�Կ��
            var ranLocRegion = ran.Next(1, 2); // random �m���Ͽﶵ�d��
            for (int i = 1; i <= ranLocRegion; i++)
            {

                if (i == ranLocRegion)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // ����ﶵ
                    _driver.FindElement(By.XPath("//*[contains(@name, 'locRegion')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }

            _driver.FindElement(By.ClassName("last-child")).Click(); // 13. �I�A�Ⱦ��I-�a��ӶU�ЫΦ�m


            _driver.FindElement(By.XPath("//*[contains(@name, 'contactTime')]")).Click(); // 14. ��K�p���ɶ��U�Կ��
            var ranContactTime = ran.Next(1, 5); // random �m���Ͽﶵ�d��
            for (int i = 1; i <= ranContactTime; i++)
            {
                if (i == ranLocRegion)
                {
                    fingerAction.SendKeys(Keys.ArrowDown).Build().Perform(); // ����ﶵ
                    _driver.FindElement(By.XPath("//*[contains(@name, 'contactTime')]")).Click();
                }
                fingerAction.SendKeys(Keys.ArrowDown);
            }

            _driver.FindElement(By.ClassName("trans-element-checkbox")).Click(); // 15. �ڤw�\Ū�æP�N

            //_driver.FindElement(By.Id("submit")).Click(); // 16. �e�X

            // Close the browser.
            _driver.Quit();


        }
    }
}
