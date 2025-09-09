using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data.SqlTypes;
using System.Threading;


namespace SeleniumPractice
{
    public class Tests
    {

    // All Tests Created By Daniel Navarro

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

        public int GetUpgradeCost(IWebDriver driver, string upgradeID)
        {
            IWebElement upgrade = driver.FindElement(By.Id(upgradeID));
            int upgradeCost = ConvertTextToInt(upgrade.Text);
            return upgradeCost;
        }

        public void ClickUpgrade(IWebDriver driver, string upgradeId) 
        {
            IWebElement upgrade = driver.FindElement(By.Id(upgradeId));
            upgrade.Click();
        }

        public void GetMoneyBuyUpgrade(IWebDriver driver, string upgradeID, int numTimes=0)
        {
            if (numTimes == 0)  //Need to find upgrade cost
            {
                numTimes = GetUpgradeCost(driver, upgradeID);
            }
            ClickBigCookie(driver, numTimes);
            ClickUpgrade(driver, upgradeID);
        }

        public int[] GetBeforeAfterCosts(IWebDriver driver, string upgradeId)
        {
            int oldUpgradeCost = GetUpgradeCost(driver, upgradeId);  //Finds the cost
            GetMoneyBuyUpgrade(driver, upgradeId, oldUpgradeCost);
            Thread.Sleep(3000);
            int newUpgradeCost = GetUpgradeCost(driver, upgradeId);  //Gets new cost
            return [oldUpgradeCost, newUpgradeCost];
        }

        public int[] GetBeforeAfterMoney(IWebDriver driver, string upgradeId)
        {
            IWebElement upgrade = driver.FindElement(By.Id(upgradeId));
            int upgradeCost = ConvertTextToInt(upgrade.Text);
            ClickBigCookie(driver, upgradeCost);  // Gets enough money to buy Cursor
            Thread.Sleep(1000);
            int prePurchaseMoney = GetCookieCount(driver);  //Gets money total before purchase
            ClickUpgrade(driver, upgradeId); // Buys upgrade
            Thread.Sleep(1000);
            int postPurchaseMoney = GetCookieCount(driver);  //Gets money total after purchase
            return [prePurchaseMoney, postPurchaseMoney];
        }

        public string GetAmountPurchased(IWebDriver driver, string upgradeId)
        {
            IWebElement upgrade = driver.FindElement(By.Id(upgradeId));
            int upgradeCost = ConvertTextToInt(upgrade.Text);
            GetMoneyBuyUpgrade(driver, upgradeId, upgradeCost);
            Thread.Sleep(3000);
            IWebElement amountPurchased = driver.FindElement(By.XPath("//*[@id=\"" + upgradeId + "\"]/div"));
            return amountPurchased.Text;
        }

    /*******************************************************************************
     *  Initialization Tests                                                      /
     ****************************************************************************/

        [Test]
        public void CookieClickedOnce()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            Random random = new Random();
            int randInt = random.Next(1, 11);
            ClickBigCookie(driver, randInt);
            Thread.Sleep(3000);
            int cookiesCount = GetCookieCount(driver);
            Assert.AreEqual(randInt, cookiesCount);
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
            int cursorCost = GetUpgradeCost(driver, cursorId);
            Assert.AreEqual(initCursorCost, cursorCost);
            driver.Close();
        }

        [Test]
        public void GrandmaPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int grandmaCost = GetUpgradeCost(driver, grandmaId);
            Assert.AreEqual(initGrandmaCost, grandmaCost);
            driver.Close();
        }

        [Test]
        public void FactoryPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int factoryCost = GetUpgradeCost(driver, factoryId);
            Assert.AreEqual(initFactoryCost, factoryCost);
            driver.Close();
        }

        [Test]
        public void MinePrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int mineCost = GetUpgradeCost(driver, mineId);
            Assert.AreEqual(initMineCost, mineCost);
            driver.Close();
        }

        [Test]
        public void ShipmentPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int shipmentCost = GetUpgradeCost(driver, shipmentId);
            Assert.AreEqual(initShipmentCost, shipmentCost);
            driver.Close();
        }

        [Test]
        public void AlchemyLabPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int alchemyLabCost = GetUpgradeCost(driver, alchemyLabId);
            Assert.AreEqual(initAlchemyLabCost, alchemyLabCost);
            driver.Close();
        }

        [Test]
        public void PortalPrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int portalCost = GetUpgradeCost(driver, portalId);
            Assert.AreEqual(initPortalCost, portalCost);
            driver.Close();
        }

        [Test]
        public void TimeMachinePrice()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int timeMachineCost = GetUpgradeCost(driver, timeMachineId);
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
            int[] costs = GetBeforeAfterCosts(driver, cursorId);
            Assert.AreNotEqual(costs[0], costs[1]);  //[0] = prepurchase, [1] = postpurchase
            driver.Close();
        }

        [Test]
        public void GrandmaCostUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] costs = GetBeforeAfterCosts(driver, grandmaId);
            Assert.AreNotEqual(costs[0], costs[1]); //[0] = prepurchase, [1] = postpurchase
            driver.Close();
        }

        //[Test]
        public void FactoryCostUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] costs = GetBeforeAfterCosts(driver, factoryId);
            Assert.AreNotEqual(costs[0], costs[1]); //[0] = prepurchase, [1] = postpurchase
            driver.Close();
        }

        //[Test]
        public void MineCostUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] costs = GetBeforeAfterCosts(driver, mineId);
            Assert.AreNotEqual(costs[0], costs[1]); //[0] = prepurchase, [1] = postpurchase
            driver.Close();
        }

        //[Test]
        public void ShipmentCostUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] costs = GetBeforeAfterCosts(driver, shipmentId);
            Assert.AreNotEqual(costs[0], costs[1]); //[0] = prepurchase, [1] = postpurchase
            driver.Close();
        }

        //[Test]
        public void AlchemyLabCostUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] costs = GetBeforeAfterCosts(driver, alchemyLabId);
            Assert.AreNotEqual(costs[0], costs[1]); //[0] = prepurchase, [1] = postpurchase
            driver.Close();
        }

        //[Test]
        public void PortalCostUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] costs = GetBeforeAfterCosts(driver, portalId);
            Assert.AreNotEqual(costs[0], costs[1]); //[0] = prepurchase, [1] = postpurchase
            driver.Close();
        }

        //[Test]
        public void TimeMachineCostUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] costs = GetBeforeAfterCosts(driver, timeMachineId);
            Assert.AreNotEqual(costs[0], costs[1]); //[0] = prepurchase, [1] = postpurchase
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
            int[] cookieCounts = GetBeforeAfterMoney(driver, cursorId);
            Assert.True(cookieCounts[0] != cookieCounts[1]);
            driver.Close();
        }

        [Test]
        public void GrandmaPriceSubtracted()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] cookieCounts = GetBeforeAfterMoney(driver, grandmaId);
            Assert.True(cookieCounts[0] != cookieCounts[1]);
            driver.Close();
        }

        //[Test]
        public void FactoryPriceSubtracted()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] cookieCounts = GetBeforeAfterMoney(driver, factoryId);
            Assert.True(cookieCounts[0] != cookieCounts[1]);
            driver.Close();
        }

        //[Test]
        public void MinePriceSubtracted()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] cookieCounts = GetBeforeAfterMoney(driver, mineId);
            Assert.True(cookieCounts[0] != cookieCounts[1]);
            driver.Close();
        }

        //[Test]
        public void ShipmentPriceSubtracted()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] cookieCounts = GetBeforeAfterMoney(driver, shipmentId);
            Assert.True(cookieCounts[0] != cookieCounts[1]);
            driver.Close();
        }

        //[Test]
        public void AlchemyLabPriceSubtracted()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] cookieCounts = GetBeforeAfterMoney(driver, alchemyLabId);
            Assert.True(cookieCounts[0] != cookieCounts[1]);
            driver.Close();
        }

        //[Test]
        public void PortalPriceSubtracted()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] cookieCounts = GetBeforeAfterMoney(driver, portalId);
            Assert.True(cookieCounts[0] != cookieCounts[1]);
            driver.Close();
        }

        //[Test]
        public void TimeMachinePriceSubtracted()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            int[] cookieCounts = GetBeforeAfterMoney(driver, timeMachineId);
            Assert.True(cookieCounts[0] != cookieCounts[1]);
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
            string amountPurchased = GetAmountPurchased(driver, cursorId);
            Assert.AreEqual("1", amountPurchased);
            driver.Close();
        }

        [Test]
        public void GrandmaAmountUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            string amountPurchased = GetAmountPurchased(driver, grandmaId);
            Assert.AreEqual("1", amountPurchased);
            driver.Close();
        }

        //[Test]
        public void FactoryAmountUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            string amountPurchased = GetAmountPurchased(driver, factoryId);
            Assert.AreEqual("1", amountPurchased);
            driver.Close();
        }

        //[Test]
        public void MineAmountUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            string amountPurchased = GetAmountPurchased(driver, mineId);
            Assert.AreEqual("1", amountPurchased);
            driver.Close();
        }

        //[Test]
        public void ShipmentAmountUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            string amountPurchased = GetAmountPurchased(driver, shipmentId);
            Assert.AreEqual("1", amountPurchased);
            driver.Close();
        }

        //[Test]
        public void AlchemyLabAmountUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            string amountPurchased = GetAmountPurchased(driver, alchemyLabId);
            Assert.AreEqual("1", amountPurchased);
            driver.Close();
        }

        //[Test]
        public void PortalAmountUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            string amountPurchased = GetAmountPurchased(driver, portalId);
            Assert.AreEqual("1", amountPurchased);
            driver.Close();
        }

        //[Test]
        public void TimeMachineAmountUpdates()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            string amountPurchased = GetAmountPurchased(driver, timeMachineId);
            Assert.AreEqual("1", amountPurchased);
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
            GetMoneyBuyUpgrade(driver, cursorId);
            Thread.Sleep(3000);
            IWebElement upgradeIcon = driver.FindElement(By.ClassName("cursor"));
            Assert.IsNotNull(upgradeIcon);
            driver.Close();
        }

        [Test]
        public void GrandmaIconAppears()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            GetMoneyBuyUpgrade(driver, grandmaId);
            Thread.Sleep(3000);
            IWebElement upgradeIcon = driver.FindElement(By.ClassName("grandma"));
            Assert.IsNotNull(upgradeIcon);
            driver.Close();
        }

        //[Test]
        public void FactoryIconAppears()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            GetMoneyBuyUpgrade(driver, factoryId);
            Thread.Sleep(3000);
            IWebElement upgradeIcon = driver.FindElement(By.ClassName("factory"));
            Assert.IsNotNull(upgradeIcon);
            driver.Close();
        }

        //[Test]
        public void MineIconAppears()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            GetMoneyBuyUpgrade(driver, mineId);
            Thread.Sleep(3000);
            IWebElement upgradeIcon = driver.FindElement(By.ClassName("mine"));
            Assert.IsNotNull(upgradeIcon);
            driver.Close();
        }

        //[Test]
        public void ShipmentIconAppears()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            GetMoneyBuyUpgrade(driver, shipmentId);
            Thread.Sleep(3000);
            IWebElement upgradeIcon = driver.FindElement(By.ClassName("shipment"));
            Assert.IsNotNull(upgradeIcon);
            driver.Close();
        }

        //[Test]
        public void AlchemyLabIconAppears()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            GetMoneyBuyUpgrade(driver, alchemyLabId);
            Thread.Sleep(3000);
            IWebElement upgradeIcon = driver.FindElement(By.ClassName("lab"));
            Assert.IsNotNull(upgradeIcon);
            driver.Close();
        }

        //[Test]
        public void PortalIconAppears()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            GetMoneyBuyUpgrade(driver, alchemyLabId);
            Thread.Sleep(3000);
            IWebElement upgradeIcon = driver.FindElement(By.ClassName("portal"));
            Assert.IsNotNull(upgradeIcon);
            driver.Close();
        }

        //[Test]
        public void TimeMachineLabIconAppears()
        {
            IWebDriver driver = new ChromeDriver();
            SetUpPage(driver);
            GetMoneyBuyUpgrade(driver, timeMachineId);
            Thread.Sleep(3000);
            IWebElement upgradeIcon = driver.FindElement(By.ClassName("time"));
            Assert.IsNotNull(upgradeIcon);
            driver.Close();
        }
    }
}