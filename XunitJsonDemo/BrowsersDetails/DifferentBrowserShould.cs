using System.IO;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Xunit;

namespace XunitJsonDemo.BrowsersDetails
{
    public class DifferentBrowserShould
    {
        public IWebDriver driver;

        [Theory]
        [JsonBrowser("BrowsersDetails/BrowsersDetails.json")]
        public void OpenPageWithSpecificOption(BrowserDetail browserDetail)
        {
            var browserName = browserDetail.Browser;
            switch (browserName)
            {
                case "Firefox":
                    {
                        driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                        break;
                    }
                case "internetExplorer":
                    {
                        driver = new InternetExplorerDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                        break;
                    }
                default:
                    {
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--lang=" + browserDetail.Language); 
                        driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                        driver.Manage().Window.Maximize();
                        break;
                    }
            }

            driver.Navigate().GoToUrl(browserDetail.Url);
            Thread.Sleep(6000);

            if (browserDetail.Language.Equals("es"))
            {
                Assert.Equal("WordPress.com: crea un sitio web o blog gratuito", driver.Title);
            }
            else
            {
                Assert.Equal("WordPress.com: Create a Free Website or Blog", driver.Title);
            }

            IWebElement element = driver.FindElement(By.TagName("html"));
            var pageLanguage = element.GetAttribute("lang");
            Assert.Equal(browserDetail.Language, pageLanguage);
            driver.Quit();
        }
    }
}
