using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Note
    {
        public String Title { get; set; }

        public String Body { get; set; }

        public String Summary { get; set; }

        public DateTime Creation { get; set; }

        public DateTime Modification { get; set; }

        public String Source { get; set; }

        public Coordinate Position { get; set; }

        public StreetAddress Address { get; set; }

        public List<Attachment> Attachments { get; set; }

        public String Folder { get; set; }

        public String PositionAddress { get; set; }
    }
}
