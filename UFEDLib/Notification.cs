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
        public string Body { get; set; }
        public DateTime DateRead { get; set; }
        public string NotificationId { get; set; }
        public string Type { get; set; }
        public string PositionAddress { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserMapping { get; set; }
        public string ServiceIdentifier { get; set; }
        #endregion

        #region models
        public Coordinate Position { get; set; }
        public Party To { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; }
        public List<Party> Participants { get; set; }

        //public List<WebAddress> Urls { get; set; } = new List<WebAddress>();
        #endregion

        #region Parsers
        public static Notification ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Notification result = new Notification();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            ParseFields(fieldElements, result, debugAttributes);
            ParseModelFields(modelFieldElements, result, debugAttributes);
            ParseMultiFields(multiFieldElements, result, debugAttributes);
            ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);

            return result;
        }

        public static List<Notification> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Notification> result = new List<Notification>();

            IEnumerable<XElement> notificationElements = element.Elements(xNamespace + "model").Where(element => element.Attribute("type").Value == "Notification");

            foreach (XElement notificationElement in notificationElements)
            {
                Notification im = ParseModel(notificationElement, debugAttributes);
                result.Add(im);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Notification result, bool debugAttributes = false)
        {
            foreach (XElement field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "NotificationId":
                        result.NotificationId = field.Value.Trim();
                        break;

                    case "Type":
                        result.Type = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Subject":
                        result.Subject = field.Value.Trim();
                        break;

                    case "PositionAddress":
                        result.PositionAddress = field.Value.Trim();
                        break;

                    case "DateRead":
                        if (field.Value.Trim() != "")
                            result.DateRead = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Notification Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Notification result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                switch (modelField.Attribute("name").Value)
                {
                    case "To":
                        result.To = Party.ParseModel(modelField, debugAttributes);
                        break;
                    case "Position":
                        result.Position = Coordinate.ParseModel(modelField, debugAttributes);
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
            IUfedModelParser<Notification>.CheckMultiFields<Notification>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Notification result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Participants":
                        result.Participants = Party.ParseMultiModel(multiModelField);
                        break;
                    case "Attachments":
                        result.Attachments = Attachment.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Notification Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion

    }
}
