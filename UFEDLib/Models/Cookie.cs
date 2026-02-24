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
        public DateTime CreationTime { get; set; } = DateTime.MinValue;
        public string Domain { get; set; } = "";
        public DateTime Expiry { get; set; } = DateTime.MinValue;
        public DateTime LastAccessTime { get; set; } = DateTime.MinValue;
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public string RelatedApplication { get; set; } = "";
        public string Value { get; set; } = "";
        #endregion


        #region Parsers
        public static Cookie ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Cookie>(element, debugAttributes);
        }

        public static List<Cookie> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Cookie>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Cookie result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                try
                {
                    switch (fieldName)
                    {
                        case "CreationTime":
                            if (field.Value.Trim() != "")
                                if (DateTime.TryParse(field.Value.Trim(), out DateTime dt))
                                {
                                    result.CreationTime = dt;
                                }
                            break;

                        case "Domain":
                            result.Domain = field.Value.Trim();
                            break;

                        case "Expiry":
                            if (field.Value.Trim() != "")
                                if (DateTime.TryParse(field.Value.Trim(), out DateTime dt))
                                {
                                    result.Expiry = dt;
                                }
                            break;

                        case "LastAccessTime":
                            if (field.Value.Trim() != "")
                                if (DateTime.TryParse(field.Value.Trim(), out DateTime dt))
                                {
                                    result.LastAccessTime = dt;
                                }
                            break;

                        case "Name":
                            result.Name = field.Value.Trim();
                            break;

                        case "Path":
                            result.Path = field.Value.Trim();
                            break;

                        case "RelatedApplication":
                            result.RelatedApplication = field.Value.Trim();
                            break;

                        case "ServiceIdentifier":
                            result.ServiceIdentifier = field.Value.Trim();
                            break;

                        case "Source":
                            result.Source = field.Value.Trim();
                            break;

                        case "UserMapping":
                            result.UserMapping = field.Value.Trim();
                            break;

                        case "Value":
                            result.Value = field.Value.Trim();
                            break;

                        default:
                            if (debugAttributes)
                            {
                                Logger.LogAttribute("Cookie Parser: Unknown field: " + fieldName);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error parsing field in Cookie Parser: " + fieldName + " - " + ex.Message);
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Cookie result, bool debugAttributes = false)
        {
            IUfedModelParser<Cookie>.CheckModelFields<Cookie>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Cookie result, bool debugAttributes = false)
        {
            IUfedModelParser<Cookie>.CheckMultiFields<Cookie>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Cookie result, bool debugAttributes = false)
        {
            IUfedModelParser<Cookie>.CheckMultiModelFields<Cookie>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
