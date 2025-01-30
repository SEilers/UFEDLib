using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace UFEDLib
{
    public class AttachmentParser
    {
        public static List<Attachment> ParseAttachments(XElement attachmentElement, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Attachment> result = new List<Attachment>();

            IEnumerable<XElement> attachments = attachmentElement.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Attachment");

            foreach (XElement attachment in attachments)
            {
                Attachment a = Parse(attachment, debugAttributes);
                result.Add(a);
            }

            return result;
        }

        public static Attachment Parse(XElement attachmentNode, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Attachment result = new Attachment();

            var fieldElements = attachmentNode.Elements(xNamespace + "field");
            var multiFieldElements = attachmentNode.Elements(xNamespace + "multiField");
            var multiModelFieldElements = attachmentNode.Elements(xNamespace + "multiModelField");

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

                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("AttachmentParser: Unknown field: " + field.Attribute("name").Value);
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
                            Console.WriteLine("AttachmentParser:Unknown multiField: " + multiField.Attribute("name").Value);
                        }
                        break;
                }
            }

            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Console.WriteLine("AttachmentParser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;

        }
    }
}
