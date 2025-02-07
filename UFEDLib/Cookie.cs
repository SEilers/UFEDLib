using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Cookie : ModelBase, IUfedModelParser<Cookie>
    {
        public static string GetXmlModelType()
        {
            return "Cookie";
        }

        #region fields
        public DateTime CreationTime { get; set; }
        public string Domain { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime LastAccessTime { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string UserMapping { get; set; }
        public string Source { get; set; }
        public string Value { get; set; }
        public string RelatedApplication { get; set; }
        public string ServiceIdentifier { get; set; }

        

        public static Cookie ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            Cookie result = new Cookie();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Domain":
                        result.Domain = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
                        break;

                    case "Path":
                        result.Path = field.Value.Trim();
                        break;

                    case "Value":
                        result.Value = field.Value.Trim();
                        break;

                    case "RelatedApplication":
                        result.RelatedApplication = field.Value.Trim();
                        break;

                    case "CreationTime":
                        if (field.Value.Trim() != "")
                            result.CreationTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "LastAccessTime":
                        if (field.Value.Trim() != "")
                            result.LastAccessTime = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Expiry":
                        if (field.Value.Trim() != "")
                            result.LastAccessTime = DateTime.Parse(field.Value.Trim());
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Cookie Parser: Unknown field: " + field.Attribute("name").Value);
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
                            Logger.LogAttribute("Cookie Parser:Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("Cookie Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
            }

            return result;
        }

        public static List<Cookie> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Cookie> result = new List<Cookie>();

            IEnumerable<XElement> cookies = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Cookie");

            foreach (XElement cookie in cookies)
            {
                Cookie c = ParseModel(cookie, debugAttributes);
                result.Add(c);
            }

            return result;
        }
        #endregion
    }
}
