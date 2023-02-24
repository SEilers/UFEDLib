using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    public class MailMessage
    {
        public String Folder { get; set; }

        // public MessageStatus 

        public Party From { get; set; }

        public List<Party> To { get; set; }

        public List<Party> CC { get; set; }

        public List<Party> BCC { get; set;}

        public String Subject { get; set; }
        public String Body { get; set; }

        public DateTime TimeStamp { get; set; }

        // public MailPriority Priority

        public List<Attachment> Attachments { get; set; }

        public String Source { get; set; }
    }

    }
}
