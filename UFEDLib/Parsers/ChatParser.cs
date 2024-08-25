using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using UFEDLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UFEDLib.Parsers
{
    public class ChatParser
    {
        public static List<Chat> ParseChats(XElement chatsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Chat> result = new List<Chat>();

            IEnumerable<XElement> chatElements = chatsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Chat");

            foreach (var chatElement in chatElements)
            {
                try
                {
                    result.Add(Parse(chatElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing chat: " + ex.Message);
                }
            }

            return result;
        }
        public static Chat Parse(XElement chatNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Chat result = new Chat();

            result.ParseAttributes(chatNode);

            var fieldElements = chatNode.Elements(xNamespace + "field");
            var multiFieldElements = chatNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = chatNode.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Description":
                        result.Description = field.Value.Trim();
                        break;

                    case "Id":
                        result.Id = field.Value.Trim();
                        break;

                    case "LastActivity":
                        if (field.Value.Trim() != "")
                            result.LastActivity = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "StartTime":
                        if (field.Value.Trim() != "")
                            result.StartTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("ChatParser: Unknown field: " + field.Attribute("name").Value);
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
                    case "ActivityLog":
                        //TODO: Parse ActivityLog
                        //result.ActivityLog = ActivityLogParser.ParseActivityLogs(multiModelField, debugAttributes);
                        break;

                    case "Messages":
                        result.Messages = InstantMessageParser.ParseMessages(multiModelField, debugAttributes);
                        break;

                    case "Participants":
                        result.Participants = PartyParser.ParseParties(multiModelField, debugAttributes);
                        break;

                    case "Photos":
                        result.Photos = ContactPhotoParser.ParseContactPhotos(multiModelField, debugAttributes);
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
