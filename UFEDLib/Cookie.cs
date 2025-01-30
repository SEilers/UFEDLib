using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{
    [Serializable]
    public class Cookie : ModelBase
    {
        #region fields
        public DateTime CreationTime { get; set; }
        public string Domain { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime LastAccessTime { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Value { get; set; }
        #endregion
    }
}
