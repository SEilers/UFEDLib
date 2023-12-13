using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class EMail
    {
        #region fields
        public string Body { get; set; }
        public string Folder { get; set; }
        public string Priority { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region models
        public Party From { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; }
        public List<Party> Bcc { get; set; }
        public List<Party> Cc { get; set; }
        public List<Party> To { get; set; }
        #endregion
    }
}
