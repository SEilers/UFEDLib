using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    public class FinancialAccount : ModelBase, IUfedModelParser<FinancialAccount>
    {
        public static string GetXmlModelType()
        {
            return "FinancialAccount";
        }

        #region fields
        public string AccountID { get; set; }
        public string FinancialAccountType { get; set; }
        public string FoundInModelId { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string Source { get; set; }
        public string UserMapping { get; set; }
        #endregion

        #region parsers
        public static FinancialAccount ParseModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            FinancialAccount result = new FinancialAccount();

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
                Logger.LogError("FinancialAccount: Error parsing xml reader attributes " + ex.Message);
            }

            return result;
        }

        public static List<FinancialAccount> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
            List<FinancialAccount> result = new List<FinancialAccount>();

            IEnumerable<XElement> FinancialAccountElements = element.Elements(xNamespace + "model").Where(x => x.Attribute("type").Value == "FinancialAccount");

            foreach (XElement FinancialAccountElement in FinancialAccountElements)
            {
                FinancialAccount em = ParseModel(FinancialAccountElement, debugAttributes);
                result.Add(em);
            }

            return result;
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, FinancialAccount result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                switch (field.Attribute("name").Value)
                {
                    case "AccountID":
                        result.AccountID = field.Value.Trim();
                        break;

                    case "DateLastUpdated":
                        if( field.Value.Trim() != "")
                            result.DateLastUpdated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "FinancialAccountType":
                        result.FinancialAccountType = field.Value.Trim();
                        break;

                    case "FoundInModelId":
                        result.FoundInModelId = field.Value.Trim();
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
                            Logger.LogAttribute("FinancialAccount Parser: Unknown field: " + field.Attribute("name").Value);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, FinancialAccount result, bool debugAttributes = false)
        {
            IUfedModelParser<FinancialAccount>.CheckModelFields<FinancialAccount>(modelFieldElements, debugAttributes);
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, FinancialAccount result, bool debugAttributes = false)
        {
            IUfedModelParser<FinancialAccount>.CheckMultiFields<FinancialAccount>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, FinancialAccount result, bool debugAttributes = false)
        {
            IUfedModelParser<FinancialAccount>.CheckMultiModelFields<FinancialAccount>(multiModelFieldElements, debugAttributes);
        }
        #endregion
    }
}