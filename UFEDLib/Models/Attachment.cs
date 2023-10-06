using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Attachment
    {
        public String Filename { get; set; }

        public String ContentType { get; set; }

        public String Charset { get; set; }

        //public DataField Data { get; set;}

        /// <summary>
        /// A URL string associated with the attachment.
        /// </summary>
        public String URL { get; set; }
    }
}
