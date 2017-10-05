using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Interfaces
{
    public interface IAssertProvider
    {
        bool ThrowExceptions { get; set; }
        IAssertProvider EnableExceptions();

        void Count(string selector, int count, By findMethod);
        void NotCount(string selector, int count, By findMethod);
        void Count(ElementProxy elements, int count);
        void NotCount(ElementProxy elements, int count);

        void CssClass(string selector, string className, By findMethod);
        void NotCssClass(string selector, string className, By findMethod);
        void CssClass(ElementProxy element, string className);
        void NotCssClass(ElementProxy element, string className);

        void Text(string selector, string text, By findMethod);
        void NotText(string selector, string text, By findMethod);
        void Text(ElementProxy element, string text);
        void NotText(ElementProxy element, string text);
        void Text(string selector, Expression<Func<string, bool>> matchFunc, By findMethod);
        void NotText(string selector, Expression<Func<string, bool>> matchFunc, By findMethod);
        void Text(ElementProxy element, Expression<Func<string, bool>> matchFunc);
        void NotText(ElementProxy element, Expression<Func<string, bool>> matchFunc);

        void Value(string selector, string value, By findMethod);
        void NotValue(string selector, string value, By findMethod);
        void Value(ElementProxy element, string value);
        void NotValue(ElementProxy element, string value);
        void Value(string selector, Expression<Func<string, bool>> matchFunc, By findMethod);
        void NotValue(string selector, Expression<Func<string, bool>> matchFunc, By findMethod);
        void Value(ElementProxy element, Expression<Func<string, bool>> matchFunc);
        void NotValue(ElementProxy element, Expression<Func<string, bool>> matchFunc);

        void Url(Uri expectedUrl);
        void NotUrl(Uri expectedUrl);
        void Url(Expression<Func<Uri, bool>> urlExpression);
        void NotUrl(Expression<Func<Uri, bool>> urlExpression);

        void True(Expression<Func<bool>> matchFunc);
        void False(Expression<Func<bool>> matchFunc);
        void Throws(Expression<Action> matchAction);
        void NotThrows(Expression<Action> matchAction);

        void Exists(string selector, By findMethod);
        void Exists(ElementProxy element);
        void NotExists(string selector, By findMethod);
        void NotExists(ElementProxy element);
        void Visible(string selector, By findMethod);
        void NotVisible(string selector, By findMethod);
        void Visible(ElementProxy element);
        void NotVisible(ElementProxy element);

        void CssProperty(string selector, string propertyName, string propertyValue, By findMethod);
        void NotCssProperty(string selector, string propertyName, string propertyValue, By findMethod);
        void CssProperty(ElementProxy element, string propertyName, string propertyValue);
        void NotCssProperty(ElementProxy element, string propertyName, string propertyValue);

        void Attribute(string selector, string attributeName, string attributeValue, By findMethod);
        void NotAttribute(string selector, string attributeName, string attributeValue, By findMethod);
        void Attribute(ElementProxy element, string attributeName, string attributeValue);
        void NotAttribute(ElementProxy element, string attributeName, string attributeValue);

        void AlertText(string text);
        void AlertNotText(string text);
        void AlertText(Expression<Func<string, bool>> matchFunc);
        void AlertNotText(Expression<Func<string, bool>> matchFunc);
    }
}