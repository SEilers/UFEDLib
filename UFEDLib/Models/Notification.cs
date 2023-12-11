using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Notification
    {
        public List<Party> Participants { get; set; }

        public Party To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }    

        public string Source { get; set; }

        public DateTime TimeStamp { get; set; }

        public DateTime DateRead { get; set; }

        public List<Attachment> Attachments { get; set; }

        public Coordinate Position { get; set; }

        // public MessageStatus Status {get;set}

        public string PositionAddress { get; set; }

        //public List<WebAddress> Urls { get; set; } = new List<WebAddress>();

        //public NotificationType Type {get;set}

        public string NotificationId { get; set; }
    }
}
