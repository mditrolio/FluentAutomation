namespace FluentAutomation
{
    using System;

    using Interfaces;

    using TinyIoC;

    public class FluentSession : IDisposable
    {
        internal bool HasBootstrappedTypes;

        internal TinyIoCContainer.RegisterOptions SyntaxProviderRegisterOptions;

        public FluentSession()
        {
            if (Current != null)
            {
                Container = Current.Container;
                SyntaxProviderRegisterOptions = Current.SyntaxProviderRegisterOptions;
                HasBootstrappedTypes = Current.HasBootstrappedTypes;
            }
            else
            {
                Container = new TinyIoCContainer();
            }

            if (FluentSettings.Current.MinimizeAllWindowsOnTestStart) Win32Magic.MinimizeAllWindows();
        }

        public TinyIoCContainer Container { get; }
        internal static FluentSession Current { get; set; }

        public static void DisableStickySession()
        {
            if (Current == null)
                return;

            Current.SyntaxProviderRegisterOptions.AsMultiInstance();
            Current = null;
            AppDomain.CurrentDomain.DomainUnload -= CurrentDomain_DomainUnload;
        }

        public static void EnableStickySession()
        {
            Current = new FluentSession();
            if (Current.SyntaxProviderRegisterOptions == null)
                Current.RegisterSyntaxProvider<ActionSyntaxProvider>();

            if (Current.HasBootstrappedTypes == false)
            {
                Current.SyntaxProviderRegisterOptions.AsSingleton();
                AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            }
        }

        public static void SetStickySession(FluentSession session)
        {
            Current = session;
            Current.SyntaxProviderRegisterOptions.AsSingleton();
        }

        public void BootstrapTypeRegistration(Action<TinyIoCContainer> containerAction)
        {
            if (Current == null)
            {
                containerAction(Container);
                return;
            }

            if (Current.HasBootstrappedTypes == false)
                containerAction(Container);

            Current.HasBootstrappedTypes = true;
        }

        public void Dispose()
        {
            try
            {
                GetSyntaxProvider().Dispose();
            }
            catch (Exception)
            {
            }
        }

        public ISyntaxProvider GetSyntaxProvider() => Container.Resolve<ISyntaxProvider>();

        public void RegisterSyntaxProvider<T>() where T : ISyntaxProvider
        {
            if (SyntaxProviderRegisterOptions == null)
                SyntaxProviderRegisterOptions = Container.Register(typeof(ISyntaxProvider), typeof(T));
        }

        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            if (Current != null)
                Current.Dispose();
        }
    }
}