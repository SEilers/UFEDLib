using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    public class MailMessage
    {

        #region fields
        public string Body { get; set; }
        public string Folder { get; set; }
        public string Source { get; set; }
        public string Subject { get; set; }
        public DateTime TimeStamp { get; set; }
        // public MessageStatus 
        #endregion

        #region models
        public Party From { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public List<Party> BCC { get; set; } = new List<Party>();
        public List<Party> CC { get; set; } = new List<Party>();
        public List<Party> To { get; set; } = new List<Party>();
        #endregion
       

        

        

        

        

        
        

        

        // public MailPriority Priority

        

        
    }
}
