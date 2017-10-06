﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using WatiNCore = global::WatiN.Core;

namespace FluentAutomation
{
    public class Element : IElement
    {
        private string selector = null;
        private Tuple<int, int, int, int> bounds = null;

        public Element(WatiNCore.Element automationElement) : this(automationElement, null)
        {
        }

        public Element(WatiNCore.Element automationElement, string selector)
        {
            this.AutomationElement = automationElement;
            this.selector = selector;

            var nativeBounds = automationElement.NativeElement.GetElementBounds();
            this.bounds = new Tuple<int, int, int, int>(nativeBounds.Left, nativeBounds.Top, nativeBounds.Size.Width, nativeBounds.Size.Height);
        }

        public WatiNCore.Element AutomationElement { get; set; }

        public By FindBy { get; }

        public string TagName
        {
            get
            {
                return this.AutomationElement.TagName;
            }
        }

        private IElementAttributeSelector attributes = null;
        public IElementAttributeSelector Attributes
        {
            get
            {
                if (attributes == null)
                {
                    attributes = new ElementAttributeSelector(this.AutomationElement);
                }

                return attributes;
            }
        }

        public string Value
        {
            get
            {
                if (this.TagName == "input")
                {
                    return this.Attributes.Get("value");
                }
                else if (this.IsSelect)
                {
                    return string.Join(",", this.SelectedOptionValues);
                }
                else
                {
                    return this.Text;
                }
            }
        }

        public string Text
        {
            get
            {
                return this.AutomationElement.Text;
            }
        }

        public string Selector
        {
            get
            {
                return this.selector;
            }
        }

        public IEnumerable<string> SelectedOptionValues
        {
            get
            {
                if (this.IsSelect)
                {
                    WatiNCore.SelectList selectElement = new WatiNCore.SelectList(this.AutomationElement.DomContainer, this.AutomationElement.NativeElement);
                    return selectElement.Options.Where(x => x.Selected).Select(x => x.GetAttributeValue("value"));
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<string> SelectedOptionTextCollection
        {
            get
            {
                if (this.IsSelect)
                {
                    WatiNCore.SelectList selectElement = new WatiNCore.SelectList(this.AutomationElement.DomContainer, this.AutomationElement.NativeElement);
                    return selectElement.Options.Where(x => x.Selected).Select(x => x.Text);
                }
                else
                {
                    return null;
                }
            }
        }

        public bool IsText
        {
            get
            {
                bool isText = false;
                switch (this.TagName.ToLower())
                {
                    case "input":
                        switch (this.Attributes.Get("type").ToLower())
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

        public bool IsSelect
        {
            get
            {
                return this.AutomationElement.TagName.ToLower() == "select";
            }
        }

        public bool IsMultipleSelect
        {
            get
            {
                if (this.IsSelect)
                {
                    WatiNCore.SelectList selectElement = new WatiNCore.SelectList(this.AutomationElement.DomContainer, this.AutomationElement.NativeElement);
                    return selectElement.Multiple;
                }
                else
                {
                    return false;
                }
            }
        }

        public int Height
        {
            get
            {
                return this.bounds.Item4;
            }
        }

        public int Width
        {
            get
            {
                return this.bounds.Item3;
            }
        }

        public int PosX
        {
            get
            {
                return this.bounds.Item1;
            }
        }

        public int PosY
        {
            get
            {
                return this.bounds.Item2;
            }
        }
    }

    public class ElementAttributeSelector : IElementAttributeSelector
    {
        private readonly WatiNCore.Element automationElement = null;

        public ElementAttributeSelector(WatiNCore.Element automationElement)
        {
            this.automationElement = automationElement;
        }

        public string Get(string name)
        {
            if (name.ToLower() == "class")
            {
                return automationElement.ClassName;
            }
            else
            {
                var attributeValue = automationElement.GetAttributeValue(name);
                return attributeValue;
            }
        }
    }
}
