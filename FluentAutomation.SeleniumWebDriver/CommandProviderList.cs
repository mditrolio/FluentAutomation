namespace FluentAutomation.Interfaces
{
    using System.Collections.Generic;

    public class CommandProviderList : List<ICommandProvider>
    {
        public CommandProviderList(IEnumerable<ICommandProvider> collection)
            : base(collection)
        {
        }
    }
}