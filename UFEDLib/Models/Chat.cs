using System.IO;

namespace UFEDLib.Models
{
    [Serializable]
    public class Chat
    {
        public List<InstantMessage> Messages { get; set; } = new List<InstantMessage>();
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime LastActivity { get; set; }

        public List<Party> Participants { get; set; } = new List<Party>();

        public string Source { get; set; }

        public string Account { get; set; }
    }
}