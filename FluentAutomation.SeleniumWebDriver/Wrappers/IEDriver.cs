namespace FluentAutomation.Wrappers
{
    using System;

    using OpenQA.Selenium.IE;

    public class IEDriverWrapper : InternetExplorerDriver
    {
        public IEDriverWrapper(string ieDriverDirectoryPath, TimeSpan commandTimeout)
            : base(ieDriverDirectoryPath, new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                EnableNativeEvents = true
            }, commandTimeout)
        {
        }
    }
}