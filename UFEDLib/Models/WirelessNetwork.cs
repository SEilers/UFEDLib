using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class WirelessNetwork
    {
        public string BSSId { get; set; }

        public string SSId { get; set; }

        public string SecurityMode { get; set; }

        public DateTime LastConnection {  get; set; }

        public DateTime LastAutoConnection { get; set; }
    }
}
