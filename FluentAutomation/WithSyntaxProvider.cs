namespace FluentAutomation
{
    using System;

    using Interfaces;

    public class WithSyntaxProvider
    {
        protected readonly IActionSyntaxProvider actionSyntaxProvider;
        protected readonly FluentSettings inlineSettings;

        public WithSyntaxProvider(IActionSyntaxProvider actionSyntaxProvider)
        {
            this.actionSyntaxProvider = actionSyntaxProvider;
            inlineSettings = FluentSettings.Current.Clone();
        }

        public IActionSyntaxProvider Then
        {
            get
            {
                var actionSyntaxProvider = (ActionSyntaxProvider)this.actionSyntaxProvider;
                actionSyntaxProvider.commandProvider.WithConfig(inlineSettings);
                return this.actionSyntaxProvider;
            }
        }

        public WithSyntaxProvider ScreenshotOnFailedAction(bool screenshotOnFail)
        {
            inlineSettings.ScreenshotOnFailedAction = screenshotOnFail;
            return this;
        }

        public WithSyntaxProvider ScreenshotOnFailedAssert(bool screenshotOnFail)
        {
            inlineSettings.ScreenshotOnFailedAssert = screenshotOnFail;
            return this;
        }

        public WithSyntaxProvider ScreenshotOnFailedExpect(bool screenshotOnFail)
        {
            inlineSettings.ScreenshotOnFailedExpect = screenshotOnFail;
            return this;
        }

        public WithSyntaxProvider ScreenshotPath(string screenshotPath)
        {
            inlineSettings.ScreenshotPath = screenshotPath;
            return this;
        }

        public WithSyntaxProvider ScreenshotPrefix(string prefix)
        {
            inlineSettings.ScreenshotPrefix = prefix;
            return this;
        }

        public WithSyntaxProvider Wait(int seconds) => Wait(TimeSpan.FromSeconds(seconds));

        public WithSyntaxProvider Wait(TimeSpan timeout)
        {
            inlineSettings.WaitTimeout = timeout;
            return this;
        }

        public WithSyntaxProvider WaitInterval(int seconds) => WaitInterval(TimeSpan.FromSeconds(seconds));

        public WithSyntaxProvider WaitInterval(TimeSpan interval)
        {
            inlineSettings.WaitUntilInterval = interval;
            return this;
        }

        public WithSyntaxProvider WaitOnAllActions(bool wait)
        {
            inlineSettings.WaitOnAllActions = wait;
            return this;
        }

        public WithSyntaxProvider WaitOnAllAsserts(bool wait)
        {
            inlineSettings.WaitOnAllAsserts = wait;
            return this;
        }

        public WithSyntaxProvider WaitOnAllExpects(bool wait)
        {
            inlineSettings.WaitOnAllExpects = wait;
            return this;
        }

        public WithSyntaxProvider WaitUntil(int seconds) => WaitUntil(TimeSpan.FromSeconds(seconds));

        public WithSyntaxProvider WaitUntil(TimeSpan timeout)
        {
            inlineSettings.WaitUntilTimeout = timeout;
            return this;
        }

        public WithSyntaxProvider WindowMaximized()
        {
            inlineSettings.WindowMaximized = true;
            return this;
        }

        public WithSyntaxProvider WindowSize(int width, int height)
        {
            inlineSettings.WindowHeight = height;
            inlineSettings.WindowWidth = width;
            return this;
        }
    }
}