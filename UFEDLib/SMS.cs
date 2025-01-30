using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace UFEDLib
{
    [Serializable]
    public class SMS : ModelBase
    {
        #region fields
        public string Body { get; set; }
        /// <summary>
        /// SMS Folder (e.g. Inbox, Draft, Sent).
        /// </summary>
        public string Folder { get; set; }
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// SMSC Number.
        /// </summary>
        public string SMSC { get; set; }

        /// <summary>
        /// SMS Source (default SMS option in the phone is left here empty).
        /// </summary>
        public string Source { get; set; }

        public string Status { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        /// <summary>
        /// SMS parties.
        /// </summary>
        public List<Party> Parties { get; set; }
        #endregion


        // todo:? public List<TimeStampEntry> AllTimeStamps { get; set; }
    }
}
