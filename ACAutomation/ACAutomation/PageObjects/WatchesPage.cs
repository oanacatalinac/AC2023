using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class WatchesPage
    {
        private IWebDriver driver;

        public WatchesPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IList<IWebElement> WatchesProductsList => driver.FindElements(By.XPath("//li[contains(@class, 'product-item')]"));

        public WatchDetailsPage NavigateToFirstWatchProduct()
        {
            WatchesProductsList.First().Click(); // se poate si WatchesProductsList[1] sau WatchesProductsList.ElementAt(1)

            return new WatchDetailsPage(driver);
        }
    }
}
