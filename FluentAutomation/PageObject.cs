namespace FluentAutomation
{
    using System;

    using Exceptions;

    using Interfaces;

    public abstract class PageObject
    {
        public Action At { get; set; }

        public Uri Uri { get; set; }

        public string Url
        {
            get => Uri.ToString();
            set
            {
                if (value.StartsWith("/"))
                    Uri = new Uri(value, UriKind.Relative);
                else
                    Uri = new Uri(value, UriKind.Absolute);
            }
        }
    }

    public abstract class PageObject<T> : PageObject where T : PageObject
    {
        public PageObject(FluentTest test)
        {
            TestObject = test;
        }

        public IActionSyntaxProvider I => TestObject.I;

        private FluentTest TestObject { get; }

        public T Go()
        {
            if (Uri == null)
                throw new FluentException("This page cannot be navigated to. Uri or Url is not set.");

            return Go(Uri);
        }

        public T Go(Uri uri) => Go(uri.ToString());

        public T Go(string url)
        {
            I.Open(url);
            if (At != null)
                try
                {
                    At();
                }
                catch (FluentException ex)
                {
                    throw new FluentException("Unable to verify page navigation succeeded. See InnerException for details.", ex);
                }

            return this as T;
        }

        public TNewPage Switch<TNewPage>() where TNewPage : PageObject
        {
            var newPage = (TNewPage)Activator.CreateInstance(typeof(TNewPage), TestObject);
            if (newPage.At != null)
                try
                {
                    newPage.At();
                }
                catch (FluentException ex)
                {
                    throw new FluentException("Unable to verify page navigation succeeded. See InnerException for details.", ex);
                }

            return newPage;
        }
    }
}