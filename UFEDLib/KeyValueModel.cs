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
        public string? Key { get; set; }
        public string? Value { get; set; }
        #endregion

        #region parsers
        public static KeyValueModel ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<KeyValueModel>(element, debugAttributes);
        }

        public static List<KeyValueModel> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<KeyValueModel>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, KeyValueModel result, bool debugAttributes = false)
        {

            foreach (var field in fieldElements)
            {
                var fieldName =  GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "Key":
                        result.Key = field.Value.Trim();
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
                            Logger.LogAttribute("KeyValueModel Parser: Unknown field: " + fieldName);
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