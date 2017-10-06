namespace FluentAutomation.Wrappers
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class EnhancedRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public EnhancedRemoteWebDriver(ICapabilities desiredCapabilities)
            : base(desiredCapabilities)
        {
        }

        public EnhancedRemoteWebDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
            : base(commandExecutor, desiredCapabilities)
        {
        }

        public EnhancedRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, desiredCapabilities, commandTimeout)
        {
        }

        public EnhancedRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities)
        {
        }

        public new Screenshot GetScreenshot()
        {
            Response response = Execute(DriverCommand.Screenshot, null);
            string responseContent = response.Value.ToString();

            return new Screenshot(responseContent);
        }
    }
}