using System.Threading.Tasks;
using JobOffersApi.Abstractions.Messaging;

namespace JobOffersApi.Infrastructure.Messaging.Outbox;

public interface IOutboxBroker
{
    bool Enabled { get; }
    Task SendAsync(params IMessage[] messages);
}