using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{

    [Serializable]
    public class UserAccount
    {
        public String? Id { get; set; }
        public String? Name { get; set; }

        public String? Username { get; set; }

        public String? Password { get; set; }

        /// <summary>
        /// The app or service from which the account was extracted.
        /// </summary>
        public String? ServiceType { get; set; }

        public String? ServerAddress { get; set; }

        /// <summary>
        /// UserAccount Photos
        /// </summary>
        public List<ContactPhoto> Photos { get; set; }

        /// <summary>
        /// UserAccount entries collection.
        /// </summary>
        public List<ContactEntry> Entries { get; set; }

        public List<String> Notes { get; set; }

        /// <summary>
        /// Addresses collection.
        /// </summary>
        public List<StreetAddress> Addresses { get; set; } = new List<StreetAddress>();

        /// <summary>
        /// Organizations collection.
        /// </summary>
        public List<Organization> Organizations { get; set; } = new List<Organization>();

        public DateTime TimeCreated { get; set; }

        public String? Source { get; set; }
    }
}
