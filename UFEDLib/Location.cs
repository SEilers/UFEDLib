using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{
    [Serializable]
    public class Location : ModelBase
    {
        #region fields
        public string Category { get; set; }

        public string Confidence { get; set; }

        public string Description { get; set; }

        public string Map { get; set; }

        public string Name { get; set; }
        public string LocationOrigin { get; set; }
        public string Origin { get; set; }
        public string PositionAddress { get; set; }
        public string Precision { get; set; }

        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }

        public string UserMapping { get; set; }
        #endregion

        #region models
        public StreetAddress Address { get; set; }
        public Coordinate Position { get; set; }
        #endregion

        #region multiModels
        #endregion






















    }
}
