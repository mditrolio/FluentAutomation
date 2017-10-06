namespace FluentAutomation.Interfaces
{
    using System.Collections.Generic;

    public interface IElement
    {
        IElementAttributeSelector Attributes { get; }
        FindBy FindBy { get; }
        int Height { get; }
        bool IsMultipleSelect { get; }
        bool IsSelect { get; }

        bool IsText { get; }

        int PosX { get; }
        int PosY { get; }
        IEnumerable<string> SelectedOptionTextCollection { get; }

        IEnumerable<string> SelectedOptionValues { get; }
        string Selector { get; }
        string TagName { get; }
        string Text { get; }

        string Value { get; }
        int Width { get; }
    }

    public interface IElementAttributeSelector
    {
        string Get(string name);
    }
}