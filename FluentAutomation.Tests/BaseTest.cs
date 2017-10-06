namespace FluentAutomation.Tests
{
    using System;

    using OpenQA.Selenium;

    using Pages;

    /// <summary>
    ///     Base Test that opens the test to the AUT
    /// </summary>
    public class BaseTest : FluentTest<IWebDriver>
    {
        public AlertsPage AlertsPage;
        public DragPage DragPage;

        public InputsPage InputsPage;
        public ScrollingPage ScrollingPage;
        public SwitchPage SwitchPage;
        public TextPage TextPage;

        public BaseTest()
        {
            FluentSession.EnableStickySession();
            Config.WaitUntilTimeout(TimeSpan.FromMilliseconds(1000));

            // Create Page Objects
            InputsPage = new InputsPage(this);
            AlertsPage = new AlertsPage(this);
            ScrollingPage = new ScrollingPage(this);
            TextPage = new TextPage(this);
            DragPage = new DragPage(this);
            SwitchPage = new SwitchPage(this);

            // Default tests use chrome and load the site
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome); //, SeleniumWebDriver.Browser.InternetExplorer, SeleniumWebDriver.Browser.Firefox);
            I.Open(SiteUrl);
        }

        public string SiteUrl => "http://localhost:38043/";
    }

    public class AssertBaseTest : BaseTest
    {
        public AssertBaseTest()
        {
            Config.OnExpectFailed((ex, state) =>
            {
                // For the purpose of these tests, allow expects to throw (break test)
                throw ex;
            });
        }
    }
}