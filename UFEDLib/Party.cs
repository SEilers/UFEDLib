using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    public class Party
    {
        public string Name { get; set; }

        public String Identifier { get; set; }

        public String Role { get; set; }

        public bool IsPhoneOwner { get; set; }

        public override string ToString()
        {
            return Name + " " + Identifier + " " + Role;
        }

    }
}
