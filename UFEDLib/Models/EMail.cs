using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class EMail
    {
        public string Folder { get; set; }

        public string Status { get; set; }

        public Party From { get; set; }

        public List<Party> To { get; set; }

        public List<Party> Cc { get; set; }

        public List<Party> Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Priority { get; set; }    

        public List<Attachment> Attachments { get; set; }

        public string Source { get; set; }  
    }
}
