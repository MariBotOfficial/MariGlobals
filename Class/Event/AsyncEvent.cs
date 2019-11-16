﻿using MariGlobals.Class.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MariGlobals.Class.Event
{
    //Thanks for D#+.
    //https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/AsyncEvent.cs

    internal delegate Task NullHandler();

    public delegate Task AsyncEventHandler();

    public delegate Task AsyncEventHandler<T>(T arg);

    #region NormalAsyncEvent

    public sealed class AsyncEvent : BaseAsyncEvent
    {
        private readonly object _lock = new object();
        private List<AsyncEventHandler> Handlers { get; }

        public AsyncEvent()
        {
            Handlers = new List<AsyncEventHandler>();
        }

        #region Register

        public void Register(AsyncEventHandler handler)
        {
            if (handler.HasNoContent())
                throw new ArgumentNullException(nameof(handler), "Handler cannot be null");

            lock (_lock)
                Handlers.Add(handler);
        }

        #endregion Register

        #region Unregister

        public void Unregister(AsyncEventHandler handler)
        {
            if (handler.HasNoContent())
                throw new ArgumentNullException(nameof(handler), "Handler cannot be null");

            lock (_lock)
                Handlers.Remove(handler);
        }

        #endregion Unregister

        #region InvokeAsync

        public async Task InvokeAsync()
        {
            List<AsyncEventHandler> handlers = null;
            lock (_lock)
                handlers = Handlers;

            if (handlers.HasNoContent())
                return;

            await InvokeAllAsync(handlers).ConfigureAwait(false);
        }

        #endregion InvokeAsync
    }

    #endregion NormalAsyncEvent

    #region GenericAsyncEvent

    public sealed class AsyncEvent<T> : BaseAsyncEvent
    {
        private readonly object _lock = new object();
        private List<AsyncEventHandler<T>> Handlers { get; }

        public AsyncEvent()
        {
            Handlers = new List<AsyncEventHandler<T>>();
        }

        #region Register

        public void Register(AsyncEventHandler<T> handler)
        {
            if (handler.HasNoContent())
                throw new ArgumentNullException(nameof(handler), "Handler cannot be null");

            lock (_lock)
                Handlers.Add(handler);
        }

        #endregion Register

        #region Unregister

        public void Unregister(AsyncEventHandler<T> handler)
        {
            if (handler.HasNoContent())
                throw new ArgumentNullException(nameof(handler), "Handler cannot be null");

            lock (_lock)
                Handlers.Remove(handler);
        }

        #endregion Unregister

        #region InvokeAsync

        public async Task InvokeAsync(T arg)
        {
            List<AsyncEventHandler<T>> handlers = null;

            lock (_lock)
                handlers = Handlers;

            if (handlers.HasNoContent())
                return;

            await InvokeAllAsync(handlers, arg).ConfigureAwait(false);
        }

        #endregion InvokeAsync
    }

    #endregion GenericAsyncEvent
}