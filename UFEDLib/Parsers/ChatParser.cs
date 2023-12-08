using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class ChatParser
    {
        public static Chat Parse(XElement chatNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Chat result = new Chat();

            var fieldElements = chatNode.Elements(xNamespace + "field");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    default:
                        break;
                }
            }

            var multiModelFieldElements = chatNode.Elements(xNamespace + "multiModelField");

            foreach (var multiField in multiModelFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "Messages":
                        result.Messages = ParseMessages(multiField);
                        break;

                    case "Participants":
                        result.Participants = ParseParticipants(multiField);
                        break;

                    default:
                        break;
                }
            }

            return result;
        }

        static List<Party> ParseParticipants(XElement participantsElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Party> result = new List<Party>();

            IEnumerable<XElement> parties = participantsElement.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "Party");

            foreach (XElement party in parties)
            {
                Party p = PartyParser.Parse(party);
                result.Add(p);
            }

            return result;
        }

        static List<InstantMessage> ParseMessages(XElement messagesElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<InstantMessage> result = new List<InstantMessage>();

            IEnumerable<XElement> instantMessages = messagesElement.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "InstantMessage");

            foreach (XElement message in instantMessages)
            {
                InstantMessage im = InstantMessageParser.Parse(message);
                result.Add(im);
            }

            return result;
        }
    }
}
