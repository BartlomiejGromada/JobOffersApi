using System.Threading;
using System.Threading.Tasks;
using JobOffersApi.Abstractions.Messaging;

namespace JobOffersApi.Infrastructure.Messaging.Dispatchers;

public interface IAsyncMessageDispatcher
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : class, IMessage;
}