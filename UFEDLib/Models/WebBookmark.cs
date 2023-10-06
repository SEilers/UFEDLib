using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class WebBookmark
    {
        public String Title { get; set; }

        public String Url { get; set; }

        public DateTime LastVisted { get; set; }

        public String Source { get; set; }

        public int VisitCount { get; set; }

        public DateTime TimeStamp { get; set; }

        public String Path { get; set; }

        public Coordinate Position { get; set; }

        public String PositionAddress { get; set; }
    }
}
