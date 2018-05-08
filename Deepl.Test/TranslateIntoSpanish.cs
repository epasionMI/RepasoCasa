using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deepl.Test
{
    class TranslateIntoSpanish
    {
        ChromeDriver Chrome;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            Chrome = new ChromeDriver();
            Chrome.Manage().Window.Maximize();
            Chrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                Chrome.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TraducirCadenaInglesEspanol()
        {
            try { 
            Console.WriteLine("✔ Navegando a https://www.deepl.com/home");
            Chrome.Navigate().GoToUrl("https://www.deepl.com/home");

            Console.WriteLine("✔ Accediendo a Translate tab");
            Chrome.FindElement(By.Id("dl_menu_translator")).Click();
            Chrome.FindElement(By.XPath("//div[@id='dl_translator']/div/div/div[2]/textarea")).Clear();

            Console.WriteLine("✔ Añadiendo cadena a traducir");
            Chrome.FindElement(By.XPath("//div[@id='dl_translator']/div/div/div[2]/textarea")).SendKeys("This is a Test");

            Console.WriteLine("✔ Seleccionando idioma de traducción");
            Chrome.FindElement(By.CssSelector("div.lmt__language_select.lmt__language_select--target > div.lmt__language_select__opener > svg.dl_closed")).Click();
            Chrome.FindElement(By.XPath("//div[@id='dl_translator']/div/div[2]/div/div/ul/li[4]")).Click();

            Console.WriteLine("✔ Comprobando que la traducción se ha completado");
            var Wait = new WebDriverWait(Chrome, TimeSpan.FromSeconds(10));
            bool IsElementInVisible = Wait.Until(x => x.FindElement(By.XPath("//*[@id='dl_translator']/div[1]/div[2]/div[3]/p[3]/span")).Displayed == true);

                Assert.AreEqual(true, IsElementInVisible, "✘ Ha ocurrido un error, la traducción no se ha completado");
            Console.WriteLine("✔ La traducción se ha completado satisfactoriamente");

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                Chrome.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                Chrome.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = Chrome.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
