using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class InstalledApplication
    {
        public String Name { get; set; }

        public String Version { get; set; }

        public String Description { get; set; }

        public String Identifier { get; set; }

        public DateTime PurchaseDate { get; set; }

        public String Copyright { get; set; }

        public DateTime DeletedDate { get; set; }

        public String AppGUID { get; set; }


    }
}
