using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{
    [Serializable]
    public class WebBookmark : ModelBase
    {
        #region fields
        public DateTime LastVisted { get; set; }
        public string Path { get; set; }
        public string PositionAddress { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int VisitCount { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        #endregion
    }
}
