using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Attachment : ModelBase, IUfedModelParser<Attachment>
    {
        public static string GetXmlModelType()
        {
            return "Attachment";
        }

        #region fields

        public string Source { get; set; }
        public string UserMapping { get; set; }
        public string AttachmentExtractedPath { get; set; }
        public string Charset { get; set; }
        public string ContentType { get; set; }
        //public DataField Data { get; set;}
        public string Filename { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// A URL string associated with the attachment.
        /// </summary>
        public string URL { get; set; }
        #endregion

        #region Parsers
        public static List<Attachment> ParseMultiModel(XElement attachmentElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Attachment> result = new List<Attachment>();

            IEnumerable<XElement> attachments = attachmentElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Attachment");

            foreach (XElement attachment in attachments)
            {
                Attachment a = ParseModel(attachment, debugAttributes);
                result.Add(a);
            }

            return result;
        }

        public static Attachment ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Attachment result = new Attachment();

            try
            {
                result.ParseAttributes(element);

                var fieldElements = element.Elements(xNamespace + "field");
                var modelFieldElements = element.Elements(xNamespace + "modelField");
                var multiFieldElements = element.Elements(xNamespace + "multiField");
                var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

                ParseFields(fieldElements, result, debugAttributes);
                ParseModelFields(modelFieldElements, result, debugAttributes);
                ParseMultiFields(multiFieldElements, result, debugAttributes);
                ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);
            }
            catch (Exception ex)
            {
                Logger.LogError("Attachment: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Attachment result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "attachment_extracted_path":
                        result.AttachmentExtractedPath = field.Value.Trim();
                        break;

                    case "Charset":
                        result.Charset = field.Value.Trim();
                        break;

                    case "ContentType":
                        result.ContentType = field.Value.Trim();
                        break;

                    case "Filename":
                        result.Filename = field.Value.Trim();
                        break;

                    case "Title":
                        result.Title = field.Value.Trim();
                        break;

                    case "URL":
                        result.URL = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Attachment Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Attachment result, bool debugAttributes = false)
        {
            IUfedModelParser<ApplicationUsage>.CheckModelFields<ApplicationUsage>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Attachment result, bool debugAttributes = false)
        {
            IUfedModelParser<ApplicationUsage>.CheckMultiFields<ApplicationUsage>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Attachment result, bool debugAttributes = false)
        {
            IUfedModelParser<ApplicationUsage>.CheckMultiModelFields<ApplicationUsage>(multiModelFieldElements, debugAttributes);
        }
        #endregion




    }
}
