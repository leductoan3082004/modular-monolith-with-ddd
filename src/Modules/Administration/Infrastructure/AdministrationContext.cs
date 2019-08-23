﻿using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Outbox;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.Members;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Members;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure
{
    public class AdministrationContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        internal DbSet<MeetingGroupProposal> MeetingGroupProposals { get; set; }

        internal DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        internal DbSet<Member> Members { get; set; }

        public AdministrationContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MeetingGroupProposalEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MemberEntityTypeConfiguration());
        }
    }
}