namespace FluentAutomation.Interfaces
{
    using System;
    using System.Linq.Expressions;

    public interface IAssertProvider
    {
        bool ThrowExceptions { get; set; }
        void AlertNotText(string text);
        void AlertNotText(Expression<Func<string, bool>> matchFunc);

        void AlertText(string text);
        void AlertText(Expression<Func<string, bool>> matchFunc);

        void Attribute(string selector, string attributeName, string attributeValue, FindBy findMethod);
        void Attribute(ElementProxy element, string attributeName, string attributeValue);

        void Count(string selector, int count, FindBy findMethod);
        void Count(ElementProxy elements, int count);

        void CssClass(string selector, string className, FindBy findMethod);
        void CssClass(ElementProxy element, string className);

        void CssProperty(string selector, string propertyName, string propertyValue, FindBy findMethod);
        void CssProperty(ElementProxy element, string propertyName, string propertyValue);
        IAssertProvider EnableExceptions();

        void Exists(string selector, FindBy findMethod);
        void Exists(ElementProxy element);
        void False(Expression<Func<bool>> matchFunc);
        void NotAttribute(string selector, string attributeName, string attributeValue, FindBy findMethod);
        void NotAttribute(ElementProxy element, string attributeName, string attributeValue);
        void NotCount(string selector, int count, FindBy findMethod);
        void NotCount(ElementProxy elements, int count);
        void NotCssClass(string selector, string className, FindBy findMethod);
        void NotCssClass(ElementProxy element, string className);
        void NotCssProperty(string selector, string propertyName, string propertyValue, FindBy findMethod);
        void NotCssProperty(ElementProxy element, string propertyName, string propertyValue);
        void NotExists(string selector, FindBy findMethod);
        void NotExists(ElementProxy element);
        void NotText(string selector, string text, FindBy findMethod);
        void NotText(ElementProxy element, string text);
        void NotText(string selector, Expression<Func<string, bool>> matchFunc, FindBy findMethod);
        void NotText(ElementProxy element, Expression<Func<string, bool>> matchFunc);
        void NotThrows(Expression<Action> matchAction);
        void NotUrl(Uri expectedUrl);
        void NotUrl(Expression<Func<Uri, bool>> urlExpression);
        void NotValue(string selector, string value, FindBy findMethod);
        void NotValue(ElementProxy element, string value);
        void NotValue(string selector, Expression<Func<string, bool>> matchFunc, FindBy findMethod);
        void NotValue(ElementProxy element, Expression<Func<string, bool>> matchFunc);
        void NotVisible(string selector, FindBy findMethod);
        void NotVisible(ElementProxy element);

        void Text(string selector, string text, FindBy findMethod);
        void Text(ElementProxy element, string text);
        void Text(string selector, Expression<Func<string, bool>> matchFunc, FindBy findMethod);
        void Text(ElementProxy element, Expression<Func<string, bool>> matchFunc);
        void Throws(Expression<Action> matchAction);

        void True(Expression<Func<bool>> matchFunc);

        void Url(Uri expectedUrl);
        void Url(Expression<Func<Uri, bool>> urlExpression);

        void Value(string selector, string value, FindBy findMethod);
        void Value(ElementProxy element, string value);
        void Value(string selector, Expression<Func<string, bool>> matchFunc, FindBy findMethod);
        void Value(ElementProxy element, Expression<Func<string, bool>> matchFunc);
        void Visible(string selector, FindBy findMethod);
        void Visible(ElementProxy element);
    }
}