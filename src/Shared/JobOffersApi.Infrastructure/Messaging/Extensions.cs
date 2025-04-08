using Microsoft.Extensions.DependencyInjection;
using JobOffersApi.Abstractions.Messaging;
using JobOffersApi.Infrastructure.Messaging.Brokers;
using JobOffersApi.Infrastructure.Messaging.Contexts;
using JobOffersApi.Infrastructure.Messaging.Dispatchers;

namespace JobOffersApi.Infrastructure.Messaging;

public static class Extensions
{
    private const string SectionName = "messaging";
        
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddTransient<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IMessageContextProvider, MessageContextProvider>();
        services.AddSingleton<IMessageContextRegistry, MessageContextRegistry>();

        var messagingOptions = services.GetOptions<MessagingOptions>(SectionName);
        services.AddSingleton(messagingOptions);

        if (messagingOptions.UseAsyncDispatcher)
        {
            services.AddHostedService<AsyncDispatcherJob>();
        }
            
        return services;
    }
}