using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class DictrionaryWord : ModelBase
    {
        #region fields
        public string Frequency { get; set; }
        public string Locale { get; set; }
        public string Source { get; set; }
        public string Word { get; set; }
        #endregion
    }
}
