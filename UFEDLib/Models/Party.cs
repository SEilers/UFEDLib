using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class Party
    {
        public string Id { get; set; }
        public string Role { get; set; }

        public string Identifier { get; set; }
        public string Name { get; set; }

        // public PartyStatus Status {get; set; }

        public DateTime DateDellivered { get; set; }

        public DateTime DateRead { get; set; }

        public DateTime DatePlayed { get; set; }

        public string IPAddress { get; set; }

        public bool IsPhoneOwner { get; set; }

        public override string ToString()
        {
            return Name + " " + Identifier + " " + Role;
        }

    }
}

