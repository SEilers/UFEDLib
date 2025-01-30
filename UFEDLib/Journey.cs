using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{
    [Serializable]
    public class Journey : ModelBase
    {

        #region fields
        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public DateTime StartTime { get; set; }
        #endregion

        #region models
        public Location FromPoint { get; set; }

        public Location ToPoint { get; set; }
        #endregion

        #region multiModels
        /// <summary>
        /// Journey locations
        /// </summary>
        public List<Location> WayPoints { get; set; } = new List<Location>();
        #endregion













    }
}
