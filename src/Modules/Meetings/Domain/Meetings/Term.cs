﻿using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class Term : ValueObject
    {
        public DateTime? StartDate { get; }

        public DateTime? EndDate { get; }

        public Term(DateTime? startDate, DateTime? endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        internal bool IsInTerm(DateTime date)
        {
            var left = !this.StartDate.HasValue || this.StartDate.Value <= date;

            var right = !this.EndDate.HasValue || this.EndDate.Value >= date;

            return left && right;
        }
    }
}