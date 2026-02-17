using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class SharedFile : ModelBase
    {
        #region fields
        public string Caption { get; set; } = "";
        public Party Owner { get; set; } = new Party();
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public string Type { get; set; } = "";
        #endregion

        #region models
        #endregion

        #region multiModels
        public List<InstantMessage> Comments { get; set; } = new List<InstantMessage>();
        public List<Party> Responders { get; set; } = new List<Party>();
        #endregion
    }
}
