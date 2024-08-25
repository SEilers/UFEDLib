using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class CarvedString : ModelBase
    {
        #region fields
        public string MetaData { get; set; }
        public string Source { get; set; }
        public string Value { get; set; }
        #endregion
    }
}
