﻿namespace FluentAutomation.Interfaces
{
    using System;

    public interface ISyntaxProvider : IDisposable
    {
        /// <summary>
        ///     Used to determine if we've finished a test and need to create a new syntax provider
        /// </summary>
        bool IsDisposed();
    }
}