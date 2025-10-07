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
        public string AttachmentExtractedPath { get; set; }
        public string Charset { get; set; }
        public string ContentType { get; set; }
        public string Filename { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region Parsers
        public static Attachment ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Attachment>(element, debugAttributes);
        }

        public static List<Attachment> ParseMultiModel(XElement attachmentElement, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Attachment>(attachmentElement, debugAttributes);
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

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "Title":
                        result.Title = field.Value.Trim();
                        break;

                    case "URL":
                        result.URL = field.Value.Trim();
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
