using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class WebBookmark
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime LastVisted { get; set; }

        public string Source { get; set; }

        public int VisitCount { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Path { get; set; }

        public Coordinate Position { get; set; }

        public string PositionAddress { get; set; }
    }
}
