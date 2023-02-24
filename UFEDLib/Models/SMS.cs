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
        public String Folder { get; set; }

        /// <summary>
        /// SMSC Number.
        /// </summary>
        public String SMSC { get; set; }

        public String Body { get; set; }

        /// <summary>
        /// SMS Source (default SMS option in the phone is left here empty).
        /// </summary>
        public String Source { get; set; }

        // public List<TimeStampEntry> AllTimeStamps { get; set; }
    }
}
