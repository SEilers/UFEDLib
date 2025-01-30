using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{
    [Serializable]
    public class Map : ModelBase
    {
        #region fields
        public string Source { get; set; }
        public int ZoomLevel { get; set; }
        #endregion

        #region multiFields
        public List<string> Tiles { get; set; } = new List<string>();
        #endregion
    }
}
