using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class PoweringEvent : ModelBase
    {
        #region fields
        public String Element { get; set; }
        public String Event { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion
    }
}
