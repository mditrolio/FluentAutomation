namespace FluentAutomation
{
    using Exceptions;

    using Interfaces;

    /// <summary>
    ///     FluentTest - To be extended by tests targeting FluentAutomation. In the constructor, a user should call an
    ///     appropriate bootstrap function from a FluentAutomation Provider.
    /// </summary>
    public class FluentTest : BaseFluentTest
    {
        public static bool IsMultiBrowserTest = false;

        private static object providerInstance;

        private FluentSession session;

        public static object ProviderInstance
        {
            get
            {
                if (IsMultiBrowserTest)
                    throw new FluentException("Accessing the Provider while using multiple browsers in a single test is unsupported.");

                return providerInstance;
            }

            set => providerInstance = value;
        }

        public FluentConfig Config => FluentConfig.Current;

        /// <summary>
        ///     Actions - Fluent's action functionality.
        /// </summary>
        public IActionSyntaxProvider I
        {
            get
            {
                var provider = SyntaxProvider as IActionSyntaxProvider;
                if (provider == null || provider.IsDisposed())
                {
                    Session.BootstrapTypeRegistration(FluentSettings.Current.ContainerRegistration);
                    SyntaxProvider = Session.GetSyntaxProvider();
                }

                // set the CommandProvider settings each time I is accessed, this allows reversion of
                // per step configuration values
                var actionSyntaxProvider = (ActionSyntaxProvider)SyntaxProvider;
                actionSyntaxProvider.WithConfig(FluentSettings.Current);

                return SyntaxProvider as IActionSyntaxProvider;
            }
        }

        public object Provider
        {
            get
            {
                if (ProviderInstance == null)
                    throw new FluentException("Provider is not available yet. Open a page with I.Open to create the provider.");

                return ProviderInstance;
            }
        }

        public FluentSession Session
        {
            get
            {
                if (session == null)
                {
                    session = new FluentSession();
                    session.RegisterSyntaxProvider<ActionSyntaxProvider>();
                }

                return session;
            }
        }

        public WithSyntaxProvider With => new WithSyntaxProvider(I);
    }

    public class FluentTest<T> : FluentTest where T : class
    {
        public new T Provider
        {
            get
            {
                if (ProviderInstance == null)
                    throw new FluentException("Provider is not available yet. Open a page with I.Open to create the provider.");

                return ProviderInstance as T;
            }
        }
    }
}