using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class WirelessNetwork
    {
        #region fields
        public string BSSId { get; set; }
        public DateTime LastAutoConnection { get; set; }
        public DateTime LastConnection { get; set; }
        public string SecurityMode { get; set; }
        public string SSId { get; set; }
        #endregion
    }
}
