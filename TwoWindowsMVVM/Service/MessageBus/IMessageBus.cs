using System;

namespace TwoWindowsMVVM.Service.MessageBus;

internal interface IMessageBus
{
    IDisposable RegisterHandler<T>(Action<T> handler);

    void Send<T>(T message);
}