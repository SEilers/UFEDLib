using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Attachment : ModelBase
    {
        #region fields

        public string AttachmentExtractedPath { get; set; }
        public string Charset { get; set; }

        public string ContentType { get; set; }

        //public DataField Data { get; set;}

        public string Filename { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// A URL string associated with the attachment.
        /// </summary>
        public string URL { get; set; }
        #endregion





    }
}
