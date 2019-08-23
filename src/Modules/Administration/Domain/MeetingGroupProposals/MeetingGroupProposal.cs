﻿using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposal : Entity, IAggregateRoot
    {
        public MeetingGroupProposalId Id { get; private set; }

        private string _name;

        private string _description;

        private MeetingGroupLocation _location;

        private DateTime _proposalDate;

        private UserId _proposalUserId;

        private MeetingGroupProposalStatus _status;

        private MeetingGroupProposalDecision _decision;

        private MeetingGroupProposal()
        {
            // Only for EF.
        }

        private MeetingGroupProposal(
            MeetingGroupProposalId id,
            string name,
            string description,
            MeetingGroupLocation location,
            UserId proposalUserId,
            DateTime proposalDate)
        {
            Id = id;
            _name = name;
            _description = description;
            _location = location;
            _proposalUserId = proposalUserId;
            _proposalDate = proposalDate;

            _status = MeetingGroupProposalStatus.ToVerify;
            _decision = MeetingGroupProposalDecision.NoDecision;

            this.AddDomainEvent(new MeetingGroupProposalVerificationRequestedDomainEvent(this.Id));
        }

        public void Accept(UserId userId)
        {
            this.CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(_decision));

            _decision = MeetingGroupProposalDecision.AcceptDecision(DateTime.UtcNow, userId);

            _status = _decision.GetStatusForDecision();

            this.AddDomainEvent(new MeetingGroupProposalAcceptedDomainEvent(this.Id));
        }

        internal void Reject(UserId userId, string rejectReason)
        {
            _decision = MeetingGroupProposalDecision.RejectDecision(DateTime.UtcNow, userId, rejectReason);

            _status = _decision.GetStatusForDecision();

            this.AddDomainEvent(new MeetingGroupProposalRejectedDomainEvent(this.Id));
        }

        public static MeetingGroupProposal CreateToVerify(
            Guid meetingGroupProposalId,
            string name,
            string description,
            MeetingGroupLocation location,
            UserId proposalUserId,
            DateTime proposalDate)
        {
            var meetingGroupProposal = new MeetingGroupProposal(
                new MeetingGroupProposalId(meetingGroupProposalId),
                name,
                description,
                location,
                proposalUserId,
                proposalDate);

            return meetingGroupProposal;
        }
    }
}
