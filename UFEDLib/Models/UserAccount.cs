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
        #region fields
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ServerAddress { get; set; }

        /// <summary>
        /// The app or service from which the account was extracted.
        /// </summary>
        public string ServiceType { get; set; }

        public string Source { get; set; }

        public DateTime TimeCreated { get; set; }

        public string Username { get; set; }

        #endregion

        #region multiFields
        public List<String> Notes { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        /// <summary>
        /// Addresses collection.
        /// </summary>
        public List<StreetAddress> Addresses { get; set; } = new List<StreetAddress>();

        /// <summary>
        /// UserAccount entries collection.
        /// </summary>
        public List<ContactEntry> Entries { get; set; } = new List<ContactEntry>();

        /// <summary>
        /// Organizations collection.
        /// </summary>
        public List<Organization> Organizations { get; set; } = new List<Organization>();

        /// <summary>
        /// UserAccount Photos
        /// </summary>
        public List<ContactPhoto> Photos { get; set; } = new List<ContactPhoto>();
        #endregion
    }
}
