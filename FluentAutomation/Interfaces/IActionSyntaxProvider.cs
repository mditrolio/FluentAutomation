namespace FluentAutomation.Interfaces
{
    using System;
    using System.Linq.Expressions;

    /// <summary>The action syntax provider interface.</summary>
    /// <seealso cref="FluentAutomation.Interfaces.ISyntaxProvider" />
    public interface IActionSyntaxProvider : ISyntaxProvider
    {
        /// <summary>Asserts - Fluent's assertion functionality.</summary>
        /// <value>The assert.</value>
        AssertSyntaxProvider Assert { get; }

        /// <summary>
        ///     Expects - Fluent's passive expect functionality. Defaults to Assert mode (fail on exception) for backwards
        ///     compatibility. This will change
        ///     in a future release. Can be set to passively expect by setting FluentAutomation.Settings.ExpectIsAssert = false
        /// </summary>
        /// <value>The expect.</value>
        AssertSyntaxProvider Expect { get; }

        /// <summary>Switch to another window or frame.</summary>
        /// <value>The switch.</value>
        ActionSyntaxProvider.SwitchSyntaxProvider Switch { get; }

        /// <summary>
        ///     Append a number or other object value into a valid input or textarea. Syntactical candy to avoid having to
        ///     call .ToString() on integers in tests.
        /// </summary>
        /// <param name="nonString">Value to enter into input or textarea.</param>
        ActionSyntaxProvider.TextAppendSyntaxProvider Append(dynamic nonString);

        /// <summary>Append text into a valid input or textarea.</summary>
        /// <param name="text">Text to enter into input or textarea.</param>
        ActionSyntaxProvider.TextAppendSyntaxProvider Append(string text);

        // native only
        /// <summary>Click a specified element.</summary>
        /// <param name="element"><see cref="IElement" /> factory function.</param>
        IActionSyntaxProvider Click(ElementProxy element);

        /// <summary>Click a specified coordinate, starting from the position of the provided <paramref name="element" />.</summary>
        /// <param name="element"><see cref="IElement" /> factory function.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        IActionSyntaxProvider Click(ElementProxy element, int x, int y);

        /// <summary>Click a specified button within an alert.</summary>
        /// <param name="accessor">The accessor.</param>
        IActionSyntaxProvider Click(Alert accessor);

        // remote commands
        /// <summary>Click at the specified coordinates.</summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate</param>
        IActionSyntaxProvider Click(int x, int y);

        /// <summary>Click the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        IActionSyntaxProvider Click(string selector);

        /// <summary>Click the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider Click(string selector, FindBy findMethod);

        /// <summary>Click a specified coordinate, starting from the position of the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        IActionSyntaxProvider Click(string selector, int x, int y);

        /// <summary>Click a specified coordinate, starting from the position of the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider Click(string selector, int x, int y, FindBy findMethod);

        /// <summary>DoubleClick a specified element.</summary>
        /// <param name="element"><see cref="IElement" /> factory function.</param>
        IActionSyntaxProvider DoubleClick(ElementProxy element);

        /// <summary>DoubleClick a specified coordinate, starting from the position of the provided <paramref name="element" />.</summary>
        /// <param name="element"><see cref="IElement" /> factory function.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        IActionSyntaxProvider DoubleClick(ElementProxy element, int x, int y);

        /// <summary>DoubleClick at the specified coordinates.</summary>
        /// <param name="x">X-coordinate specified.</param>
        /// <param name="y">Y-coordinate specified.</param>
        IActionSyntaxProvider DoubleClick(int x, int y);

        /// <summary>DoubleClick the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        IActionSyntaxProvider DoubleClick(string selector);

        /// <summary>DoubleClick the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider DoubleClick(string selector, FindBy findMethod);

        /// <summary>
        ///     DoubleClick a specified coordinate, starting from the position of the element matching
        ///     <paramref name="selector" />.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        IActionSyntaxProvider DoubleClick(string selector, int x, int y);

        /// <summary>
        ///     DoubleClick a specified coordinate, starting from the position of the element matching
        ///     <paramref name="selector" />.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider DoubleClick(string selector, int x, int y, FindBy findMethod);

        /// <summary>Begin a Drag/Drop operation starting with the specified element.</summary>
        /// <param name="element"><see cref="IElement" /> factory function.</param>
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(ElementProxy element);

        /// <summary>Begin a Drag/Drop operation starting with the specified by <paramref name="selector" /> and an offset.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="offsetX">The offset x.</param>
        /// <param name="offsetY">The offset y.</param>
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(string selector, int offsetX, int offsetY);

        /// <summary>Begin a Drag/Drop operation starting with the specified by <paramref name="selector" /> and an offset.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="offsetX">The offset x.</param>
        /// <param name="offsetY">The offset y.</param>
        /// <param name="findMethod">The find method.</param>
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(string selector, int offsetX, int offsetY, FindBy findMethod);

        /// <summary>Begin a Drag/Drop operation starting with the specified element and an offset.</summary>
        /// <param name="element"><see cref="IElement" /> factory function.</param>
        /// <param name="offsetX">The offset x.</param>
        /// <param name="offsetY">The offset y.</param>
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(ElementProxy element, int offsetX, int offsetY);

        /// <summary>Begin a Drag/Drop operation using coordinates.</summary>
        /// <param name="sourceX">The source x.</param>
        /// <param name="sourceY">The source y.</param>
        ActionSyntaxProvider.DragDropByPositionSyntaxProvider Drag(int sourceX, int sourceY);

        /// <summary>Begin a Drag/Drop operation starting with the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(string selector);

        /// <summary>Begin a Drag/Drop operation starting with the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        ActionSyntaxProvider.DragDropSyntaxProvider Drag(string selector, FindBy findMethod);

        /// <summary>
        ///     Enter a number or other object value into a valid input or textarea. Syntactical candy to avoid having to call
        ///     .ToString() on integers in tests.
        /// </summary>
        /// <param name="nonString">Value to enter into input or textarea.</param>
        ActionSyntaxProvider.TextEntrySyntaxProvider Enter(dynamic nonString);

        /// <summary>Enter text into a valid input or textarea.</summary>
        /// <param name="text">Text to enter into input or textarea.</param>
        ActionSyntaxProvider.TextEntrySyntaxProvider Enter(string text);

        /// <summary>Find an element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The DOM selector.</param>
        ElementProxy Find(string selector);

        /// <summary>Find an element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The DOM selector.</param>
        /// <param name="findMethod">The find method.</param>
        ElementProxy Find(string selector, FindBy findMethod);

        /// <summary>Find a set of elements matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        ElementProxy FindMultiple(string selector);

        /// <summary>Find a set of elements matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        ElementProxy FindMultiple(string selector, FindBy findMethod);

        /// <summary>Sets the focus to a specific element.</summary>
        /// <param name="element">IElement factory function.</param>
        IActionSyntaxProvider Focus(ElementProxy element);

        /// <summary>Sets the focus to element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        IActionSyntaxProvider Focus(string selector);

        /// <summary>Sets the focus to element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider Focus(string selector, FindBy findMethod);

        /// <summary>Causes the mouse to hover over a specified element.</summary>
        /// <param name="element">IElement factory function.</param>
        IActionSyntaxProvider Hover(ElementProxy element);

        /// <summary>
        ///     Causes the mouse to hover over a specific coordinate, starting from the position of the provided
        ///     <paramref name="element" />.
        /// </summary>
        /// <param name="element">IElement factory function.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        IActionSyntaxProvider Hover(ElementProxy element, int x, int y);

        /// <summary>Causes the mouse to hover over a specified coordinate.</summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        IActionSyntaxProvider Hover(int x, int y);

        /// <summary>Causes the mouse to hover over element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        IActionSyntaxProvider Hover(string selector);

        /// <summary>Causes the mouse to hover over element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider Hover(string selector, FindBy findMethod);

        /// <summary>
        ///     Causes the mouse to hover over a specific coordinate, starting from the position of the element matching
        ///     <paramref name="selector" />.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        IActionSyntaxProvider Hover(string selector, int x, int y);

        /// <summary>
        ///     Causes the mouse to hover over a specific coordinate, starting from the position of the element matching
        ///     <paramref name="selector" />.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider Hover(string selector, int x, int y, FindBy findMethod);

        /// <summary>Open a web browser and navigate to specified URL</summary>
        /// <param name="url">Fully-qualified URL. Example: <c>http://google.com/</c></param>
        IActionSyntaxProvider Open(string url);

        /// <summary>Open a web browser and navigate to the specified URI</summary>
        /// <param name="uri">Absolute URI. Example: <c>new Uri("http://www.google.com/");</c></param>
        IActionSyntaxProvider Open(Uri uri);

        /// <summary>Triggers keypress events using WinForms SendKeys.</summary>
        /// <param name="keys">WinForms SendKeys values. Example: <c>{ENTER}</c></param>
        IActionSyntaxProvider Press(string keys);

        /// <summary>RightClick a specified element.</summary>
        /// <param name="element"><see cref="IElement" /> factory function.</param>
        IActionSyntaxProvider RightClick(ElementProxy element);

        /// <summary>RightClick at the specified coordinates.</summary>
        /// <param name="x">X-coordinate specified.</param>
        /// <param name="y">Y-coordinate specified.</param>
        IActionSyntaxProvider RightClick(int x, int y);

        /// <summary>RightClick the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        IActionSyntaxProvider RightClick(string selector);

        /// <summary>RightClick the element matching <paramref name="selector" />.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider RightClick(string selector, FindBy findMethod);

        /// <summary>
        ///     RightClick a specified coordinate, starting from the position of the element matching
        ///     <paramref name="selector" />.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        IActionSyntaxProvider RightClick(string selector, int x, int y);

        /// <summary>
        ///     RightClick a specified coordinate, starting from the position of the element matching
        ///     <paramref name="selector" />.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider RightClick(string selector, int x, int y, FindBy findMethod);

        /// <summary>RightClick a specified coordinate, starting from the position of the provided <paramref name="element" />.</summary>
        /// <param name="element"><see cref="IElement" /> factory function.</param>
        /// <param name="x">X-coordinate specified.</param>
        /// <param name="y">Y-coordinate specified.</param>
        IActionSyntaxProvider RightClick(ElementProxy element, int x, int y);

        /// <summary>Scrolls the viewport to the specified coordinates. Alias for Hover(int, int).</summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        IActionSyntaxProvider Scroll(int x, int y);

        /// <summary>Scrolls the viewport to the element matching <paramref name="selector" />. Alias for Hover(string).</summary>
        /// <param name="selector">The selector.</param>
        IActionSyntaxProvider Scroll(string selector);

        /// <summary>Scrolls the viewport to the element matching <paramref name="selector" />. Alias for Hover(string).</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider Scroll(string selector, FindBy findMethod);

        /// <summary>Scrolls the viewport to the specified <paramref name="element" />. Alias for Hover(ElementProxy).</summary>
        /// <param name="element">IElement factory function.</param>
        IActionSyntaxProvider Scroll(ElementProxy element);

        /// <summary>
        ///     Manipulates a <c>&lt;select /&gt;</c> DOM element by selecting options with matching
        ///     <paramref name="values" /> using the specified <paramref name="mode" />.
        /// </summary>
        /// <param name="mode">Mode of interaction with the <c>&lt;select /&gt;</c>; by Text or Value.</param>
        /// <param name="values">Options to be selected.</param>
        ActionSyntaxProvider.SelectSyntaxProvider Select(Option mode, params string[] values);

        /// <summary>
        ///     Manipulates a <c>&lt;select /&gt;</c> DOM element by selecting an option with matching
        ///     <paramref name="value" /> using the specified <paramref name="mode" />.
        /// </summary>
        /// <param name="mode">Mode of interaction with the <c>&lt;select /&gt;</c>; by Text or Value.</param>
        /// <param name="value">Option to be selected.</param>
        ActionSyntaxProvider.SelectSyntaxProvider Select(Option mode, string value);

        /// <summary>
        ///     Manipulates a <c>&lt;select /&gt;</c> DOM element by selecting options at the specified
        ///     <paramref name="indices" />.
        /// </summary>
        /// <param name="indices">Options to be selected by Index.</param>
        ActionSyntaxProvider.SelectSyntaxProvider Select(params int[] indices);

        /// <summary>Manipulates a <c>&lt;select /&gt;</c> DOM element by selecting options with matching <paramref name="text" />.</summary>
        /// <param name="text">The text.</param>
        ActionSyntaxProvider.SelectSyntaxProvider Select(params string[] text);

        /// <summary>
        ///     Manipulates a <c>&lt;select /&gt;</c> DOM element by selecting an option at the specified
        ///     <paramref name="index" />.
        /// </summary>
        /// <param name="index">Option to be selected by Index.</param>
        ActionSyntaxProvider.SelectSyntaxProvider Select(int index);

        /// <summary>
        ///     Manipulates a <c>&lt;select /&gt;</c> DOM element by selecting an option with matching
        ///     <paramref name="text" />.
        /// </summary>
        /// <param name="text">Option to be selected by Text.</param>
        ActionSyntaxProvider.SelectSyntaxProvider Select(string text);

        /// <summary>Takes a screenshot of the active web browser window.</summary>
        /// <param name="screenshotName">Filename to save the screenshot with.</param>
        IActionSyntaxProvider TakeScreenshot(string screenshotName);

        /// <summary>
        ///     Triggers Win32 events per character in the provided string. Not for use with WinForms SendKeys commands, only
        ///     simple characters. Useful
        ///     for entering text into applications that can gain focus but do not have proper DOM representation for use with
        ///     <see cref="Enter" />.
        /// </summary>
        /// <param name="text">String to be sent, one character at a time.</param>
        IActionSyntaxProvider Type(string text);

        /// <summary>
        ///     Upload a file via a standard
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element. Triggers a click event at the specified coordinate, starting from the position of the provided
        ///     <paramref name="element" />.
        /// </summary>
        /// <param name="element">
        ///     IElement factory function for the
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element.
        /// </param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="fileName">
        ///     Path to the local file to be uploaded. Example:
        ///     <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c>
        /// </param>
        IActionSyntaxProvider Upload(ElementProxy element, int x, int y, string fileName);

        /// <summary>
        ///     Upload a file via a standard
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="fileName">
        ///     Path to the local file to be uploaded. Example:
        ///     <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c>
        /// </param>
        IActionSyntaxProvider Upload(ElementProxy element, string fileName);

        /// <summary>
        ///     Upload a file via a standard
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element. Triggers a click event at the specified coordinate, starting from the position of the element matching
        ///     <paramref name="selector" />.
        /// </summary>
        /// <param name="selector">
        ///     Selector for the
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element.
        /// </param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="fileName">
        ///     Path to the local file to be uploaded. Example:
        ///     <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c>
        /// </param>
        IActionSyntaxProvider Upload(string selector, int x, int y, string fileName);

        /// <summary>
        ///     Upload a file via a standard
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element. Triggers a click event at the specified coordinate, starting from the position of the element matching
        ///     <paramref name="selector" />.
        /// </summary>
        /// <param name="selector">
        ///     Selector for the
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element.
        /// </param>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="fileName">
        ///     Path to the local file to be uploaded. Example:
        ///     <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c>
        /// </param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider Upload(string selector, int x, int y, string fileName, FindBy findMethod);

        /// <summary>
        ///     Uploads a file via a standard
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element.
        /// </summary>
        /// <param name="selector">
        ///     Selector for the
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element.
        /// </param>
        /// <param name="fileName">
        ///     Path to the local file to be uploaded. Example:
        ///     <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c>
        /// </param>
        IActionSyntaxProvider Upload(string selector, string fileName);

        /// <summary>
        ///     Uploads a file via a standard
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element.
        /// </summary>
        /// <param name="selector">
        ///     Selector for the
        ///     <c>
        ///         <input type="file"></input>
        ///     </c>
        ///     DOM element.
        /// </param>
        /// <param name="fileName">
        ///     Path to the local file to be uploaded. Example:
        ///     <c>C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum.jpg</c>
        /// </param>
        /// <param name="findMethod">The find method.</param>
        IActionSyntaxProvider Upload(string selector, string fileName, FindBy findMethod);

        /// <summary>Waits the duration of the WaitTimeout as specified in settings.</summary>
        IActionSyntaxProvider Wait();

        /// <summary>Waits a determined period of time.</summary>
        /// <param name="seconds">Seconds to wait.</param>
        IActionSyntaxProvider Wait(int seconds);

        /// <summary>Waits a determined period of time.</summary>
        /// <param name="timeSpan">TimeSpan to wait.</param>
        IActionSyntaxProvider Wait(TimeSpan timeSpan);

        /// <summary>
        ///     Wait until the provided <paramref name="conditionAction">action</paramref> succeeds. Intended for use with
        ///     I.Expect.* methods.
        /// </summary>
        /// <param name="conditionAction">
        ///     Action to be repeated until it succeeds or exceeds the timeout.
        ///     <see cref="Settings.DefaultWaitUntilTimeout" /> determines the timeout.
        /// </param>
        IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction);

        /// <summary>
        ///     Wait until the provided <paramref name="conditionAction">action</paramref> succeeds. Intended for use with
        ///     I.Expect.* methods.
        /// </summary>
        /// <param name="conditionAction">Action to be repeated until it succeeds or exceeds the timeout.</param>
        /// <param name="timeout">Timeout for this specific action.</param>
        IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, TimeSpan timeout);

        /// <summary>
        ///     Wait until the provided <paramref name="conditionAction">action</paramref> succeeds. Intended for use with
        ///     I.Expect.* methods.
        /// </summary>
        /// <param name="conditionAction">Action to be repeated until it succeeds or exceeds the timeout.</param>
        /// <param name="seconds">Timeout in seconds for this specific action.</param>
        IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, int seconds);

        /// <summary>Wait until the provided <paramref name="conditionFunc">function</paramref> returns <c>true</c>.</summary>
        /// <param name="conditionFunc">
        ///     Function to be repeated until it returns true or exceeds the timeout.
        ///     <see cref="Settings.DefaultWaitUntilTimeout" /> determines the timeout.
        /// </param>
        IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc);

        /// <summary>Wait until the provided <paramref name="conditionFunc">function</paramref> returns <c>true</c>.</summary>
        /// <param name="conditionFunc">Function to be repeated until it returns true or exceeds the timeout.</param>
        /// <param name="seconds">Timeout in seconds for this specific action.</param>
        IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, int seconds);

        /// <summary>Wait until the provided <paramref name="conditionFunc">function</paramref> returns <c>true</c>.</summary>
        /// <param name="conditionFunc">Function to be repeated until it returns true or exceeds the timeout.</param>
        /// <param name="timeout">Timeout for this specific action.</param>
        IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout);
    }
}