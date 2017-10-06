namespace FluentAutomation
{
    using System;

    using Exceptions;

    using TinyIoC;

    public class FluentConfig
    {
        public static FluentConfig Current { get; } = new FluentConfig();

        public FluentSettings Settings => FluentSettings.Current;

        public FluentConfig Configure(FluentSettings settings)
        {
            FluentSettings.Current = settings;
            return this;
        }

        public FluentConfig ContainerRegistration(Action<TinyIoCContainer> registrationMethod)
        {
            Settings.ContainerRegistration = registrationMethod;
            return this;
        }

        public FluentConfig ExpectIsAssert(bool isAssert)
        {
            Settings.ExpectIsAssert = isAssert;
            return this;
        }

        public FluentConfig MinimizeAllWindowsOnTestStart(bool minimize)
        {
            Settings.MinimizeAllWindowsOnTestStart = minimize;
            return this;
        }

        public FluentConfig OnAssertFailed(Action<FluentAssertFailedException, WindowState> action)
        {
            Settings.OnAssertFailed = action;
            return this;
        }

        public FluentConfig OnExpectFailed(Action<FluentExpectFailedException, WindowState> action)
        {
            Settings.OnExpectFailed = action;
            return this;
        }

        public FluentConfig ScreenshotOnFailedAction(bool screenshotOnFail)
        {
            Settings.ScreenshotOnFailedAction = screenshotOnFail;
            return this;
        }

        public FluentConfig ScreenshotOnFailedAssert(bool screenshotOnFail)
        {
            Settings.ScreenshotOnFailedAssert = screenshotOnFail;
            return this;
        }

        public FluentConfig ScreenshotOnFailedExpect(bool screenshotOnFail)
        {
            Settings.ScreenshotOnFailedExpect = screenshotOnFail;
            return this;
        }

        public FluentConfig ScreenshotPath(string screenshotPath)
        {
            Settings.ScreenshotPath = screenshotPath;
            return this;
        }

        public FluentConfig ScreenshotPrefix(string prefix)
        {
            Settings.ScreenshotPrefix = prefix;
            return this;
        }

        public FluentConfig UserTempDirectory(string tempDir)
        {
            Settings.UserTempDirectory = tempDir;
            return this;
        }

        public FluentConfig WaitOnAllActions(bool wait)
        {
            Settings.WaitOnAllActions = wait;
            return this;
        }

        public FluentConfig WaitOnAllExpects(bool wait)
        {
            Settings.WaitOnAllExpects = wait;
            return this;
        }

        public FluentConfig WaitTimeout(TimeSpan timeout)
        {
            Settings.WaitTimeout = timeout;
            return this;
        }

        public FluentConfig WaitUntilInterval(TimeSpan sleep)
        {
            Settings.WaitUntilInterval = sleep;
            return this;
        }

        public FluentConfig WaitUntilTimeout(TimeSpan timeout)
        {
            Settings.WaitUntilTimeout = timeout;
            return this;
        }

        public FluentConfig WindowMaximized(bool isMaximized)
        {
            Settings.WindowMaximized = isMaximized;
            return this;
        }

        public FluentConfig WindowSize(int width, int height)
        {
            Settings.WindowHeight = height;
            Settings.WindowWidth = width;
            return this;
        }
    }
}