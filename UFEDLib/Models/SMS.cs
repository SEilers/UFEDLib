using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace UFEDLib.Models
{
    [Serializable]
    public class SMS
    {
        public DateTime TimeStamp { get; set; }

        //public MessageStatus Status { get; set; }

        /// <summary>
        /// SMS parties.
        /// </summary>
        public List<Party> Parties { get; set; }

        /// <summary>
        /// SMS Folder (e.g. Inbox, Draft, Sent).
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// SMSC Number.
        /// </summary>
        public string SMSC { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// SMS Source (default SMS option in the phone is left here empty).
        /// </summary>
        public string Source { get; set; }

        // public List<TimeStampEntry> AllTimeStamps { get; set; }
    }
}
