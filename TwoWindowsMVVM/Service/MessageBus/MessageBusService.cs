using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TwoWindowsMVVM.Service.MessageBus;

internal class MessageBusService : IMessageBus
{
    private class Subscription<T> : IDisposable
    {
        private WeakReference<MessageBusService> _bus;
        public Action<T> Handler { get; }

        public Subscription(MessageBusService bus, Action<T> handler)
        {
            _bus = new (bus);
            Handler = handler;
        }

        public void Dispose()
        {
            if (!_bus.TryGetTarget(out var bus))
                return;
            var @lock = bus._lock;
            @lock.EnterWriteLock();
            var messageType = typeof(T);
            try
            {
                if (!bus._subscriptions.TryGetValue(messageType, out var refs))
                    return;

                var updatedRefs = refs.Where(r => r.IsAlive).ToList();

                WeakReference? currentRef = null;
                foreach(var @ref in updatedRefs)
                    if(ReferenceEquals(@ref.Target, this))
                    {
                        currentRef = @ref;
                        break;
                    }
                if (currentRef is null)
                    return;

                updatedRefs.Remove(currentRef);
                bus._subscriptions[messageType] = updatedRefs; 
            }
            finally
            {
                @lock.ExitWriteLock();
            }
        }
    }

    private readonly Dictionary<Type, IEnumerable<WeakReference>> _subscriptions = new();

    private readonly ReaderWriterLockSlim _lock = new();
    public IDisposable RegisterHandler<T>(Action<T> handler)
    {
        var subscription = new Subscription<T>(this, handler);

        _lock.EnterWriteLock();
        try
        {
            var subscriptionReference = new WeakReference(subscription);
            var messageType = typeof(T);
            _subscriptions[messageType] = _subscriptions.TryGetValue(messageType, out var subscriptions)
                ? subscriptions.Append(subscriptionReference)
                : new[] { subscriptionReference };

        }
        finally { _lock.ExitWriteLock(); }
        return subscription;
    }

    public void Send<T>(T message)
    {
        if (GetHandlers<T>() is not { } handlers)
            return;

        foreach(var handler in handlers)    
            handler(message);
    }

    private IEnumerable<Action<T>>? GetHandlers<T>()
    {
        var handlers = new List<Action<T>>();
        var messageType = typeof (T);
        var existDieRefs = false;

        _lock.EnterReadLock();
        try
        {
            if (!_subscriptions.TryGetValue(messageType, out var refs))
                return null;

            foreach (var @ref in refs)
                if (@ref.Target is Subscription<T> { Handler: var handler })
                    handlers.Add(handler);
                else
                    existDieRefs = true;
        }
        finally { _lock.ExitReadLock(); }

        if (!existDieRefs) return handlers;

        _lock.EnterWriteLock();
        try
        {
            if (!_subscriptions.TryGetValue(messageType, out var refs))
                if (refs.Where(r => r.IsAlive).ToArray() is { Length: > 0 } newRefs)
                    _subscriptions[messageType] = newRefs;
                else
                    _subscriptions.Remove(messageType);
        }
        finally { _lock.ExitWriteLock(); }

        return handlers;
    }
}

