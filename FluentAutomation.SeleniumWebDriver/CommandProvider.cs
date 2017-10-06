namespace FluentAutomation
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Exceptions;

    using Interfaces;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    public class CommandProvider : BaseCommandProvider, ICommandProvider, IDisposable
    {
        public static IAlert ActiveAlert;
        private readonly IFileStoreProvider fileStoreProvider;
        private readonly Lazy<IWebDriver> lazyWebDriver;

        private string mainWindowHandle;

        public CommandProvider(Func<IWebDriver> webDriverFactory, IFileStoreProvider fileStoreProvider)
        {
            FluentTest.ProviderInstance = null;

            lazyWebDriver = new Lazy<IWebDriver>(() =>
            {
                var webDriver = webDriverFactory();
                if (!FluentTest.IsMultiBrowserTest && FluentTest.ProviderInstance == null)
                    FluentTest.ProviderInstance = webDriver;

                webDriver.Manage().Cookies.DeleteAllCookies();
                webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

                // If an alert is open, the world ends if we touch the size property. Ignore this and let it get set by the next command chain
                try
                {
                    if (Settings.WindowMaximized)
                    {
                        // store window size back before maximizing so we can 'undo' this action if necessary
                        var windowSize = webDriver.Manage().Window.Size;
                        if (!Settings.WindowWidth.HasValue)
                            Settings.WindowWidth = windowSize.Width;

                        if (!Settings.WindowHeight.HasValue)
                            Settings.WindowHeight = windowSize.Height;

                        webDriver.Manage().Window.Maximize();
                    }
                    else if (Settings.WindowHeight.HasValue && Settings.WindowWidth.HasValue)
                    {
                        webDriver.Manage().Window.Size = new Size(Settings.WindowWidth.Value, Settings.WindowHeight.Value);
                    }
                    else
                    {
                        var windowSize = webDriver.Manage().Window.Size;
                        Settings.WindowHeight = windowSize.Height;
                        Settings.WindowWidth = windowSize.Width;
                    }
                }
                catch (UnhandledAlertException)
                {
                }

                mainWindowHandle = webDriver.CurrentWindowHandle;

                return webDriver;
            });

            this.fileStoreProvider = fileStoreProvider;
        }

        public string Source => WebDriver.PageSource;

        public Uri Url => new Uri(WebDriver.Url, UriKind.Absolute);
        private IWebDriver WebDriver => lazyWebDriver.Value;

        public void AlertClick(Alert accessor)
        {
            SetActiveAlert();
            if (ActiveAlert == null)
                return;

            try
            {
                Act(CommandType.Action, () =>
                {
                    try
                    {
                        if (accessor == Alert.OK)
                            ActiveAlert.Accept();
                        else
                            ActiveAlert.Dismiss();
                    }
                    catch (NoAlertPresentException ex)
                    {
                        throw new FluentException(ex.Message, ex);
                    }
                });
            }
            finally
            {
                ActiveAlert = null;
            }
        }

        public void AlertEnterText(string text)
        {
            SetActiveAlert();
            ActiveAlert.SendKeys(text);

            try
            {
                // just do it - attempting to get behaviors between browsers to match
                ActiveAlert.Accept();
            }
            catch (Exception)
            {
            }
        }

        public void AlertText(Action<string> matchFunc)
        {
            SetActiveAlert();
            matchFunc(ActiveAlert.Text);
        }

        public void AppendText(ElementProxy element, string text)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;
                unwrappedElement.WebElement.SendKeys(text);
            });
        }

        public void AppendTextWithoutEvents(ElementProxy element, string text)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;
                ((IJavaScriptExecutor)WebDriver).ExecuteScript(string.Format("if (typeof fluentjQuery != 'undefined') {{ fluentjQuery(\"{0}\").val(fluentjQuery(\"{0}\").val() + \"{1}\").trigger('change'); }}", unwrappedElement.Selector.Replace("\"", ""), text.Replace("\"", "")));
            });
        }

        public void Click(int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                var rootElement = Find("html", Defaults.FindMethod).Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(rootElement.WebElement, x, y)
                    .Click()
                    .Perform();
            });
        }

        public void Click(ElementProxy element, int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                var containerElement = element.Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(containerElement.WebElement, x, y)
                    .Click()
                    .Perform();
            });
        }

        public void Click(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                var containerElement = element.Element as Element;
                new Actions(WebDriver)
                    .Click(containerElement.WebElement)
                    .Perform();
            });
        }

        public void CssPropertyValue(ElementProxy element, string propertyName, Action<bool, string> action)
        {
            Act(CommandType.Action, () =>
            {
                var propertyValue = ((IJavaScriptExecutor)WebDriver).ExecuteScript(string.Format("return fluentjQuery(\"{0}\").css(\"{1}\")", element.Element.Selector, propertyName));
                if (propertyValue == null)
                    action(false, string.Empty);
                else
                    action(true, propertyValue.ToString());
            });
        }

        public void Dispose()
        {
            try
            {
                WebDriver.Manage().Cookies.DeleteAllCookies();
                WebDriver.Quit();
                WebDriver.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public void DoubleClick(int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                var rootElement = Find("html", Defaults.FindMethod).Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(rootElement.WebElement, x, y)
                    .DoubleClick()
                    .Perform();
            });
        }

        public void DoubleClick(ElementProxy element, int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                var containerElement = element.Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(containerElement.WebElement, x, y)
                    .DoubleClick()
                    .Perform();
            });
        }

        public void DoubleClick(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                var containerElement = element.Element as Element;
                new Actions(WebDriver)
                    .DoubleClick(containerElement.WebElement)
                    .Perform();
            });
        }

        public void DragAndDrop(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            Act(CommandType.Action, () =>
            {
                var rootElement = Find("html", Defaults.FindMethod).Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(rootElement.WebElement, sourceX, sourceY)
                    .ClickAndHold()
                    .MoveToElement(rootElement.WebElement, destinationX, destinationY)
                    .Release()
                    .Perform();
            });
        }

        public void DragAndDrop(ElementProxy source, int sourceOffsetX, int sourceOffsetY, ElementProxy target, int targetOffsetX, int targetOffsetY)
        {
            Act(CommandType.Action, () =>
            {
                var element = source.Element as Element;
                var targetElement = target.Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(element.WebElement, sourceOffsetX, sourceOffsetY)
                    .ClickAndHold()
                    .MoveToElement(targetElement.WebElement, targetOffsetX, targetOffsetY)
                    .Release()
                    .Perform();
            });
        }

        public void DragAndDrop(ElementProxy source, ElementProxy target)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedSource = source.Element as Element;
                var unwrappedTarget = target.Element as Element;

                new Actions(WebDriver)
                    .DragAndDrop(unwrappedSource.WebElement, unwrappedTarget.WebElement)
                    .Perform();
            });
        }

        public void EnterText(ElementProxy element, string text)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                unwrappedElement.WebElement.Clear();
                unwrappedElement.WebElement.SendKeys(text);
            });
        }

        public void EnterTextWithoutEvents(ElementProxy element, string text)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                ((IJavaScriptExecutor)WebDriver).ExecuteScript(string.Format("if (typeof fluentjQuery != 'undefined') {{ fluentjQuery(\"{0}\").val(\"{1}\").trigger('change'); }}", unwrappedElement.Selector.Replace("\"", ""), text.Replace("\"", "")));
            });
        }

        public ElementProxy Find(string selector) => Find(selector, Defaults.FindMethod);

        public ElementProxy Find(string selector, FindBy findMethod)
        {
            return new ElementProxy(this, () =>
            {
                try
                {
                    var webElement = WebDriver.FindElement(findMethod == FindBy.Css ? Sizzle.Find(selector) : By.XPath(selector));
                    return new Element(webElement, selector, findMethod);
                }
                catch (NoSuchElementException)
                {
                    throw new FluentElementNotFoundException("Unable to find element with selector [{0}]", selector);
                }
            });
        }

        public ElementProxy FindMultiple(string selector) => FindMultiple(selector, Defaults.FindMethod);

        public ElementProxy FindMultiple(string selector, FindBy findMethod)
        {
            var finalResult = new ElementProxy();

            finalResult.Children.Add(() =>
            {
                var result = new ElementProxy();
                var webElements = WebDriver.FindElements(findMethod == FindBy.Css ? Sizzle.Find(selector) : By.XPath(selector));
                if (webElements.Count == 0)
                    throw new FluentElementNotFoundException("Unable to find element with selector [{0}].", selector);

                foreach (var element in webElements)
                    result.Elements.Add(new Tuple<ICommandProvider, Func<IElement>>(this, () => new Element(element, selector, findMethod)));

                return result;
            });

            return finalResult;
        }

        public void Focus(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                switch (unwrappedElement.WebElement.TagName)
                {
                    case "input":
                    case "select":
                    case "textarea":
                    case "a":
                    case "iframe":
                    case "button":
                        var executor = (IJavaScriptExecutor)WebDriver;
                        executor.ExecuteScript("arguments[0].focus();", unwrappedElement.WebElement);
                        break;
                }
            });
        }

        public void Hover(int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                var rootElement = Find("html", Defaults.FindMethod).Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(rootElement.WebElement, x, y)
                    .Perform();
            });
        }

        public void Hover(ElementProxy element, int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                var containerElement = element.Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(containerElement.WebElement, x, y)
                    .Perform();
            });
        }

        public void Hover(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(unwrappedElement.WebElement)
                    .Perform();
            });
        }

        public void MultiSelectIndex(ElementProxy element, int[] optionIndices)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (var optionIndex in optionIndices)
                    selectElement.SelectByIndex(optionIndex);
            });
        }

        public void MultiSelectText(ElementProxy element, string[] optionTextCollection)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (var optionText in optionTextCollection)
                    selectElement.SelectByText(optionText);
            });
        }

        public void MultiSelectValue(ElementProxy element, string[] optionValues)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();

                foreach (var optionValue in optionValues)
                    selectElement.SelectByValue(optionValue);
            });
        }

        public void Navigate(Uri url)
        {
            Act(CommandType.Action, () =>
            {
                var currentUrl = new Uri(WebDriver.Url);
                var baseUrl = currentUrl.GetLeftPart(UriPartial.Authority);

                if (!url.IsAbsoluteUri)
                    url = new Uri(new Uri(baseUrl), url.ToString());

                WebDriver.Navigate().GoToUrl(url);
            });
        }

        public void Press(string keys)
        {
            Act(CommandType.Action, () => SendKeys.SendWait(keys));
        }

        public void RightClick(int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                var rootElement = Find("html", Defaults.FindMethod).Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(rootElement.WebElement, x, y)
                    .ContextClick()
                    .Perform();
            });
        }

        public void RightClick(ElementProxy element, int x, int y)
        {
            Act(CommandType.Action, () =>
            {
                var containerElement = element.Element as Element;
                new Actions(WebDriver)
                    .MoveToElement(containerElement.WebElement, x, y)
                    .ContextClick()
                    .Perform();
            });
        }

        public void RightClick(ElementProxy element)
        {
            Act(CommandType.Action, () =>
            {
                var containerElement = element.Element as Element;
                new Actions(WebDriver)
                    .ContextClick(containerElement.WebElement)
                    .Perform();
            });
        }

        public void SelectIndex(ElementProxy element, int optionIndex)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByIndex(optionIndex);
            });
        }

        public void SelectText(ElementProxy element, string optionText)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByText(optionText);
            });
        }

        public void SelectValue(ElementProxy element, string optionValue)
        {
            Act(CommandType.Action, () =>
            {
                var unwrappedElement = element.Element as Element;

                SelectElement selectElement = new SelectElement(unwrappedElement.WebElement);
                if (selectElement.IsMultiple) selectElement.DeselectAll();
                selectElement.SelectByValue(optionValue);
            });
        }

        public void SwitchToFrame(string frameNameOrSelector)
        {
            Act(CommandType.Action, () =>
            {
                if (frameNameOrSelector == string.Empty)
                {
                    WebDriver.SwitchTo().DefaultContent();
                    return;
                }

                // try to locate frame using argument as a selector, if that fails pass it into Frame so it can be
                // evaluated as a name by Selenium
                IWebElement frameBySelector = null;
                try
                {
                    frameBySelector = WebDriver.FindElement(Sizzle.Find(frameNameOrSelector));
                }
                catch (NoSuchElementException)
                {
                }

                if (frameBySelector == null)
                    WebDriver.SwitchTo().Frame(frameNameOrSelector);
                else
                    WebDriver.SwitchTo().Frame(frameBySelector);
            });
        }

        public void SwitchToFrame(ElementProxy frameElement)
        {
            Act(CommandType.Action, () => { WebDriver.SwitchTo().Frame((frameElement.Element as Element).WebElement); });
        }

        public void SwitchToWindow(string windowName)
        {
            Act(CommandType.Action, () =>
            {
                if (windowName == string.Empty)
                {
                    WebDriver.SwitchTo().Window(mainWindowHandle);
                    return;
                }

                var matchFound = false;
                foreach (var windowHandle in WebDriver.WindowHandles)
                {
                    WebDriver.SwitchTo().Window(windowHandle);

                    if (WebDriver.Title == windowName || WebDriver.Url.EndsWith(windowName))
                    {
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                    throw new FluentException("No window with a title or URL matching [{0}] could be found.", windowName);
            });
        }

        public override void TakeScreenshot(string screenshotName)
        {
            Act(CommandType.Action, () =>
            {
                // get raw screenshot
                var screenshotDriver = (ITakesScreenshot)WebDriver;
                var tmpImagePath = Path.Combine(Settings.UserTempDirectory, screenshotName);
                screenshotDriver.GetScreenshot().SaveAsFile(tmpImagePath, ImageFormat.Png);

                // save to file store
                fileStoreProvider.SaveScreenshot(Settings, File.ReadAllBytes(tmpImagePath), screenshotName);
                File.Delete(tmpImagePath);
            });
        }

        public void Type(string text)
        {
            Act(CommandType.Action, () =>
            {
                foreach (var character in text)
                {
                    SendKeys.SendWait(character.ToString());
                    Wait(TimeSpan.FromMilliseconds(20));
                }
            });
        }

        public void UploadFile(ElementProxy element, int x, int y, string fileName)
        {
            Act(CommandType.Action, () =>
            {
                // wait before typing in the field
                var task = Task.Factory.StartNew(() =>
                {
                    Wait(TimeSpan.FromMilliseconds(1000));
                    Type(fileName);
                });

                if (x == 0 && y == 0)
                    Click(element);
                else
                    Click(element, x, y);

                task.Wait();
                Wait(TimeSpan.FromMilliseconds(1500));
            });
        }

        public void Visible(ElementProxy element, Action<bool> action)
        {
            Act(CommandType.Action, () =>
            {
                var isVisible = (element.Element as Element).WebElement.Displayed;
                action(isVisible);
            });
        }

        public ICommandProvider WithConfig(FluentSettings settings)
        {
            // If an alert is open, the world ends if we touch the size property. Ignore this and let it get set by the next command chain
            try
            {
                if (settings.WindowMaximized)
                {
                    // store window size back before maximizing so we can 'undo' this action if necessary
                    // this code intentionally touches this.Settings before its been replaced with the local
                    // configuration code, so that when the With.___.Then block is completed, the outer settings
                    // object has the correct window size to work with.
                    var windowSize = WebDriver.Manage().Window.Size;
                    if (!Settings.WindowWidth.HasValue)
                        Settings.WindowWidth = windowSize.Width;

                    if (!Settings.WindowHeight.HasValue)
                        Settings.WindowHeight = windowSize.Height;

                    WebDriver.Manage().Window.Maximize();
                }
                // If the browser size has changed since the last config change, update it
                else if (settings.WindowWidth.HasValue && settings.WindowHeight.HasValue)
                {
                    WebDriver.Manage().Window.Size = new Size(settings.WindowWidth.Value, settings.WindowHeight.Value);
                }
            }
            catch (UnhandledAlertException)
            {
            }

            Settings = settings;

            return this;
        }

        private void SetActiveAlert()
        {
            if (ActiveAlert == null)
                Act(CommandType.Action, () =>
                {
                    try
                    {
                        ActiveAlert = WebDriver.SwitchTo().Alert();
                    }
                    catch (Exception ex)
                    {
                        throw new FluentException(ex.Message, ex);
                    }
                });
        }
    }
}