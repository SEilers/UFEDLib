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
        public string Title { get; set; }

        public string Body { get; set; }

        public string Summary { get; set; }

        public DateTime Creation { get; set; }

        public DateTime Modification { get; set; }

        public string Source { get; set; }

        public Coordinate Position { get; set; }

        public StreetAddress Address { get; set; }

        public List<Attachment> Attachments { get; set; }

        public string Folder { get; set; }

        public string PositionAddress { get; set; }
    }
}
