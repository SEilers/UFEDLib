using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class BluetoothDevice
    {
        public String Name { get; set; }

        public String MACAddress { get; set; }

        public String Info { get; set; }

        public DateTime LastConnected { get; set; }
    }
}
