using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Gobigreviews.Drivers
{
    public static class WebDriverFactory 
    {
        public static IWebDriver Create()
        {
            var options = new ChromeOptions();
            //options.AddArgument($"user-data-dir={Path.Combine(Directory.GetCurrentDirectory(), "ChromeTestProfile")}");
            //options.AddArgument("--headless=new");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
        var driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
        return driver; 
        }
    }
}