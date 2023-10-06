using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class PoweringEvent
    {
        public PowerElementType Element { get; set; }

        public enum PowerElementType { Device }

        public DateTime TimeStamp { get; set; }

        public EventEnum Event { get; set; }

        public enum EventEnum  { On, Off, Reset }
    }
}
