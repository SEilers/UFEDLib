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

        /// <summary>
        /// Entry value (phone number or email string).
        /// </summary>
        public String Value { get; set; }

        /// <summary>
        /// Entry category (work, home etc).
        /// </summary>
        public String Category { get; set; }

        /// <summary>
        /// Entry domain (phone number, email, web address etc)
        /// </summary>
        public String Domain { get; set; }
    }
}
