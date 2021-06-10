using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Banco.Infrastructure.Web.Test
{
    public class ExampleBingTests
    {
        private IWebDriver driver;
        private string appURL;

        [SetUp]
        public void Setup()
        {
            appURL = "http://www.bing.com/";

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }

        [Ignore("Implementar en su proyecto")]
        [Test]
        public void TheBingSearchTest()
        {
            //driver.Navigate().GoToUrl(appURL + "/");
            //var textBusqueda=driver.FindElement(By.Id("sb_form_q"));
            //textBusqueda.SendKeys("Azure Pipelines");
            ////driver.FindElement(By.XPath("css=.search>svg")).Click();
            //Assert.IsTrue(textBusqueda.Text, "Verified title of the page");
        }

        [TearDown]
        public void Down() 
        {
           // driver.Quit();
        }
    }
}