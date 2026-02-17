using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Notification : ModelBase, IUfedModelParser<Notification>
    {
        public static string GetXmlModelType()
        {
            return "Notification";
        }

        #region fields
        public string Body { get; set; } = "";
        public DateTime DateRead { get; set; } = DateTime.MinValue;
        public string NotificationId { get; set; } = "";
        public string PositionAddress { get; set; } = "";
        public string Status { get; set; } = "";
        public string Subject { get; set; } = "";
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
        public string Type { get; set; } = "";
        #endregion

        #region models
        public Coordinate? Position { get; set; } = null;
        public Party? To { get; set; } = null;
        #endregion

        #region multiFields
        public List<string> Notes { get; set; } = new List<string>();
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public List<Party> Participants { get; set; } = new List<Party>();

        //public List<WebAddress> Urls { get; set; } = new List<WebAddress>();
        #endregion

        #region Parsers
        public static Notification ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Notification>(element, debugAttributes);
        }

        public static List<Notification> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Notification>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Notification result, bool debugAttributes = false)
        {
            foreach (XElement field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "DateRead":
                        if (field.Value.Trim() != "")
                            result.DateRead = DateTime.Parse(field.Value.Trim());
                        break;

                    case "NotificationId":
                        result.NotificationId = field.Value.Trim();
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "Subject":
                        result.Subject = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
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
                            Logger.LogAttribute("Notification Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Notification result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                XNamespace ns = modelField.Name.Namespace;
                XElement modelElement = modelField.Element(ns + "model");

                switch (modelField.Attribute("name").Value)
                {
                    case "Position":
                        result.Position = Coordinate.ParseModel(modelElement, debugAttributes);
                        break;

                    case "To":
                        result.To = Party.ParseModel(modelElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Notification Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Notification result, bool debugAttributes = false)
        {
            foreach (var multiField in multiFieldElements)
            {
                var multiFieldName = ModelBase.GetAttributeValueOrLog(multiField, "name", debugAttributes);
                if (multiFieldName == null) continue;

                switch (multiFieldName)
                {
                    case "Notes":
                        result.Notes = multiField.Elements().Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Notification Parser: Unknown multiField: " + multiFieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Notification result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                var multiModelFieldName = ModelBase.GetAttributeValueOrLog(multiModelField, "name", debugAttributes);
                if (multiModelFieldName == null) continue;

                switch (multiModelFieldName)
                {
                    case "Attachments":
                        result.Attachments = Attachment.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Participants":
                        result.Participants = Party.ParseMultiModel(multiModelField);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Notification Parser: Unknown multiModelField: " + multiModelFieldName);
                        }
                        break;
                }
            }
        }
        #endregion

    }
}
