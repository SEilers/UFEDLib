using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UFEDLib
{
    [Serializable]
    public class CreditCard : ModelBase, IUfedModelParser<CreditCard>
    {
        public static string GetXmlModelType()
        {
            return "CreditCard";
        }

        #region fields
        public string Company { get; set; } = "";
        public string CreditCardNumber { get; set; } = "";
        public string CVV { get; set; } = "";
        public DateTime DateLastUsed { get; set; } = DateTime.MinValue;
        public DateTime ExpirationDate { get; set; } = DateTime.MinValue;
        public string NameOnCard { get; set; } = "";
        #endregion

        #region models
        public StreetAddress BillingAddress { get; set; } = new StreetAddress();
        #endregion

        #region Parsers
        public static CreditCard ParseModel(XElement element, bool debugAttributes = false)
        {
            return DefaultModelParser<CreditCard>(element, debugAttributes);
        }

        public static List<CreditCard> ParseMultiModel(XElement element, bool debugAttributes = false)
        {
            return DefaultMultiModelParser<CreditCard>(element, debugAttributes);
        }

        public static void ParseFields(IEnumerable<XElement> fieldElements, CreditCard result, bool debugAttributes = false)
        {
            foreach (var field in fieldElements)
            {
                var fieldName = ModelBase.GetAttributeValueOrLog(field, "name", debugAttributes);
                if (fieldName == null) continue;

                switch (fieldName)
                {
                    case "Company":
                        result.Company = field.Value.Trim();
                        break;

                    case "CreditCardNumber":
                        result.CreditCardNumber = field.Value.Trim();
                        break;

                    case "CVV":
                        result.CVV = field.Value.Trim();
                        break;

                    case "DateLastUsed":
                        if (field.Value.Trim() != "")
                            result.DateLastUsed = DateTime.Parse(field.Value.Trim());
                        break;

                    case "ExpirationDate":
                        if (field.Value.Trim() != "")
                            result.ExpirationDate = DateTime.Parse(field.Value.Trim());
                        break;

                    case "NameOnCard":
                        result.NameOnCard = field.Value.Trim();
                        break;

                    case "ServiceIdentifier":
                        result.ServiceIdentifier = field.Value.Trim();
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
                            Logger.LogAttribute("CreditCard Parser: Unknown field: " + fieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseModelFields(IEnumerable<XElement> modelFieldElements, CreditCard result, bool debugAttributes = false)
        {
            foreach (var modelField in modelFieldElements)
            {
                XNamespace ns = modelField.Name.Namespace;
                XElement? modelElement = modelField.Element(ns + "model");

                if( modelElement == null) continue;

                var modelFieldName = ModelBase.GetAttributeValueOrLog(modelField, "name", debugAttributes);
                if (modelFieldName == null) continue;
                
                switch (modelFieldName)
                {
                    case "BillingAddress":
                        var modelItem = modelElement.Element("model");
                        
                        if (modelItem == null) continue;

                        result.BillingAddress = StreetAddress.ParseModel(modelItem, debugAttributes);
                        break;

                    default:
                        if (debugAttributes)
                        {
                            Logger.LogAttribute("CreditCard Parser: Unknown modelField: " + modelFieldName);
                        }
                        break;
                }
            }
        }

        public static void ParseMultiFields(IEnumerable<XElement> multiFieldElements, CreditCard result, bool debugAttributes = false)
        {
            IUfedModelParser<CreditCard>.CheckMultiFields<CreditCard>(multiFieldElements, debugAttributes);
        }

        public static void ParseMultiModelFields(IEnumerable<XElement> multiModelFieldElements, CreditCard result, bool debugAttributes = false)
        {
            IUfedModelParser<CreditCard>.CheckMultiModelFields<CreditCard>(multiModelFieldElements, debugAttributes);
        }

        #endregion
    }
}
