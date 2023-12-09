using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class EMail
    {
        public String Folder { get; set; }

        public String Status { get; set; }

        public Party From { get; set; }

        public List<Party> To { get; set; }

        public List<Party> Cc { get; set; }

        public List<Party> Bcc { get; set; }

        public String Subject { get; set; }

        public String Body { get; set; }

        public DateTime TimeStamp { get; set; }

        public String Priority { get; set; }    

        public List<Attachment> Attachments { get; set; }

        public String Source { get; set; }  
    }
}
