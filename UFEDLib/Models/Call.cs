using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Call
    {
        public string Id { get; set; }

        //public CallType Type { get; set; }

        public string Type { get; set; }

        public List<Party> Parties { get; set; } = new List<Party>();

        public DateTime TimeStamp { get; set; }

        public TimeSpan Duration { get; set; }

        public string Source { get; set; }

        public string NetworkName { get; set; }

        public string NetworkCode { get; set; }

        public string Direction { get; set; }

        public string Status { get; set; }

        public bool VideoCall { get; set; }

        public string CountryCode { get; set; }

        public override string ToString()
        {
            return Source + " " + Duration + " " + Parties.Count.ToString();
        }

    }
}
