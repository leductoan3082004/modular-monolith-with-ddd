﻿using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.EventBus;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.EventsBus
{
    internal class EventsBusModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEventBusClient>()
                .As<IEventsBus>()
                .SingleInstance();
        }
    }
}