﻿using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups
{
    public class MeetingGroupMember : Entity
    {
        internal MeetingGroupId MeetingGroupId { get; private set; }

        internal MemberId MemberId { get; private set; }

        private MeetingGroupMemberRole _role;

        internal DateTime JoinedDate { get; private set; }

        private bool _isActive;

        private DateTime? _leaveDate;

        private MeetingGroupMember()
        {
            // Only for EF.
        }

        public MeetingGroupMember(
            MeetingGroupId meetingGroupId, 
            MemberId memberId,
            MeetingGroupMemberRole role)
        {
            this.MeetingGroupId = meetingGroupId;
            this.MemberId = memberId;
            this._role = role;
            this.JoinedDate = DateTime.UtcNow;
            this._isActive = true;

            this.AddDomainEvent(new NewMeetingGroupMemberJoinedDomainEvent(this.MeetingGroupId, this.MemberId, this._role));
        }

        public void Leave()
        {
            _isActive = false;
            _leaveDate = DateTime.UtcNow;

            this.AddDomainEvent(new MeetingGroupMemberLeavedDomainEvent(this.MeetingGroupId, this.MemberId));
        }

        internal bool IsMember(MemberId memberId)
        {
            return this._isActive && this.MemberId == memberId;
        }

        internal bool IsOrganizer(MemberId memberId)
        {
            return this.IsMember(memberId) && _role == MeetingGroupMemberRole.Organizer;
        }
    }
}