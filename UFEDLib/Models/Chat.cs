using System;
using System.Collections.Generic;
using System.IO;

namespace UFEDLib.Models
{
    [Serializable]
    public class Chat : ModelBase
    {
        #region fields
        public string Account { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public DateTime LastActivity { get; set; }
        public string Name { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public DateTime StartTime { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        
        public List<InstantMessage> Messages { get; set; } = new List<InstantMessage>();
        public List<Party> Participants { get; set; } = new List<Party>();
        public List<ContactPhoto> Photos { get; set; } = new List<ContactPhoto>();
        #endregion
    }
}