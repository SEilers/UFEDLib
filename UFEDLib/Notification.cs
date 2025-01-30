using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class Notification : ModelBase
    {
        #region fields
        public string Body { get; set; }
        public DateTime DateRead { get; set; }
        public string NotificationId { get; set; }
        //public NotificationType Type {get;set}
        public string PositionAddress { get; set; }
        public string Source { get; set; }
        // public MessageStatus Status {get;set}
        public string Subject { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        public Party To { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; }
        public List<Party> Participants { get; set; }
        //public List<WebAddress> Urls { get; set; } = new List<WebAddress>();
        #endregion

    }
}
