using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class BluetoothDevice
    {
        #region fields
        public string Info { get; set; }

        public DateTime LastConnected { get; set; }

        public string MACAddress { get; set; }

        public string Name { get; set; }
        #endregion
    }
}
