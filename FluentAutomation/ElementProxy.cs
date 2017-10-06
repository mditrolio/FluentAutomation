namespace FluentAutomation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    public class ElementProxy
    {
        private readonly List<Tuple<ICommandProvider, Func<IElement>>> elements = new List<Tuple<ICommandProvider, Func<IElement>>>();

        /// <summary>
        ///     Base constructor should not be executed by user-code. Exists for framework use only.
        /// </summary>
        public ElementProxy()
        {
            Children = new List<Func<ElementProxy>>();
        }

        /// <summary>
        ///     Representation of an element tied to a specific command provider/browser instance.
        /// </summary>
        /// <param name="commandProvider"></param>
        /// <param name="element"></param>
        public ElementProxy(ICommandProvider commandProvider, Func<IElement> element)
        {
            Elements.Add(new Tuple<ICommandProvider, Func<IElement>>(commandProvider, element));
        }

        public IElement Element => Elements.First().Item2();

        /// <summary>
        ///     Representation of an element across command providers, wrapped for lazy loading.
        /// </summary>
        public List<Tuple<ICommandProvider, Func<IElement>>> Elements
        {
            get
            {
                // if a FindMultiple result has been passed in, we need to 'elevate' its items first
                if (Children != null)
                {
                    foreach (var proxy in Children)
                    {
                        foreach (var element in proxy().Elements)
                            elements.Add(new Tuple<ICommandProvider, Func<IElement>>(element.Item1, element.Item2));
                    }

                    Children = null;
                }

                return elements;
            }
        }

        public List<Func<ElementProxy>> Children { get; set; }
    }
}