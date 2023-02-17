using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class InstantMessage
    {
        public String Id { get; set; }

        public String ChatId { get; set; }

        public String Erased { get; set; }

        public String Source { get; set; }

        public String Body { get; set; }

        public Party From { get; set; }

        public String FromIsOwner { get; set; }

        public List<Party> To { get; set; } = new List<Party>();

        public String Identifier { get; set; }

        public DateTime TimeStamp { get; set; }

        public String Status { get; set; }

        public String Type { get; set; }

        public String SourceApplication { get; set; }
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
