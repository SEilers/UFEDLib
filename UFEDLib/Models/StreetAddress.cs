using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    public class StreetAddress
    {
        /// <summary>
        /// Street information.
        /// </summary>
        public String Street1 { get; set; }

        /// <summary>
        /// Additional street information
        /// </summary>
        public String Street2 { get; set; }

        public String HouseNumber { get; set;}

        public String City { get; set;}

        public String State { get; set;}

        public String Country { get; set;}

        /// <summary>
        /// Address Postal Code or ZIP.
        /// </summary>
        public String PostalCode { get; set;}

        public String POBox { get; set;}

        public string Neighborhood { get; set;}

        /// <summary>
        /// Same values as ContactEntry categories
        /// </summary>
        public String Category { get; set;}

    }
}
