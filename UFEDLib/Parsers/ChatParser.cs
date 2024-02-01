using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using UFEDLib.Models;
using System;

namespace UFEDLib.Parsers
{
    public class ChatParser
    {
        public static Chat Parse(XElement chatNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Chat result = new Chat();

            var fieldElements = chatNode.Elements(xNamespace + "field");
            var multiFieldElements = chatNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = chatNode.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastActivity":
                        if (field.Value.Trim() != "")
                            result.LastActivity = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("ChatParser: Unknown attribute: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach(var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("ChatParser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }
            

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Messages":
                        result.Messages = InstantMessageParser.ParseMessages(multiModelField, debugAttributes);
                        break;

                    case "Participants":
                        result.Participants = PartyParser.ParseParties(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("ChatParser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

    }
}
