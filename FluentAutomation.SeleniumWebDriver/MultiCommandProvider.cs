namespace FluentAutomation
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Exceptions;

    using Interfaces;

    public class MultiCommandProvider : ICommandProvider
    {
        private readonly CommandProviderList commandProviders;

        public MultiCommandProvider(CommandProviderList commandProviders)
        {
            this.commandProviders = commandProviders;
        }

        public string Source => commandProviders.First().Source;

        public Uri Url => commandProviders.First().Url;

        public Tuple<FluentAssertFailedException, WindowState> PendingAssertFailedExceptionNotification { get; set; }

        public Tuple<FluentExpectFailedException, WindowState> PendingExpectFailedExceptionNotification { get; set; }

        public void Act(CommandType commandType, Action action)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.Act(commandType, action)));
        }

        public void AlertClick(Alert accessor)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.AlertClick(accessor)));
        }

        public void AlertEnterText(string text)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.AlertEnterText(text)));
        }

        public void AlertText(Action<string> matchFunc)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.AlertText(matchFunc)));
        }

        public void AppendText(ElementProxy element, string text)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => { e.Item1.AppendText(new ElementProxy(e.Item1, e.Item2), text); }));
        }

        public void AppendTextWithoutEvents(ElementProxy element, string text)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.AppendTextWithoutEvents(new ElementProxy(e.Item1, e.Item2), text)));
        }

        public void Click(int x, int y)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, xx => xx.Click(x, y)));
        }

        public void Click(ElementProxy element, int x, int y)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, xx => xx.Click(xx.Find(element.Element.Selector, element.Element.FindBy), x, y)));
        }

        public void Click(ElementProxy element)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.Click(new ElementProxy(e.Item1, e.Item2))));
        }

        public void CssPropertyValue(ElementProxy element, string propertyName, Action<bool, string> action)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.CssPropertyValue(element, propertyName, action)));
        }

        public void Dispose()
        {
            Parallel.ForEach(commandProviders, x => x.Dispose());
        }

        public void DoubleClick(int x, int y)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, xx => xx.DoubleClick(x, y)));
        }

        public void DoubleClick(ElementProxy element, int x, int y)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.DoubleClick(new ElementProxy(e.Item1, e.Item2), x, y)));
        }

        public void DoubleClick(ElementProxy element)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.DoubleClick(new ElementProxy(e.Item1, e.Item2))));
        }

        public void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.DragAndDrop(sourceX, sourceY, destinationX, destinationY)));
        }

        public void DragAndDrop(ElementProxy source, int sourceOffsetX, int sourceOffsetY, ElementProxy target, int targetOffsetX, int targetOffsetY)
        {
            RepackExceptions(() => Parallel.ForEach(source.Elements, e =>
            {
                e.Item1.DragAndDrop(
                    new ElementProxy(e.Item1, e.Item2), sourceOffsetX, sourceOffsetY,
                    new ElementProxy(e.Item1, target.Elements.First(x => x.Item1 == e.Item1).Item2), targetOffsetX, targetOffsetY
                );
            }));
        }

        public void DragAndDrop(ElementProxy source, ElementProxy target)
        {
            RepackExceptions(() => Parallel.ForEach(source.Elements, e =>
            {
                e.Item1.DragAndDrop(
                    new ElementProxy(e.Item1, e.Item2),
                    new ElementProxy(e.Item1, target.Elements.First(x => x.Item1 == e.Item1).Item2)
                );
            }));
        }

        public void EnterText(ElementProxy element, string text)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.EnterText(new ElementProxy(e.Item1, e.Item2), text)));
        }

        public void EnterTextWithoutEvents(ElementProxy element, string text)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.EnterTextWithoutEvents(new ElementProxy(e.Item1, e.Item2), text)));
        }

        public ElementProxy Find(string selector) => Find(selector, Defaults.FindMethod);

        public ElementProxy Find(string selector, FindBy findMethod)
        {
            var result = new ElementProxy();

            RepackExceptions(() => Parallel.ForEach(commandProviders, x => result.Elements.Add(new Tuple<ICommandProvider, Func<IElement>>(x, x.Find(selector, findMethod).Elements.First().Item2))));

            return result;
        }

        public ElementProxy FindMultiple(string selector) => FindMultiple(selector, Defaults.FindMethod);

        public ElementProxy FindMultiple(string selector, FindBy findMethod)
        {
            var result = new ElementProxy();

            RepackExceptions(() => Parallel.ForEach(commandProviders, x =>
            {
                foreach (var element in x.FindMultiple(selector, findMethod).Elements)
                    result.Elements.Add(new Tuple<ICommandProvider, Func<IElement>>(x, element.Item2));
            }));

            return result;
        }

        public void Focus(ElementProxy element)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.Focus(new ElementProxy(e.Item1, e.Item2))));
        }

        public void Hover(int x, int y)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, xx => xx.Hover(x, y)));
        }

        public void Hover(ElementProxy element, int x, int y)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.Hover(new ElementProxy(e.Item1, e.Item2), x, y)));
        }

        public void Hover(ElementProxy element)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.Hover(new ElementProxy(e.Item1, e.Item2))));
        }

        public void MultiSelectIndex(ElementProxy element, int[] optionIndices)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.MultiSelectIndex(new ElementProxy(e.Item1, e.Item2), optionIndices)));
        }

        public void MultiSelectText(ElementProxy element, string[] optionTextCollection)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.MultiSelectText(new ElementProxy(e.Item1, e.Item2), optionTextCollection)));
        }

        public void MultiSelectValue(ElementProxy element, string[] optionValues)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.MultiSelectValue(new ElementProxy(e.Item1, e.Item2), optionValues)));
        }

        public void Navigate(Uri url)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.Navigate(url)));
        }

        public void Press(string keys)
        {
            throw new NotImplementedException("Win32 based events are not currently functioning in multi-browser tests");
        }

        public void RightClick(int x, int y)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, xx => xx.RightClick(x, y)));
        }

        public void RightClick(ElementProxy element, int x, int y)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.RightClick(new ElementProxy(e.Item1, e.Item2), x, y)));
        }

        public void RightClick(ElementProxy element)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.RightClick(new ElementProxy(e.Item1, e.Item2))));
        }

        public void SelectIndex(ElementProxy element, int optionIndex)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.SelectIndex(new ElementProxy(e.Item1, e.Item2), optionIndex)));
        }

        public void SelectText(ElementProxy element, string optionText)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.SelectText(new ElementProxy(e.Item1, e.Item2), optionText)));
        }

        public void SelectValue(ElementProxy element, string optionValue)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.SelectValue(new ElementProxy(e.Item1, e.Item2), optionValue)));
        }

        public void SwitchToFrame(string frameName)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.SwitchToFrame(frameName)));
        }

        public void SwitchToFrame(ElementProxy frameElement)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.SwitchToFrame(frameElement)));
        }

        public void SwitchToWindow(string windowName)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.SwitchToWindow(windowName)));
        }

        public void TakeScreenshot(string screenshotName)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.TakeScreenshot(screenshotName)));
        }

        public void Type(string text)
        {
            throw new NotImplementedException("Win32 based events are not currently functioning in multi-browser tests");
        }

        public void UploadFile(ElementProxy element, int x, int y, string fileName)
        {
            RepackExceptions(() => Parallel.ForEach(element.Elements, e => e.Item1.UploadFile(new ElementProxy(e.Item1, e.Item2), x, y, fileName)));
        }

        public void Visible(ElementProxy element, Action<bool> action)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.Visible(element, action)));
        }

        public void Wait()
        {
            commandProviders.First().Wait();
        }

        public void Wait(TimeSpan timeSpan)
        {
            commandProviders.First().Wait(timeSpan);
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.WaitUntil(conditionFunc)));
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.WaitUntil(conditionFunc, timeout)));
        }

        public void WaitUntil(Expression<Action> conditionAction)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.WaitUntil(conditionAction)));
        }

        public void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            RepackExceptions(() => Parallel.ForEach(commandProviders, x => x.WaitUntil(conditionAction, timeout)));
        }

        public ICommandProvider WithConfig(FluentSettings settings)
        {
            Parallel.ForEach(commandProviders, x => x.WithConfig(settings));
            return this;
        }

        private void RepackExceptions(Action action)
        {
            try
            {
                action();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerExceptions.First();
            }
        }
    }
}