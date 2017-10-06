namespace FluentAutomation
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using Exceptions;

    using TinyIoC;

    public class FluentSettings
    {
        public FluentSettings()
        {
            // Toggle features on/off
            WaitOnAllExpects = false;
            WaitOnAllAsserts = true;
            WaitOnAllActions = true;
            MinimizeAllWindowsOnTestStart = false;
            ScreenshotOnFailedExpect = false;
            ScreenshotOnFailedAssert = false;
            ScreenshotOnFailedAction = false;
            ExpectIsAssert = false; // determine if Expects are treated as Asserts (v2.x behavior)

            // browser size
            WindowHeight = null;
            WindowWidth = null;
            WindowMaximized = false;

            // timeouts
            WaitTimeout = TimeSpan.FromSeconds(1);
            WaitUntilTimeout = TimeSpan.FromSeconds(5);
            WaitUntilInterval = TimeSpan.FromMilliseconds(100);

            // paths
            UserTempDirectory = Path.GetTempPath();
            ScreenshotPath = UserTempDirectory;

            // IoC registration
            ContainerRegistration = c => { };

            // events
            OnExpectFailed = (ex, state) =>
            {
                var fluentException = ex.InnerException as FluentException;
                if (fluentException != null)
                    Trace.WriteLine("[EXPECT FAIL] " + fluentException.Message);
                else
                    Trace.WriteLine("[EXPECT FAIL] " + ex.Message);
            };

            OnAssertFailed = (ex, state) =>
            {
                var fluentException = ex.InnerException as FluentException;
                if (fluentException != null)
                    Trace.WriteLine("[ASSERT FAIL] " + fluentException.Message);
                else
                    Trace.WriteLine("[ASSERT FAIL] " + ex.Message);
            };
        }

        public static FluentSettings Current { get; set; } = new FluentSettings();

        public Action<TinyIoCContainer> ContainerRegistration { get; set; }
        public bool ExpectIsAssert { get; set; }
        public bool MinimizeAllWindowsOnTestStart { get; set; }
        public Action<FluentAssertFailedException, WindowState> OnAssertFailed { get; set; }
        public Action<FluentExpectFailedException, WindowState> OnExpectFailed { get; set; }
        public bool ScreenshotOnFailedAction { get; set; }
        public bool ScreenshotOnFailedAssert { get; set; }
        public bool ScreenshotOnFailedExpect { get; set; }
        public string ScreenshotPath { get; set; }
        public string ScreenshotPrefix { get; set; }
        public string UserTempDirectory { get; set; }
        public bool WaitOnAllActions { get; set; }
        public bool WaitOnAllAsserts { get; set; }

        public bool WaitOnAllExpects { get; set; }
        public TimeSpan WaitTimeout { get; set; }
        public TimeSpan WaitUntilInterval { get; set; }
        public TimeSpan WaitUntilTimeout { get; set; }
        public int? WindowHeight { get; set; }
        public bool WindowMaximized { get; set; }
        public int? WindowWidth { get; set; }

        internal FluentSettings Clone() => (FluentSettings)MemberwiseClone();
    }
}