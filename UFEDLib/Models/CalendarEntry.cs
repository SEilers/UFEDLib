using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class CalendarEntry
    {
        public string Category { get; set; }

        public string Subject { get; set; }

        public string Location { get; set; }

        public string Details { get; set; }

        public List<Party> Attendees { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Reminder { get; set; }

        public EventPriority Priority { get; set; }

        public enum EventPriority { Unknown, Low, Normal, High }

        public EventStatus Status { get; set; }

        public enum EventStatus { Unknown, Accepted, NeedsAction, Sent, Tentative, Confirmed, Declined,
            Completed, Delegated, InProgress, WaitingOnInfo }

        public EventClass Class { get; set; }

        public enum EventClass { Normal, Personal, Private, Confidential }

        public RepeatRuleEnum RepeatRule { get; set; }

        public enum RepeatRuleEnum { None, Daily, Weekly, Monthly, Yearly }

        public DateTime RepeatUntil { get; set; }

        public RepeatDayEnum RepeatDay { get; set; }

        public enum RepeatDayEnum { None, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, First, Second, Third, Fourth, Last }

        public int RepeatInterval { get; set; }

        public string Source { get; set; }

    }
}
