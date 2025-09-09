using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data.SqlTypes;
using System.Threading;

namespace SeleniumPractice
{
    public class Tests
    {
    /*******************************************************************************
     *  Variables and Helper Methods                                              /
     ****************************************************************************/
        const string url = "https://orteil.dashnet.org/experiments/cookie/";
        const int initCursorCost = 15;
        const int initGrandmaCost = 100;
        const int initFactoryCost = 500;
        const int initMineCost = 2000;
        const int initShipmentCost = 7000;
        const int initAlchemyLabCost = 50000;
        const int initPortalCost = 1000000;
        const int initTimeMachineCost = 123456789;

        const string cursorId = "buyCursor";
        const string grandmaId = "buyGrandma";
        const string factoryId = "buyFactory";
        const string mineId = "buyMine";
        const string shipmentId = "buyShipment";
        const string alchemyLabId = "buyAlchemy lab";
        const string portalId = "buyPortal";
        const string timeMachineId = "buyTime machine";
        const string bigCookieId = "cookie";
        const string moneyId = "money";

        public int ConvertTextToInt(string text)
        {
            int start;

            if (text.Contains('-'))
            {
                start = text.IndexOf('-') + 2;
            } 
            else
            {
                start = 0;
            }
            string amount = "";
            for (int i = start; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]))
                {
                    amount += text[i];
                }
                else if (text[i] == ',')
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            return Int32.Parse(amount);
        }

        public void ClickBigCookie(IWebDriver driver, int numTimes)
        {
            IWebElement bigCookie = driver.FindElement(By.Id(bigCookieId));
            for(int i = 0; i < numTimes; i++)
            {
                bigCookie.Click();
            }
        }

        public void SetUpPage(IWebDriver driver) 
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            Thread.Sleep(3000);
        }

        public int GetCookieCount(IWebDriver driver)
        {
            IWebElement money = driver.FindElement(By.Id(moneyId));
            int cookieCount = ConvertTextToInt(money.Text);
            return cookieCount;
        }

    /*******************************************************************************
     *  Initialization Tests                                                      /
     ****************************************************************************/

        [Test]
        public void CookieClickedOnce()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            ClickBigCookie(driver, 1);
            Thread.Sleep(3000);
            int cookiesCount = GetCookieCount(driver);
            Assert.AreEqual(1, cookiesCount);
            driver.Close();
        }

    /*******************************************************************************
     *  Initialized Price Tests                                                   /
     ****************************************************************************/

        [Test]
        public void CursorPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement cursor = driver.FindElement(By.Id(cursorId));
            int cursorCost = ConvertTextToInt(cursor.Text);
            Assert.AreEqual(initCursorCost, cursorCost);
            driver.Close();
        }

        [Test]
        public void GrandmaPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement grandma = driver.FindElement(By.Id(grandmaId));
            int grandmaCost = ConvertTextToInt(grandma.Text);
            Assert.AreEqual(initGrandmaCost, grandmaCost);
            driver.Close();
        }

        [Test]
        public void FactoryPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement factory = driver.FindElement(By.Id(factoryId));
            int factoryCost = ConvertTextToInt(factory.Text);
            Assert.AreEqual(initFactoryCost, factoryCost);
            driver.Close();
        }

        [Test]
        public void MinePrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement mine = driver.FindElement(By.Id(mineId));
            int mineCost = ConvertTextToInt(mine.Text);
            Assert.AreEqual(initMineCost, mineCost);
            driver.Close();
        }

        [Test]
        public void ShipmentPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);            
            IWebElement shipment = driver.FindElement(By.Id(shipmentId));
            int shipmentCost = ConvertTextToInt(shipment.Text);
            Assert.AreEqual(initShipmentCost, shipmentCost);
            driver.Close();
        }

        [Test]
        public void AlchemyLabPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement alchemyLab = driver.FindElement(By.Id(alchemyLabId));
            int alchemyLabCost = ConvertTextToInt(alchemyLab.Text);
            Assert.AreEqual(initAlchemyLabCost, alchemyLabCost);
            driver.Close();
        }

        [Test]
        public void PortalPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement portal = driver.FindElement(By.Id(portalId));
            int portalCost = ConvertTextToInt(portal.Text);
            Assert.AreEqual(initPortalCost, portalCost);
            driver.Close();
        }

        [Test]
        public void TimeMachinePrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement timeMachine = driver.FindElement(By.Id(timeMachineId));
            int timeMachineCost = ConvertTextToInt(timeMachine.Text);
            Assert.AreEqual(initTimeMachineCost, timeMachineCost);
            driver.Close();
        }

    /*******************************************************************************
     *  Upgrade Costs Increase Tests                                              /
     ****************************************************************************/

        [Test]
        public void CursorCostUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement cursor = driver.FindElement(By.Id(cursorId));
            int oldCursorCost = ConvertTextToInt(cursor.Text);
            ClickBigCookie(driver, oldCursorCost);
            cursor.Click();
            Thread.Sleep(3000);
            IWebElement cursorStateChanged = driver.FindElement(By.Id(cursorId));
            int newCursorCost = ConvertTextToInt(cursorStateChanged.Text);
            Assert.AreNotEqual(oldCursorCost, newCursorCost);
            driver.Close();
        }

    /*******************************************************************************
     *  Money Subtraction Tests                                                   /
     ****************************************************************************/
        [Test]
        public void CursorPriceSubtracted()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement cursor = driver.FindElement(By.Id(cursorId));
            int cursorCost = ConvertTextToInt(cursor.Text);
            ClickBigCookie(driver, cursorCost); // Gets enough money to buy Cursor
            int prePurchaseMoney = GetCookieCount(driver);
            cursor.Click(); // Buys the Cursor
            int postPurchaseMoney = GetCookieCount(driver);
            Thread.Sleep(3000);
            Assert.True(prePurchaseMoney != postPurchaseMoney);
            driver.Close();
        }

    /*******************************************************************************
     *  Upgrade Amount Purchased Tests                                            /
     ****************************************************************************/
        [Test]
        public void CursorAmountUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver); 
            IWebElement cursor = driver.FindElement(By.Id(cursorId));
            int cursorCost = ConvertTextToInt(cursor.Text);
            ClickBigCookie(driver, cursorCost);
            cursor.Click();
            Thread.Sleep(3000);
            IWebElement amountPurchased = driver.FindElement(By.XPath("//*[@id=\"" + cursorId + "\"]/div"));
            Assert.AreEqual("1", amountPurchased.Text);
            driver.Close();
        }

    /*******************************************************************************
     *  Upgrade Icon Tests                                                        /
     ****************************************************************************/
        [Test]
        public void CursorIconAppears()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            IWebElement cursor = driver.FindElement(By.Id(cursorId));
            int cursorCost = ConvertTextToInt(cursor.Text);
            ClickBigCookie(driver, cursorCost);
            cursor.Click();
            Thread.Sleep(3000);
            IWebElement cursorIcon = driver.FindElement(By.ClassName("cursor"));
            Assert.IsNotNull(cursorIcon);
            driver.Close();
        }
    }
}