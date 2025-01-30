using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{
    [Serializable]
    public class SharedFile : ModelBase
    {
        #region fields
        public string Caption { get; set; }
        public Party Owner { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        public List<InstantMessage> Comments { get; set; } = new List<InstantMessage>();
        public List<Party> Responders { get; set; }
        #endregion
    }
}
