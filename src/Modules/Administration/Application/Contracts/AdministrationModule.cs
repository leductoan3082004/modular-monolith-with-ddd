﻿using System.Threading.Tasks;
using Autofac;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Processing;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    public class AdministrationModule : IAdministrationModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = AdministrationCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}