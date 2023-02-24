using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Organization
    {
        /// <summary>
        /// Oragnization name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Contact’s position in the organization
        /// </summary>
        public String Position { get; set; }
    }
}
