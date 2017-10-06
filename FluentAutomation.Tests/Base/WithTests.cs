namespace FluentAutomation.Tests.Base
{
    using System;

    using Xunit;

    public class WithTests : BaseTest
    {
        [Fact]
        public void WithTimeouts()
        {
            With
                .Wait(1)
                .Wait(TimeSpan.FromMilliseconds(50))
                .WaitInterval(1)
                .WaitInterval(TimeSpan.FromMilliseconds(50))
                .WaitOnAllActions(false)
                .WaitOnAllAsserts(false)
                .WaitOnAllExpects(false)
                .WindowSize(800, 600)
                .ScreenshotOnFailedAction(false)
                .ScreenshotOnFailedAssert(false)
                .ScreenshotOnFailedExpect(false)
                .ScreenshotPath("")
                .ScreenshotPrefix("");
        }
    }
}