using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class Price : ModelBase, IUfedModelParser<Price>
    {
        public static string GetXmlModelType()
        {
            return "Price";
        }

        #region fields
        public double Amount { get; set; }
        public string Currency { get; set; }
        #endregion

        #region parsers
        public static Price ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<Price>(element, debugAttributes);
        }

        public static List<Price> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<Price>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Price result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Amount":
                        if (double.TryParse(field.Value, out double amount))
                        {
                            result.Amount = amount;
                        }
                        break;

                    case "Currency":
                        result.Currency = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("Price Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, Price result, bool debugAttributes = false)
        {
            IUfedModelParser<Price>.CheckModelFields<Price>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, Price result, bool debugAttributes = false)
        {
            IUfedModelParser<Price>.CheckMultiFields<Price>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, Price result, bool debugAttributes = false)
        {
            IUfedModelParser<Price>.CheckMultiModelFields<Price>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}