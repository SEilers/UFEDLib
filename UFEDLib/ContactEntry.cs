using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class ContactEntry
    {
        #region fields
        /// <summary>
        /// Entry category (work, home etc).
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Entry domain (phone number, email, web address etc)
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Entry value (phone number or email string).
        /// </summary>
        public string Value { get; set; }
        #endregion






    }
}
