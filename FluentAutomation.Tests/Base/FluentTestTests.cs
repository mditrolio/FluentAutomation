namespace FluentAutomation.Tests.Base
{
    using OpenQA.Selenium;

    using Xunit;

    public class FluentTestTests : BaseTest
    {
        [Fact]
        public void WebDriverIsAvailable()
        {
            Assert.True(Provider != null);
        }
    }

    /// <summary>
    ///     Need to test that the non-generic FluentTest instance can still get access
    ///     to Selenium Provider
    /// </summary>
    public class MoreFluentTestTests : FluentTest
    {
        public MoreFluentTestTests()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);

            Config.MinimizeAllWindowsOnTestStart(true);
        }

        [Fact]
        public void ProviderIsAvailable()
        {
            I.Open("http://google.com/");
            Assert.True(Provider != null);
            Assert.True(Provider as IWebDriver != null);

            Config.MinimizeAllWindowsOnTestStart(false);
        }
    }
}