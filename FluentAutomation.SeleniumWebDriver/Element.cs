namespace FluentAutomation
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class Element : IElement
    {
        private IElementAttributeSelector attributes;

        public Element(IWebElement webElement, string selector, FindBy findBy)
        {
            WebElement = webElement;
            Selector = selector;
            FindBy = findBy;
            TagName = WebElement.TagName;
        }

        public IElementAttributeSelector Attributes => attributes ?? (attributes = new ElementAttributeSelector(WebElement));

        public FindBy FindBy { get; }

        public int Height => WebElement.Size.Height;

        public bool IsMultipleSelect
        {
            get
            {
                if (!IsSelect)
                    return false;
                SelectElement selectElement = new SelectElement(WebElement);
                return selectElement.IsMultiple;
            }
        }

        public bool IsSelect => WebElement.TagName.ToLower() == "select";

        public bool IsText
        {
            get
            {
                bool isText = false;
                switch (TagName)
                {
                    case "input":
                        switch (Attributes.Get("type").ToLower())
                        {
                            case "text":
                            case "email":
                            case "search":
                            case "url":
                            case "tel":
                            case "number":
                            case "password":
                            case "hidden":
                                isText = true;
                                break;
                        }

                        break;
                    case "textarea":
                        isText = true;
                        break;
                }

                return isText;
            }
        }

        public int PosX => WebElement.Location.X;

        public int PosY => WebElement.Location.Y;

        public IEnumerable<string> SelectedOptionTextCollection
        {
            get
            {
                if (!IsSelect)
                    return null;
                SelectElement selectElement = new SelectElement(WebElement);
                return selectElement.AllSelectedOptions.Select(x => x.Text);
            }
        }

        public IEnumerable<string> SelectedOptionValues
        {
            get
            {
                if (!IsSelect)
                    return null;
                SelectElement selectElement = new SelectElement(WebElement);
                return selectElement.AllSelectedOptions.Select(x => x.GetAttribute("value"));
            }
        }

        public string Selector { get; }

        public string TagName { get; }

        public string Text => TagName == "input" || TagName == "textarea" ? Value : WebElement.Text;

        public string Value
        {
            get
            {
                if (TagName == "input" || TagName == "textarea")
                    return Attributes.Get("value");
                return IsSelect ? string.Join(",", SelectedOptionValues) : Text;
            }
        }

        public int Width => WebElement.Size.Width;

        public IWebElement WebElement { get; set; }
    }

    public class ElementAttributeSelector : IElementAttributeSelector
    {
        private readonly IWebElement webElement;

        public ElementAttributeSelector(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        public string Get(string name)
        {
            var attributeValue = webElement.GetAttribute(name);
            return attributeValue;
        }
    }
}