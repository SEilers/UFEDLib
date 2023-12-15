using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class VoiceMail
    {
        #region fields
        public TimeSpan Duration { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region models
        public Party From { get; set; }
        #endregion
    }
}
