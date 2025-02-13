using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
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
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            Price result = new Price();

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
                Logger.LogError("Price: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<Price> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<Price> result = new List<Price>();

            IEnumerable<XElement> PriceElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "Price");

            foreach (XElement PriceElement in PriceElements)
            {
                Price em = ParseModel(PriceElement, debugAttributes);
                result.Add(em);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, Price result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Amount":
                        if(double.TryParse(field.Value, out double amount))
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