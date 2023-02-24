using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Coordinate
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Elevation { get; set; }

        /// <summary>
        /// Free text map to which the coordinate relates.
        /// </summary>
        public String Map { get; set; }

        public String Comment { get; set; }

        public String PositionAddress { get; set; }


    }
}
