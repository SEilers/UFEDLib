using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class EMail : ModelBase, IUfedModelParser<EMail>
    {
        public static string GetXmlModelType()
        {
            return "EMail";
        }

        #region fields
        public string Body { get; set; }
        public string Folder { get; set; }
        public string Priority { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region models
        public Party From { get; set; }
        #endregion

        #region multiModels
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public List<Party> Bcc { get; set; } = new List<Party>();
        public List<Party> Cc { get; set; } = new List<Party>();
        public List<Party> To { get; set; } = new List<Party>();
        #endregion

        #region Parsers
        public static List<EMail> ParseMultiModel(XElement emailsElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<EMail> result = new List<EMail>();

            IEnumerable<XElement> emailElements = emailsElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "EMail");

            foreach (var emailElement in emailElements)
            {
                try
                {
                    result.Add(ParseModel(emailElement, debugAttributes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing EMail: " + ex.Message);
                }
            }

            return result;
        }
        public static EMail ParseModel(XElement emailNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            EMail result = new EMail();

            var fieldElements = emailNode.Elements(xNamespace + "field");
            var multiFieldElements = emailNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = emailNode.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Folder":
                        result.Folder = field.Value.Trim();
                        break;

                    case "Status":
                        result.Status = field.Value.Trim();
                        break;

                    case "From":
                        result.From = Party.ParseModel(field);
                        break;

                    case "Subject":
                        result.Subject = field.Value.Trim();
                        break;

                    case "Body":
                        result.Body = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Priority":
                        result.Priority = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("EMail Parser: Unknown attribute: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("EMail Parser: Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "To":
                        result.To = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Cc":
                        result.Cc = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Bcc":
                        result.Bcc = Party.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    case "Attachments":
                        result.Attachments = Attachment.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("EMail Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
