using System.IO;

namespace UFEDLib.Models
{
    [Serializable]
    public class Chat
    {
        #region fields
        public string Account { get; set; }
        public string Id { get; set; }
        public DateTime LastActivity { get; set; }
        public string Source { get; set; }
        public DateTime StartTime { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        public List<InstantMessage> Messages { get; set; } = new List<InstantMessage>();
        public List<Party> Participants { get; set; } = new List<Party>();
        #endregion
    }
}