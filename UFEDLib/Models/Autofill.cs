using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Autofill : ModelBase, IUfedModelParser<Autofill>
    {
        public static string GetXmlModelType()
        {
            return "Autofill";
        }

        #region fields
        public string Account { get; set; }
        public string Key { get; set; }
        public DateTime LastUsedDate { get; set; }
        public string ServiceIdentifier { get; set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserMapping { get; set; }
        public string Value { get; set; }
        #endregion

        #region Parsers
        public static Autofill ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Autofill>(element, debugAttributes);
        }

        public static List<Autofill> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Autofill>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Autofill result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Account":
                        result.Account = field.Value.Trim();
                        break;

                    case "Key":
                        result.Key = field.Value.Trim();
                        break;

                    case "LastUsedDate":
                        if (field.Value.Trim() != "")
                            result.LastUsedDate = DateTime.Parse(field.Value.Trim());
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "TimeStamp":
                        if (field.Value.Trim() != "")
                            result.TimeStamp = DateTime.Parse(field.Value.Trim());
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
                            Logger.LogAttribute("Autofill Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Autofill result, bool debugAttributes = false)
        {
            IUfedModelParser<Autofill>.CheckModelFields<Autofill>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Autofill result, bool debugAttributes = false)
        {
            IUfedModelParser<Autofill>.CheckMultiFields<Autofill>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Autofill result, bool debugAttributes = false)
        {
            IUfedModelParser<Autofill>.CheckMultiModelFields<Autofill>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
