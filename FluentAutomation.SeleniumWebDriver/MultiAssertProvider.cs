﻿using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public class MultiAssertProvider : IAssertProvider
    {
        private readonly CommandProviderList commandProviders = null;
        private readonly List<KeyValuePair<IAssertProvider, ICommandProvider>> providers = null;

        private IAssertProvider BuildAssertProvider(ICommandProvider commandProvider)
        {
            var assertProvider = new AssertProvider(commandProvider);

            if (this.ThrowExceptions)
                return assertProvider.EnableExceptions();

            return assertProvider;
        }

        private IAssertProvider BuildAssertProvider(IAssertProvider assertProvider)
        {
            if (this.ThrowExceptions)
                return assertProvider.EnableExceptions();

            return assertProvider;
        }

        public MultiAssertProvider(CommandProviderList commandProviders)
        {
            this.commandProviders = commandProviders; // Easier than recomposing it for EnableExceptions() call, so storing it
            this.providers = commandProviders.Select(x => new KeyValuePair<IAssertProvider, ICommandProvider>(new AssertProvider(x), x)).ToList();
        }

        public void Count(string selector, int count, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Count(selector, count, findMethod));
        }

        public void NotCount(string selector, int count, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotCount(selector, count, findMethod));
        }

        public void Count(ElementProxy element, int count)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).Count(new ElementProxy(e.Item1, e.Item2), count);
            });
        }

        public void NotCount(ElementProxy element, int count)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).NotCount(new ElementProxy(e.Item1, e.Item2), count);
            });
        }

        public void CssClass(string selector, string className, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).CssClass(selector, className, findMethod));
        }

        public void NotCssClass(string selector, string className, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotCssClass(selector, className, findMethod));
        }

        public void CssClass(ElementProxy element, string className)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).CssClass(new ElementProxy(e.Item1, e.Item2), className);
            });
        }

        public void NotCssClass(ElementProxy element, string className)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).NotCssClass(new ElementProxy(e.Item1, e.Item2), className);
            });
        }

        public void Text(string selector, string text, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Text(selector, text, findMethod));
        }

        public void NotText(string selector, string text, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotText(selector, text, findMethod));
        }

        public void Text(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).Text(new ElementProxy(e.Item1, e.Item2), text);
            });
        }

        public void NotText(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).NotText(new ElementProxy(e.Item1, e.Item2), text);
            });
        }

        public void Text(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Text(selector, matchFunc, findMethod));
        }

        public void NotText(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotText(selector, matchFunc, findMethod));
        }

        public void Text(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).Text(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void NotText(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).NotText(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void Value(string selector, string value, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Value(selector, value, findMethod));
        }

        public void NotValue(string selector, string value, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotValue(selector, value, findMethod));
        }

        public void Value(ElementProxy element, string value)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).Value(new ElementProxy(e.Item1, e.Item2), value);
            });
        }

        public void NotValue(ElementProxy element, string value)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).NotValue(new ElementProxy(e.Item1, e.Item2), value);
            });
        }

        public void Value(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Value(selector, matchFunc, findMethod));
        }

        public void NotValue(string selector, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotValue(selector, matchFunc, findMethod));
        }

        public void Value(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).Value(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void NotValue(ElementProxy element, System.Linq.Expressions.Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                this.BuildAssertProvider(e.Item1).NotValue(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void Url(Uri expectedUrl)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Url(expectedUrl));
        }
        public void NotUrl(Uri expectedUrl)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotUrl(expectedUrl));
        }

        public void Url(System.Linq.Expressions.Expression<Func<Uri, bool>> urlExpression)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Url(urlExpression));
        }

        public void NotUrl(System.Linq.Expressions.Expression<Func<Uri, bool>> urlExpression)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotUrl(urlExpression));
        }

        public void True(System.Linq.Expressions.Expression<Func<bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).True(matchFunc));
        }

        public void False(System.Linq.Expressions.Expression<Func<bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).False(matchFunc));
        }

        public void Throws(System.Linq.Expressions.Expression<Action> matchAction)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Throws(matchAction));
        }

        public void NotThrows(System.Linq.Expressions.Expression<Action> matchAction)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotThrows(matchAction));
        }

        public void Exists(string selector, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Exists(selector, findMethod));
        }

        public void NotExists(string selector, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotExists(selector, findMethod));
        }

        public void Exists(ElementProxy element)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Exists(element));
        }

        public void NotExists(ElementProxy element)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotExists(element));
        }

        public void AlertText(string text)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).AlertText(text));
        }

        public void AlertNotText(string text)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).AlertNotText(text));
        }

        public void AlertText(Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).AlertText(matchFunc));
        }

        public void AlertNotText(Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).AlertNotText(matchFunc));
        }

        public void Visible(string selector, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Visible(selector, findMethod));
        }

        public void NotVisible(string selector, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotVisible(selector, findMethod));
        }

        public void Visible(ElementProxy element)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Visible(element));
        }

        public void NotVisible(ElementProxy element)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotVisible(element));
        }

        public void CssProperty(string selector, string propertyName, string propertyValue, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).CssProperty(selector, propertyName, propertyValue, findMethod));
        }

        public void NotCssProperty(string selector, string propertyName, string propertyValue, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotCssProperty(selector, propertyName, propertyValue, findMethod));
        }

        public void CssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).CssProperty(element, propertyName, propertyValue));
        }

        public void NotCssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotCssProperty(element, propertyName, propertyValue));
        }

        public void Attribute(string selector, string attributeName, string attributeValue, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Attribute(selector, attributeName, attributeValue, findMethod));
        }

        public void NotAttribute(string selector, string attributeName, string attributeValue, By findMethod)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotAttribute(selector, attributeName, attributeValue, findMethod));
        }

        public void Attribute(ElementProxy element, string attributeName, string attributeValue)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).Attribute(element, attributeName, attributeValue));
        }

        public void NotAttribute(ElementProxy element, string attributeName, string attributeValue)
        {
            Parallel.ForEach(this.providers, x => this.BuildAssertProvider(x.Key).NotAttribute(element, attributeName, attributeValue));
        }

        public bool ThrowExceptions { get; set; }

        public IAssertProvider EnableExceptions()
        {
            var provider = new MultiAssertProvider(this.commandProviders);
            provider.ThrowExceptions = true;

            return provider;
        }
    }
}
