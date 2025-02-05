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
        public string Word { get; set; }
        #endregion

        #region Parsers
        public static DictionaryWord ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            DictionaryWord result = new DictionaryWord();
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

                    case "Word":
                        result.Word = field.Value.Trim();
                        break;

                    case "Frequency":
                        if (field.Value.Trim() != "")
                        {
                            result.Frequency = int.Parse(field.Value.Trim(), CultureInfo.InvariantCulture);
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

            foreach (var multiField in multiFieldElements)
            {
                switch (multiField.Attribute("name").Value)
                {
                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("DictionaryWord Parser:Unknown multiField: " + multiField.Attribute("name").Value);
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
                            Logger.LogAttribute("DictionaryWord Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value);
                        }
                        break;
                }
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
        #endregion
    }
}
