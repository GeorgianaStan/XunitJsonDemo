using System.IO;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Xunit;

namespace XunitJsonDemo.BrowserOptions
{
    public class BrowserShould
    {
        public IWebDriver driver;
              
        [Theory]
        [InlineData("BrowserOptions/browser.json", "--lang=en", "--window-size=1920,1080", "chrome","https://www.wordpress.com")]
        public void OpenPageInASpecificLanguage(string filePath, string languageOption, string windowsSize, string browserName,string url)
        {
            Browser browser = JsonConvert.DeserializeObject<Browser>(File.ReadAllText(filePath));

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
                        browser.OptionArguments.ForEach(argument => options.AddArgument(argument));
                        driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                        break;
                    } 
            }
            
            driver.Navigate().GoToUrl(browser.Url);
            Thread.Sleep(6000);

            Assert.Equal("WordPress.com: Create a Free Website or Blog", driver.Title);
            IWebElement element = driver.FindElement(By.TagName("html"));
            var pageLanguage = element.GetAttribute("lang");
            var languageExpected = languageOption.Split("=")[1];
            Assert.Equal(languageExpected, pageLanguage);
            driver.Quit();
        }
    }
}
