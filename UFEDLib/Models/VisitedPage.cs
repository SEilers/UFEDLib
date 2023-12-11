using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class VisitedPage
    {
        public string Title { get; set; }   

        public string Url { get; set; }

        public string LastVisited {  get; set; }

        public int VisitCount { get; set; }

        public string Source { get; set; }
    }
}
