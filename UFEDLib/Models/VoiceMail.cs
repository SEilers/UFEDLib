using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class VoiceMail
    {
        public Party From { get; set; }

        public string Name { get; set; }

        public DateTime TimeStamp { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
