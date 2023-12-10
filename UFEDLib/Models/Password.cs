using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class Password
    {
        public String AccessGroup { get; set; }

        public String Account { get; set; } 

        // The password itself
        public String Data { get; set; }

        public String GenericAttribute { get; set; }

        public String Label { get; set; }

        public String Server {  get; set; }

        public String Service { get; set; }

        // enum with the following values: "Default", "Key", "Secret", "Token"
        public String Type { get; set; }

    }
}
