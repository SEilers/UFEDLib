using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class InstantMessageParser
    {
        public static List<InstantMessage> ParseMessages(XElement messagesElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<InstantMessage> result = new List<InstantMessage>();

            //IEnumerable<XElement> instantMessages = messagesElement.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "InstantMessage");
            IEnumerable<XElement> instantMessages = messagesElement.Elements(xNamespace + "model").Where(element => element.Attribute("type").Value == "InstantMessage");

            foreach (XElement message in instantMessages)
            {
                InstantMessage im = InstantMessageParser.Parse(message);
                result.Add(im);
            }

            return result;
        }

        public static InstantMessage Parse(XElement xElement)
        {

            InstantMessage result = new InstantMessage();
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            var fieldElements = xElement.Elements(xNamespace + "field");

            try
            {
                foreach (XElement field in fieldElements)
                {
                    switch (field.Attribute("name").Value)
                    {
                        case "Body":
                            result.Body = field.Value.Trim();
                            break;
                        case "Id":
                            result.Id = field.Value.Trim();
                            break;
                        case "SourceApplication":
                            result.SourceApplication = field.Value.Trim();
                            break;
                        case "TimeStamp":
                            if (field.Value.Trim() != "")
                                result.TimeStamp = DateTime.Parse(field.Value.Trim());
                            break;
                        case "Subject":
                            result.Subject = field.Value.Trim();
                            break;
                        case "DateRead":
                            if (field.Value.Trim() != "")
                                result.DateRead = DateTime.Parse(field.Value.Trim());
                            break;
                        case "DateDelivered":
                            if (field.Value.Trim() != "")
                                result.DateDelivered = DateTime.Parse(field.Value.Trim());
                            break;
                        case "Label":
                            result.Label = field.Value.Trim();
                            break;
                        case "PositionAddress":
                            result.PositionAddress = field.Value.Trim();
                            break;
                        case "ChatId":
                            result.ChatId = field.Value.Trim();
                            break;
                        case "Erased":
                            result.Erased = field.Value.Trim();
                            break;
                        case "Source":
                            result.Source = field.Value.Trim();
                            break;
                        case "FromIsOwner":
                            result.FromIsOwner = field.Value.Trim();
                            break;
                        case "Identifier":
                            result.Identifier = field.Value.Trim();
                            break;
                        case "Status":
                            result.Status = field.Value.Trim();
                            break;
                        case "Type":
                            result.Type = field.Value.Trim();
                            break;
                        default:
                            //Trace.WriteLine("Unknown field: " + field.Attribute("name").Value);
                            break;
                    }
                }

                var modelFieldElements = xElement.Elements(xNamespace + "modelField");

                foreach (var modelField in modelFieldElements)
                {
                    switch (modelField.Attribute("name").Value)
                    {
                        case "Attachment":
                            result.Attachment = AttachmentParser.Parse(modelField);
                            break;
                        case "From":
                            result.From = PartyParser.Parse(modelField);
                            break;
                        case "Position":
                            result.Position = CoordinateParser.Parse(modelField);
                            break;
                        case "JumpTargetId":
                            result.JumpTargetId = modelField.Value.Trim();
                            break;
                        default:
                            break;
                    }
                }

                var multiModelFieldElements = xElement.Elements(xNamespace + "multiModelField");

                foreach (var multiField in multiModelFieldElements)
                {
                    switch (multiField.Attribute("name").Value)
                    {
                        case "To":
                            result.To = PartyParser.ParseParties(multiField);
                            break;
                        case "Attachments":
                            result.Attachments = AttachmentParser.ParseAttachments(multiField);
                            break;
                        case "SharedContacts":
                            result.SharedContacts = ContactParser.ParseContacts(multiField);
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
