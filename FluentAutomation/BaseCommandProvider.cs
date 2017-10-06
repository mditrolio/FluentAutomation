namespace FluentAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq.Expressions;
    using System.Threading;

    using Exceptions;

    public abstract class BaseCommandProvider
    {
        public FluentSettings Settings = FluentSettings.Current;
        protected bool ExecuteImmediate = true;
        protected List<Action> StoredActions = new List<Action>();

        public Tuple<FluentAssertFailedException, WindowState> PendingAssertFailedExceptionNotification { get; set; }
        public Tuple<FluentExpectFailedException, WindowState> PendingExpectFailedExceptionNotification { get; set; }

        public void Act(CommandType commandType, Action action)
        {
            bool originalWaitOnActions = Settings.WaitOnAllActions;
            try
            {
                if (WaitOnAction(commandType))
                {
                    WaitUntil(() => action(), Settings.WaitUntilTimeout);
                }
                else
                {
                    // If we've decided we don't wait on this type AND its an expect or assert, we want to disable
                    // waits on actions as well because the Expects use actions to fetch elements for verification
                    if (commandType == CommandType.Expect || commandType == CommandType.Assert)
                        Settings.WaitOnAllActions = false;

                    action();
                }
            }
            catch (FluentAssertFailedException ex)
            {
                if (Settings.ScreenshotOnFailedExpect)
                {
                    var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.ToFileTime());
                    ex.ScreenshotPath = Path.Combine(Settings.ScreenshotPath, screenshotName);
                    TakeScreenshot(ex.ScreenshotPath);
                }

                // fire related event before throwing/breaking
                if (PendingAssertFailedExceptionNotification != null)
                    FireAssertFailed();

                throw;
            }
            catch (FluentException ex)
            {
                if (Settings.ScreenshotOnFailedAction)
                {
                    var screenshotName = string.Format(CultureInfo.CurrentCulture, "ActionFailed_{0}", DateTimeOffset.Now.ToFileTime());
                    ex.ScreenshotPath = Path.Combine(Settings.ScreenshotPath, screenshotName);
                    TakeScreenshot(ex.ScreenshotPath);
                }

                if (ex.InnerException != null)
                    if (ex.InnerException.GetType() == typeof(FluentExpectFailedException))
                    {
                        if (Settings.ScreenshotOnFailedExpect)
                        {
                            var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.ToFileTime());
                            ex.ScreenshotPath = Path.Combine(Settings.ScreenshotPath, screenshotName);
                            TakeScreenshot(ex.ScreenshotPath);
                        }

                        if (PendingAssertFailedExceptionNotification != null)
                            FireAssertFailed();
                    }
                    else if (ex.InnerException.GetType() == typeof(FluentAssertFailedException))
                    {
                        if (Settings.ScreenshotOnFailedAssert)
                        {
                            var screenshotName = string.Format(CultureInfo.CurrentCulture, "AssertFailed_{0}", DateTimeOffset.Now.ToFileTime());
                            ex.ScreenshotPath = Path.Combine(Settings.ScreenshotPath, screenshotName);
                            TakeScreenshot(ex.ScreenshotPath);
                        }

                        // fire related event before throwing/breaking
                        if (PendingAssertFailedExceptionNotification != null)
                            FireAssertFailed();
                    }

                // fire related event before throwing/breaking
                if (PendingAssertFailedExceptionNotification != null)
                    FireAssertFailed();

                throw;
            }
            finally
            {
                // fire off event for expect failures
                if (PendingExpectFailedExceptionNotification != null)
                    FireExpectFailed();

                // restore WaitOnAllActions settings in all cases (protection for future cases where above catches dont rethrow)
                Settings.WaitOnAllActions = originalWaitOnActions;
            }
        }

        public abstract void TakeScreenshot(string screenshotName);

        public void Wait()
        {
            Wait(Settings.WaitTimeout);
        }

        public void Wait(TimeSpan timeSpan)
        {
            Act(CommandType.Wait, () => Thread.Sleep(timeSpan));
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            WaitUntil(conditionFunc, Settings.WaitUntilTimeout);
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            Act(CommandType.Wait, () =>
            {
                DateTime dateTimeTimeout = DateTime.Now.Add(timeout);
                bool isFuncValid = false;
                var compiledFunc = conditionFunc.Compile();

                FluentException lastException = null;
                while (DateTime.Now < dateTimeTimeout)
                {
                    try
                    {
                        if (compiledFunc())
                        {
                            isFuncValid = true;
                            break;
                        }

                        Thread.Sleep(Settings.WaitUntilInterval);
                    }
                    catch (FluentException ex)
                    {
                        lastException = ex;
                    }
                    catch (Exception ex)
                    {
                        throw new FluentException("An unexpected exception was thrown inside WaitUntil(Func<bool>). See InnerException for details.", ex);
                    }
                }

                // If func is still not valid, assume we've hit the timeout.
                if (isFuncValid == false)
                    throw new FluentException("Conditional wait passed the timeout [{0}ms] for expression [{1}]. See InnerException for details of the last FluentException thrown.", lastException, timeout.TotalMilliseconds, conditionFunc.ToExpressionString());
            });
        }

        public void WaitUntil(Expression<Action> conditionAction)
        {
            WaitUntil(conditionAction, Settings.WaitUntilTimeout);
        }

        public void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            Act(CommandType.Wait, () =>
            {
                DateTime dateTimeTimeout = DateTime.Now.Add(timeout);
                bool threwException = false;
                var compiledAction = conditionAction.Compile();

                FluentException lastFluentException = null;
                while (DateTime.Now < dateTimeTimeout)
                {
                    try
                    {
                        threwException = false;
                        compiledAction();
                    }
                    catch (FluentException ex)
                    {
                        threwException = true;
                        lastFluentException = ex;
                    }
                    catch (Exception ex)
                    {
                        throw new FluentException("An unexpected exception was thrown inside WaitUntil(Action). See InnerException for details.", ex);
                    }

                    if (!threwException)
                        break;

                    Thread.Sleep(Settings.WaitUntilInterval);
                }

                // If an exception was thrown the last loop, assume we hit the timeout
                if (threwException)
                    throw new FluentException("Conditional wait passed the timeout [{0}ms] for expression [{1}]. See InnerException for details of the last FluentException thrown.", lastFluentException, timeout.TotalMilliseconds, conditionAction.ToExpressionString());
            });
        }

        private void FireAssertFailed()
        {
            var cachedAssertNotification = PendingAssertFailedExceptionNotification;
            PendingAssertFailedExceptionNotification = null;

            Settings.OnAssertFailed(cachedAssertNotification.Item1, cachedAssertNotification.Item2);
        }

        private void FireExpectFailed()
        {
            var cachedExpectFailed = PendingExpectFailedExceptionNotification;
            PendingExpectFailedExceptionNotification = null;

            Settings.OnExpectFailed(cachedExpectFailed.Item1, cachedExpectFailed.Item2);
        }

        private bool WaitOnAction(CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.Action:
                    return Settings.WaitOnAllActions;
                case CommandType.Assert:
                    return Settings.WaitOnAllAsserts;
                case CommandType.Expect:
                    return Settings.WaitOnAllExpects;
                case CommandType.Wait:
                default:
                    return false;
            }
        }
    }
}