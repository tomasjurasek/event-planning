﻿using EventPlanning.Writer.Domain.Common;
using EventPlanning.Writer.Domain.Event;
using FluentResults;

namespace EventPlanning.Writer.Application.Commands
{
    public class CancelEventCommandHandler : CommandHandlerBase<CancelEventCommand>
    {
        private readonly IAggregateRootRepository<EventAggregate> _eventRepository;

        public CancelEventCommandHandler(IAggregateRootRepository<EventAggregate> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        protected override async Task<Result<CommandResult>> HandleAsync(CancelEventCommand command)
        {
            var result = await _eventRepository.FindAsync(command.Id);

            result.Value.Cancel();

            return await StoreAsync(result.Value);

            // TODO
            async Task<Result<CommandResult>> StoreAsync(EventAggregate aggregate)
            {
                var result = await _eventRepository.StoreAsync(aggregate);

                return result switch
                {
                    { IsSuccess: true } => Result.Ok(new CommandResult
                    {
                        Id = aggregate.Id
                    }),
                    _ => result
                };
            }
        }
    }
}
