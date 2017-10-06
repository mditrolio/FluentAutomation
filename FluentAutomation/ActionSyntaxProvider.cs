namespace FluentAutomation
{
    using System;
    using System.Linq.Expressions;

    using Exceptions;

    using Interfaces;

    public class ActionSyntaxProvider : IActionSyntaxProvider, IDisposable
    {
        internal readonly IAssertProvider assertProvider;
        internal readonly ICommandProvider commandProvider;
        internal FluentSettings settings;

        private AssertSyntaxProvider assert;
        private AssertSyntaxProvider expect;

        private bool isDisposed;

        private SwitchSyntaxProvider switchProvider;

        public ActionSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, FluentSettings settings)
        {
            this.commandProvider = commandProvider.WithConfig(settings);
            this.assertProvider = assertProvider;
            this.settings = settings;
        }

        public AssertSyntaxProvider Assert
        {
            get
            {
                if (assert == null)
                    assert = new AssertSyntaxProvider(commandProvider, assertProvider.EnableExceptions());

                return assert;
            }
        }

        public AssertSyntaxProvider Expect
        {
            get
            {
                if (expect == null)
                    expect = new AssertSyntaxProvider(commandProvider, settings.ExpectIsAssert ? assertProvider.EnableExceptions() : assertProvider);

                return expect;
            }
        }

        public SwitchSyntaxProvider Switch
        {
            get
            {
                if (switchProvider == null)
                    switchProvider = new SwitchSyntaxProvider(this);

                return switchProvider;
            }
        }

        public TextAppendSyntaxProvider Append(string text) => new TextAppendSyntaxProvider(this, text);

        public TextAppendSyntaxProvider Append(dynamic nonString) => this.Append(nonString.ToString());

        public IActionSyntaxProvider Click(int x, int y)
        {
            commandProvider.Click(x, y);
            return this;
        }

        public IActionSyntaxProvider Click(string selector, int x, int y) => Click(selector, x, y, Defaults.FindMethod);

        public IActionSyntaxProvider Click(string selector, int x, int y, FindBy findMethod) => Click(Find(selector, findMethod), x, y);

        public IActionSyntaxProvider Click(ElementProxy element, int x, int y)
        {
            commandProvider.Click(element, x, y);
            return this;
        }

        public IActionSyntaxProvider Click(string selector) => Click(selector, Defaults.FindMethod);

        public IActionSyntaxProvider Click(string selector, FindBy findMethod) => Click(Find(selector, findMethod));

        public IActionSyntaxProvider Click(ElementProxy element)
        {
            commandProvider.Click(element);
            return this;
        }

        public IActionSyntaxProvider Click(Alert alertAccessor)
        {
            if (alertAccessor.Field != AlertField.OKButton && alertAccessor.Field != AlertField.CancelButton)
            {
                Click(Alert.Cancel);
                throw new FluentException("FluentAutomation only supports clicking the OK or Cancel buttons of an alert/prompt.");
            }

            commandProvider.AlertClick(alertAccessor);
            return this;
        }

        public void Dispose()
        {
            isDisposed = true;
            commandProvider.Dispose();
        }

        public IActionSyntaxProvider DoubleClick(int x, int y)
        {
            commandProvider.DoubleClick(x, y);
            return this;
        }


        public IActionSyntaxProvider DoubleClick(string selector, int x, int y) => DoubleClick(selector, x, y, Defaults.FindMethod);

        public IActionSyntaxProvider DoubleClick(string selector, int x, int y, FindBy findMethod) => DoubleClick(Find(selector, findMethod), x, y);

        public IActionSyntaxProvider DoubleClick(ElementProxy element, int x, int y)
        {
            commandProvider.DoubleClick(element, x, y);
            return this;
        }

        public IActionSyntaxProvider DoubleClick(string selector) => DoubleClick(selector, Defaults.FindMethod);

        public IActionSyntaxProvider DoubleClick(string selector, FindBy findMethod) => DoubleClick(Find(selector, findMethod));

        public IActionSyntaxProvider DoubleClick(ElementProxy element)
        {
            commandProvider.DoubleClick(element);
            return this;
        }

        public DragDropSyntaxProvider Drag(string selector) => Drag(selector, Defaults.FindMethod);

        public DragDropSyntaxProvider Drag(string selector, FindBy findMethod) => Drag(Find(selector, findMethod));

        public DragDropSyntaxProvider Drag(ElementProxy element) => new DragDropSyntaxProvider(this, element);

        public DragDropSyntaxProvider Drag(string selector, int sourceX, int sourceY) => Drag(selector, sourceX, sourceY, Defaults.FindMethod);

        public DragDropSyntaxProvider Drag(string selector, int sourceX, int sourceY, FindBy findMethod) => Drag(Find(selector, findMethod), sourceX, sourceY);

        public DragDropSyntaxProvider Drag(ElementProxy element, int sourceX, int sourceY) => new DragDropSyntaxProvider(this, element, sourceX, sourceY);

        public DragDropByPositionSyntaxProvider Drag(int sourceX, int sourceY) => new DragDropByPositionSyntaxProvider(this, sourceX, sourceY);

        public TextEntrySyntaxProvider Enter(string text) => new TextEntrySyntaxProvider(this, text);

        public TextEntrySyntaxProvider Enter(dynamic nonString) => this.Enter(nonString.ToString());

        public ElementProxy Find(string selector) => Find(selector, Defaults.FindMethod);

        public ElementProxy Find(string selector, FindBy findMethod) => commandProvider.Find(selector, findMethod);

        public ElementProxy FindMultiple(string selector) => FindMultiple(selector, Defaults.FindMethod);

        public ElementProxy FindMultiple(string selector, FindBy findMethod) => commandProvider.FindMultiple(selector, findMethod);

        public IActionSyntaxProvider Focus(string selector) => Focus(selector, Defaults.FindMethod);

        public IActionSyntaxProvider Focus(string selector, FindBy findMethod) => Focus(Find(selector, findMethod));

        public IActionSyntaxProvider Focus(ElementProxy element)
        {
            commandProvider.Focus(element);
            return this;
        }

        public IActionSyntaxProvider Hover(int x, int y)
        {
            commandProvider.Hover(x, y);
            return this;
        }

        public IActionSyntaxProvider Hover(string selector, int x, int y) => Hover(selector, x, y, Defaults.FindMethod);

        public IActionSyntaxProvider Hover(string selector, int x, int y, FindBy findMethod) => Hover(Find(selector, findMethod), x, y);

        public IActionSyntaxProvider Hover(ElementProxy element, int x, int y)
        {
            commandProvider.Hover(element, x, y);
            return this;
        }

        public IActionSyntaxProvider Hover(string selector) => Hover(selector, Defaults.FindMethod);

        public IActionSyntaxProvider Hover(string selector, FindBy findMethod) => Hover(Find(selector, findMethod));

        public IActionSyntaxProvider Hover(ElementProxy element)
        {
            commandProvider.Hover(element);
            return this;
        }

        public bool IsDisposed() => isDisposed;
        public IActionSyntaxProvider Open(string url) => Open(url.StartsWith("/") ? new Uri(url, UriKind.Relative) : new Uri(url, UriKind.Absolute));

        public IActionSyntaxProvider Open(Uri url)
        {
            commandProvider.Navigate(url);
            return this;
        }

        public IActionSyntaxProvider Press(string keys)
        {
            commandProvider.Press(keys);
            return this;
        }

        public IActionSyntaxProvider RightClick(int x, int y)
        {
            commandProvider.RightClick(x, y);
            return this;
        }

        public IActionSyntaxProvider RightClick(string selector, int x, int y) => RightClick(selector, x, y, Defaults.FindMethod);

        public IActionSyntaxProvider RightClick(string selector, int x, int y, FindBy findMethod) => RightClick(Find(selector, findMethod), x, y);

        public IActionSyntaxProvider RightClick(ElementProxy element, int x, int y)
        {
            commandProvider.RightClick(element, x, y);
            return this;
        }


        public IActionSyntaxProvider RightClick(string selector) => RightClick(selector, Defaults.FindMethod);

        public IActionSyntaxProvider RightClick(string selector, FindBy findMethod) => RightClick(Find(selector, findMethod));

        public IActionSyntaxProvider RightClick(ElementProxy element)
        {
            commandProvider.RightClick(element);
            return this;
        }

        public IActionSyntaxProvider Scroll(int x, int y)
        {
            commandProvider.Hover(x, y);
            return this;
        }

        public IActionSyntaxProvider Scroll(string selector) => Scroll(selector, Defaults.FindMethod);

        public IActionSyntaxProvider Scroll(string selector, FindBy findMethod) => Scroll(Find(selector, findMethod));

        public IActionSyntaxProvider Scroll(ElementProxy element)
        {
            commandProvider.Hover(element);
            return this;
        }

        public SelectSyntaxProvider Select(string value) => new SelectSyntaxProvider(this, value, SelectionOption.Text);

        public SelectSyntaxProvider Select(Option mode, string value) => new SelectSyntaxProvider(this, value, mode == Option.Text ? SelectionOption.Text : SelectionOption.Value);

        public SelectSyntaxProvider Select(params string[] values) => new SelectSyntaxProvider(this, values, SelectionOption.Text);

        public SelectSyntaxProvider Select(Option mode, params string[] values) => new SelectSyntaxProvider(this, values, mode == Option.Text ? SelectionOption.Text : SelectionOption.Value);

        public SelectSyntaxProvider Select(int index) => new SelectSyntaxProvider(this, index, SelectionOption.Index);

        public SelectSyntaxProvider Select(params int[] indices) => new SelectSyntaxProvider(this, indices, SelectionOption.Index);

        public IActionSyntaxProvider TakeScreenshot(string screenshotName)
        {
            commandProvider.TakeScreenshot(screenshotName);
            return this;
        }

        public IActionSyntaxProvider Type(string text)
        {
            commandProvider.Type(text);
            return this;
        }

        public IActionSyntaxProvider Upload(string selector, string fileName) => Upload(selector, fileName, Defaults.FindMethod);

        public IActionSyntaxProvider Upload(string selector, string fileName, FindBy findMethod) => Upload(selector, 0, 0, fileName, findMethod);


        public IActionSyntaxProvider Upload(string selector, int x, int y, string fileName) => Upload(selector, x, y, fileName, Defaults.FindMethod);

        public IActionSyntaxProvider Upload(string selector, int x, int y, string fileName, FindBy findMethod) => Upload(Find(selector, findMethod), x, y, fileName);

        public IActionSyntaxProvider Upload(ElementProxy element, string fileName) => Upload(element, 0, 0, fileName);

        public IActionSyntaxProvider Upload(ElementProxy element, int x, int y, string fileName)
        {
            commandProvider.UploadFile(element, x, y, fileName);
            return this;
        }

        public IActionSyntaxProvider Wait()
        {
            commandProvider.Wait();
            return this;
        }

        public IActionSyntaxProvider Wait(int seconds) => Wait(TimeSpan.FromSeconds(seconds));

        public IActionSyntaxProvider Wait(TimeSpan timeSpan)
        {
            commandProvider.Wait(timeSpan);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            commandProvider.WaitUntil(conditionFunc);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction)
        {
            commandProvider.WaitUntil(conditionAction);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, int secondsToWait) => WaitUntil(conditionFunc, TimeSpan.FromSeconds(secondsToWait));

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            commandProvider.WaitUntil(conditionFunc, timeout);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, int secondsToWait) => WaitUntil(conditionAction, TimeSpan.FromSeconds(secondsToWait));

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            commandProvider.WaitUntil(conditionAction, timeout);
            return this;
        }

        internal ActionSyntaxProvider WithConfig(FluentSettings settings)
        {
            commandProvider.WithConfig(settings);
            this.settings = settings;
            return this;
        }

        public class DragDropByPositionSyntaxProvider
        {
            protected readonly int sourceX;
            protected readonly int sourceY;
            protected readonly ActionSyntaxProvider syntaxProvider;

            public DragDropByPositionSyntaxProvider(ActionSyntaxProvider syntaxProvider, int sourceX, int sourceY)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceX = sourceX;
                this.sourceY = sourceY;
            }

            /// <summary>
            ///     End Drag/Drop operation at specified coordinates.
            /// </summary>
            /// <param name="destinationX">X coordinate</param>
            /// <param name="destinationY">Y coordinate</param>
            public IActionSyntaxProvider To(int destinationX, int destinationY)
            {
                syntaxProvider.commandProvider.DragAndDrop(sourceX, sourceY, destinationX, destinationY);
                return syntaxProvider;
            }

            /// <summary>End Drag/Drop operation at the element specified by <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            public void To(string selector) => To(selector, Defaults.FindMethod);

            /// <summary>End Drag/Drop operation at the element specified by <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public void To(string selector, FindBy findMethod) => To(syntaxProvider.Find(selector, findMethod));

            /// <summary>
            ///     End Drag/Drop operation at specified <paramref name="targetElement" />.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            public void To(ElementProxy targetElement)
                => syntaxProvider.commandProvider.DragAndDrop(syntaxProvider.commandProvider.Find("html", Defaults.FindMethod), sourceX, sourceY, targetElement, 0, 0);

            /// <summary>End Drag/Drop operation at the element specified by <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public void To(string selector, int targetOffsetX, int targetOffsetY) => To(selector, targetOffsetX, targetOffsetY, Defaults.FindMethod);

            /// <summary>End Drag/Drop operation at the element specified by <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            /// <param name="findMethod">The find method.</param>
            public void To(string selector, int targetOffsetX, int targetOffsetY, FindBy findMethod)
                => To(syntaxProvider.Find(selector, findMethod), targetOffsetX, targetOffsetY);

            /// <summary>
            ///     End Drag/Drop operation at specified <paramref name="targetElement" />.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public void To(ElementProxy targetElement, int targetOffsetX, int targetOffsetY)
                => syntaxProvider.commandProvider.DragAndDrop(syntaxProvider.commandProvider.Find("html", Defaults.FindMethod), sourceX, sourceY, targetElement, targetOffsetX, targetOffsetY);
        }

        public class DragDropSyntaxProvider
        {
            protected readonly int offsetX;
            protected readonly int offsetY;
            protected readonly ElementProxy sourceElement;
            protected readonly ActionSyntaxProvider syntaxProvider;

            internal DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, ElementProxy element)
            {
                this.syntaxProvider = syntaxProvider;
                sourceElement = element;
            }

            internal DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, ElementProxy element, int offsetX, int offsetY)
            {
                this.syntaxProvider = syntaxProvider;
                sourceElement = element;
                this.offsetX = offsetX;
                this.offsetY = offsetY;
            }

            /// <summary>End Drag/Drop operation at element matching <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            public IActionSyntaxProvider To(string selector) => To(selector, Defaults.FindMethod);

            /// <summary>End Drag/Drop operation at element matching <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public IActionSyntaxProvider To(string selector, FindBy findMethod) => To(syntaxProvider.Find(selector, findMethod));

            /// <summary>
            ///     End Drag/Drop operation at specified <paramref name="targetElement" />.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            public IActionSyntaxProvider To(ElementProxy targetElement)
            {
                if (offsetX != 0 || offsetY != 0)
                    syntaxProvider.commandProvider.DragAndDrop(sourceElement, offsetX, offsetY, targetElement, 0, 0);
                else
                    syntaxProvider.commandProvider.DragAndDrop(sourceElement, targetElement);

                return syntaxProvider;
            }

            /// <summary>End Drag/Drop operation at element specified by <paramref name="selector" /> with offset.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            /// <param name="findMethod">The find method.</param>
            public IActionSyntaxProvider To(string selector, int targetOffsetX, int targetOffsetY, FindBy findMethod = Defaults.FindMethod)
                => To(syntaxProvider.Find(selector, findMethod), targetOffsetX, targetOffsetY);

            /// <summary>
            ///     End Drag/Drop operation at specified <paramref name="targetElement" />.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            /// <param name="targetOffsetX">X-offset for drop.</param>
            /// <param name="targetOffsetY">Y-offset for drop.</param>
            public IActionSyntaxProvider To(ElementProxy targetElement, int targetOffsetX, int targetOffsetY)
            {
                syntaxProvider.commandProvider.DragAndDrop(sourceElement, offsetX, offsetY, targetElement, targetOffsetX, targetOffsetY);
                return syntaxProvider;
            }
        }

        public class SelectSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider;
            protected readonly dynamic value;
            internal readonly SelectionOption mode;

            internal SelectSyntaxProvider(ActionSyntaxProvider syntaxProvider, dynamic value, SelectionOption mode)
            {
                this.syntaxProvider = syntaxProvider;
                this.value = value;
                this.mode = mode;
            }

            /// <summary>Select from element matching <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            public IActionSyntaxProvider From(string selector) => From(selector, Defaults.FindMethod);

            /// <summary>Select from element matching <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public IActionSyntaxProvider From(string selector, FindBy findMethod) => From(syntaxProvider.Find(selector, findMethod));

            /// <summary>
            ///     Select from specified <paramref name="element" />.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider From(ElementProxy element)
            {
                if (mode == SelectionOption.Value)
                {
                    if (value is string)
                        syntaxProvider.commandProvider.SelectValue(element, value);
                    else if (value is string[])
                        syntaxProvider.commandProvider.MultiSelectValue(element, value);
                }
                else if (mode == SelectionOption.Text)
                {
                    if (value is string)
                        syntaxProvider.commandProvider.SelectText(element, value);
                    else if (value is string[])
                        syntaxProvider.commandProvider.MultiSelectText(element, value);
                }
                else if (value is int)
                {
                    syntaxProvider.commandProvider.SelectIndex(element, value);
                }
                else if (value is int[])
                {
                    syntaxProvider.commandProvider.MultiSelectIndex(element, value);
                }

                return syntaxProvider;
            }
        }

        public class SwitchSyntaxProvider
        {
            private readonly ActionSyntaxProvider syntaxProvider;

            internal SwitchSyntaxProvider(ActionSyntaxProvider syntaxProvider)
            {
                this.syntaxProvider = syntaxProvider;
            }

            /// <summary>
            ///     Switch to a frame/iframe via page selector or ID
            /// </summary>
            /// <param name="frameSelector"></param>
            public IActionSyntaxProvider Frame(string frameSelector)
            {
                syntaxProvider.commandProvider.SwitchToFrame(frameSelector);
                return syntaxProvider;
            }

            /// <summary>
            ///     Switch back to the top-level document
            /// </summary>
            /// <returns></returns>
            public IActionSyntaxProvider Frame() => Frame(string.Empty);

            /// <summary>
            ///     Switch focus to a previously selected frame/iframe
            /// </summary>
            /// <param name="frameElement"></param>
            /// <returns></returns>
            public IActionSyntaxProvider Frame(ElementProxy frameElement)
            {
                syntaxProvider.commandProvider.SwitchToFrame(frameElement);
                return syntaxProvider;
            }

            /// <summary>
            ///     Switch to a window by name or URL (can be relative such as /about -- matches on the end of the URL)
            /// </summary>
            /// <param name="windowName"></param>
            public IActionSyntaxProvider Window(string windowName)
            {
                syntaxProvider.commandProvider.SwitchToWindow(windowName);
                return syntaxProvider;
            }

            /// <summary>
            ///     Switch back to the primary window
            /// </summary>
            public IActionSyntaxProvider Window() => Window(string.Empty);
        }

        public class TextAppendSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider;
            protected readonly string text;
            protected bool eventsEnabled = true;
            protected bool isAppend = false;

            internal TextAppendSyntaxProvider(ActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            /// <summary>Enter text into input or textarea element matching <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            public IActionSyntaxProvider To(string selector) => To(selector, Defaults.FindMethod);

            /// <summary>Enter text into input or textarea element matching <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public IActionSyntaxProvider To(string selector, FindBy findMethod) => To(syntaxProvider.Find(selector, findMethod));

            /// <summary>
            ///     Enter text into specified <paramref name="element" />.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider To(ElementProxy element)
            {
                if (!element.Element.IsText)
                    throw new FluentException("Append().To() is only supported on text elements (input, textarea, etc).");

                if (eventsEnabled)
                    syntaxProvider.commandProvider.AppendText(element, text);
                else
                    syntaxProvider.commandProvider.AppendTextWithoutEvents(element, text);

                return syntaxProvider;
            }

            /// <summary>
            ///     Set text entry to set value without firing key events. Faster, but may cause issues with applications
            ///     that bind to the keyup/keydown/keypress events to function.
            /// </summary>
            /// <returns>
            ///     <c>TextEntrySyntaxProvider</c>
            /// </returns>
            public TextAppendSyntaxProvider WithoutEvents()
            {
                eventsEnabled = false;
                return this;
            }
        }

        public class TextEntrySyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider;
            protected readonly string text;
            protected bool eventsEnabled = true;

            internal TextEntrySyntaxProvider(ActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            /// <summary>Enter text into input or textarea element matching <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            public IActionSyntaxProvider In(string selector) => In(selector, Defaults.FindMethod);

            /// <summary>Enter text into input or textarea element matching <paramref name="selector" />.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public IActionSyntaxProvider In(string selector, FindBy findMethod) => In(syntaxProvider.Find(selector, findMethod));

            /// <summary>
            ///     Enter text into specified <paramref name="element" />.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider In(ElementProxy element)
            {
                if (!element.Element.IsText)
                    throw new FluentException("Enter().In() is only supported on text elements (input, textarea, etc).");

                if (eventsEnabled)
                    syntaxProvider.commandProvider.EnterText(element, text);
                else
                    syntaxProvider.commandProvider.EnterTextWithoutEvents(element, text);

                return syntaxProvider;
            }

            /// <summary>
            ///     Enter text into the active prompt
            /// </summary>
            /// <param name="accessor"></param>
            public IActionSyntaxProvider In(Alert accessor)
            {
                if (accessor.Field != AlertField.Input)
                {
                    syntaxProvider.commandProvider.AlertClick(Alert.Cancel);
                    throw new FluentException("FluentAutomation only supports entering text in text input of a prompt.");
                }

                syntaxProvider.commandProvider.AlertEnterText(text);
                return syntaxProvider;
            }

            /// <summary>
            ///     Set text entry to set value without firing key events. Faster, but may cause issues with applications
            ///     that bind to the keyup/keydown/keypress events to function.
            /// </summary>
            /// <returns>
            ///     <c>TextEntrySyntaxProvider</c>
            /// </returns>
            public TextEntrySyntaxProvider WithoutEvents()
            {
                eventsEnabled = false;
                return this;
            }
        }
    }

    /// <summary>
    ///     Option mode for <select /> manipulation: Text, Value or Index
    /// </summary>
    internal enum SelectionOption
    {
        Text = 1,
        Value = 2,
        Index = 3
    }

    public enum Option
    {
        Text = 1,
        Value = 2
    }
}