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
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            DictionaryWord result = new DictionaryWord();

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
                Logger.LogError("DictionaryWord: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<DictionaryWord> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<DictionaryWord> result = new List<DictionaryWord>();

            IEnumerable<XElement> dictionaryWords = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "DictionaryWord");

            foreach (XElement dictionaryWord in dictionaryWords)
            {
                DictionaryWord dw = ParseModel(dictionaryWord, debugAttributes);
                result.Add(dw);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, DictionaryWord result, bool debugAttributes = false)
        {
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

                    case "Locale":
                        result.Locale = field.Value.Trim();
                        break;

                    case "UsagePattern":
                        result.UsagePattern = field.Value.Trim();
                        break;

                    case "Word":
                        result.Word = field.Value.Trim();
                        break;

                    case "Frequency":
                        if (field.Value.Trim() != "")
                        {
                            if( int.TryParse(field.Value.Trim(), out int frequency))
                                result.Frequency = frequency;
                        }
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
