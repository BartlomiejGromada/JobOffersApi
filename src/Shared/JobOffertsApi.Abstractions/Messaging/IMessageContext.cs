using System;
using JobOffersApi.Abstractions.Contexts;

namespace JobOffersApi.Abstractions.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}