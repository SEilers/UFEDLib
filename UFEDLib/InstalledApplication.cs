using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class InstalledApplication : ModelBase
    {
        #region fields
        public string AppGUID { get; set; }
        public string Copyright { get; set; }
        public DateTime DeletedDate { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Version { get; set; }
        #endregion
    }
}
