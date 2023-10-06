using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class Cookie
    {
        public String Name { get; set; }

        public String Value { get; set; }

        public String Domain { get; set; }

        public String Path { get; set; }

        public DateTime Expiry { get; set; }

        public DateTime CreationTime { get; set; }  

        public DateTime LastAccessTime { get; set; }   
    }
}
