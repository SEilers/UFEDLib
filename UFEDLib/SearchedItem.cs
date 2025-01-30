using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{
    [Serializable]
    public class SearchedItem : ModelBase
    {
        #region fields
        public string PositionAddress { get; set; }
        public string SearchResults { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Value { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        #endregion

        #region multiModels
        #endregion
    }
}
