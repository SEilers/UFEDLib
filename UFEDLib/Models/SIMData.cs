using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class SIMData : ModelBase, IUfedModelParser<SIMData>
    {
        public static string GetXmlModelType()
        {
            return "SIMData";
        }

        #region fields
        public string Category { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        public string Value { get; set; }
        #endregion

        #region parsers
        public static SIMData ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<SIMData>(element, debugAttributes);
        }

        public static List<SIMData> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<SIMData>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, SIMData result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Category":
                        result.Category = field.Value.Trim();
                        break;

                    case "Name":
                        result.Name = field.Value.Trim();
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
                            Logger.LogAttribute("SIMData Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, SIMData result, bool debugAttributes = false)
        {
            IUfedModelParser<SIMData>.CheckModelFields<SIMData>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, SIMData result, bool debugAttributes = false)
        {
            IUfedModelParser<SIMData>.CheckMultiFields<SIMData>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, SIMData result, bool debugAttributes = false)
        {
            IUfedModelParser<SIMData>.CheckMultiModelFields<SIMData>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}