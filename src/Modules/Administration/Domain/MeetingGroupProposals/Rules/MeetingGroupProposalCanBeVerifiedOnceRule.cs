﻿using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules
{
    internal class MeetingGroupProposalCanBeVerifiedOnceRule : IBusinessRule
    {
        private readonly MeetingGroupProposalDecision _actualDecision;
        internal MeetingGroupProposalCanBeVerifiedOnceRule(MeetingGroupProposalDecision actualDecision)
        {
            _actualDecision = actualDecision;
        }

        public bool IsBroken() => _actualDecision != MeetingGroupProposalDecision.NoDecision;

        public string Message => "Meeting group proposal can be verified only once";
    }
}