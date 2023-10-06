using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class SearchedItem
    {
        public DateTime TimeStamp { get; set; }

        public String Value { get; set; }   

        public String Source { get; set; }  

        public String SearchResults { get; set; }   

        public Coordinate Posítion { get; set; }

        public String PositionAddress { get; set; } 
    }
}
