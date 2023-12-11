using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class SharedFile
    {
        public string Caption { get; set; } 

        public string Source { get; set; }  

        //public FileType Type { get; set; }  

        public DateTime TimeStamp { get; set; }

        public Party Owner { get; set; }    

        public List<Party> Responders { get; set; }

        public List<InstantMessage> Comments { get; set; }
    }
}
