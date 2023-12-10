using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    internal class AttachmentParser
    {
        public static List<Attachment> ParseAttachments(XElement attachmentElement)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Attachment> result = new List<Attachment>();

            IEnumerable<XElement> attachments = attachmentElement.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "Attachment");

            foreach (XElement attachment in attachments)
            {
                Attachment a = Parse(attachment);
                result.Add(a);
            }

            return result;
        }

        public static Attachment Parse(XElement attachmentNode)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Attachment result = new Attachment();

            var fieldElements = attachmentNode.Elements(xNamespace + "field");
 
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Filenmae":
                        result.Filename = field.Value.Trim();
                        break;

                    case "ContentType":
                        result.ContentType = field.Value.Trim();
                        break;

                    case "Charset":
                        result.Charset = field.Value.Trim();
                        break;

                    case "URL":
                        result.URL = field.Value.Trim();
                        break;
                }
            }

            return result;

        }
    }
}
