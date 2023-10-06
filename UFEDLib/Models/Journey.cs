using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class Journey
    {
        public String Name { get; set; }

        /// <summary>
        /// Journey locations
        /// </summary>

        public List<Location> WayPoints { get; set; }

        public String Source { get; set; }  

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }   

        public Location FromPoint { get; set; }

        public Location ToPoint { get; set; }   
    }
}
