using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class SharedFile
    {
        #region fields
        public string Caption { get; set; }
        public Party Owner { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public String Type { get; set; }  
        #endregion

        #region models
        #endregion

        #region multiModels
        public List<InstantMessage> Comments { get; set; }  = new List<InstantMessage>();
        public List<Party> Responders { get; set; }
        #endregion
    }
}
