using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class FinancialAsset : ModelBase, IUfedModelParser<FinancialAsset>
    {
        public static string GetXmlModelType()
        {
            return "FinancialAsset";
        }

        #region fields
        public string Currency { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region parsers
        public static FinancialAsset ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<FinancialAsset>(element, debugAttributes);
        }

        public static List<FinancialAsset> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<FinancialAsset>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, FinancialAsset result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "Currency":
                        result.Currency = field.Value.Trim();
                        break;

                    case "DateLastUpdated":
                        if (field.Value.Trim() != "")
                            result.DateLastUpdated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "Source":
                        result.Source = field.Value.Trim();
                        break;

                    case "UserMapping":
                        result.UserMapping = field.Value.Trim();
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("FinancialAsset Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, FinancialAsset result, bool debugAttributes = false)
        {
            IUfedModelParser<FinancialAsset>.CheckModelFields<FinancialAsset>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, FinancialAsset result, bool debugAttributes = false)
        {
            IUfedModelParser<FinancialAsset>.CheckMultiFields<FinancialAsset>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, FinancialAsset result, bool debugAttributes = false)
        {
            IUfedModelParser<FinancialAsset>.CheckMultiModelFields<FinancialAsset>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}