﻿using JobOffersApi.Abstractions.Messaging;

namespace JobOffersApi.Shared.Tests;

public class TestMessageBroker : IMessageBroker
{
    private readonly List<IMessage> _messages = new();

    public IReadOnlyList<IMessage> Messages => _messages;

    public Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
    {
        _messages.Add(message);
        return Task.CompletedTask;
    }

    public Task PublishAsync(IMessage[] messages, CancellationToken cancellationToken = default)
    {
        _messages.AddRange(messages);
        return Task.CompletedTask;
    }
}