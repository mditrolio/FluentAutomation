namespace FluentAutomation
{
    using System;
    using System.Linq.Expressions;

    using Exceptions;

    using Interfaces;

    /// <summary>The assert syntax provider.</summary>
    public class AssertSyntaxProvider : BaseAssertSyntaxProvider
    {
        /// <summary>Initializes a new instance of the <see cref="AssertSyntaxProvider" /> class.</summary>
        /// <param name="commandProvider">The command provider.</param>
        /// <param name="assertProvider">The assert provider.</param>
        public AssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider)
            : base(commandProvider, assertProvider)
        {
        }

        /// <summary>Negative assertions</summary>
        /// <value>The not.</value>
        public NotAssertSyntaxProvider Not => new NotAssertSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider);

        /// <summary>Assert that a matching attribute is found.</summary>
        /// <param name="attributeName">Attribute name. Example: src</param>
        public AssertAttributeSyntaxProvider Attribute(string attributeName) => Attribute(attributeName, null);

        /// <summary>Assert that a matching CSS property is found.</summary>
        /// <param name="attributeName">Attribute name. Example: src</param>
        /// <param name="propertyValue">The property value.</param>
        public AssertAttributeSyntaxProvider Attribute(string attributeName, string propertyValue) => new AssertAttributeSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, attributeName, propertyValue);

        /// <summary>Assert that a matching CSS class is found.</summary>
        /// <param name="className">CSS class name. Example: .row</param>
        public AssertClassSyntaxProvider Class(string className) => new AssertClassSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, className);

        /// <summary>Assert a specific count.</summary>
        /// <param name="count">Number of elements found.</param>
        public AssertCountSyntaxProvider Count(int count) => new AssertCountSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, count);

        /// <summary>Assert that a matching CSS property is found.</summary>
        /// <param name="propertyName">CSS property name. Example: color</param>
        public AssertCssPropertySyntaxProvider Css(string propertyName) => Css(propertyName, null);

        /// <summary>Assert that a matching CSS property is found.</summary>
        /// <param name="propertyName">CSS property name. Example: color</param>
        /// <param name="propertyValue">CSS property value. Example: red</param>
        public AssertCssPropertySyntaxProvider Css(string propertyName, string propertyValue) => new AssertCssPropertySyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, propertyName, propertyValue);

        /// <summary>Assert the element specified exists.</summary>
        /// <param name="selector">Element selector.</param>
        public AssertSyntaxProvider Exists(string selector) => Exists(selector, Defaults.FindMethod);

        /// <summary>Assert the element specified exists.</summary>
        /// <param name="selector">Element selector.</param>
        /// <param name="findMethod">The find method.</param>
        public AssertSyntaxProvider Exists(string selector, FindBy findMethod)
        {
            assertProvider.Exists(selector, findMethod);
            return assertSyntaxProvider;
        }

        /// <summary>Assert the element specified exists.</summary>
        /// <param name="element">Reference to element</param>
        public AssertSyntaxProvider Exists(ElementProxy element)
        {
            assertProvider.Exists(element);
            return assertSyntaxProvider;
        }

        /// <summary>Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns false.</summary>
        /// <param name="matchFunc">The match function.</param>
        public AssertSyntaxProvider False(Expression<Func<bool>> matchFunc)
        {
            assertProvider.False(matchFunc);
            return assertSyntaxProvider;
        }

        /// <summary>Assert that Text matches specified <paramref name="text" />.</summary>
        /// <param name="text">Text that must be exactly matched.</param>
        public AssertTextSyntaxProvider Text(string text) => new AssertTextSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, text);

        /// <summary>Assert that Text provided to specified <paramref name="matchFunc">match function</paramref> returns true.</summary>
        /// <param name="matchFunc">Function to evaluate if Text matches. Example: (text) =&gt; text.Contains("Hello")</param>
        public AssertTextSyntaxProvider Text(Expression<Func<string, bool>> matchFunc) => new AssertTextSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, matchFunc);

        /// <summary>Assert that an arbitrary <paramref name="matchAction">action</paramref> throws an Exception.</summary>
        /// <param name="matchAction">The match action.</param>
        public AssertSyntaxProvider Throws(Expression<Action> matchAction)
        {
            assertProvider.Throws(matchAction);
            return assertSyntaxProvider;
        }

        /// <summary>Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> returns true.</summary>
        /// <param name="matchFunc">The match function.</param>
        public AssertSyntaxProvider True(Expression<Func<bool>> matchFunc)
        {
            assertProvider.True(matchFunc);
            return assertSyntaxProvider;
        }

        /// <summary>Assert the current web browser's URL to match <paramref name="expectedUrl" />.</summary>
        /// <param name="expectedUrl">Fully-qualified URL to use for matching..</param>
        public AssertSyntaxProvider Url(string expectedUrl) => Url(new Uri(expectedUrl, UriKind.Absolute));

        /// <summary>Assert the current web browser's URI to match <paramref name="expectedUri" />.</summary>
        /// <param name="expectedUri">Absolute URI to use for matching..</param>
        public AssertSyntaxProvider Url(Uri expectedUri)
        {
            assertProvider.Url(expectedUri);
            return assertSyntaxProvider;
        }

        /// <summary>
        ///     Assert the current web browser's URI provided to the specified
        ///     <paramref name="uriExpression">URI expression</paramref> will return true;
        /// </summary>
        /// <param name="uriExpression">URI expression to use for matching..</param>
        public AssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression)
        {
            assertProvider.Url(uriExpression);
            return assertSyntaxProvider;
        }

        /// <summary>Assert a specific integer <paramref name="value" /></summary>
        /// <param name="value">Int32 value expected.</param>
        public AssertValueSyntaxProvider Value(int value) => Value(value.ToString());

        /// <summary>Assert a specific string <paramref name="value" />.</summary>
        /// <param name="value">String value.</param>
        public AssertValueSyntaxProvider Value(string value) => new AssertValueSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, value);

        /// <summary>Values the specified match function.</summary>
        /// <param name="matchFunc">The match function.</param>
        public AssertValueSyntaxProvider Value(Expression<Func<string, bool>> matchFunc) => new AssertValueSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, matchFunc);

        /// <summary>Assert that the element matching the selector is visible and can be interacted with.</summary>
        /// <param name="selector">The selector.</param>
        public AssertSyntaxProvider Visible(string selector) => Visible(selector, Defaults.FindMethod);

        /// <summary>Assert that the element matching the selector is visible and can be interacted with.</summary>
        /// <param name="selector">The selector.</param>
        /// <param name="findMethod">The find method.</param>
        public AssertSyntaxProvider Visible(string selector, FindBy findMethod)
        {
            assertProvider.Visible(selector, findMethod);
            return this;
        }

        /// <summary>Assert that the element is visible and can be interacted with.</summary>
        /// <param name="element">The element.</param>
        public AssertSyntaxProvider Visible(ElementProxy element)
        {
            assertProvider.Visible(element);
            return this;
        }

        /// <summary>The assert attribute syntax provider.</summary>
        public class AssertAttributeSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string attributeName;
            private readonly string attributeValue;
            private readonly bool notMode;

            /// <summary>Initializes a new instance of the <see cref="AssertAttributeSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="attributeName">Name of the attribute.</param>
            /// <param name="attributeValue">The attribute value.</param>
            public AssertAttributeSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string attributeName, string attributeValue)
                : this(commandProvider, assertProvider, assertSyntaxProvider, attributeName, attributeValue, false)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="AssertAttributeSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="attributeName">Name of the attribute.</param>
            /// <param name="attributeValue">The attribute value.</param>
            /// <param name="notMode">if set to <see langword="true" /> [not mode].</param>
            public AssertAttributeSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string attributeName, string attributeValue, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.attributeName = attributeName;
                this.attributeValue = attributeValue;
                this.notMode = notMode;
            }

            /// <summary>Assert that CSS Class does not match - Reverses assertions in this chain.</summary>
            /// <value>The not.</value>
            public AssertAttributeSyntaxProvider Not => new AssertAttributeSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, attributeName, attributeValue, true);

            /// <summary>Element matching <paramref name="selector" /> that should have matching CSS class.</summary>
            /// <param name="selector">The selector.</param>
            public AssertSyntaxProvider On(string selector) => On(selector, Defaults.FindMethod);

            /// <summary>Element matching <paramref name="selector" /> that should have matching CSS class.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public AssertSyntaxProvider On(string selector, FindBy findMethod)
            {
                if (notMode)
                    assertProvider.NotAttribute(selector, attributeName, attributeValue, findMethod);
                else
                    assertProvider.Attribute(selector, attributeName, attributeValue, findMethod);

                return assertSyntaxProvider;
            }

            /// <summary>Specified <paramref name="element" /> that should have matching CSS class.</summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider On(ElementProxy element)
            {
                if (notMode)
                    assertProvider.NotAttribute(element, attributeName, attributeValue);
                else
                    assertProvider.Attribute(element, attributeName, attributeValue);

                return assertSyntaxProvider;
            }
        }

        /// <summary>The assert class syntax provider.</summary>
        public class AssertClassSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly string className;
            private readonly bool notMode;

            /// <summary>Initializes a new instance of the <see cref="AssertClassSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="className">Name of the class.</param>
            public AssertClassSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string className)
                : this(commandProvider, assertProvider, assertSyntaxProvider, className, false)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="AssertClassSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="className">Name of the class.</param>
            /// <param name="notMode">if set to <see langword="true" /> [not mode].</param>
            public AssertClassSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string className, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.className = className;
                this.notMode = notMode;
            }

            /// <summary>Assert that CSS Class does not match - Reverses assertions in this chain.</summary>
            /// <value>The not.</value>
            public AssertClassSyntaxProvider Not => new AssertClassSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, className, true);

            /// <summary>Element matching <paramref name="selector" /> that should have matching CSS class.</summary>
            /// <param name="selector">The selector.</param>
            public AssertSyntaxProvider On(string selector) => On(selector, Defaults.FindMethod);

            /// <summary>Element matching <paramref name="selector" /> that should have matching CSS class.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public AssertSyntaxProvider On(string selector, FindBy findMethod)
            {
                if (notMode)
                    assertProvider.NotCssClass(selector, className, findMethod);
                else
                    assertProvider.CssClass(selector, className, findMethod);

                return assertSyntaxProvider;
            }

            /// <summary>Specified <paramref name="element" /> that should have matching CSS class.</summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider On(ElementProxy element)
            {
                if (notMode)
                    assertProvider.NotCssClass(element, className);
                else
                    assertProvider.CssClass(element, className);

                return assertSyntaxProvider;
            }
        }

        /// <summary>The assert count syntax provider.</summary>
        public class AssertCountSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly int count;
            private readonly bool notMode;

            /// <summary>Initializes a new instance of the <see cref="AssertCountSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="count">The count.</param>
            public AssertCountSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, int count)
                : this(commandProvider, assertProvider, assertSyntaxProvider, count, false)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="AssertCountSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="count">The count.</param>
            /// <param name="notMode">if set to <see langword="true" /> [not mode].</param>
            public AssertCountSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, int count, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.count = count;
                this.notMode = notMode;
            }

            /// <summary>Assert that the Count does not match - Reverses assertions in this chain.</summary>
            /// <value>The not.</value>
            public AssertCountSyntaxProvider Not => new AssertCountSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, count, true);

            /// <summary>Elements matching <paramref name="selector" /> to be counted.</summary>
            /// <param name="selector">The selector.</param>
            public AssertSyntaxProvider Of(string selector) => Of(selector, Defaults.FindMethod);

            /// <summary>Elements matching <paramref name="selector" /> to be counted.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public AssertSyntaxProvider Of(string selector, FindBy findMethod)
            {
                if (notMode)
                    assertProvider.NotCount(selector, count, findMethod);
                else
                    assertProvider.Count(selector, count, findMethod);

                return assertSyntaxProvider;
            }

            /// <summary>Specified <paramref name="elements" /> to be counted.</summary>
            /// <param name="elements">IElement collection factory function.</param>
            public AssertSyntaxProvider Of(ElementProxy elements)
            {
                if (notMode)
                    assertProvider.NotCount(elements, count);
                else
                    assertProvider.Count(elements, count);

                return assertSyntaxProvider;
            }
        }

        /// <summary>The assert CSS property syntax provider.</summary>
        public class AssertCssPropertySyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly bool notMode;
            private readonly string propertyName;
            private readonly string propertyValue;

            /// <summary>Initializes a new instance of the <see cref="AssertCssPropertySyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="propertyName">Name of the property.</param>
            /// <param name="propertyValue">The property value.</param>
            public AssertCssPropertySyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string propertyName, string propertyValue)
                : this(commandProvider, assertProvider, assertSyntaxProvider, propertyName, propertyValue, false)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="AssertCssPropertySyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="propertyName">Name of the property.</param>
            /// <param name="propertyValue">The property value.</param>
            /// <param name="notMode">if set to <see langword="true" /> [not mode].</param>
            public AssertCssPropertySyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string propertyName, string propertyValue, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.propertyName = propertyName;
                this.propertyValue = propertyValue;
                this.notMode = notMode;
            }

            /// <summary>Assert that CSS Class does not match - Reverses assertions in this chain.</summary>
            /// <value>The not.</value>
            public AssertCssPropertySyntaxProvider Not => new AssertCssPropertySyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, propertyName, propertyValue, true);

            /// <summary>Element matching <paramref name="selector" /> that should have matching CSS class.</summary>
            /// <param name="selector">The selector.</param>
            public AssertSyntaxProvider On(string selector) => On(selector, Defaults.FindMethod);

            /// <summary>Element matching <paramref name="selector" /> that should have matching CSS class.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public AssertSyntaxProvider On(string selector, FindBy findMethod)
            {
                if (notMode)
                    assertProvider.NotCssProperty(selector, propertyName, propertyValue, findMethod);
                else
                    assertProvider.CssProperty(selector, propertyName, propertyValue, findMethod);

                return assertSyntaxProvider;
            }

            /// <summary>Specified <paramref name="element" /> that should have matching CSS class.</summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider On(ElementProxy element)
            {
                if (notMode)
                    assertProvider.NotCssProperty(element, propertyName, propertyValue);
                else
                    assertProvider.CssProperty(element, propertyName, propertyValue);

                return assertSyntaxProvider;
            }
        }

        /// <summary>The assert text syntax provider.</summary>
        public class AssertTextSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly Expression<Func<string, bool>> matchFunc;
            private readonly bool notMode;
            private readonly string text;

            /// <summary>Initializes a new instance of the <see cref="AssertTextSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="text">The text.</param>
            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string text)
                : this(commandProvider, assertProvider, assertSyntaxProvider, text, false)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="AssertTextSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="text">The text.</param>
            /// <param name="notMode">if set to <see langword="true" /> [not mode].</param>
            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string text, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.text = text;
                this.notMode = notMode;
            }

            /// <summary>Initializes a new instance of the <see cref="AssertTextSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="matchFunc">The match function.</param>
            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc)
                : this(commandProvider, assertProvider, assertSyntaxProvider, matchFunc, false)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="AssertTextSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="matchFunc">The match function.</param>
            /// <param name="notMode">if set to <see langword="true" /> [not mode].</param>
            public AssertTextSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.matchFunc = matchFunc;
                this.notMode = notMode;
            }

            /// <summary>Assert that Text does not match - Reverses assertions in this chain.</summary>
            /// <value>The not.</value>
            public AssertTextSyntaxProvider Not
            {
                get
                {
                    if (matchFunc != null)
                        return new AssertTextSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, matchFunc, true);
                    return new AssertTextSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, text, true);
                }
            }

            /// <summary>Element matching <paramref name="selector" /> that should match Text.</summary>
            /// <param name="selector">The selector.</param>
            public AssertSyntaxProvider In(string selector) => In(selector, Defaults.FindMethod);

            /// <summary>Element matching <paramref name="selector" /> that should match Text.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public AssertSyntaxProvider In(string selector, FindBy findMethod)
            {
                if (!string.IsNullOrEmpty(text))
                    if (notMode)
                        assertProvider.NotText(selector, text, findMethod);
                    else
                        assertProvider.Text(selector, text, findMethod);
                else if (matchFunc != null)
                    if (notMode)
                        assertProvider.NotText(selector, matchFunc, findMethod);
                    else
                        assertProvider.Text(selector, matchFunc, findMethod);

                return assertSyntaxProvider;
            }

            /// <summary>Specified <paramref name="element" /> that should match Text.</summary>
            /// <param name="element">IElement factory function.</param>
            public AssertSyntaxProvider In(ElementProxy element)
            {
                if (!string.IsNullOrEmpty(text))
                    if (notMode)
                        assertProvider.NotText(element, text);
                    else
                        assertProvider.Text(element, text);
                else if (matchFunc != null)
                    if (notMode)
                        assertProvider.NotText(element, matchFunc);
                    else
                        assertProvider.Text(element, matchFunc);

                return assertSyntaxProvider;
            }

            /// <summary>
            ///     Look in the active Alert/Prompt for the specified text. If the text does not match the prompt will be cleanly
            ///     exited to allow clean failure or continuation of the test.
            /// </summary>
            /// <param name="accessor">The accessor.</param>
            /// <exception cref="FluentException">FluentAutomation only supports checking the message in an alerts or prompts.</exception>
            public AssertSyntaxProvider In(Alert accessor)
            {
                if (accessor != Alert.Message)
                    throw new FluentException("FluentAutomation only supports checking the message in an alerts or prompts.");

                if (matchFunc == null)
                {
                    if (notMode)
                        assertProvider.AlertNotText(text);
                    else
                        assertProvider.AlertText(text);
                }
                else
                {
                    if (notMode)
                        assertProvider.AlertNotText(matchFunc);
                    else
                        assertProvider.AlertText(matchFunc);
                }

                return assertSyntaxProvider;
            }
        }

        /// <summary>The assert value syntax provider.</summary>
        public class AssertValueSyntaxProvider : BaseAssertSyntaxProvider
        {
            private readonly Expression<Func<string, bool>> matchFunc;
            private readonly bool notMode;
            private readonly string value;


            /// <summary>Initializes a new instance of the <see cref="AssertValueSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="value">The value.</param>
            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string value)
                : this(commandProvider, assertProvider, assertSyntaxProvider, value, false)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="AssertValueSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="value">The value.</param>
            /// <param name="notMode">if set to <see langword="true" /> [not mode].</param>
            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, string value, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.value = value;
                this.notMode = notMode;
            }

            /// <summary>Initializes a new instance of the <see cref="AssertValueSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="matchFunc">The match function.</param>
            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc)
                : this(commandProvider, assertProvider, assertSyntaxProvider, matchFunc, false)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="AssertValueSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            /// <param name="matchFunc">The match function.</param>
            /// <param name="notMode">if set to <see langword="true" /> [not mode].</param>
            public AssertValueSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider, Expression<Func<string, bool>> matchFunc, bool notMode)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
                this.matchFunc = matchFunc;
                this.notMode = notMode;
            }

            /// <summary>Assert that Value does not match - Reverses assertions in this chain.</summary>
            /// <value>The not.</value>
            public AssertValueSyntaxProvider Not
            {
                get
                {
                    if (matchFunc != null)
                        return new AssertValueSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, matchFunc, true);
                    return new AssertValueSyntaxProvider(commandProvider, assertProvider, assertSyntaxProvider, value, true);
                }
            }

            /// <summary>Element matching <paramref name="selector" /> that should have a matching Value.</summary>
            /// <param name="selector">The selector.</param>
            public AssertSyntaxProvider In(string selector) => In(selector, Defaults.FindMethod);

            /// <summary>Element matching <paramref name="selector" /> that should have a matching Value.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public AssertSyntaxProvider In(string selector, FindBy findMethod)
            {
                if (!string.IsNullOrEmpty(value))
                    if (notMode)
                        assertProvider.NotValue(selector, value, findMethod);
                    else
                        assertProvider.Value(selector, value, findMethod);
                else if (matchFunc != null)
                    if (notMode)
                        assertProvider.NotValue(selector, matchFunc, findMethod);
                    else
                        assertProvider.Value(selector, matchFunc, findMethod);

                return assertSyntaxProvider;
            }

            /// <summary>Specified <paramref name="element" /> that should have a matching Value.</summary>
            /// <param name="element">The element.</param>
            public AssertSyntaxProvider In(ElementProxy element)
            {
                if (!string.IsNullOrEmpty(value))
                    if (notMode)
                        assertProvider.NotValue(element, value);
                    else
                        assertProvider.Value(element, value);
                else if (matchFunc != null)
                    if (notMode)
                        assertProvider.NotValue(element, matchFunc);
                    else
                        assertProvider.Value(element, matchFunc);

                return assertSyntaxProvider;
            }

            /// <summary>Look in the active Alert/Prompt for the specified value.</summary>
            /// <param name="accessor">The accessor.</param>
            /// <exception cref="FluentException">FluentAutomation only supports checking the message in an alerts or prompts.</exception>
            public AssertSyntaxProvider In(Alert accessor)
            {
                if (accessor != Alert.Message)
                {
                    commandProvider.AlertClick(Alert.Cancel);
                    throw new FluentException("FluentAutomation only supports checking the message in an alerts or prompts.");
                }

                if (matchFunc == null)
                {
                    if (notMode)
                        assertProvider.AlertNotText(value);
                    else
                        assertProvider.AlertText(value);
                }
                else
                {
                    if (notMode)
                        assertProvider.AlertNotText(matchFunc);
                    else
                        assertProvider.AlertText(matchFunc);
                }

                return assertSyntaxProvider;
            }
        }

        /// <summary>The not assert syntax provider.</summary>
        public class NotAssertSyntaxProvider : BaseAssertSyntaxProvider
        {
            /// <summary>Initializes a new instance of the <see cref="NotAssertSyntaxProvider" /> class.</summary>
            /// <param name="commandProvider">The command provider.</param>
            /// <param name="assertProvider">The assert provider.</param>
            /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
            public NotAssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider)
                : base(commandProvider, assertProvider, assertSyntaxProvider)
            {
            }

            /// <summary>Assert the element specified does not exist.</summary>
            /// <param name="selector">Element selector.</param>
            public AssertSyntaxProvider Exists(string selector) => Exists(selector, Defaults.FindMethod);

            /// <summary>Assert the element specified does not exist.</summary>
            /// <param name="selector">Element selector.</param>
            /// <param name="findMethod">The find method.</param>
            public AssertSyntaxProvider Exists(string selector, FindBy findMethod)
            {
                assertProvider.NotExists(selector, findMethod);
                return assertSyntaxProvider;
            }

            /// <summary>Assert the element specified does not exist.</summary>
            /// <param name="element">The element.</param>
            public AssertSyntaxProvider Exists(ElementProxy element)
            {
                assertProvider.NotExists(element);
                return assertSyntaxProvider;
            }

            /// <summary>Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> does not return false.</summary>
            /// <param name="matchFunc">The match function.</param>
            public AssertSyntaxProvider False(Expression<Func<bool>> matchFunc)
            {
                assertProvider.True(matchFunc);
                return assertSyntaxProvider;
            }

            /// <summary>Assert that an arbitrary <paramref name="matchAction">action</paramref> does not throw an Exception.</summary>
            /// <param name="matchAction">The match action.</param>
            public AssertSyntaxProvider Throws(Expression<Action> matchAction)
            {
                assertProvider.NotThrows(matchAction);
                return assertSyntaxProvider;
            }

            /// <summary>Assert that an arbitrary <paramref name="matchFunc">matching function</paramref> does not return true.</summary>
            /// <param name="matchFunc">The match function.</param>
            public AssertSyntaxProvider True(Expression<Func<bool>> matchFunc)
            {
                assertProvider.False(matchFunc);
                return assertSyntaxProvider;
            }

            /// <summary>Assert the current web browser's URL not to match <paramref name="expectedUrl" />.</summary>
            /// <param name="expectedUrl">Fully-qualified URL to use for matching..</param>
            public AssertSyntaxProvider Url(string expectedUrl) => Url(new Uri(expectedUrl, UriKind.Absolute));

            /// <summary>Assert the current web browser's URI shouldn't match <paramref name="expectedUri" />.</summary>
            /// <param name="expectedUri">Absolute URI to use for matching.</param>
            public AssertSyntaxProvider Url(Uri expectedUri)
            {
                assertProvider.NotUrl(expectedUri);
                return assertSyntaxProvider;
            }

            /// <summary>
            ///     Assert the current web browser's URI provided to the specified
            ///     <paramref name="uriExpression">URI expression</paramref> will return false;
            /// </summary>
            /// <param name="uriExpression">URI expression to use for matching.</param>
            public AssertSyntaxProvider Url(Expression<Func<Uri, bool>> uriExpression)
            {
                assertProvider.NotUrl(uriExpression);
                return assertSyntaxProvider;
            }

            /// <summary>Assert that the element matching the selector is not visible and cannot be interacted with.</summary>
            /// <param name="selector">The selector.</param>
            public AssertSyntaxProvider Visible(string selector) => Visible(selector, Defaults.FindMethod);

            /// <summary>Assert that the element matching the selector is not visible and cannot be interacted with.</summary>
            /// <param name="selector">The selector.</param>
            /// <param name="findMethod">The find method.</param>
            public AssertSyntaxProvider Visible(string selector, FindBy findMethod)
            {
                assertProvider.NotVisible(selector, findMethod);
                return assertSyntaxProvider;
            }

            /// <summary>Assert that the element is not visible and cannot be interacted with.</summary>
            /// <param name="element">The element.</param>
            public AssertSyntaxProvider Visible(ElementProxy element)
            {
                assertProvider.NotVisible(element);
                return assertSyntaxProvider;
            }
        }
    }

    /// <summary>The base assert syntax provider.</summary>
    public class BaseAssertSyntaxProvider
    {
        /// <summary>The assert provider.</summary>
        internal readonly IAssertProvider assertProvider;

        /// <summary>The assert syntax provider.</summary>
        internal readonly AssertSyntaxProvider assertSyntaxProvider;

        /// <summary>The command provider.</summary>
        internal readonly ICommandProvider commandProvider;

        /// <summary>Initializes a new instance of the <see cref="BaseAssertSyntaxProvider" /> class.</summary>
        /// <param name="commandProvider">The command provider.</param>
        /// <param name="assertProvider">The assert provider.</param>
        public BaseAssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider)
            : this(commandProvider, assertProvider, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="BaseAssertSyntaxProvider" /> class.</summary>
        /// <param name="commandProvider">The command provider.</param>
        /// <param name="assertProvider">The assert provider.</param>
        /// <param name="assertSyntaxProvider">The assert syntax provider.</param>
        public BaseAssertSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, AssertSyntaxProvider assertSyntaxProvider)
        {
            this.commandProvider = commandProvider;
            this.assertProvider = assertProvider;
            this.assertSyntaxProvider = assertSyntaxProvider == null ? (AssertSyntaxProvider)this : assertSyntaxProvider;
        }
    }
}