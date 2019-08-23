﻿using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Processing.InternalCommands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}