namespace FluentAutomation
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Exceptions;

    using Interfaces;

    public class AssertProvider : IAssertProvider
    {
        private readonly ICommandProvider commandProvider;

        public AssertProvider(ICommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
        }

        public bool ThrowExceptions { get; set; }
        private CommandType CommandType => ThrowExceptions ? CommandType.Assert : CommandType.Expect;

        public void AlertNotText(string text)
        {
            commandProvider.AlertText(alertText =>
            {
                if (IsTextMatch(alertText, text))
                {
                    // because the browser blocks, we dismiss the alert when a failure happens so we can cleanly shutdown.
                    commandProvider.AlertClick(Alert.Cancel);
                    ReportError("Expected alert text not to be [{0}] but it was.", text);
                }
            });
        }

        public void AlertNotText(Expression<Func<string, bool>> matchFunc)
        {
            var compiledFunc = matchFunc.Compile();
            commandProvider.AlertText(alertText =>
            {
                if (compiledFunc(alertText))
                {
                    // because the browser blocks, we dismiss the alert when a failure happens so we can cleanly shutdown.
                    commandProvider.AlertClick(Alert.Cancel);
                    ReportError("Expected alert text not to match expression [{0}] but it did.", matchFunc.ToExpressionString());
                }
            });
        }

        public void AlertText(string text)
        {
            commandProvider.AlertText(alertText =>
            {
                if (!IsTextMatch(alertText, text))
                {
                    // because the browser blocks, we dismiss the alert when a failure happens so we can cleanly shutdown.
                    commandProvider.AlertClick(Alert.Cancel);
                    ReportError("Expected alert text to be [{0}] but it was actually [{1}].", text, alertText);
                }
            });
        }

        public void AlertText(Expression<Func<string, bool>> matchFunc)
        {
            var compiledFunc = matchFunc.Compile();
            commandProvider.AlertText(alertText =>
            {
                if (!compiledFunc(alertText))
                {
                    // because the browser blocks, we dismiss the alert when a failure happens so we can cleanly shutdown.
                    commandProvider.AlertClick(Alert.Cancel);
                    ReportError("Expected alert text to match expression [{0}] but it was actually [{1}].", matchFunc.ToExpressionString(), alertText);
                }
            });
        }

        public void Attribute(string selector, string attributeName, string attributeValue, FindBy findMethod)
        {
            Attribute(commandProvider.Find(selector, findMethod), attributeName, attributeValue);
        }

        public void Attribute(ElementProxy element, string attributeName, string attributeValue)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = element.Element.Attributes.Get(attributeName);
                if (result == null)
                    ReportError("Expected element [{0}] to have attribute [{1}] but it did not.", element.Element.Selector, attributeName);
                else if (attributeValue != null && !IsTextMatch(result, attributeValue))
                    ReportError("Expected element [{0}]'s attribute [{1}] to have a value of [{2}] but it was actually [{3}].", element.Element.Selector, attributeName, attributeValue, result);
            });
        }

        public void Count(string selector, int count, FindBy findMethod)
        {
            commandProvider.Act(CommandType, () =>
            {
                try
                {
                    var elements = commandProvider.FindMultiple(selector, findMethod).Elements;
                    if (elements.Count() != count)
                        ReportError("Expected count of elements matching selector [{0}] to be [{1}] but instead it was [{2}]", selector, count, elements.Count());
                }
                catch (FluentElementNotFoundException)
                {
                    if (count != 0)
                        ReportError("Expected count of elements matching selector [{0}] to be [{1}] but no matching elements could be found.", selector, count);
                }
            });
        }

        public void Count(ElementProxy elements, int count)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (CountElementsInProxy(elements) != count)
                    ReportError("Expected count of elements in collection to be [{0}] but instead it was [{1}].", count, elements.Elements.Count());
            });
        }

        public void CssClass(string selector, string className, FindBy findMethod)
        {
            CssClass(commandProvider.Find(selector, findMethod), className);
        }

        public void CssClass(ElementProxy element, string className)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = ElementHasClass(element, className);
                if (!result.HasClass)
                    ReportError("Expected element [{0}] to include CSS class [{1}] but current class attribute is [{2}].", element.Element.Selector, className, result.ActualClass);
            });
        }

        public void CssProperty(string selector, string propertyName, string propertyValue, FindBy findMethod)
        {
            CssProperty(commandProvider.Find(selector, findMethod), propertyName, propertyValue);
        }

        public void CssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = ElementHasCssProperty(element, propertyName, propertyValue);
                if (!result.HasProperty)
                    ReportError("Expected element [{0}] to have CSS property [{1}] but it did not.", element.Element.Selector, propertyName);
                else if (propertyValue != null && !result.PropertyMatches)
                    ReportError("Expected element [{0}]'s CSS property [{1}] to have a value of [{2}] but it was actually [{3}].", element.Element.Selector, propertyName, propertyValue, result.PropertyValue);
            });
        }

        public IAssertProvider EnableExceptions()
        {
            var provider = new AssertProvider(commandProvider);
            provider.ThrowExceptions = true;

            return provider;
        }

        public void Exists(string selector, FindBy findMethod)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (!ElementExists(selector, findMethod))
                    ReportError("Expected element matching selector [{0}] to exist.", selector);
            });
        }

        public void Exists(ElementProxy element)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (!ElementExists(element))
                    ReportError("Expected element provided to exist.");
            });
        }

        public void False(Expression<Func<bool>> matchFunc)
        {
            commandProvider.Act(CommandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                if (compiledFunc())
                    ReportError("Expected expression [{0}] to return false.", matchFunc.ToExpressionString());
            });
        }

        public void NotAttribute(string selector, string attributeName, string attributeValue, FindBy findMethod)
        {
            NotAttribute(commandProvider.Find(selector, findMethod), attributeName, attributeValue);
        }

        public void NotAttribute(ElementProxy element, string attributeName, string attributeValue)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = element.Element.Attributes.Get(attributeName);
                if (attributeValue == null && result != null)
                    ReportError("Expected element [{0}] not to have attribute [{1}] but it did.", element.Element.Selector, attributeName);
                else if (result != null && IsTextMatch(result, attributeValue))
                    ReportError("Expected element [{0}]'s attribute [{1}] not to have a value of [{2}] but it did.", element.Element.Selector, attributeName, attributeValue);
            });
        }

        public void NotCount(string selector, int count, FindBy findMethod)
        {
            commandProvider.Act(CommandType, () =>
            {
                try
                {
                    var elements = commandProvider.FindMultiple(selector, findMethod).Elements;
                    if (elements.Count() == count)
                        ReportError("Expected count of elements matching selector [{0}] not to be [{1}] but it was.", selector, count);
                }
                catch (FluentElementNotFoundException)
                {
                    if (count == 0)
                        ReportError("Expected count of elements matching selector [{0}] not to be [{1}] but it was.", selector, count);
                }
            });
        }

        public void NotCount(ElementProxy elements, int count)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (CountElementsInProxy(elements) == count)
                    ReportError("Expected count of elements in collection not to be [{0}] but it was.", count);
            });
        }

        public void NotCssClass(string selector, string className, FindBy findMethod)
        {
            NotCssClass(commandProvider.Find(selector, findMethod), className);
        }

        public void NotCssClass(ElementProxy element, string className)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = ElementHasClass(element, className);
                if (result.HasClass)
                    ReportError("Expected element [{0}] not to include CSS class [{1}] but current class attribute is [{2}].", element.Element.Selector, className, result.ActualClass);
            });
        }

        public void NotCssProperty(string selector, string propertyName, string propertyValue, FindBy findMethod)
        {
            NotCssProperty(commandProvider.Find(selector, findMethod), propertyName, propertyValue);
        }

        public void NotCssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = ElementHasCssProperty(element, propertyName, propertyValue);
                if (propertyValue == null && result.HasProperty)
                    ReportError("Expected element [{0}] not to have CSS property [{1}] but it did.", element.Element.Selector, propertyName);
                else if (result.PropertyMatches)
                    ReportError("Expected element [{0}]'s CSS property [{1}] not to have a value of [{2}] but it did.", element.Element.Selector, propertyName, propertyValue);
            });
        }

        public void NotExists(string selector, FindBy findMethod)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (ElementExists(selector, findMethod))
                    ReportError("Expected element matching selector [{0}] not to exist.", selector);
            });
        }

        public void NotExists(ElementProxy element)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (ElementExists(element))
                    ReportError("Expected element provided not to exist.");
            });
        }

        public void NotText(string selector, string text, FindBy findMethod)
        {
            NotText(commandProvider.Find(selector, findMethod), text);
        }

        public void NotText(string selector, Expression<Func<string, bool>> matchFunc, FindBy findMethod)
        {
            NotText(commandProvider.Find(selector, findMethod), matchFunc);
        }

        public void NotText(ElementProxy element, string text)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = ElementHasText(element, elText => IsTextMatch(elText, text));
                if (result.HasText)
                    if (element.Element.IsMultipleSelect)
                        ReportError("Expected SelectElement [{0}] selected options to have no options with text of [{1}]. Selected option text values include [{2}]", result.Selector, text, string.Join(",", element.Element.SelectedOptionTextCollection));
                    else if (element.Element.IsSelect)
                        ReportError("Expected SelectElement [{0}] selected option text not to be [{1}] but it was.", result.Selector, text);
                    else
                        ReportError("Expected {0} [{1}] text not to be [{2}] but it was.", result.ElementType, result.Selector, text);
            });
        }

        public void NotText(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            commandProvider.Act(CommandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var result = ElementHasText(element, elText => compiledFunc(elText));
                if (result.HasText)
                    if (element.Element.IsMultipleSelect)
                        ReportError("Expected SelectElement [{0}] selected options to have no options with text matching expression [{1}]. Selected option text values include [{2}]", result.Selector, matchFunc.ToExpressionString(), string.Join(",", element.Element.SelectedOptionTextCollection));
                    else if (element.Element.IsSelect)
                        ReportError("Expected SelectElement [{0}] selected option text not to match expression [{1}] but it did.", result.Selector, matchFunc.ToExpressionString());
                    else
                        ReportError("Expected {0} [{1}] text not to match expression [{2}] but it did.", result.ElementType, result.Selector, matchFunc.ToExpressionString());
            });
        }

        public void NotThrows(Expression<Action> matchAction)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (ThrowsException(matchAction))
                    ReportError("Expected expression [{0}] not to throw an exception.", matchAction.ToExpressionString());
            });
        }

        public void NotUrl(Uri expectedUrl)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (expectedUrl.ToString().Equals(commandProvider.Url.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    ReportError("Expected URL not to match [{0}] but it was actually [{1}].", expectedUrl.ToString(), commandProvider.Url.ToString());
            });
        }

        public void NotUrl(Expression<Func<Uri, bool>> urlExpression)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (urlExpression.Compile()(commandProvider.Url))
                    ReportError("Expected expression [{0}] to return false. URL was [{1}].", urlExpression.ToExpressionString(), commandProvider.Url.ToString());
            });
        }

        public void NotValue(string selector, string text, FindBy findMethod)
        {
            NotValue(commandProvider.Find(selector, findMethod), text);
        }

        public void NotValue(string selector, Expression<Func<string, bool>> matchFunc, FindBy findMethod)
        {
            NotValue(commandProvider.Find(selector, findMethod), matchFunc);
        }

        public void NotValue(ElementProxy element, string value)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = ElementHasValue(element, elValue => IsTextMatch(elValue, value));
                if (result.HasValue)
                    if (element.Element.IsMultipleSelect)
                        ReportError("Expected SelectElement [{0}] selected options to have no options with value of [{1}]. Selected option values include [{2}]", result.Selector, value, string.Join(",", element.Element.SelectedOptionValues));
                    else if (element.Element.IsSelect)
                        ReportError("Expected SelectElement [{0}] selected option value not to be [{1}] but it was.", result.Selector, value);
                    else
                        ReportError("Expected {0} [{1}] value not to be [{2}] but it was.", result.ElementType, result.Selector, value);
            });
        }

        public void NotValue(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            commandProvider.Act(CommandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var result = ElementHasValue(element, elValue => compiledFunc(elValue));
                if (result.HasValue)
                    if (element.Element.IsMultipleSelect)
                        ReportError("Expected SelectElement [{0}] selected options to have no options with value matching expression [{1}]. Selected option values include [{2}]", result.Selector, matchFunc.ToExpressionString(), string.Join(",", element.Element.SelectedOptionValues));
                    else if (element.Element.IsSelect)
                        ReportError("Expected SelectElement [{0}] selected option value not to match expression [{1}] but it did.", result.Selector, matchFunc.ToExpressionString());
                    else
                        ReportError("Expected {0} [{1}] value not to match expression [{2}] but it did.", result.ElementType, result.Selector, matchFunc.ToExpressionString());
            });
        }

        public void NotVisible(string selector, FindBy findMethod)
        {
            NotVisible(commandProvider.Find(selector, findMethod));
        }

        public void NotVisible(ElementProxy element)
        {
            commandProvider.Act(CommandType, () =>
            {
                commandProvider.Visible(element, isVisible =>
                {
                    if (isVisible)
                        ReportError("Expected element [{0}] not to be visible.", element.Element.Selector);
                });
            });
        }

        public virtual void ReportError(string message, params object[] formatParams)
        {
            if (ThrowExceptions)
            {
                var assertException = new FluentAssertFailedException(message, formatParams);
                commandProvider.PendingAssertFailedExceptionNotification = new Tuple<FluentAssertFailedException, WindowState>(assertException, new WindowState
                {
                    Source = commandProvider.Source,
                    Url = commandProvider.Url
                });

                throw assertException;
            }
            var expectException = new FluentExpectFailedException(message, formatParams);
            commandProvider.PendingExpectFailedExceptionNotification = new Tuple<FluentExpectFailedException, WindowState>(expectException, new WindowState
            {
                Source = commandProvider.Source,
                Url = commandProvider.Url
            });
        }

        public void Text(string selector, string text, FindBy findMethod)
        {
            Text(commandProvider.Find(selector, findMethod), text);
        }

        public void Text(string selector, Expression<Func<string, bool>> matchFunc, FindBy findMethod)
        {
            Text(commandProvider.Find(selector, findMethod), matchFunc);
        }

        public void Text(ElementProxy element, string text)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = ElementHasText(element, elText => IsTextMatch(elText, text));
                if (!result.HasText)
                    if (element.Element.IsMultipleSelect)
                        ReportError("Expected SelectElement [{0}] selected options to have at least one option with text of [{1}]. Selected option text values include [{2}]", result.Selector, text, string.Join(",", element.Element.SelectedOptionTextCollection));
                    else if (element.Element.IsSelect)
                        ReportError("Expected SelectElement [{0}] selected option text to match expression [{1}] but it was actually [{2}].", result.Selector, text, result.ActualText);
                    else
                        ReportError("Expected {0} [{1}] text to be [{2}] but it was actually [{3}].", result.ElementType, result.Selector, text, result.ActualText);
            });
        }

        public void Text(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            commandProvider.Act(CommandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var result = ElementHasText(element, elText => compiledFunc(elText));
                if (!result.HasText)
                    if (element.Element.IsMultipleSelect)
                        ReportError("Expected SelectElement [{0}] selected options to have at least one option with text matching expression [{1}]. Selected option text values include [{2}]", result.Selector, matchFunc.ToExpressionString(), string.Join(",", element.Element.SelectedOptionTextCollection));
                    else if (element.Element.IsSelect)
                        ReportError("Expected SelectElement [{0}] selected option text to match expression [{1}] but it was actually [{2}].", result.Selector, matchFunc.ToExpressionString(), result.ActualText);
                    else
                        ReportError("Expected {0} [{1}] text to match expression [{2}] but it was actually [{3}].", result.ElementType, result.Selector, matchFunc.ToExpressionString(), result.ActualText);
            });
        }

        public void Throws(Expression<Action> matchAction)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (!ThrowsException(matchAction))
                    ReportError("Expected expression [{0}] to throw an exception.", matchAction.ToExpressionString());
            });
        }

        public void True(Expression<Func<bool>> matchFunc)
        {
            commandProvider.Act(CommandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                if (!compiledFunc())
                    ReportError("Expected expression [{0}] to return true.", matchFunc.ToExpressionString());
            });
        }

        public void Url(Uri expectedUrl)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (!expectedUrl.ToString().Equals(commandProvider.Url.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    ReportError("Expected URL to match [{0}] but it was actually [{1}].", expectedUrl.ToString(), commandProvider.Url.ToString());
            });
        }

        public void Url(Expression<Func<Uri, bool>> urlExpression)
        {
            commandProvider.Act(CommandType, () =>
            {
                if (urlExpression.Compile()(commandProvider.Url) != true)
                    ReportError("Expected expression [{0}] to return true. URL was [{1}].", urlExpression.ToExpressionString(), commandProvider.Url.ToString());
            });
        }

        public void Value(string selector, string text, FindBy findMethod)
        {
            Value(commandProvider.Find(selector, findMethod), text);
        }

        public void Value(string selector, Expression<Func<string, bool>> matchFunc, FindBy findMethod)
        {
            Value(commandProvider.Find(selector, findMethod), matchFunc);
        }

        public void Value(ElementProxy element, string value)
        {
            commandProvider.Act(CommandType, () =>
            {
                var result = ElementHasValue(element, elValue => IsTextMatch(elValue, value));
                if (!result.HasValue)
                    if (element.Element.IsMultipleSelect)
                        ReportError("Expected SelectElement [{0}] selected options to have at least one option with value of [{1}]. Selected option values include [{2}]", result.Selector, value, string.Join(",", element.Element.SelectedOptionValues));
                    else if (element.Element.IsSelect)
                        ReportError("Expected SelectElement [{0}] selected option value to match expression [{1}] but it was actually [{2}].", result.Selector, value, result.ActualValue);
                    else
                        ReportError("Expected {0} [{1}] value to be [{2}] but it was actually [{3}].", result.ElementType, result.Selector, value, result.ActualValue);
            });
        }

        public void Value(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            commandProvider.Act(CommandType, () =>
            {
                var compiledFunc = matchFunc.Compile();
                var result = ElementHasValue(element, elValue => compiledFunc(elValue));
                if (!result.HasValue)
                    if (element.Element.IsMultipleSelect)
                        ReportError("Expected SelectElement [{0}] selected options to have at least one option with value matching expression [{1}]. Selected option values include [{2}]", result.Selector, matchFunc.ToExpressionString(), string.Join(",", element.Element.SelectedOptionValues));
                    else if (element.Element.IsSelect)
                        ReportError("Expected SelectElement [{0}] selected option value to match expression [{1}] but it was actually [{2}].", result.Selector, matchFunc.ToExpressionString(), result.ActualValue);
                    else
                        ReportError("Expected {0} [{1}] value to match expression [{2}] but it was actually [{3}].", result.ElementType, result.Selector, matchFunc.ToExpressionString(), result.ActualValue);
            });
        }

        public void Visible(string selector, FindBy findMethod)
        {
            Visible(commandProvider.Find(selector, findMethod));
        }

        public void Visible(ElementProxy element)
        {
            commandProvider.Act(CommandType, () =>
            {
                commandProvider.Visible(element, isVisible =>
                {
                    if (!isVisible)
                        ReportError("Expected element [{0}] to be visible.", element.Element.Selector);
                });
            });
        }

        private static int CountElementsInProxy(ElementProxy elements)
        {
            int count = 0;

            foreach (var element in elements.Elements)
            {
                try
                {
                    element.Item2();
                    count++;
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
            }

            return count;
        }

        private static bool ElementExists(ElementProxy element)
        {
            bool exists = false;
            try
            {
                exists = element.Element != null;
            }
            catch (FluentException)
            {
            }

            return exists;
        }

        private static ElementHasClassResult ElementHasClass(ElementProxy element, string className)
        {
            var hasClass = false;
            var unwrappedElement = element.Element;
            var elementClassAttributeValue = unwrappedElement.Attributes.Get("class").Trim();

            className = className.Replace(".", "").Trim();

            if (elementClassAttributeValue.Contains(' '))
            {
                elementClassAttributeValue.Split(' ').ToList().ForEach(cssClass =>
                {
                    cssClass = cssClass.Trim();
                    if (!string.IsNullOrEmpty(cssClass) && className.Equals(cssClass))
                        hasClass = true;
                });
            }
            else
            {
                if (className.Equals(elementClassAttributeValue))
                    hasClass = true;
            }

            return new ElementHasClassResult
            {
                HasClass = hasClass,
                ActualClass = elementClassAttributeValue
            };
        }

        private static ElementHasTextResult ElementHasText(ElementProxy element, Func<string, bool> textMatcher)
        {
            var hasText = false;
            var unwrappedElement = element.Element;
            var actualText = unwrappedElement.Text;
            if (unwrappedElement.IsSelect)
            {
                foreach (var optionText in unwrappedElement.SelectedOptionTextCollection)
                {
                    if (textMatcher(optionText))
                    {
                        hasText = true;
                        break;
                    }
                }

                actualText = string.Join(", ", unwrappedElement.SelectedOptionTextCollection.Select(x => x).ToArray());
            }
            else
            {
                if (textMatcher(unwrappedElement.Text))
                    hasText = true;
            }

            var elementType = "DOM Element";
            if (unwrappedElement.IsText)
                elementType = "TextElement";
            else if (unwrappedElement.IsMultipleSelect)
                elementType = "MultipleSelectElement";
            else if (unwrappedElement.IsSelect)
                elementType = "SelectElement";

            return new ElementHasTextResult
            {
                HasText = hasText,
                ActualText = actualText,
                ElementType = elementType,
                Selector = element.Element.Selector
            };
        }

        private static ElementHasValueResult ElementHasValue(ElementProxy element, Func<string, bool> valueMatcher)
        {
            var hasValue = false;
            var unwrappedElement = element.Element;
            if (unwrappedElement.IsMultipleSelect)
            {
                foreach (var optionValue in unwrappedElement.SelectedOptionValues)
                {
                    if (valueMatcher(optionValue))
                    {
                        hasValue = true;
                        break;
                    }
                }
            }
            else
            {
                if (valueMatcher(unwrappedElement.Value))
                    hasValue = true;
            }

            var elementType = "DOM Element";
            if (unwrappedElement.IsText)
                elementType = "TextElement";
            else if (unwrappedElement.IsMultipleSelect)
                elementType = "MultipleSelectElement";
            else if (unwrappedElement.IsSelect)
                elementType = "SelectElement";

            return new ElementHasValueResult
            {
                HasValue = hasValue,
                ElementType = elementType,
                ActualValue = unwrappedElement.Value,
                Selector = element.Element.Selector
            };
        }

        private static bool IsTextMatch(string elementText, string text) => string.Equals(elementText, text, StringComparison.InvariantCultureIgnoreCase);

        private static bool ThrowsException(Expression<Action> matchAction)
        {
            bool threwException = false;
            var compiledAction = matchAction.Compile();

            try
            {
                compiledAction();
            }
            catch (FluentAssertFailedException)
            {
                threwException = true;
            }
            catch (FluentException)
            {
                threwException = true;
            }

            return threwException;
        }

        private bool ElementExists(string selector, FindBy findMethod) => ElementExists(commandProvider.Find(selector, findMethod));

        private ElementHasCssPropertyResult ElementHasCssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            var result = new ElementHasCssPropertyResult();

            commandProvider.CssPropertyValue(element, propertyName, (hasProperty, actualPropertyValue) =>
            {
                if (!hasProperty) return;

                result.HasProperty = true;
                result.PropertyValue = actualPropertyValue;

                if (propertyValue != null && IsTextMatch(actualPropertyValue, propertyValue))
                    result.PropertyMatches = true;
            });

            return result;
        }

        private class ElementHasClassResult
        {
            public string ActualClass { get; set; }
            public bool HasClass { get; set; }
        }

        private class ElementHasCssPropertyResult
        {
            public bool HasProperty { get; set; }

            public bool PropertyMatches { get; set; }

            public string PropertyValue { get; set; }
        }

        private class ElementHasTextResult
        {
            public string ActualText { get; set; }

            public string ElementType { get; set; }
            public bool HasText { get; set; }

            public string Selector { get; set; }
        }

        private class ElementHasValueResult
        {
            public string ActualValue { get; set; }

            public string ElementType { get; set; }
            public bool HasValue { get; set; }

            public string Selector { get; set; }
        }
    }
}