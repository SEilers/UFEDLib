using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class KeyValueModel : ModelBase, IUfedModelParser<KeyValueModel>
    {
        public static string GetXmlModelType()
        {
            return "KeyValueModel";
        }

        #region fields
        public string Key { get; set; }
        public string Value { get; set; }
        #endregion

        #region parsers
        public static KeyValueModel ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            KeyValueModel result = new KeyValueModel();
            result.ParseAttributes(element);

            var fieldElements = element.Elements(xNamespace + "field");
            var modelFieldElements = element.Elements(xNamespace + "modelField");
            var multiFieldElements = element.Elements(xNamespace + "multiField");
            var multiModelFieldElements = element.Elements(xNamespace + "multiModelField");

            ParseFields(fieldElements, result, debugAttributes);
            ParseModelFields(modelFieldElements, result, debugAttributes);
            ParseMultiFields(multiFieldElements, result, debugAttributes);
            ParseMultiModelFields(multiModelFieldElements, result, debugAttributes);

            return result;
        }

        public static List<KeyValueModel> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<KeyValueModel> result = new List<KeyValueModel>();

            IEnumerable<XElement> KeyValueModelElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "KeyValueModel");

            foreach (XElement KeyValueModelElement in KeyValueModelElements)
            {
                KeyValueModel em = ParseModel(KeyValueModelElement, debugAttributes);
                result.Add(em);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, KeyValueModel result, bool debugAttributes = false)
        {

            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Key":
                        result.Key = field.Value.Trim();
                        break;

                    case "Value":
                        result.Value = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("KeyValueModel Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }

        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, KeyValueModel result, bool debugAttributes = false)
        {
            IUfedModelParser<KeyValueModel>.CheckModelFields<KeyValueModel>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, KeyValueModel result, bool debugAttributes = false)
        {
            IUfedModelParser<KeyValueModel>.CheckMultiFields<KeyValueModel>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, KeyValueModel result, bool debugAttributes = false)
        {
            IUfedModelParser<KeyValueModel>.CheckMultiModelFields<KeyValueModel>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}