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
        #region fields
        public string Body { get; set; }

        public string ChatId { get; set; }

        public DateTime DateDelivered { get; set; }

        public DateTime DateRead { get; set; }

        public string Erased { get; set; }

        public string FromIsOwner { get; set; }

        public string Id { get; set; }

        public string Identifier { get; set; }

        public string JumpTargetId { get; set; }

        public string Label { get; set; }

        public string PositionAddress { get; set; }

        public string Source { get; set; }

        public string SourceApplication { get; set; }

        public string Status { get; set; }

        public string Subject { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Type { get; set; }

        #endregion

        #region models
        public Attachment Attachment { get; set; }
        public Party From { get; set; }
        public Coordinate Position { get; set; }

        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; }
        public List<Contact> SharedContacts { get; set; }
        public List<Party> To { get; set; } = new List<Party>();
        #endregion

        //public MessageStatus Status {get;set;}
        
        public override string ToString()
        {
            return SourceApplication;
        }
        
    }
}
