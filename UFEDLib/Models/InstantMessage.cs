using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class InstantMessage
    {
        public String Id { get; set; }

        public Party From { get; set; }

        public List<Party> To { get; set; } = new List<Party>();

        public String Subject { get; set; }

        public String Body { get; set; }
        public String SourceApplication { get; set; }

        public DateTime TimeStamp { get; set; }

        public DateTime DateRead { get; set; }

        public DateTime DateDelivered { get; set; }

        public List<Attachment> Attachments { get; set; }

        public Coordinate Position { get; set; }

        //public MessageStatus Status {get;set;}

        public List<Contact> ShardedContacts { get; set; }

        public String Label { get; set; }

        public String PositionAddress { get; set; }

        public String ChatId { get; set; }

        public String Erased { get; set; }

        public String Source { get; set; }
       
        public String FromIsOwner { get; set; }

        public String Identifier { get; set; }

        public String Status { get; set; }

        public String Type { get; set; }

        
        public String AttachentFileName { get; set; }

        public String AttachmentType { get; set; }

        public override string ToString()
        {
            return SourceApplication;
        }
        public String Transcripion { get; set; }

        public String JumpTargetId { get; set; }
    }
}
