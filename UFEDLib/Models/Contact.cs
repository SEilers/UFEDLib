using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Contact
    {
        public string Id { get; set; }

        /// <summary>
        /// Contact Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Contact Photos.
        /// </summary>
        public List<ContactPhoto> Photos { get; set; } = new List<ContactPhoto>();

        /// <summary>
        /// Contact entries collection.
        /// </summary>
        public List<ContactEntry> Entries { get; set; } = new List<ContactEntry>();

        /// <summary>
        /// Addresses collection.
        /// </summary>
        public List<StreetAddress> Addresses { get; set; } = new List<StreetAddress>();

        /// <summary>
        /// Organizations collection.
        /// </summary>
        public List<Organization> Organizations { get; set; } = new List<Organization>();

        /// <summary>
        /// Contact Notes.
        /// </summary>
        public List<String> Notes { get; set; } = new List<String>();

        /// <summary>
        /// Contact Source.
        /// </summary>
        public string Source { get; set; }
        public string Group { get; set; }
        public string Account { get; set; }

        public DateTime TimeContacted { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTime TimeModified { get; set; }

        public int TimesContacted { get; set; }
    }
}
