﻿using Xena.MemoryBus.Interfaces;

namespace Xena.MemoryBus;

internal class XenaMemoryBus : IXenaMemoryBus
{
    private readonly XenaQueryBus _queryBus;
    private readonly XenaCommandBus _commandBus;
    private readonly XenaEventBus _eventBus;

    public XenaMemoryBus(
        XenaQueryBus queryBus,
        XenaCommandBus commandBus,
        XenaEventBus eventBus)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
        _eventBus = eventBus;
    }

    public async Task Publish<TEvent>(TEvent @event) where TEvent : IXenaEvent
    {
        await _eventBus.Publish(@event);
    }

    public async Task Send<TCommand>(TCommand command) where TCommand : IXenaCommand
    {
        await _commandBus.Send(command);
    }

    public async Task<TResult> Query<TResult>(IXenaQuery<TResult> query)
    {
        var result = await _queryBus.Query(query);
        return result;
    }
}