using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{

    [Serializable]
    public class UserAccount
    {
        public String? Id { get; set; }
        public String? Source { get; set; }
        public String? Name { get; set; }
        public String? Username { get; set; }
        public String? ServiceType { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
