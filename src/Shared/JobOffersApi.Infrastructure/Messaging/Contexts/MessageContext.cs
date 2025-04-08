using System;
using JobOffersApi.Abstractions.Contexts;
using JobOffersApi.Abstractions.Messaging;

namespace JobOffersApi.Infrastructure.Messaging.Contexts;

public class MessageContext : IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }

    public MessageContext(Guid messageId, IContext context)
    {
        MessageId = messageId;
        Context = context;
    }
}