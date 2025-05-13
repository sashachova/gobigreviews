using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Gobigreviews.Utils
{
    public static class ElementHelper
    {
        public static bool IsDisplayed(IWebDriver driver, By locator, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static bool Click(IWebDriver driver, By locator, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
                return true;
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Click failed: {ex.Message}");
                return false;
            }
        }

        public static void Type(IWebDriver driver, By locator, string text, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
                element.Clear();
                element.SendKeys(text);
                //check is locator is added 
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error typing into element {locator}: {ex.Message}");
       
            }
            
            
        }

        public static string GetAttribute(IWebDriver driver, By locator, string attribute, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(locator)).GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting attribute of element {locator}: {ex.Message}");
                return string.Empty;
            }
            
            
        }
    }
}