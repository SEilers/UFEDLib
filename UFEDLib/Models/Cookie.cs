using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class Cookie
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Domain { get; set; }

        public string Path { get; set; }

        public DateTime Expiry { get; set; }

        public DateTime CreationTime { get; set; }  

        public DateTime LastAccessTime { get; set; }   
    }
}
