using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Map
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
