using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class Location
    {
        public Coordinate Position { get; set; }

        public StreetAddress Address { get; set; }

        public DateTime TimeStamp { get; set; } 

        public String Name { get; set; }    

        public String Description { get; set; } 

        public String Type { get; set; }    

        public String Precision { get; set; }

        public String Map { get; set; }

        public String Category { get; set; }    

        public String Confidence { get; set; }

        public String Origin { get; set; }

        //public enum LocationOrigin { Unknown, Device, External}

        public String PositionAddress { get; set; } 

    }
}
