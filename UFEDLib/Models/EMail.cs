using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Email : ModelBase, IUfedModelParser<Email>
    {
        public static string GetXmlModelType()
        {
            return "Email";
        }

        #region fields
        public string Account { get; set; }
        public string Body { get; set; }
        public string EmailHeader { get; set; }
        public string Folder { get; set; }
        public string Priority { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Snippet { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region models
        public Party From { get; set; }
        #endregion

        #region multiFields
        public List<string> Labels { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public List<Party> Bcc { get; set; } = new List<Party>();
        public List<Party> Cc { get; set; } = new List<Party>();
        public List<Party> To { get; set; } = new List<Party>();
        #endregion

        #region Parsers
       
        public static Email ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Email>(element, debugAttributes);
        }
        public static List<Email> ParseMultiModel(XElement emailsElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Email>(emailsElement, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Email result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "EmailHeader":
                        result.EmailHeader = field.Value.Trim();
                        break;

                    case "Folder":
                        result.Folder = field.Value.Trim();
                        break;

                    case "Priority":
                        result.Priority = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Snippet":
                        result.Snippet = field.Value.Trim();
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

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Email Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Email result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                XNamespace ns = modelField.Name.Namespace;
                XElement modelElement = modelField.Element(ns + "model");

                switch (modelField.Attribute("name").Value)
                {
                    case "From":
                        result.From = Party.ParseModel(modelElement, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Email Parser: Unknown modelField: " + modelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Email result, bool debugAttributes = false)
        {
            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    case "Labels":
                        result.Labels = multiField.Elements().Select(x => x.Value.Trim()).ToList();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Email Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Email result, bool debugAttributes = false)
        {
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Attachments":
                        result.Attachments = Attachment.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Bcc":
                        result.Bcc = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Cc":
                        result.Cc = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "To":
                        result.To = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Email Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
