using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class BluetoothDevice
    {
        public string Name { get; set; }

        public string MACAddress { get; set; }

        public string Info { get; set; }

        public DateTime LastConnected { get; set; }
    }
}
