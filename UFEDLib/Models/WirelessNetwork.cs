using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class WirelessNetwork
    {
        public String BSSId { get; set; }

        public String SSId { get; set; }

        public String SecurityMode { get; set; }

        public DateTime LastConnection {  get; set; }

        public DateTime LastAutoConnection { get; set; }
    }
}
