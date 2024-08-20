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
    public class InstantMessageParser
    {
        public static List<InstantMessage> ParseMessages(XElement messagesElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<InstantMessage> result = new List<InstantMessage>();

            IEnumerable<XElement> instantMessages = messagesElement.Elements(xNamespace + "model").Where(element => element.Attribute("type").Value == "InstantMessage");

            foreach (XElement message in instantMessages)
            {
                InstantMessage im = InstantMessageParser.Parse(message, debugAttributes);
                result.Add(im);
            }

            return result;
        }

        public static InstantMessage Parse(XElement xElement, bool debugAttributes = false)
        {

            InstantMessage result = new InstantMessage();
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            var fieldElements = xElement.Elements(xNamespace + "field");
            var multiFieldElements = xElement.Elements(xNamespace + "multiField");
            var multiModelFieldElements = xElement.Elements(xNamespace + "multiModelField");

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
                        case "Folder":
                            result.Folder = field.Value.Trim();
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
                        case "DeletionReason":
                            result.DeletionReason = field.Value.Trim();
                            break;
                        case "DateDeleted":
                            if (field.Value.Trim() != "")
                                result.DateDeleted = DateTime.Parse(field.Value.Trim());
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
                        case "Platform":
                            result.Platform = field.Value.Trim();
                            break;
                        case "PositionAddress":
                            result.PositionAddress = field.Value.Trim();
                            break;
                        case "ChatId":
                            result.ChatId = field.Value.Trim();
                            break;
                        case "IsLocationSharing":
                            result.IsLocationSharing = field.Value.Trim();
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
                        case "ServiceIdentifier":
                            result.ServiceIdentifier = field.Value.Trim();
                            break;
                        case "Status":
                            result.Status = field.Value.Trim();
                            break;
                        case "Type":
                            result.Type = field.Value.Trim();
                            break;
                        case "UserMapping":
                            result.UserMapping = field.Value.Trim();
                            break;
                        default:
                            if (debugAttributes)
                            {
                                Console.WriteLine("InstantMessageParser: Unknown field: " + field.Attribute("name").Value);
                            }
                            break;
                    }
                }
                
                foreach (var multiField in multiFieldElements)
                {
                    switch (multiField.Attribute("name").Value)
                    {
                        case "Attachment":
                            result.Attachment = AttachmentParser.Parse(multiField, debugAttributes);
                            break;
                        case "From":
                            result.From = PartyParser.Parse(multiField, debugAttributes);
                            break;
                        case "Position":
                            result.Position = CoordinateParser.Parse(multiField, debugAttributes);
                            break;
                        case "JumpTargetId":
                            result.JumpTargetId = multiField.Value.Trim();
                            break;
                        default:
                            if (debugAttributes)
                            {
                                Console.WriteLine("InstantMessageParser: Unknown multiField: " + multiField.Attribute("name").Value);
                            }
                            break;
                    }
                }
               
                foreach (var multiModelField in multiModelFieldElements)
                {
                    switch (multiModelField.Attribute("name").Value)
                    {
                        case "To":
                            result.To = PartyParser.ParseParties(multiModelField);
                            break;
                        case "Attachments":
                            result.Attachments = AttachmentParser.ParseAttachments(multiModelField, debugAttributes);
                            break;
                        case "SharedContacts":
                            result.SharedContacts = ContactParser.ParseContacts(multiModelField, debugAttributes);
                            break;
                        case "MessageExtraData":
                            // TODO: Implement MessageExtraDataParser
                            //result.MessageExtraData = MessageExtraDataParser.Parse(multiModelField, debugAttributes);
                            break;

                        default:
                            if (debugAttributes)
                            {
                                Console.WriteLine("InstantMessageParser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                            }
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
