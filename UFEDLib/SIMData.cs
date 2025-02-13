using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class SIMData : ModelBase, IUfedModelParser<SIMData>
    {
        public static string GetXmlModelType()
        {
            return "SIMData";
        }

        #region fields
        public string Category { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region parsers
        public static SIMData ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            SIMData result = new SIMData();

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
                Logger.LogError("SIMData: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<SIMData> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<SIMData> result = new List<SIMData>();

            IEnumerable<XElement> SIMDataElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "SIMData");

            foreach (XElement SIMDataElement in SIMDataElements)
            {
                SIMData em = ParseModel(SIMDataElement, debugAttributes);
                result.Add(em);
            }

            return result;
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