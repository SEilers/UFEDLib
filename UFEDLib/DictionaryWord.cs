using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class DictionaryWord : ModelBase, IUfedModelParser<DictionaryWord>
    {
        public static string GetXmlModelType()
        {
            return "DictionaryWord";
        }

        #region fields
        public int Frequency { get; set; }
        public string Locale { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        public string UsagePattern { get; set; }
        public string Word { get; set; }
        #endregion

        #region Parsers
        public static DictionaryWord ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<DictionaryWord>(element, debugAttributes);
        }

        public static List<DictionaryWord> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<DictionaryWord>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, DictionaryWord result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Frequency":
                        if (field.Value.Trim() != "")
                        {
                            if (int.TryParse(field.Value.Trim(), out int frequency))
                                result.Frequency = frequency;
                        }
                        break;

                    case "Locale":
                        result.Locale = field.Value.Trim();
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UsagePattern":
                        result.UsagePattern = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    case "Word":
                        result.Word = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("DictionaryWord Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, DictionaryWord result, bool debugAttributes = false)
        {
            IUfedModelParser<DictionaryWord>.CheckModelFields<DictionaryWord>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, DictionaryWord result, bool debugAttributes = false)
        {
            IUfedModelParser<DictionaryWord>.CheckMultiFields<DictionaryWord>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, DictionaryWord result, bool debugAttributes = false)
        {
            IUfedModelParser<DictionaryWord>.CheckMultiModelFields<DictionaryWord>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}
