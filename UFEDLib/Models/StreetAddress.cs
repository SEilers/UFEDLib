using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    public class StreetAddress
    {
        #region fields
        /// <summary>
        /// Same values as ContactEntry categories
        /// </summary>
        public string Category { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string HouseNumber { get; set; }

        public string Neighborhood { get; set; }

        public string POBox { get; set; }

        /// <summary>
        /// Address Postal Code or ZIP.
        /// </summary>
        public string PostalCode { get; set; }

        public string State { get; set; }

        /// <summary>
        /// Street information.
        /// </summary>
        public string Street1 { get; set; }

        /// <summary>
        /// Additional street information
        /// </summary>
        public string Street2 { get; set; }

        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion
    }
}
