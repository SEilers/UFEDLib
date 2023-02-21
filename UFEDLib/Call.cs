using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class Call
    {
        public Call()
        {
            Parties = new List<Party>();
        }

        public String? Source { get; set; }

        public String? Direction { get; set; } // Outgoing

        public bool VideoCall { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime TimeStamp { get; set; }

        public List<Party> Parties { get; set; }

        public override string ToString()
        {
            return Source + " " + Duration + " " + Parties.Count.ToString();
        }

    }
}
