using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Party : ModelBase
    {
        #region fields
    
        public DateTime DateDellivered { get; set; }
        public DateTime DatePlayed { get; set; }
        public DateTime DateRead { get; set; }
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string IPAddress { get; set; }
        public string IsGroupAdmin { get; set; }
        public bool IsPhoneOwner { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        // public PartyStatus Status {get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

        public override string ToString()
        {
            return Name + " " + Identifier + " " + Role;
        }

    }
}

