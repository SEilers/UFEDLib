using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class FinancialAccount : ModelBase, IUfedModelParser<FinancialAccount>
    {
        public static string GetXmlModelType()
        {
            return "FinancialAccount";
        }

        #region fields
        public string AccountID { get; set; } = "";
        public DateTime DateLastUpdated { get; set; } = DateTime.MinValue;
        public string FinancialAccountType { get; set; } = "";
        public string FoundInField { get; set; } = "";
        public string FoundInModelId { get; set; } = "";
        public string FoundInModelType { get; set; } = "";
        #endregion

        #region multiModels
        public List<FinancialAsset> Assets { get; set; }
        #endregion

        #region parsers
        public static FinancialAccount ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<FinancialAccount>(element, debugAttributes);
        }

        public static List<FinancialAccount> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<FinancialAccount>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, FinancialAccount result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "AccountID":
                        result.AccountID = field.Value.Trim();
                        break;

                    case "DateLastUpdated":
                        if (field.Value.Trim() != "")
                            result.DateLastUpdated = DateTime.Parse(field.Value.Trim());
                        break;

                    case "FinancialAccountType":
                        result.FinancialAccountType = field.Value.Trim();
                        break;

                    case "FoundInField":
                        result.FoundInField = field.Value.Trim();
                        break;

                    case "FoundInModelId":
                        result.FoundInModelId = field.Value.Trim();
                        break;

                    case "FoundInModelType":
                        result.FoundInModelType = field.Value.Trim();
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
                            Logger.LogAttribute("FinancialAccount Parser: Unknown field: " + fieldName);
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
            foreach (var multiModelField in multiModelFieldElements)
            {
                switch (multiModelField.Attribute("name").Value)
                {
                    case "Assets":
                        result.Assets = FinancialAsset.ParseMultiModel(multiModelField, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            string debugAttrubuteText = "FinancialAccount Parser: Unknown multiModelField: " + multiModelField.Attribute("name").Value;
                            Logger.LogAttribute(debugAttrubuteText);
                        }
                        break;
                }
            }
        }
        #endregion
    }
}