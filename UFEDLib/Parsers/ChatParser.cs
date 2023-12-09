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
                        result.Messages = InstantMessageParser.ParseMessages(multiField);
                        break;

                    case "Participants":
                        result.Participants = PartyParser.ParseParties(multiField);
                        break;

                    default:
                        break;
                }
            }

            return result;
        }

    }
}
