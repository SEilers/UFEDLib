using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class Contact
    {
        public String Id { get; set; }
        public string Name { get; set; }
        public String Source { get; set; }
        public String Group { get; set; }
        public String Account { get; set; }
    }
}
