using JobOffersApi.Abstractions.Messaging;

namespace JobOffersApi.Infrastructure.Messaging.Contexts;

public interface IMessageContextRegistry
{
    void Set(IMessage message, IMessageContext context);
}