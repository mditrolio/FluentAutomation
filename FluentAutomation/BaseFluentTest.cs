namespace FluentAutomation
{
    using System;

    using Interfaces;

    public class BaseFluentTest : IDisposable
    {
        public ISyntaxProvider SyntaxProvider { get; set; }

        public void Dispose()
        {
            try
            {
                if (FluentSession.Current == null)
                    SyntaxProvider?.Dispose();

                if (FluentSettings.Current.MinimizeAllWindowsOnTestStart) Win32Magic.RestoreAllWindows();
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }
    }
}