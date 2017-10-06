namespace FluentAutomation.Interfaces
{
    using System;
    using System.Linq.Expressions;

    using Exceptions;

    public interface ICommandProvider : IDisposable
    {
        string Source { get; }
        Uri Url { get; }
        Tuple<FluentAssertFailedException, WindowState> PendingAssertFailedExceptionNotification { get; set; }
        Tuple<FluentExpectFailedException, WindowState> PendingExpectFailedExceptionNotification { get; set; }

        void Act(CommandType commandType, Action action);
        void AlertClick(Alert accessor);
        void AlertEnterText(string text);
        void AlertText(Action<string> matchFunc);
        void AppendText(ElementProxy element, string text);
        void AppendTextWithoutEvents(ElementProxy element, string text);

        void Click(int x, int y);
        void Click(ElementProxy element, int x, int y);
        void Click(ElementProxy element);

        void CssPropertyValue(ElementProxy element, string propertyName, Action<bool, string> action);

        void DoubleClick(int x, int y);
        void DoubleClick(ElementProxy element, int x, int y);
        void DoubleClick(ElementProxy element);

        void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY);
        void DragAndDrop(ElementProxy source, ElementProxy target);
        void DragAndDrop(ElementProxy source, int sourceOffsetX, int sourceOffsetY, ElementProxy target, int targetOffsetX, int targetOffsetY);
        void EnterText(ElementProxy element, string text);
        void EnterTextWithoutEvents(ElementProxy element, string text);
        ElementProxy Find(string selector);
        ElementProxy Find(string selector, FindBy findMethod);
        ElementProxy FindMultiple(string selector);
        ElementProxy FindMultiple(string selector, FindBy findMethod);

        void Focus(ElementProxy element);

        void Hover(int x, int y);
        void Hover(ElementProxy element, int x, int y);
        void Hover(ElementProxy element);
        void MultiSelectIndex(ElementProxy element, int[] optionIndices);

        void MultiSelectText(ElementProxy element, string[] optionTextCollection);
        void MultiSelectValue(ElementProxy element, string[] optionValues);

        void Navigate(Uri url);

        void Press(string keys);

        void RightClick(int x, int y);
        void RightClick(ElementProxy element, int x, int y);
        void RightClick(ElementProxy element);
        void SelectIndex(ElementProxy element, int optionIndex);

        void SelectText(ElementProxy element, string optionText);
        void SelectValue(ElementProxy element, string optionValue);

        void SwitchToFrame(string frameName);
        void SwitchToFrame(ElementProxy frameElement);
        void SwitchToWindow(string windowName);

        void TakeScreenshot(string screenshotName);
        void Type(string text);
        void UploadFile(ElementProxy element, int x, int y, string fileName);
        void Visible(ElementProxy element, Action<bool> action);

        void Wait();
        void Wait(TimeSpan timeSpan);
        void WaitUntil(Expression<Func<bool>> conditionFunc);
        void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout);
        void WaitUntil(Expression<Action> conditionAction);
        void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout);

        ICommandProvider WithConfig(FluentSettings settings);
    }
}