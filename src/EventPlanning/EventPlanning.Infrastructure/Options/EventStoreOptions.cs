﻿namespace EventPlanning.Infrastructure.Options
{
    public record class EventStoreOptions
    {
        public static string Name = "EventStore";
        public required string ConnectionString { get; init; }
    }
}
