using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class Note : ModelBase
    {
        #region fields
        public string Body { get; set; }
        public DateTime Creation { get; set; }
        public string Folder { get; set; }
        public DateTime Modification { get; set; }
        public string PositionAddress { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }

        public StreetAddress Address { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; }
        #endregion
    }
}
