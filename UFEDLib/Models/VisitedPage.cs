using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class VisitedPage
    {
        public String Title { get; set; }   

        public String Url { get; set; }

        public String LastVisited {  get; set; }

        public int VisitCount { get; set; }

        public String Source { get; set; }
    }
}
