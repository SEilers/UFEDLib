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
        public string Id { get; set; }
        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// The app or service from which the account was extracted.
        /// </summary>
        public string ServiceType { get; set; }

        public string ServerAddress { get; set; }

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

        public string Source { get; set; }
    }
}
