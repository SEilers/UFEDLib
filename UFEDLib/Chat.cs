using System.IO;

namespace UFEDLib
{
    public class Chat
    {
        public String Id { get; set; }

        public String Source { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime LastActivity { get; set; }
        public List<Party> Participants { get; set; } = new List<Party>();

        public List<InstantMessage> InstantMessages { get; set; } = new List<InstantMessage>();

        public String Account { get; set; }
    }
}