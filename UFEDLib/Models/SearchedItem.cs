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

        public string Value { get; set; }   

        public string Source { get; set; }  

        public string SearchResults { get; set; }   

        public Coordinate Position { get; set; }

        public string PositionAddress { get; set; } 
    }
}
