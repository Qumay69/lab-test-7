using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace testLab_7
{
    public class Tests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestPageTitle()
        {
            _driver.Navigate().GoToUrl("https://ria.ru/");

            string expectedTitle = "Строительство домов и коттеджей под ключ в Калининграде проекты и цены | СтройГрупп";
            string actualTitle = _driver.Title;
            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "Заголовок страницы не равен ожидаемому");
        }

        [Test]
        public void TestElementVisibility()
        {
            _driver.Navigate().GoToUrl("https://ria.ru/");

            IWebElement element = _driver.FindElement(By.CssSelector(".nott"));

            Assert.That(element.Displayed, Is.True, "Заголовок страницы не видим");
        }

        [Test]
        public void TestNavigation()
        {
            _driver.Navigate().GoToUrl("https://ria.ru/");

            IWebElement link = _driver.FindElement(By.CssSelector(".dark-btns > a:nth-child(1)"));
            link.Click();

            Assert.That(_driver.Url, Does.Contain("projects"), "Переход по ссылке не выполнен.");
        }

        [Test]
        public void TestFillTextField()
        {
            _driver.Navigate().GoToUrl("https://ria.ru/");

            IWebElement name = _driver.FindElement(By.XPath("//*[@id=\"sfb_name\"]"));
            name.SendKeys("Тестовое имя");

            IWebElement phone = _driver.FindElement(By.XPath("//*[@id=\"sfb_phone\"]"));
            phone.SendKeys("79999999999");


            Assert.That(name.GetAttribute("value"), Is.EqualTo("Тестовое имя"), "Имя не совпадает");
            Assert.That(phone.GetAttribute("value"), Is.EqualTo("+7 (999) 999-99-99"), "Телефон не совпадает");
        }

        [Test]
        public void TestButtonClick()
        {
            _driver.Navigate().GoToUrl("https://ria.ru/");

            IWebElement button = _driver.FindElement(By.XPath("/html/body/div[1]/header/section[1]/div/div/button"));

            button.Click();

            Assert.Throws<NoSuchElementException>(() => _driver.FindElement(By.XPath("//*[@id=\"openModalCollaboration\"]")));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}