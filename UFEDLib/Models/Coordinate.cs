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
        #region fields
        public string Comment { get; set; }
        public double Elevation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        /// <summary>
        /// Free text map to which the coordinate relates.
        /// </summary>
        public string Map { get; set; }
        public string PositionAddress { get; set; }
        #endregion
    }
}
