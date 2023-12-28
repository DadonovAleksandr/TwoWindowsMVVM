using System;

namespace TwoWindowsMVVM.Service.MessageBus;

internal class MessageBusService : IMessageBus
{
    public IDisposable RegisterHandler<T>(Action<T> handler)
    {
        throw new NotImplementedException();
    }

    public void Send<T>(T message)
    {
        throw new NotImplementedException();
    }
}

